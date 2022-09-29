using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using System;

namespace OrbbecUnity
{
    public class OrbbecPipelineFrameSource : OrbbecFrameSource
    {
        public OrbbecPipeline pipeline;

        void Start()
        {
            pipeline.SetFramesetCallback(OnFrameset);
        }

        private void OnFrameset(Frameset frameset)
        {
            var colorFrame = frameset.GetColorFrame();
            FrameToOrbbecFrame(colorFrame, ref obColorFrame);

            var depthFrame = frameset.GetDepthFrame();
            FrameToOrbbecFrame(depthFrame, ref obDepthFrame);

            var irFrame = frameset.GetIRFrame();
            FrameToOrbbecFrame(irFrame, ref obIrFrame);

            frameset.Dispose();
        }
    }
}
