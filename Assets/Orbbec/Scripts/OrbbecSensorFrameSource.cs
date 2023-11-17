using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using System;

namespace OrbbecUnity
{
    public class OrbbecSensorFrameSource : OrbbecFrameSource
    {
        public OrbbecSensor colorSensor;
        public OrbbecSensor depthSensor;
        public OrbbecSensor irSensor;
        public OrbbecSensor irLeftSensor;
        public OrbbecSensor irRightSensor;
        
        void Start()
        {
            if(colorSensor != null)
            {
                colorSensor.SetFrameCallback(OnFrame);
            }
            if(depthSensor != null)
            {
                depthSensor.SetFrameCallback(OnFrame);
            }
            if(irSensor != null)
            {
                irSensor.SetFrameCallback(OnFrame);
            }
            if(irLeftSensor != null)
            {
                irLeftSensor.SetFrameCallback(OnFrame);
            }
            if(irRightSensor != null)
            {
                irRightSensor.SetFrameCallback(OnFrame);
            }
        }

        private void OnFrame(Frame frame)
        {
            switch (frame.GetFrameType())
            {
                case FrameType.OB_FRAME_COLOR:
                    var colorFrame = frame.As<ColorFrame>();
                    FrameToOrbbecFrame(colorFrame, ref obColorFrame);
                    break;
                case FrameType.OB_FRAME_DEPTH:
                    var depthFrame = frame.As<DepthFrame>();
                    FrameToOrbbecFrame(depthFrame, ref obDepthFrame);
                    break;
                case FrameType.OB_FRAME_IR:
                    var irFrame = frame.As<IRFrame>();
                    FrameToOrbbecFrame(irFrame, ref obIrFrame);
                    break;
                case FrameType.OB_FRAME_IR_LEFT:
                    var irLeftFrame = frame.As<IRFrame>();
                    FrameToOrbbecFrame(irLeftFrame, ref obIrLeftFrame);
                    break;
                case FrameType.OB_FRAME_IR_RIGHT:
                    var irRightFrame = frame.As<IRFrame>();
                    FrameToOrbbecFrame(irRightFrame, ref obIrRightFrame);
                    break;
            } 
            frame.Dispose();
        }
    }
}
