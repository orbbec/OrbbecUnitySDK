using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class DepthImageView : MonoBehaviour
{
    public OrbbecFrameSource frameSource;

    private Texture2D depthTexture;
    

    void Update()
    {
        var obDepthFrame = frameSource.GetDepthFrame();

        if(obDepthFrame ==null || obDepthFrame.width == 0 || obDepthFrame.height == 0 || obDepthFrame.data == null || obDepthFrame.data.Length == 0)
        {
            return;
        }
        if(obDepthFrame.frameType != FrameType.OB_FRAME_DEPTH)
        {
            return;
        }
        if(depthTexture == null)
        {
            depthTexture = new Texture2D(obDepthFrame.width, obDepthFrame.height, TextureFormat.RG16, false);
            GetComponent<Renderer>().material.mainTexture = depthTexture;
        }
        if(depthTexture.width != obDepthFrame.width || depthTexture.height != obDepthFrame.height)
        {
            depthTexture.Resize(obDepthFrame.width, obDepthFrame.height);
        }
        depthTexture.LoadRawTextureData(obDepthFrame.data);
        depthTexture.Apply();
    }
}
