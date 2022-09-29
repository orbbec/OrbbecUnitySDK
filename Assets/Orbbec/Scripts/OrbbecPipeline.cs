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
        public bool record;
        public string recordPath;
        public bool playback;
        public string playbackPath;
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
            if(!playback)
            {
                orbbecDevice.onDeviceFound.AddListener(InitPipeline);
            }
            else
            {
                InitPipeline(null);
            }
        }

        void OnDestroy()
        {
            if(hasInit)
            {
                if(!playback)
                {
                    config.Dispose();
                }
                pipeline.Dispose();
            }
        }

        private void InitPipeline(Device device)
        {
            if(!playback)
            {
                pipeline = new Pipeline(device);
                InitConfig();
            }
            else
            {
                pipeline = new Pipeline(playbackPath);
            }
            hasInit = true;
            onPipelineInit.Invoke();
        }

        private void InitConfig()
        {
            config = new Config();
            foreach (var orbbecProfile in orbbecProfiles)
            {
                var streamProfile = FindProfile(orbbecProfile);
                if(streamProfile != null)
                {
                    config.EnableStream(streamProfile);
                }
            }
        }

        private StreamProfile FindProfile(OrbbecProfile orbbecProfile)
        {
            var profileList = pipeline.GetStreamProfileList(orbbecProfile.sensorType);
            var streamProfile = profileList.GetVideoStreamProfile(orbbecProfile.width, orbbecProfile.height, orbbecProfile.format, orbbecProfile.fps);
            if(streamProfile != null)
            {
                Debug.Log(string.Format("Profile found: {0}x{1}@{2} {3}", 
                        streamProfile.GetWidth(), streamProfile.GetHeight(), streamProfile.GetFPS(), streamProfile.GetFormat()));
            }
            else
            {
                Debug.LogWarning("Profile not found");
            }
            return streamProfile;
        }

        public void StartPipeline(FramesetCallback callback)
        {
            if(!playback)
            {
                pipeline.Start(config, callback);
                if(record)
                {
                    pipeline.StartRecord(recordPath);
                }
            }
            else
            {
                pipeline.Start(null, callback);
            }
        }

        public void StopPipeline()
        {
            if(record)
            {
                pipeline.StopRecord();
            }
            pipeline.Stop();
        }
    }
}