using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;

[System.Serializable]
public struct ImageMode
{
    public int width;
    public int height;
    public int fps;
    public Format format;
}

public class StreamData
{
    public int width;
    public int height;
    public Format format;
    public byte[] data;
}

public class OrbbecDeviceManager : MonoBehaviour, OrbbecManager
{
    public ImageMode colorMode;
    public ImageMode depthMode;
    public ImageMode irMode;
    public bool enableColor;
    public bool enableDepth;
    public bool enableIR;
    public bool autoStart;

    private Context context;
    private DeviceList deviceList;
    private List<Device> devices;
    private Device currentDevice;
    private Sensor colorSensor;
    private Sensor depthSensor;
    private Sensor irSensor;
    private StreamProfile[] colorProfiles;
    private StreamProfile[] depthProfiles;
    private StreamProfile[] irProfiles;
    private StreamProfile colorProfile;
    private StreamProfile depthProfile;
    private StreamProfile irProfile;
    private StreamData colorData;
    private StreamData depthData;
    private StreamData irData;

    private bool hasInit = false;

    private OrbbecInitHandle initHandle;

    // Use this for initialization
    void Start()
    {
        if (!hasInit)
        {
            InitSDK();
        }
    }

    private void InitSDK()
    {
        Debug.Log(string.Format("Orbbec SDK version: {0}.{1}.{2}",
                                    Version.GetMajorVersion(),
                                    Version.GetMinorVersion(),
                                    Version.GetPatchVersion()));
        devices = new List<Device>();
        context = new Context();
        context.SetDeviceChangedCallback(OnDeviceChanged);
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidDeviceManager.Init();
#else
        deviceList = context.QueryDeviceList();
        if (deviceList.DeviceCount() > 0)
        {
            for(uint i = 0; i < deviceList.DeviceCount(); i++)
            {
                Device device = deviceList.GetDevice(i);
                devices.Add(device);
            }
            OpenDevice(0);
            hasInit = true;
            if(initHandle != null)
            {
                initHandle.Invoke();
            }
        }
#endif
    }

    private void OpenDevice(int index)
    {
        currentDevice = devices[index];
        colorSensor = currentDevice.GetSensor(SensorType.OB_SENSOR_COLOR);
        depthSensor = currentDevice.GetSensor(SensorType.OB_SENSOR_DEPTH);
        irSensor = currentDevice.GetSensor(SensorType.OB_SENSOR_IR);
        colorProfiles = colorSensor.GetStreamProfiles();
        depthProfiles = depthSensor.GetStreamProfiles();
        irProfiles = irSensor.GetStreamProfiles();
        foreach (var profile in colorProfiles)
        {
            if (profile.GetWidth() == colorMode.width &&
                profile.GetHeight() == colorMode.height &&
                profile.GetFPS() == colorMode.fps &&
                profile.GetFormat() == colorMode.format)
            {
                colorProfile = profile;
                Debug.Log(string.Format("color profile found: {0}x{1}@{2} {3}",
                            colorProfile.GetWidth(),
                            colorProfile.GetHeight(),
                            colorProfile.GetFPS(),
                            colorProfile.GetFormat()));
            }
        }
        if (colorProfile == null)
        {
            colorProfile = colorProfiles[0];
            Debug.Log(string.Format("color profile not found, use default: {0}x{1}@{2} {3}",
                            colorProfile.GetWidth(),
                            colorProfile.GetHeight(),
                            colorProfile.GetFPS(),
                            colorProfile.GetFormat()));
        }
        foreach (var profile in depthProfiles)
        {
            if (profile.GetWidth() == depthMode.width &&
                profile.GetHeight() == depthMode.height &&
                profile.GetFPS() == depthMode.fps &&
                profile.GetFormat() == depthMode.format)
            {
                depthProfile = profile;
                Debug.Log(string.Format("depth profile found: {0}x{1}@{2} {3}",
                            depthProfile.GetWidth(),
                            depthProfile.GetHeight(),
                            depthProfile.GetFPS(),
                            depthProfile.GetFormat()));
            }
        }
        if (depthProfile == null)
        {
            depthProfile = depthProfiles[0];
            Debug.Log(string.Format("depth profile not found, use default: {0}x{1}@{2} {3}",
                        depthProfile.GetWidth(),
                        depthProfile.GetHeight(),
                        depthProfile.GetFPS(),
                        depthProfile.GetFormat()));
        }
        foreach (var profile in irProfiles)
        {
            if (profile.GetWidth() == irMode.width &&
                profile.GetHeight() == irMode.height &&
                profile.GetFPS() == irMode.fps &&
                profile.GetFormat() == irMode.format)
            {
                irProfile = profile;
                Debug.Log(string.Format("ir profile found: {0}x{1}@{2} {3}",
                            irProfile.GetWidth(),
                            irProfile.GetHeight(),
                            irProfile.GetFPS(),
                            irProfile.GetFormat()));
            }
        }
        if (irProfile == null)
        {
            irProfile = irProfiles[0];
            Debug.Log(string.Format("ir profile not found, use default: {0}x{1}@{2} {3}",
                        irProfile.GetWidth(),
                        irProfile.GetHeight(),
                        irProfile.GetFPS(),
                        irProfile.GetFormat()));
        }
        if (enableColor && autoStart)
        {
            colorSensor.Start(colorProfile, OnColorFrame);
            Debug.Log("auto start color stream");
        }
        if (enableDepth && autoStart)
        {
            depthSensor.Start(depthProfile, OnDepthFrame);
            Debug.Log("auto start depth stream");
        }
        if (enableIR && autoStart)
        {
            irSensor.Start(irProfile, OnIRFrame);
            Debug.Log("auto start ir stream");
        }
        Debug.Log("device has opened");
    }

