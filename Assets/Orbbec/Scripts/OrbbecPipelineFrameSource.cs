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
            var colorFrame = frameset.GetFrame(FrameType.OB_FRAME_COLOR);
            if(colorFrame != null)
            {
                FrameToOrbbecFrame(colorFrame.As<VideoFrame>(), ref obColorFrame);
            }

            var depthFrame = frameset.GetFrame(FrameType.OB_FRAME_DEPTH);
            if(depthFrame != null)
            {
                FrameToOrbbecFrame(depthFrame.As<VideoFrame>(), ref obDepthFrame);
            }

            var irFrame = frameset.GetFrame(FrameType.OB_FRAME_IR);
            if(irFrame != null)
            {
                FrameToOrbbecFrame(irFrame.As<VideoFrame>(), ref obIrFrame);
            }

            var irLeftFrame = frameset.GetFrame(FrameType.OB_FRAME_IR_LEFT);
            if(irLeftFrame != null)
            {
                FrameToOrbbecFrame(irLeftFrame.As<VideoFrame>(), ref obIrLeftFrame);
            }

            var irRightFrame = frameset.GetFrame(FrameType.OB_FRAME_IR_RIGHT);
            if(irRightFrame != null)
            {
                FrameToOrbbecFrame(irRightFrame.As<VideoFrame>(), ref obIrRightFrame);
            }

            frameset.Dispose();
        }
    }
}
