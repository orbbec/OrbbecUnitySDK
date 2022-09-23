using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using System.Collections.Concurrent;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace OrbbecUnity
{
public class OrbbecPipelineManager : Singleton<OrbbecPipelineManager>
{
    public ImageMode colorMode;
    public ImageMode depthMode;
    public ImageMode irMode;
    public bool enableColor;
    public bool enableDepth;
    public bool enableIR;
    public bool enableD2C;
    // public bool enablePointcloud;
    public bool autoStart;

    private Context context;
    private Device device;
    private DeviceList deviceList;
    private Pipeline pipeline;
    private Config config;
    private StreamProfileList colorProfiles;
    private StreamProfileList depthProfiles;
    private StreamProfileList irProfiles;
    private StreamProfile colorProfile;
    private StreamProfile depthProfile;
    private StreamProfile irProfile;
    // private CommonFrame colorData;
    // private CommonFrame rgbData;
    // private CommonFrame depthData;
    // private CommonFrame irData;
    private FormatConvertFilter formatConvertFilter;
    // private PointCloudFilter pointCloudFilter;

    private const int MAX_QUEUE_SIZE = 1;
    private ConcurrentQueue<ColorFrame> colorFrameQueue = new ConcurrentQueue<ColorFrame>();
    // private FixedSizedQueue<ColorFrame> rgbFrameQueue = new FixedSizedQueue<ColorFrame>(MAX_QUEUE_SIZE);
    private ConcurrentQueue<DepthFrame> depthFrameQueue = new ConcurrentQueue<DepthFrame>();
    private ConcurrentQueue<IRFrame> irFrameQueue = new ConcurrentQueue<IRFrame>();

    private bool hasInit = false;
    private bool hasPipelineStart = false;

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
                InitPipeline();
                hasInit = true;
                if(initHandle != null)
                {
                    initHandle.Invoke();
                }
                break;
            }
            else
            {
                deviceList.Dispose();
            }
        }
    }

    private void InitPipeline()
    {
        pipeline = new Pipeline(device);
        config = new Config();
        formatConvertFilter = new FormatConvertFilter();
        formatConvertFilter.SetConvertFormat(ConvertFormat.FORMAT_MJPEG_TO_RGB888);
        // pointCloudFilter = new PointCloudFilter();
        // pointCloudFilter.SetAlignState(true);
        // pointCloudFilter.SetPointFormat(Orbbec.Format.OB_FORMAT_POINT);
        // pointCloudFilter.SetCameraParam(pipeline.GetCameraParam());

        colorProfiles = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_COLOR);
        depthProfiles = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_DEPTH);
        irProfiles = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_IR);

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

        if (enableColor)
        {
            config.EnableStream(colorProfile);
            Debug.Log("enable color stream");
        }
        if (enableDepth)
        {
            config.EnableStream(depthProfile);
            Debug.Log("enable depth stream");
        }
        if (enableIR)
        {
            config.EnableStream(irProfile);
            Debug.Log("enable ir stream");
        }
        if (enableD2C)
        {
            config.SetAlignMode(Orbbec.AlignMode.ALIGN_D2C_HW_MODE);
        }
        if (autoStart)
        {
            StartPipeline();
            Debug.Log("pipeline has started");
        }
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
        if (config != null)
        {
            config.Dispose();
        }
        if (pipeline != null)
        {
            pipeline.Dispose();
        }
        if (device != null)
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

    private void OnFrameset(Frameset frameset)
    {
        if (frameset == null)
        {
            return;
        }

        ColorFrame colorFrame = frameset.GetColorFrame();
        if (colorFrame != null)
        {
            // Debug.Log(colorFrame.GetFrameType());
            if(colorFrameQueue.Count < MAX_QUEUE_SIZE)
            {
                colorFrameQueue.Enqueue(colorFrame);
            }
            else
            {
                colorFrame.Dispose();
            }
        }

        DepthFrame depthFrame = frameset.GetDepthFrame();
        if (depthFrame != null)
        {
            // Debug.Log(depthFrame.GetFrameType());
            if(depthFrameQueue.Count < MAX_QUEUE_SIZE)
            {
                depthFrameQueue.Enqueue(depthFrame);
            }
            else
            {
                depthFrame.Dispose();
            }
        }

        IRFrame irFrame = frameset.GetIRFrame();
        if (irFrame != null)
        {
            Debug.Log(irFrame.GetFrameType());
            if(irFrameQueue.Count < MAX_QUEUE_SIZE)
            {
                irFrameQueue.Enqueue(irFrame);
            }
            else
            {
                irFrame.Dispose();
            }
        }

        frameset.Dispose();
    }

    public ColorFrame FetchColorFrame()
    {
        ColorFrame frame = null;
        if(!colorFrameQueue.TryDequeue(out frame))
        {
            // Debug.LogWarning("Color frame queue is empty!");
        }
        return frame;
    }

    public DepthFrame FetchDepthFrame()
    {
        DepthFrame frame = null;
        if(!depthFrameQueue.TryDequeue(out frame))
        {
            // Debug.LogWarning("Depth frame queue is empty!");
        }
        return frame;
    }

    public IRFrame FetchIRFrame()
    {
        IRFrame frame = null;
        if(!irFrameQueue.TryDequeue(out frame))
        {
            // Debug.LogWarning("IR frame queue is empty!");
        }
        return frame;
    }

    public ColorFrame ConvertMjpegToRGBFrame(ColorFrame mjpegFrame)
    {
        ColorFrame rgbFrame = null;
        if(mjpegFrame.GetFormat() == Orbbec.Format.OB_FORMAT_MJPG)
        {
            var frame = formatConvertFilter.Process(mjpegFrame);
            if(frame != null)
            {
                rgbFrame = frame.As<ColorFrame>();
            }
            frame.Dispose();
        }
        else
        {
            Debug.LogWarning("Color frame format is not mjpeg!");
        }
        return rgbFrame;
    }

    public bool HasInit()
    {
        return hasInit;
    }

    public Config GetConfig()
    {
        return config;
    }

    public void SetConfig(Config config)
    {
        this.config = config;
    }

    public void SwitchConfig(Config config)
    {
        pipeline.SwitchConfig(config);
        ColorFrame colorFrame;
        while(colorFrameQueue.TryDequeue(out colorFrame))
        {
            colorFrame.Dispose();
        }
        DepthFrame depthFrame;
        while(depthFrameQueue.TryDequeue(out depthFrame))
        {
            depthFrame.Dispose();
        }
        IRFrame irFrame;
        while(irFrameQueue.TryDequeue(out irFrame))
        {
            irFrame.Dispose();
        }
        formatConvertFilter = new FormatConvertFilter();
        formatConvertFilter.SetConvertFormat(ConvertFormat.FORMAT_MJPEG_TO_RGB888);
        // pointCloudFilter = new PointCloudFilter();
        // pointCloudFilter.SetAlignState(true);
        // pointCloudFilter.SetPointFormat(Orbbec.Format.OB_FORMAT_POINT);
        // pointCloudFilter.SetCameraParam(pipeline.GetCameraParam());
    }

    public void StartPipeline()
    {
        if(hasPipelineStart) return;
        pipeline.Start(config, OnFrameset);
        hasPipelineStart = true;
    }

    public void StopPipeline()
    {
        if(!hasPipelineStart) return;
        pipeline.Stop();
        hasPipelineStart = false;
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

    // public CommonFrame GetStreamData(StreamType streamType)
    // {
    //     // switch (streamType)
    //     // {
    //     //     case StreamType.OB_STREAM_COLOR:
    //     //         return colorData;
    //     //     case StreamType.OB_STREAM_DEPTH:
    //     //         return depthData;
    //     //     case StreamType.OB_STREAM_IR:
    //     //         return irData;
    //     // }
    //     // Debug.Log(string.Format("no stream type: {0} data found", streamType));
    //     return null;
    // }

    // public CommonFrame GetFrame(FrameType frameType)
    // {
    //     // CommonFrame frame;
    //     // switch (frameType)
    //     // {
    //     //     case FrameType.COLOR:
    //     //         if(colorFrameQueue.TryDequeue(out frame))
    //     //         {
    //     //             return frame;
    //     //         }
    //     //         break;
    //     //     case FrameType.DEPTH:
    //     //         if(depthFrameQueue.TryDequeue(out frame))
    //     //         {
    //     //             return frame;
    //     //         }
    //     //         break;
    //     //     case FrameType.IR:
    //     //         if(irFrameQueue.TryDequeue(out frame))
    //     //         {
    //     //             return frame;
    //     //         }
    //     //         break;
    //     // }
    //     return null; 
    // }

    // public CommonFrame GetRGBFrame()
    // {
    //     // CommonFrame frame;
    //     // if(rgbFrameQueue.TryDequeue(out frame))
    //     // {
    //     //     return frame;
    //     // }
    //     return null;
    // }

    // public void SetD2CEnable(bool enable)
    // {
    //     if (device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL))
    //     {
    //         device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL, enable);
    //     }
    //     else if(device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL))
    //     {
	// 		device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL, enable);
    //     }
    // }

    public Device GetDevice()
    {
        return device;
    }

    public Pipeline GetPipeline()
    {
        return pipeline;
    }

    public void SetInitHandle(OrbbecInitHandle handle)
    {
        initHandle = handle;
    }
}
}