    private void CloseDevice()
    {
        if (colorSensor != null)
        {
            colorSensor.Dispose();
        }
        if (depthSensor != null)
        {
            depthSensor.Dispose();
        }
        if (irSensor != null)
        {
            irSensor.Dispose();
        }
        if (colorProfiles != null)
        {
            foreach (var profile in colorProfiles)
            {
                profile.Dispose();
            }
        }
        if (depthProfiles != null)
        {
            foreach (var profile in depthProfiles)
            {
                profile.Dispose();
            }
        }
        if (irProfiles != null)
        {
            foreach (var profile in irProfiles)
            {
                profile.Dispose();
            }
        }
        if(currentDevice != null)
        {
            currentDevice.Dispose();
        }
        Debug.Log("device has closed");
    }

    private void OnDeviceChanged(DeviceList removed, DeviceList added)
    {
        Debug.Log(string.Format("on device changed: removed count {0}, added count {1}", removed.DeviceCount(), added.DeviceCount()));
        for(uint i = 0; i < added.DeviceCount(); i++)
        {
            Device device = added.GetDevice(i);
            devices.Add(device);
            Debug.Log(string.Format("added device: {0} {1} {2} {3}", added.Name(i), added.Vid(i), added.Pid(i), added.Uid(i)));
        }
        for(uint i = 0; i < removed.DeviceCount(); i++)
        {
            Debug.Log(string.Format("removed device: {0} {1} {2} {3}", removed.Name(i), removed.Vid(i), removed.Pid(i), removed.Uid(i)));
            if(removed.Uid(i) == currentDevice.GetDeviceInfo().Uid())
            {
                CloseDevice();
                devices.Remove(currentDevice);
                hasInit = false;
            }
            else
            {
                for(int j = devices.Count - 1; j >= 0; j--)
                {
                    if(removed.Uid(i) == devices[j].GetDeviceInfo().Uid())
                    {
                        devices.RemoveAt(j);
                    }
                }
            }
        }

        if(!hasInit)
        {
            OpenDevice(0);
            hasInit = true;
            if(initHandle != null)
            {
                initHandle.Invoke();
            }
        }
        
        removed.Dispose();
        added.Dispose();
    }

    void OnDestroy()
    {
        if (hasInit)
        {
            ReleaseSDK();
        }
    }

    private void ReleaseSDK()
    {
        CloseDevice();
        foreach(var device in devices)
        {
            if (device != null)
            {
                device.Dispose();
            }
        }
        if (deviceList != null)
        {
            deviceList.Dispose();
        }
        if (context != null)
        {
            context.Dispose();
        }
        Debug.Log("SDK has released");
        hasInit = false;
    }

    private void OnColorFrame(Frame frame)
    {
        if (frame == null)
        {
            return;
        }
        // Debug.Log(frame.GetFrameType());
        ColorFrame colorFrame = frame as ColorFrame;
        int dataSize = (int)colorFrame.GetDataSize();
        if (colorData == null)
        {
            colorData = new StreamData();
        }
        if (colorData.data == null || colorData.data.Length != dataSize)
        {
            colorData.data = new byte[dataSize];
        }
        colorFrame.CopyData(ref colorData.data);
        colorData.width = (int)colorFrame.GetWidth();
        colorData.height = (int)colorFrame.GetHeight();
        colorData.format = colorFrame.GetFormat();
        frame.Dispose();
    }

