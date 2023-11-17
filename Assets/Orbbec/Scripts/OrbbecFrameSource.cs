using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using System;

namespace OrbbecUnity
{
    public class OrbbecFrameSource : MonoBehaviour
    {
        protected OrbbecVideoFrame obColorFrame = new OrbbecVideoFrame();
        protected OrbbecVideoFrame obDepthFrame = new OrbbecVideoFrame();
        protected OrbbecVideoFrame obIrFrame = new OrbbecVideoFrame();
        protected OrbbecVideoFrame obIrLeftFrame = new OrbbecVideoFrame();
        protected OrbbecVideoFrame obIrRightFrame = new OrbbecVideoFrame();

        protected void FrameToOrbbecFrame(VideoFrame frame, ref OrbbecVideoFrame orbbecFrame)
        {
            if (frame != null)
            {
                orbbecFrame.width = (int)frame.GetWidth();
                orbbecFrame.height = (int)frame.GetHeight();
                orbbecFrame.frameType = frame.GetFrameType();
                orbbecFrame.format = frame.GetFormat();
                var dataSize = frame.GetDataSize();
                if (orbbecFrame.data == null || orbbecFrame.data.Length != dataSize)
                {
                    orbbecFrame.data = new byte[dataSize];
                }
                frame.CopyData(ref orbbecFrame.data);
                frame.Dispose();
            }
        }

        public OrbbecVideoFrame GetColorFrame()
        {
            return obColorFrame;
        }

        public OrbbecVideoFrame GetDepthFrame()
        {
            return obDepthFrame;
        }

        public OrbbecVideoFrame GetIrFrame()
        {
            return obIrFrame;
        }

        public OrbbecVideoFrame GetIrLeftFrame()
        {
            return obIrLeftFrame;
        }

        public OrbbecVideoFrame GetIrRightFrame()
        {
            return obIrRightFrame;
        }
    }
}
