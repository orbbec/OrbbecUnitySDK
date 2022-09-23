using System.Collections;
using Orbbec;
using UnityEngine;
using UnityEngine.Events;

namespace OrbbecUnity
{
    [System.Serializable]
    public class PipelineInitEvent : UnityEvent {}

    public class OrbbecPipeline : Singleton<OrbbecPipeline>
    {
        public int deviceIndex;
        public OrbbecConfig orbbecConfig;
        public PipelineInitEvent onPipelineInit;

        private Context context;
        private DeviceList deviceList;
        private Device device;
        private Pipeline pipeline;
        private Config config;

        public Pipeline Pipeline
        {
            get
            {
                return pipeline;
            }
        }

        void Start()
        {
            context = OrbbecContext.Instance.Context;
            if(OrbbecContext.Instance.HasInit)
            {
                StartCoroutine(WaitForDevice());
            }
        }

        private IEnumerator WaitForDevice()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                deviceList = context.QueryDeviceList();
                if (deviceList.DeviceCount() > deviceIndex)
                {
                    device = deviceList.GetDevice((uint)deviceIndex);
                    DeviceInfo deviceInfo = device.GetDeviceInfo();
                    Debug.Log(string.Format(
                        "Device found: {0} {1} {2:X} {3:X}", 
                        deviceInfo.Name(), 
                        deviceInfo.SerialNumber(),
                        deviceInfo.Vid(),
                        deviceInfo.Pid()));
                    InitPipeline();
                    onPipelineInit.Invoke();
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
        }

        private StreamProfile FindProfile(OrbbecProfile orbbecProfile)
        {
            StreamProfile streamProfile = null;
            var profileList = pipeline.GetStreamProfileList(orbbecProfile.sensorType);
            for (int i = 0; i < profileList.ProfileCount(); i++)
            {
                var profile = profileList.GetProfile(i);
                if(profile.GetWidth() == orbbecProfile.width && 
                    profile.GetHeight() == orbbecProfile.height && 
                    profile.GetFPS() == orbbecProfile.fps && 
                    profile.GetFormat() == orbbecProfile.format)
                {
                    Debug.Log(string.Format("Profile found: {0}x{1}@{2} {3}", 
                        orbbecProfile.width, orbbecProfile.height, orbbecProfile.fps, orbbecProfile.format));
                    streamProfile = profile;
                    break;
                }
            }
            return streamProfile;
        }

        public void StartPipeline(FramesetCallback callback)
        {
            foreach (var orbbecProfile in orbbecConfig.orbbecProfile)
            {
                var streamProfile = FindProfile(orbbecProfile);
                if(streamProfile != null)
                {
                    config.EnableStream(streamProfile);
                }
            }
            pipeline.Start(config, callback);
        }

        public void StopPipeline()
        {
            pipeline.Stop();
        }
    }
}