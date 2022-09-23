using System.Collections;
using Orbbec;
using UnityEngine;

namespace OrbbecUnity
{
    [CreateAssetMenu(menuName = "OrbbecConfig")]
    public class OrbbecConfig : ScriptableObject
    {
        public OrbbecProfile[] orbbecProfile;
    }
}