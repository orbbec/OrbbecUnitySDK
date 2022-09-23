using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using System;
using Version = Orbbec.Version;

namespace OrbbecUnity
{
public class OrbbecDeviceManager : Singleton<OrbbecDeviceManager>
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
    // private List<Device> devices;
    private Device device;
    private Sensor colorSensor;
    private Sensor depthSensor;
    private Sensor irSensor;
    private StreamProfileList colorProfiles;
    private StreamProfileList depthProfiles;
    private StreamProfileList irProfiles;
    private StreamProfile colorProfile;
    private StreamProfile depthProfile;
    private StreamProfile irProfile;
    // private CommonFrame colorData;
    // private CommonFrame depthData;
    // private CommonFrame irData;

    private bool hasInit = false;
    private bool hasColorStart = false;
    private bool hasDepthStart = false;
    private bool hasIrStart = false;

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
        context = new Context();
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidDeviceManager.Init();
#endif
        StartCoroutine(WaitForDevice());
    }

    private IEnumerator WaitForDevice()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            deviceList = context.QueryDeviceList();
            if (deviceList.DeviceCount() > 0)
            {
                device = deviceList.GetDevice(0);
                InitDevice();
                hasInit = true;
                if(initHandle != null)
                {
                    initHandle.Invoke();
                }
                break;
            }
            else
            {
                Debug.LogWarning("device not found");
                deviceList.Dispose();
            }
        }
    }

    private void InitDevice()
    {
        colorSensor = device.GetSensor(SensorType.OB_SENSOR_COLOR);
        depthSensor = device.GetSensor(SensorType.OB_SENSOR_DEPTH);
        irSensor = device.GetSensor(SensorType.OB_SENSOR_IR);
        colorProfiles = colorSensor.GetStreamProfileList();
        depthProfiles = depthSensor.GetStreamProfileList();
        irProfiles = irSensor.GetStreamProfileList();
        for (int i = 0; i < colorProfiles.ProfileCount(); i++)
        {
            var profile = colorProfiles.GetProfile(i);
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
            colorProfile = colorProfiles.GetProfile(0);
            Debug.Log(string.Format("color profile not found, use default: {0}x{1}@{2} {3}",
                            colorProfile.GetWidth(),
                            colorProfile.GetHeight(),
                            colorProfile.GetFPS(),
                            colorProfile.GetFormat()));
        }
        for (int i = 0; i < depthProfiles.ProfileCount(); i++)
        {
            var profile = depthProfiles.GetProfile(i);
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
            depthProfile = depthProfiles.GetProfile(0);
            Debug.Log(string.Format("depth profile not found, use default: {0}x{1}@{2} {3}",
                        depthProfile.GetWidth(),
                        depthProfile.GetHeight(),
                        depthProfile.GetFPS(),
                        depthProfile.GetFormat()));
        }
        for (int i = 0; i < irProfiles.ProfileCount(); i++)
        {
            var profile = irProfiles.GetProfile(i);
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
            irProfile = irProfiles.GetProfile(0);
            Debug.Log(string.Format("ir profile not found, use default: {0}x{1}@{2} {3}",
                        irProfile.GetWidth(),
                        irProfile.GetHeight(),
                        irProfile.GetFPS(),
                        irProfile.GetFormat()));
        }
        if (enableColor && autoStart)
        {
            colorSensor.Start(colorProfile, OnColorFrame);
            hasColorStart = true;
            Debug.Log("auto start color stream");
        }
        if (enableDepth && autoStart)
        {
            depthSensor.Start(depthProfile, OnDepthFrame);
            hasDepthStart = true;
            Debug.Log("auto start depth stream");
        }
        if (enableIR && autoStart)
        {
            irSensor.Start(irProfile, OnIRFrame);
            hasIrStart = true;
            Debug.Log("auto start ir stream");
        }
        Debug.Log("device has opened");
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
            colorProfiles.Dispose();
        }
        if (depthProfiles != null)
        {
            depthProfiles.Dispose();
        }
        if (irProfiles != null)
        {
            irProfiles.Dispose();
        }
        if(device != null)
        {
            device.Dispose();
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

    private void OnColorFrame(Orbbec.Frame frame)
    {
        // if (frame == null)
        // {
        //     return;
        // }
        // ColorFrame colorFrame = frame as ColorFrame;
        // // Debug.Log(String.Format("get colorFrame frame {0}x{1} {2}",
        // //         colorFrame.GetWidth(), colorFrame.GetHeight(), colorFrame.GetFormat()));
        // int dataSize = (int)colorFrame.GetDataSize();
        // if (colorData == null)
        // {
        //     colorData = new CommonFrame();
        // }
        // byte[] mjpegData = new byte[dataSize];
        // colorFrame.CopyData(ref mjpegData);
        // if ( mjpegData[ 0 ] != 0xff || 
        //         mjpegData[ 1 ] != 0xd8 || 
        //         ( mjpegData[ dataSize - 2 ] != 0xff && 
        //         mjpegData[ dataSize - 2 - 2 ] != 0 && 
        //         mjpegData[ dataSize - 2 - 2 ] != 0xd9 ) || 
        //         ( mjpegData[ dataSize - 2 - 1 ] != 0xd9 && 
        //         mjpegData[ dataSize - 2 - 1 ] != 0 ) ) {
        //     // colorData = null;
        // }
        // else
        // {
        //     // int frameWidth = (int)colorFrame.GetWidth();
        //     // int frameHeight = (int)colorFrame.GetHeight();
        //     // if (colorData.data == null || colorData.data.Length != frameWidth * frameHeight * 3)
        //     // {
        //     //     colorData.data = new byte[frameWidth * frameHeight * 3];
        //     // }
        //     // long startDecodeTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        //     if(mjpegUtil != null)
        //     {
        //         mjpegUtil.Decompress(mjpegData, mjpegData.Length, out colorData.width, out colorData.height, out colorData.data);
        //     }
        //     // long endDecodeTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        //     // long runDecodeTime = endDecodeTime - startDecodeTime;
        //     // Debug.Log("decode run time: " + runDecodeTime);

        //     // colorData.width = (int)colorFrame.GetWidth();
        //     // colorData.height = (int)colorFrame.GetHeight();
        //     colorData.format = Format.RGB;
        // }
        // frame.Dispose();
    }

    private void OnDepthFrame(Orbbec.Frame frame)
    {
        // if (frame == null)
        // {
        //     return;
        // }
        // DepthFrame depthFrame = frame as DepthFrame;
        // // Debug.Log(String.Format("get depth frame {0}x{1} {2}",
        // //         depthFrame.GetWidth(), depthFrame.GetHeight(), depthFrame.GetFormat()));
        // int dataSize = (int)depthFrame.GetDataSize();
        // if (depthData == null)
        // {
        //     depthData = new CommonFrame();
        // }
        // if (depthData.data == null || depthData.data.Length != dataSize)
        // {
        //     depthData.data = new byte[dataSize];
        // }
        // depthFrame.CopyData(ref depthData.data);
        // depthData.width = (int)depthFrame.GetWidth();
        // depthData.height = (int)depthFrame.GetHeight();
        // depthData.format = Format.Y16;
        // frame.Dispose();
    }

    private void OnIRFrame(Orbbec.Frame frame)
    {
        // if (frame == null)
        // {
        //     return;
        // }
        // IRFrame irFrame = frame as IRFrame;
        // int dataSize = (int)irFrame.GetDataSize();
        // // Debug.Log(String.Format("get ir frame {0}x{1} {2}",
        // //         irFrame.GetWidth(), irFrame.GetHeight(), irFrame.GetFormat()));
        // if (irData == null)
        // {
        //     irData = new CommonFrame();
        // }
        // if (irData.data == null || irData.data.Length != dataSize)
        // {
        //     irData.data = new byte[dataSize];
        // }
        // irFrame.CopyData(ref irData.data);
        // irData.width = (int)irFrame.GetWidth();
        // irData.height = (int)irFrame.GetHeight();
        // irData.format = Format.Y16;
        // frame.Dispose();
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
                if(!hasColorStart)
                {
                    colorSensor.Start(colorProfile, OnColorFrame);
                    hasColorStart = true;
                    // Debug.Log("start color stream");
                }
                break;
            case StreamType.OB_STREAM_DEPTH:
                if(!hasDepthStart)
                {
                    depthSensor.Start(depthProfile, OnDepthFrame);
                    hasDepthStart = true;
                    // Debug.Log("start depth stream");
                }
                break;
            case StreamType.OB_STREAM_IR:
                if(!hasIrStart)
                {
                    irSensor.Start(irProfile, OnIRFrame);
                    hasIrStart = true;
                    // Debug.Log("start ir stream");
                }
                break;
        }
    }

    public void StopStream(StreamType streamType)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                if(hasColorStart)
                {
                    colorSensor.Stop();
                    hasColorStart = false;
                    Debug.Log("stop color stream");
                }
                break;
            case StreamType.OB_STREAM_DEPTH:
                if(hasDepthStart)
                {
                    depthSensor.Stop();
                    hasDepthStart = false;
                    Debug.Log("stop depth stream");
                }
                break;
            case StreamType.OB_STREAM_IR:
                if(hasIrStart)
                {
                    irSensor.Stop();
                    hasIrStart = false;
                    Debug.Log("stop ir stream");
                }
                break;
        }
    }

    public StreamProfileList GetStreamProfiles(StreamType streamType)
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
                Debug.Log(string.Format("set color profile: {0}x{1}@{2} {3}",
                            colorProfile.GetWidth(),
                            colorProfile.GetHeight(),
                            colorProfile.GetFPS(),
                            colorProfile.GetFormat()));
                return;
            case StreamType.OB_STREAM_DEPTH:
                depthProfile = profile;
                Debug.Log(string.Format("set depth profile: {0}x{1}@{2} {3}",
                            depthProfile.GetWidth(),
                            depthProfile.GetHeight(),
                            depthProfile.GetFPS(),
                            depthProfile.GetFormat()));
                return;
            case StreamType.OB_STREAM_IR:
                irProfile = profile;
                Debug.Log(string.Format("set ir profile: {0}x{1}@{2} {3}",
                            irProfile.GetWidth(),
                            irProfile.GetHeight(),
                            irProfile.GetFPS(),
                            irProfile.GetFormat()));
                return;
        }
        Debug.Log(string.Format("no stream type: {0} profile found", streamType));
    }

    // public CommonFrame GetStreamData(StreamType streamType)
    // {
    //     switch (streamType)
    //     {
    //         case StreamType.OB_STREAM_COLOR:
    //             return colorData;
    //         case StreamType.OB_STREAM_DEPTH:
    //             return depthData;
    //         case StreamType.OB_STREAM_IR:
    //             return irData;
    //     }
    //     Debug.Log(string.Format("no stream type: {0} data found", streamType));
    //     return null;
    // }

    public void SetD2CEnable(bool enable)
    {
        if (device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL))
        {
            device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL, enable);
        }
        else if(device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL))
        {
			device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL, enable);
        }
    }

    public void SetMirror(StreamType streamType, bool mirror)
    {
        if(streamType == StreamType.OB_STREAM_COLOR)
        {
            if(device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_COLOR_MIRROR_BOOL))
            {
                device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_COLOR_MIRROR_BOOL, mirror);
            }
        }
        else if(streamType == StreamType.OB_STREAM_DEPTH)
        {
            if(device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_MIRROR_BOOL))
            {
                device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_MIRROR_BOOL, mirror);
            }
        }
        else if(streamType == StreamType.OB_STREAM_IR)
        {
            if(device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_IR_MIRROR_BOOL))
            {
                device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_IR_MIRROR_BOOL, mirror);
            }
        }
    }

    public DeviceInfo GetDeviceInfo()
    {
        return device.GetDeviceInfo();
    }

    public Device GetDevice()
    {
        return device;
    }

    public void SetInitHandle(OrbbecInitHandle handle)
    {
        initHandle = handle;
    }
}
}