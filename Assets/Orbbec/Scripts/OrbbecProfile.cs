using System.Collections;
using Orbbec;
using UnityEngine;

namespace OrbbecUnity
{
    [CreateAssetMenu(menuName = "OrbbecProfile")]
    public class OrbbecProfile : ScriptableObject
    {
        public SensorType sensorType;
        public int width;
        public int height;
        public int fps;
        public Format format;
    }
}