using System;
using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class SyncAlignController : MonoBehaviour
{
	public OrbbecPipeline pipeline;
	public Toggle syncToggle;
    public Toggle hardAlignToggle;
	public Toggle softAlignToggle;

    // Use this for initialization
    void Start()
    {
        pipeline.onPipelineInit.AddListener(OnSDKInit);
    }

    private void OnSDKInit()
    {
		syncToggle.onValueChanged.AddListener((value) =>
		{
			if(value)
			{
				pipeline.Pipeline.EnableFrameSync();
			}
			else
			{
				pipeline.Pipeline.DisableFrameSync();
			}
		});
		
		hardAlignToggle.onValueChanged.AddListener((value) =>
		{
			Config config = pipeline.Pipeline.GetConfig();
			if (value)
			{
				config.SetAlignMode(AlignMode.ALIGN_D2C_HW_MODE);
			}
			else
			{
				config.SetAlignMode(AlignMode.ALIGN_DISABLE);
			}
			pipeline.Pipeline.SwitchConfig(config);
		});

		softAlignToggle.onValueChanged.AddListener((value) =>
		{
			Config config = pipeline.Pipeline.GetConfig();
			if (value)
			{
				config.SetAlignMode(AlignMode.ALIGN_D2C_SW_MODE);
			}
			else
			{
				config.SetAlignMode(AlignMode.ALIGN_DISABLE);
			}
			pipeline.Pipeline.SwitchConfig(config);
		});
    }
}