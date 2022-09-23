using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbbec;
using OrbbecUnity;
using System;

public class FramesetProcessor : MonoBehaviour
{
    public OrbbecPipeline pipeline;

    private OrbbecFrame obColorFrame;
    private OrbbecFrame obDepthFrame;
    private OrbbecFrame obIrFrame;

    // Start is called before the first frame update
    void Start()
    {
        pipeline.onPipelineInit.AddListener(()=>{
            pipeline.StartPipeline(OnFrameset);
        });
        
        obColorFrame = new OrbbecFrame();
        obDepthFrame = new OrbbecFrame();
        obIrFrame = new OrbbecFrame();
    }

    private void OnFrameset(Frameset frameset)
    {
        var colorFrame = frameset.GetColorFrame();
        if(colorFrame != null)
        {
            obColorFrame.width = (int)colorFrame.GetWidth();
            obColorFrame.height = (int)colorFrame.GetHeight();
            var dataSize = colorFrame.GetDataSize();
            if(obColorFrame.data == null || obColorFrame.data.Length != dataSize)
            {
                obColorFrame.data = new byte[dataSize];
            }
            colorFrame.CopyData(ref obColorFrame.data);
            colorFrame.Dispose();
        }

        var depthFrame = frameset.GetDepthFrame();
        if(depthFrame != null)
        {
            obDepthFrame.width = (int)depthFrame.GetWidth();
            obDepthFrame.height = (int)depthFrame.GetHeight();
            var dataSize = depthFrame.GetDataSize();
            if(obDepthFrame.data == null || obDepthFrame.data.Length != dataSize)
            {
                obDepthFrame.data = new byte[dataSize];
            }
            depthFrame.CopyData(ref obDepthFrame.data);
            depthFrame.Dispose();
        }

        var irFrame = frameset.GetIRFrame();
        if(irFrame != null)
        {
            obIrFrame.width = (int)irFrame.GetWidth();
            obIrFrame.height = (int)irFrame.GetHeight();
            var dataSize = irFrame.GetDataSize();
            if(obIrFrame.data == null || obIrFrame.data.Length != dataSize)
            {
                obIrFrame.data = new byte[dataSize];
            }
            irFrame.CopyData(ref obIrFrame.data);
            irFrame.Dispose();
        }
        frameset.Dispose();
    }

    public OrbbecFrame GetColorFrame()
    {
        return obColorFrame;
    }

    public OrbbecFrame GetDepthFrame()
    {
        return obDepthFrame;
    }

    public OrbbecFrame GetIrFrame()
    {
        return obIrFrame;
    }
}
