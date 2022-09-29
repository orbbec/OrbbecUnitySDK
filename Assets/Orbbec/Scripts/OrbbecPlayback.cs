using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;

namespace OrbbecUnity
{
    public class OrbbecPlayback : MonoBehaviour
    {
        private Pipeline pipeline;
        private FramesetCallback framesetCallback;

        public void SetFramesetCallback(FramesetCallback callback)
        {
            framesetCallback = callback;
        }

        public void StartPlayback(string playbackPath)
        {
            pipeline = new Pipeline(playbackPath);
            pipeline.Start(null, framesetCallback);
        }

        public void StopPlayback()
        {
            pipeline.Stop();
            pipeline.Dispose();
        }
    }
}