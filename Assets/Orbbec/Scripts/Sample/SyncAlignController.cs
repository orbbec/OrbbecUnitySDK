/**
 * 同步对齐示例
 *
 * 该示例可能会由于深度或者彩色sensor不支持镜像而出现深度图和彩色图镜像状态不一致的情况，
 * 从而导致深度图和彩色图显示的图像是相反的，如遇到该情况，则通过设置镜像接口保持两个镜像状态一致即可
 * 另外可能存在某些设备获取到的分辨率不支持D2C功能，因此D2C功能以实际支持的D2C分辨率为准
 *
 * DaBai DCW支持的D2C的分辨率为640x360，而实际该示例获取到的分辨率可能为640x480，此时用户根据实际模组情况获取
 * 对应的640x360分辨率即可
 *
 * Femto Bolt不支持硬件D2C，只支持软件D2C
 */
 
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
			try
			{
				if(value)
				{
					pipeline.Pipeline.EnableFrameSync();
				}
				else
				{
					pipeline.Pipeline.DisableFrameSync();
				}
			}
			catch (System.Exception)
			{
				Debug.LogWarning("Device not support frame sync");
			}

		});
		
		hardAlignToggle.onValueChanged.AddListener((value) =>
		{
			try
			{
				pipeline.StopPipeline();
				if (value)
				{
					pipeline.Config.SetAlignMode(AlignMode.ALIGN_D2C_HW_MODE);
				}
				else
				{
					pipeline.Config.SetAlignMode(AlignMode.ALIGN_DISABLE);
				}
				pipeline.StartPipeline();
			}
			catch (System.Exception)
			{
				Debug.LogWarning("Device not support hardware d2c");
			}
		});

		softAlignToggle.onValueChanged.AddListener((value) =>
		{
			try
			{
				pipeline.StopPipeline();
				if (value)
				{
					pipeline.Config.SetAlignMode(AlignMode.ALIGN_D2C_SW_MODE);
				}
				else
				{
					pipeline.Config.SetAlignMode(AlignMode.ALIGN_DISABLE);
				}
				pipeline.StartPipeline();
			}
			catch (System.Exception)
			{
				Debug.LogWarning("Device not support software d2c");
			}
		});
    }
}