using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrbbecUnity;
using Orbbec;
using System;

public class RecordPlayback : MonoBehaviour
{
    public OrbbecPipeline pipeline;
    public OrbbecRecord record;
    public OrbbecPlayback playback;

    public ColorImageView colorImageView;
    public DepthImageView depthImageView;
    public OrbbecPipelineFrameSource pipelineFrameSource;
    public OrbbecPlaybackFrameSource playbackFrameSource;

    private bool isRecording;
    private bool isPlaybacking;

    // Start is called before the first frame update
    void Start()
    {
        pipeline.onPipelineInit.AddListener(OnSDKInit);
    }

    private void OnSDKInit()
    {
        Config config = pipeline.Config;
		config.SetAlignMode(AlignMode.ALIGN_D2C_SW_MODE);
        try
        {
            pipeline.Pipeline.EnableFrameSync();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Device not support frame sync");
        }
        pipeline.StartPipeline();
    }

    public void StartRecord()
    {
        if(isRecording) return;
        record.StartRecord(Application.persistentDataPath + "/OrbbecRecord.bag");
        isRecording = true;
    }

    public void StopRecord()
    {
        if(!isRecording) return;
        record.StopRecord();
        isRecording = false;
    }

    public void StartPlayback()
    {
        if(isPlaybacking) return;
        playback.StartPlayback(Application.persistentDataPath + "/OrbbecRecord.bag");
        isPlaybacking = true;

        colorImageView.frameSource = playbackFrameSource;
        depthImageView.frameSource = playbackFrameSource;
    }

    public void StopPlayback()
    {
        if(!isPlaybacking) return;
        playback.StopPlayback();
        isPlaybacking = false;

        colorImageView.frameSource = pipelineFrameSource;
        depthImageView.frameSource = pipelineFrameSource;
    }
}
