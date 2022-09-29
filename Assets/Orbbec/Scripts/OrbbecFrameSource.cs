using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using System;

namespace OrbbecUnity
{
    public class OrbbecFrameSource : MonoBehaviour
    {
        protected OrbbecFrame obColorFrame = new OrbbecFrame();
        protected OrbbecFrame obDepthFrame = new OrbbecFrame();
        protected OrbbecFrame obIrFrame = new OrbbecFrame();

        protected void FrameToOrbbecFrame(VideoFrame frame, ref OrbbecFrame orbbecFrame)
        {
            if (frame != null)
            {
                orbbecFrame.width = (int)frame.GetWidth();
                orbbecFrame.height = (int)frame.GetHeight();
                var dataSize = frame.GetDataSize();
                if (orbbecFrame.data == null || orbbecFrame.data.Length != dataSize)
                {
                    orbbecFrame.data = new byte[dataSize];
                }
                frame.CopyData(ref orbbecFrame.data);
                frame.Dispose();
            }
        }

        public OrbbecFrame GetColorFrame()
        {
            return obColorFrame;
        }

        public OrbbecFrame GetDepthFrame()
        {
            return obDepthFrame;
        }

        public OrbbecFrame GetIrFrame()
        {
            return obIrFrame;
        }
    }
}
