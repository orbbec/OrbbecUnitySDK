using System;
using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

public class SyncSwitch : MonoBehaviour
{

    public Toggle syncToggle;
    private OrbbecPipelineManager pipelineManager;

    // Use this for initialization
    void Start()
    {
        pipelineManager = FindObjectOfType<OrbbecPipelineManager>();
        if (pipelineManager.HasInit())
        {
            OnSDKInit();
        }
        else
        {
            pipelineManager.SetInitHandle(OnSDKInit);
        }
    }

    private void OnSDKInit()
    {
        syncToggle.onValueChanged.AddListener((value) =>
        {
			if(value)
			{
				pipelineManager.GetPipeline().EnableFrameSync();
			}
			else
			{
				pipelineManager.GetPipeline().DisableFrameSync();
			}
        });
    }
}
