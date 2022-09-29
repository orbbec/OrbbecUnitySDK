using System.Collections;
using System.Collections.Generic;
using OrbbecUnity;
using UnityEngine;

public class AutoStartPipeline : MonoBehaviour
{
    public OrbbecPipeline pipeline;
    
    // Start is called before the first frame update
    void Start()
    {
        pipeline.onPipelineInit.AddListener(()=>{
            pipeline.StartPipeline();
        });
    }
}
