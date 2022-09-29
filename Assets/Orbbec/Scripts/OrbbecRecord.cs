using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;

namespace OrbbecUnity
{
    public class OrbbecRecord : MonoBehaviour
    {
        public OrbbecPipeline pipeline;

        public void StartRecord(string recordPath)
        {
            if(pipeline.HasInit)
            {
                pipeline.Pipeline.StartRecord(recordPath);
            }
        }

        public void StopRecord()
        {
            if(pipeline.HasInit)
            {
                pipeline.Pipeline.StopRecord();
            }
        }
    }
}