using System;
using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

public class SyncAlignController : MonoBehaviour
{
	public Toggle syncToggle;
    public Toggle hardAlignToggle;
	public Toggle softAlignToggle;
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
			
            if (device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL))
            {
                bool align = device.GetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL);
                hardAlignToggle.isOn = align;
            }
			else
			{
				hardAlignToggle.isOn = false;
				hardAlignToggle.interactable = false;
			}
            hardAlignToggle.onValueChanged.AddListener((value) =>
            {
                if (device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL))
                {
					pipelineManager.StopDefaultStreams();
                    device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_HARDWARE_BOOL, value);
					pipelineManager.StartDefaultStreams();
                }
            });

			if(device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL))
			{
				bool align = device.GetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL);
                softAlignToggle.isOn = align;
			}
			else
			{
				softAlignToggle.isOn = false;
				softAlignToggle.interactable = false;
			}
            softAlignToggle.onValueChanged.AddListener((value) =>
            {
                if(device.IsPropertySupported(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL))
				{
                    pipelineManager.StopDefaultStreams();
					device.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_DEPTH_ALIGN_SOFTWARE_BOOL, value);
                    pipelineManager.StartDefaultStreams();
                }
            });
        }
    }
}
