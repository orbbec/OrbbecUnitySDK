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
        
        void Start()
        {
            colorSensor.SetFrameCallback(OnFrame);
            depthSensor.SetFrameCallback(OnFrame);
            irSensor.SetFrameCallback(OnFrame);
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
                    FrameToOrbbecFrame(depthFrame, ref obColorFrame);
                    break;
                case FrameType.OB_FRAME_IR:
                    var irFrame = frame.As<IRFrame>();
                    FrameToOrbbecFrame(irFrame, ref obColorFrame);
                    break;
            } 
            frame.Dispose();
        }
    }
}
