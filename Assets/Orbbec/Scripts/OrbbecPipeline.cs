using System.Collections;
using Orbbec;
using UnityEngine;
using UnityEngine.Events;

namespace OrbbecUnity
{
    [System.Serializable]
    public class PipelineInitEvent : UnityEvent {}

    public class OrbbecPipeline : MonoBehaviour
    {
        public OrbbecDevice orbbecDevice;
        public OrbbecConfig orbbecConfig;
        public PipelineInitEvent onPipelineInit;

        private bool hasInit;
        private Pipeline pipeline;
        private Config config;

        public bool HasInit
        {
            get
            {
                return hasInit;
            }
        }

        public Pipeline Pipeline
        {
            get
            {
                return pipeline;
            }
        }

        void Start()
        {
            orbbecDevice.onDeviceFound.AddListener(InitPipeline);
        }

        void OnDestroy()
        {
            if(hasInit)
            {
                config.Dispose();
                pipeline.Dispose();
            }
        }

        private void InitPipeline(Device device)
        {
            pipeline = new Pipeline(device);
            config = new Config();
            hasInit = true;
            onPipelineInit.Invoke();
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