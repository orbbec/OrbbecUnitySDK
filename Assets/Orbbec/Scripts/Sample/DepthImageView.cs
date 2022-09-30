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
    
    // Start is called before the first frame update
    void Start()
    {
        depthTexture = new Texture2D(2, 2, TextureFormat.RG16, false);
        GetComponent<Renderer>().material.mainTexture = depthTexture;
    }

    // Update is called once per frame
    void Update()
    {
        OrbbecFrame obDepthFrame = frameSource.GetDepthFrame();

        if(obDepthFrame ==null || obDepthFrame.width == 0 || obDepthFrame.height == 0 || obDepthFrame.data == null || obDepthFrame.data.Length == 0)
        {
            return;
        }
        if(obDepthFrame.frameType != FrameType.OB_FRAME_DEPTH)
        {
            return;
        }
        if(depthTexture.width != obDepthFrame.width || depthTexture.height != obDepthFrame.height)
        {
            depthTexture.Resize(obDepthFrame.width, obDepthFrame.height);
        }
        depthTexture.LoadRawTextureData(obDepthFrame.data);
        depthTexture.Apply();
    }
}
