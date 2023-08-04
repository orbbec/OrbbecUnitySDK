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
            for (int i = 0; i < orbbecProfiles.Length - 1; i++)
            {
                var streamProfile = FindProfile(orbbecProfiles[i], StreamType.OB_STREAM_COLOR);
                if(streamProfile != null)
                {
                    config.EnableStream(streamProfile);
                    break;
                }
            }
            for (int i = 0; i < orbbecProfiles.Length - 1; i++)
            {
                var streamProfile = FindProfile(orbbecProfiles[i], StreamType.OB_STREAM_DEPTH);
                if(streamProfile != null)
                {
                    config.EnableStream(streamProfile);
                    break;
                }
            }
            for (int i = 0; i < orbbecProfiles.Length - 1; i++)
            {
                var streamProfile = FindProfile(orbbecProfiles[i], StreamType.OB_STREAM_IR);
                if(streamProfile != null)
                {
                    config.EnableStream(streamProfile);
                    break;
                }
            }
        }

        private VideoStreamProfile FindProfile(OrbbecProfile obProfile, StreamType streamType)
        {
            var profileList = pipeline.GetStreamProfileList(obProfile.sensorType);
            try
            {
                VideoStreamProfile streamProfile = profileList.GetVideoStreamProfile(obProfile.width, obProfile.height, obProfile.format, obProfile.fps);
                if(streamProfile != null && streamProfile.GetStreamType() == streamType)
                {
                    Debug.LogFormat("Profile found: {0}x{1}@{2} {3}", 
                            streamProfile.GetWidth(), 
                            streamProfile.GetHeight(), 
                            streamProfile.GetFPS(), 
                            streamProfile.GetFormat());
                    return streamProfile;
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
            
            return null;
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