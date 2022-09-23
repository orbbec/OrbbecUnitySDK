using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;

public class PipelineSample : MonoBehaviour
{
    FormatConvertFilter filter;
    Pipeline pipeline;
    Config config;

    // Start is called before the first frame update
    void Start()
    {
        filter = new FormatConvertFilter();
        filter.SetConvertFormat(ConvertFormat.FORMAT_MJPEG_TO_RGB888);
        pipeline = new Pipeline();
        //StreamProfile colorProfile = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_COLOR).GetProfile(0);
        StreamProfile colorProfile = null;
        StreamProfileList colorProfiles = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_COLOR);
        for(int i = 0; i < colorProfiles.ProfileCount(); i++)
        {
            var profile = colorProfiles.GetProfile(i);
            if(profile.GetWidth() == 640 && profile.GetHeight() == 480 && profile.GetFPS() == 30 && profile.GetFormat() == Format.OB_FORMAT_MJPG)
            {
                Debug.Log(string.Format("color profile: {0}x{1} {2}", profile.GetWidth(), profile.GetHeight(), profile.GetFormat()));
                colorProfile = profile;
            }
        }
        StreamProfile depthProfile = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_DEPTH).GetProfile(0);
        config = new Config();
        config.EnableStream(colorProfile);
        config.EnableStream(depthProfile);

        byte[] colorData = null;
        byte[] depthData = null;

        pipeline.Start(config, (frameset) =>{
            if(frameset == null)
            {
                Debug.Log("empty frameset");
                return;
            }

            ColorFrame colorFrame = frameset.GetColorFrame();
            if(colorFrame != null)
            {
                if(colorData == null)
                {
                    colorData = new byte[colorFrame.GetDataSize()];
                }
                int width = (int)colorFrame.GetWidth();
                int height = (int)colorFrame.GetHeight();
                Debug.Log(string.Format("Color {0} x {1} {2}", width, height, colorFrame.GetDataSize()));
                colorFrame.CopyData(ref colorData);
                Debug.Log(string.Format("Color {0}", colorData[colorData.Length / 2]));

                var frame = filter.Process(colorFrame);
                if (frame != null)
                {
                    var rgbFrame = frame.As<ColorFrame>();
                    Debug.Log(string.Format("RGB {0} x {1} {2} {3}", rgbFrame.GetWidth(), rgbFrame.GetHeight(), rgbFrame.GetDataSize(), rgbFrame.GetFormat()));
                    rgbFrame.Dispose();
                }
                frame.Dispose();

                colorFrame.Dispose();
            }


            DepthFrame depthFrame = frameset.GetDepthFrame();
            if(depthFrame != null)
            {
                if(depthData == null)
                {
                    depthData = new byte[depthFrame.GetDataSize()];
                }
                Debug.Log(string.Format("Depth {0} x {1} {2}", depthFrame.GetWidth(), depthFrame.GetHeight(), depthFrame.GetDataSize()));
                depthFrame.CopyData(ref depthData);
                Debug.Log(string.Format("Depth {0}", depthData[depthData.Length / 2]));
                depthFrame.Dispose();
            }
            
            frameset.Dispose();
        });
    }

    int count = 0;
    // Update is called once per frame
    void Update()
    {
        count++;
        if(count == 1000 || count == 3000 || count == 5000 || count == 7000 || count == 9000)
        {
            StreamProfile colorProfile = null;
            StreamProfileList colorProfiles = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_COLOR);
            for(int i = 0; i < colorProfiles.ProfileCount(); i++)
            {
                var profile = colorProfiles.GetProfile(i);
                if(profile.GetWidth() == 1280 && profile.GetHeight() == 720 && profile.GetFPS() == 30 && profile.GetFormat() == Format.OB_FORMAT_MJPG)
                {
                    Debug.Log(string.Format("color profile: {0}x{1} {2}", profile.GetWidth(), profile.GetHeight(), profile.GetFormat()));
                    colorProfile = profile;
                    break;
                }
            }
            config.EnableStream(colorProfile);
            pipeline.SwitchConfig(config);
            filter = new FormatConvertFilter();
            filter.SetConvertFormat(ConvertFormat.FORMAT_MJPEG_TO_RGB888);
        }
        if(count == 2000 || count == 4000 || count == 6000 || count == 8000 || count == 10000)
        {
            StreamProfile colorProfile = null;
            StreamProfileList colorProfiles = pipeline.GetStreamProfileList(SensorType.OB_SENSOR_COLOR);
            for(int i = 0; i < colorProfiles.ProfileCount(); i++)
            {
                var profile = colorProfiles.GetProfile(i);
                if(profile.GetWidth() == 1920 && profile.GetHeight() == 1080 && profile.GetFPS() == 30 && profile.GetFormat() == Format.OB_FORMAT_MJPG)
                {
                    Debug.Log(string.Format("color profile: {0}x{1} {2}", profile.GetWidth(), profile.GetHeight(), profile.GetFormat()));
                    colorProfile = profile;
                    break;
                }
            }
            config.EnableStream(colorProfile);
            pipeline.SwitchConfig(config);
            filter = new FormatConvertFilter();
            filter.SetConvertFormat(ConvertFormat.FORMAT_MJPEG_TO_RGB888);
        }
    }
}
