﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using UnityEngine.Events;
using System.Collections.Concurrent;

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
    private Device device;
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

    // Use this for initialization
    void Start()
    {
        if(!hasInit)
        {
            InitSDK();
        }
    }

    private void InitSDK()
    {
        context = new Context();
        deviceList = context.QueryDeviceList();
        device = deviceList.GetDevice(0);
        colorSensor = device.GetSensor(SensorType.OB_SENSOR_COLOR);
        depthSensor = device.GetSensor(SensorType.OB_SENSOR_DEPTH);
        irSensor = device.GetSensor(SensorType.OB_SENSOR_IR);
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
                Debug.Log(string.Format("ir profile found: {0}x{1}@{2} {3}", 
                            irProfile.GetWidth(), 
                            irProfile.GetHeight(), 
                            irProfile.GetFPS(), 
                            irProfile.GetFormat()));
                irProfile = profile;
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
            colorSensor.Start(colorProfile, ColorFrameCallback);
            Debug.Log("auto start color stream");
        }
        if (enableDepth && autoStart)
        {
            depthSensor.Start(depthProfile, DepthFrameCallback);
            Debug.Log("auto start depth stream");
        }
        if (enableIR && autoStart)
        {
            irSensor.Start(irProfile, IRFrameCallback);
            Debug.Log("auto start ir stream");
        }
        Debug.Log("SDK has intialized");
        hasInit = true;
    }

    void OnDestroy()
    {
        if(hasInit)
        {
            ReleaseSDK();
        }
    }

    private void ReleaseSDK()
    {
        colorSensor.Dispose();
        depthSensor.Dispose();
        irSensor.Dispose();
        foreach (var profile in colorProfiles)
        {
            profile.Dispose();
        }
        foreach (var profile in depthProfiles)
        {
            profile.Dispose();
        }
        foreach (var profile in irProfiles)
        {
            profile.Dispose();
        }
        device.Dispose();
        deviceList.Dispose();
        context.Dispose();
        Debug.Log("SDK has released");
        hasInit = false;
    }

    private void ColorFrameCallback(Frame frame)
    {
        if(frame == null)
        {
            return;
        }
        // Debug.Log(frame.GetFrameType());
        ColorFrame colorFrame = frame as ColorFrame;
        int dataSize = (int)colorFrame.GetDataSize();
        if(colorData == null)
        {
            colorData = new StreamData();
        }
        if(colorData.data == null || colorData.data.Length != dataSize)
        {
            colorData.data = new byte[dataSize];
        }
        colorFrame.CopyData(ref colorData.data);
        colorData.width = (int)colorFrame.GetWidth();
        colorData.height = (int)colorFrame.GetHeight();
        colorData.format = colorFrame.GetFormat();
        frame.Dispose();
    }

    private void DepthFrameCallback(Frame frame)
    {
        if(frame == null)
        {
            return;
        }
        // Debug.Log(frame.GetFrameType());
        DepthFrame depthFrame = frame as DepthFrame;
        int dataSize = (int)depthFrame.GetDataSize();
        if(depthData == null)
        {
            depthData = new StreamData();
        }
        if(depthData.data == null || depthData.data.Length != dataSize)
        {
            depthData.data = new byte[dataSize];
        }
        depthFrame.CopyData(ref depthData.data);
        depthData.width = (int)depthFrame.GetWidth();
        depthData.height = (int)depthFrame.GetHeight();
        depthData.format = depthFrame.GetFormat();
        frame.Dispose();
    }

    private void IRFrameCallback(Frame frame)
    {
        if(frame == null)
        {
            return;
        }
        // Debug.Log(frame.GetFrameType());
        IRFrame irFrame = frame as IRFrame;
        int dataSize = (int)irFrame.GetDataSize();
        if(irData == null)
        {
            irData = new StreamData();
        }
        if(irData.data == null || irData.data.Length != dataSize)
        {
            irData.data = new byte[dataSize];
        }
        irFrame.CopyData(ref irData.data);
        irData.width = (int)irFrame.GetWidth();
        irData.height = (int)irFrame.GetHeight();
        irData.format = irFrame.GetFormat();
        frame.Dispose();
    }

    public void StartStream(StreamType streamType)
    {
        switch (streamType)
        {
            case StreamType.OB_STREAM_COLOR:
                colorSensor.Start(colorProfile, ColorFrameCallback);
                break;
            case StreamType.OB_STREAM_DEPTH:
                depthSensor.Start(depthProfile, DepthFrameCallback);
                break;
            case StreamType.OB_STREAM_IR:
                irSensor.Start(irProfile, IRFrameCallback);
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

    public void StartAllStream()
    {
        colorSensor.Start(colorProfile, ColorFrameCallback);
        depthSensor.Start(depthProfile, DepthFrameCallback);
        irSensor.Start(irProfile, IRFrameCallback);
    }
    
    public void StopAllStream()
    {
        colorSensor.Stop();
        depthSensor.Stop();
        irSensor.Stop();
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
}
