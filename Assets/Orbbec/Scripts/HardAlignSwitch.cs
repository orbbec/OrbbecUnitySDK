using System;
using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

public class HardAlignSwitch : MonoBehaviour
{

    public Toggle alignToggle;
    private OrbbecPipelineManager pipelineManager;
    private Device device;

    // Use this for initialization
    void Start()
    {
        pipelineManager = FindObjectOfType<OrbbecPipelineManager>();
		if(pipelineManager.HasInit())
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
        if (pipelineManager.HasInit())
        {
            device = pipelineManager.GetDevice();
            if (device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL))
            {
                bool align = device.GetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL);
                alignToggle.isOn = align;
            }
			else
			{
				alignToggle.isOn = false;
				alignToggle.interactable = false;
			}
            alignToggle.onValueChanged.AddListener((value) =>
            {
                if (device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL))
                {
					pipelineManager.StopDefaultStreams();
                    device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL, value);
					pipelineManager.StartDefaultStreams();
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
