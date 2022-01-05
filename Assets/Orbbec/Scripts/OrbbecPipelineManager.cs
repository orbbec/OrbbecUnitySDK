using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using UnityEngine.Events;
using System.Collections.Concurrent;
using System;

public class OrbbecPipelineManager : MonoBehaviour
{
    public ImageMode colorMode;
    public ImageMode depthMode;
    public ImageMode irMode;
    public bool enableColor;
    public bool enableDepth;
    public bool enableIR;
    public bool autoStart;
    public ImageData colorData;
    public ImageData depthData;
    public ImageData irData;

    private Pipeline pipeline;
    private Config config;
    private StreamProfile[] colorProfiles;
    private StreamProfile[] depthProfiles;
    private StreamProfile[] irProfiles;
    private StreamProfile colorProfile;
    private StreamProfile depthProfile;
    private StreamProfile irProfile;

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
        pipeline = new Pipeline();
        config = new Config();

        colorProfiles = pipeline.GetStreamProfiles(SensorType.OB_SENSOR_COLOR);
        depthProfiles = pipeline.GetStreamProfiles(SensorType.OB_SENSOR_DEPTH);
        irProfiles = pipeline.GetStreamProfiles(SensorType.OB_SENSOR_IR);
        
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
        
        if (enableColor)
        {
            config.EnableStream(colorProfile);
            Debug.Log("auto start color stream");
        }
        if (enableDepth)
        {
            config.EnableStream(depthProfile);
            Debug.Log("auto start depth stream");
        }
        if (enableIR)
        {
            config.EnableStream(irProfile);
            Debug.Log("auto start ir stream");
        }
        if(autoStart)
        {
            pipeline.Start(config, FrameSetCallback);
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
        config.Dispose();
        pipeline.Dispose();
        Debug.Log("SDK has released");
        hasInit = false;
    }

    private void FrameSetCallback(Frameset frameset)
    {
        if(frameset == null)
        {
            return;
        }

        ColorFrame colorFrame = frameset.GetColorFrame();
        if(colorFrame != null)
        {
            int dataSize = (int)colorFrame.GetDataSize();
            if(colorData.data == null || colorData.data.Length != dataSize)
            {
                colorData.data = new byte[dataSize];
            }
            colorFrame.CopyData(ref colorData.data);
            colorData.width = (int)colorFrame.GetWidth();
            colorData.height = (int)colorFrame.GetHeight();
            colorData.format = colorFrame.GetFormat();
            colorFrame.Dispose();
        }

        DepthFrame depthFrame = frameset.GetDepthFrame();
        if(depthFrame != null)
        {
            int dataSize = (int)depthFrame.GetDataSize();
            if(depthData.data == null || depthData.data.Length != dataSize)
            {
                depthData.data = new byte[dataSize];
            }
            depthFrame.CopyData(ref depthData.data);
            depthData.width = (int)depthFrame.GetWidth();
            depthData.height = (int)depthFrame.GetHeight();
            depthData.format = depthFrame.GetFormat();
            depthFrame.Dispose();
        }

        IRFrame irFrame = frameset.GetIRFrame();
        if(irFrame != null)
        {
            int dataSize = (int)irFrame.GetDataSize();
            if(irData.data == null || irData.data.Length != dataSize)
            {
                irData.data = new byte[dataSize];
            }
            irFrame.CopyData(ref irData.data);
            irData.width = (int)irFrame.GetWidth();
            irData.height = (int)irFrame.GetHeight();
            irData.format = irFrame.GetFormat();
            irFrame.Dispose();
        }

        frameset.Dispose();
    }
}
