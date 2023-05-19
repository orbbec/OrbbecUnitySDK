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
        public OrbbecProfile[] orbbecProfiles;
        public PipelineInitEvent onPipelineInit;

        private bool hasInit;
        private Pipeline pipeline;
        private Config config;
        private FramesetCallback framesetCallback;

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

        public Config Config
        {
            get
            {
                return config;
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
            InitConfig();
            hasInit = true;
            onPipelineInit.Invoke();
        }

        private void InitConfig()
        {
            config = new Config();
            for (int i = orbbecProfiles.Length - 1; i >= 0; i--)
            {
                var streamProfile = FindProfile(orbbecProfiles[i]);
                if(streamProfile != null)
                {
                    config.EnableStream(streamProfile);
                }
            }
        }

        private StreamProfile FindProfile(OrbbecProfile obProfile)
        {
            var profileList = pipeline.GetStreamProfileList(obProfile.sensorType);
            VideoStreamProfile streamProfile = null;
            try
            {
                streamProfile = profileList.GetVideoStreamProfile(obProfile.width, obProfile.height, obProfile.format, obProfile.fps);
                if(streamProfile != null)
                {
                    Debug.LogFormat("Profile found: {0}x{1}@{2} {3}", 
                            streamProfile.GetWidth(), 
                            streamProfile.GetHeight(), 
                            streamProfile.GetFPS(), 
                            streamProfile.GetFormat());
                }
                else
                {
                    Debug.LogWarning("Profile not found");
                }
            }
            catch (NativeException e)
            {
                Debug.Log(e.Message);
            }
            
            return streamProfile;
        }

        public void SetFramesetCallback(FramesetCallback callback)
        {
            framesetCallback = callback;
        }

        public void StartPipeline()
        {
            pipeline.Start(config, framesetCallback);
        }

        public void StopPipeline()
        {
            pipeline.Stop();
        }
    }
}