    private void OnDepthFrame(Frame frame)
    {
        if (frame == null)
        {
            return;
        }
        // Debug.Log(frame.GetFrameType());
        DepthFrame depthFrame = frame as DepthFrame;
        int dataSize = (int)depthFrame.GetDataSize();
        if (depthData == null)
        {
            depthData = new StreamData();
        }
        if (depthData.data == null || depthData.data.Length != dataSize)
        {
            depthData.data = new byte[dataSize];
        }
        depthFrame.CopyData(ref depthData.data);
        depthData.width = (int)depthFrame.GetWidth();
        depthData.height = (int)depthFrame.GetHeight();
        depthData.format = depthFrame.GetFormat();
        frame.Dispose();
    }

    private void OnIRFrame(Frame frame)
    {
        if (frame == null)
        {
            return;
        }
        // Debug.Log(frame.GetFrameType());
        IRFrame irFrame = frame as IRFrame;
        int dataSize = (int)irFrame.GetDataSize();
        if (irData == null)
        {
            irData = new StreamData();
        }
        if (irData.data == null || irData.data.Length != dataSize)
        {
            irData.data = new byte[dataSize];
        }
        irFrame.CopyData(ref irData.data);
        irData.width = (int)irFrame.GetWidth();
        irData.height = (int)irFrame.GetHeight();
        irData.format = irFrame.GetFormat();
        frame.Dispose();
    }

    public bool HasInit()
    {
        return hasInit;
    }

    public void StartStream(StreamType streamType)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                colorSensor.Start(colorProfile, OnColorFrame);
                break;
            case StreamType.OB_STREAM_DEPTH:
                depthSensor.Start(depthProfile, OnDepthFrame);
                break;
            case StreamType.OB_STREAM_IR:
                irSensor.Start(irProfile, OnIRFrame);
                break;
        }
    }

    public void StopStream(StreamType streamType)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                colorSensor.Stop();
                break;
            case StreamType.OB_STREAM_DEPTH:
                depthSensor.Stop();
                break;
            case StreamType.OB_STREAM_IR:
                irSensor.Stop();
                break;
        }
    }

    public void StartAllStreams()
    {
        colorSensor.Start(colorProfile, OnColorFrame);
        depthSensor.Start(depthProfile, OnDepthFrame);
        irSensor.Start(irProfile, OnIRFrame);
    }

    public void StopAllStreams()
    {
        colorSensor.Stop();
        depthSensor.Stop();
        irSensor.Stop();
    }

    public StreamProfile[] GetStreamProfiles(StreamType streamType)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                return colorProfiles;
            case StreamType.OB_STREAM_DEPTH:
                return depthProfiles;
            case StreamType.OB_STREAM_IR:
                return irProfiles;
        }
        Debug.Log(string.Format("no stream type: {0} profiles found", streamType));
        return null;
    }

    public StreamProfile GetStreamProfile(StreamType streamType)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                return colorProfile;
            case StreamType.OB_STREAM_DEPTH:
                return depthProfile;
            case StreamType.OB_STREAM_IR:
                return irProfile;
        }
        Debug.Log(string.Format("no stream type: {0} profile found", streamType));
        return null;
    }

    public void SetStreamProfile(StreamType streamType, StreamProfile profile)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                colorProfile = profile;
                return;
            case StreamType.OB_STREAM_DEPTH:
                depthProfile = profile;
                return;
            case StreamType.OB_STREAM_IR:
                irProfile = profile;
                return;
        }
        Debug.Log(string.Format("no stream type: {0} profile found", streamType));
    }

    public StreamData GetStreamData(StreamType streamType)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                return colorData;
            case StreamType.OB_STREAM_DEPTH:
                return depthData;
            case StreamType.OB_STREAM_IR:
                return irData;
        }
        Debug.Log(string.Format("no stream type: {0} data found", streamType));
        return null;
    }

    public Device GetCurrentDevice()
    {
        return currentDevice;
    }

    public Device GetDevice(int index)
    {
        return devices[index];
    }

    public int GetDeviceCount()
    {
        return (int)deviceList.DeviceCount();
    }

    public void ChangeDevice(int index)
    {
        CloseDevice();
        OpenDevice(index);
    }

    public Sensor GetSensor(SensorType sensorType)
    {
        switch (sensorType)
        {
            case SensorType.OB_SENSOR_COLOR:
                return colorSensor;
            case SensorType.OB_SENSOR_DEPTH:
                return depthSensor;
            case SensorType.OB_SENSOR_IR:
                return irSensor;
        }
        Debug.Log(string.Format("no sensor type: {0} found", sensorType));
        return null;
    }

    public void SetInitHandle(OrbbecInitHandle handle)
    {
        initHandle = handle;
    }
}
