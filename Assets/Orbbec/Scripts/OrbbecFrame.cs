using Orbbec;
using UnityEngine;

namespace OrbbecUnity
{
    public class OrbbecFrame
    {
        public byte[] data;
        public FrameType frameType;
        public Format format;
        public ulong systemTimestamp;
        public ulong timestamp;
    }

    public class OrbbecVideoFrame : OrbbecFrame
    {
        public int width;
        public int height;
    }

    public class OrbbecImuFrame : OrbbecFrame
    {
        public Vector3 value;
        public float temperature;
    }
}