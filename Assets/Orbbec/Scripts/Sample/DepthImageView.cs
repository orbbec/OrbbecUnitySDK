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
    private byte[] colorData;
    private float depthScale = 1000f;

    void Update()
    {
        var obDepthFrame = frameSource.GetDepthFrame();

        if (obDepthFrame == null || obDepthFrame.width == 0 || obDepthFrame.height == 0 || obDepthFrame.data == null || obDepthFrame.data.Length == 0)
        {
            return;
        }
        if (obDepthFrame.frameType != FrameType.OB_FRAME_DEPTH)
        {
            return;
        }
        if (depthTexture == null)
        {
            depthTexture = new Texture2D(obDepthFrame.width, obDepthFrame.height, TextureFormat.RGB24, false);
            GetComponent<Renderer>().material.mainTexture = depthTexture;
        }
        if (depthTexture.width != obDepthFrame.width || depthTexture.height != obDepthFrame.height)
        {
            depthTexture.Resize(obDepthFrame.width, obDepthFrame.height);
        }

        int colorDataLength = (obDepthFrame.data.Length / 2) * 3;
        if (colorData == null || colorData.Length != colorDataLength)
        {
            colorData = new byte[colorDataLength];
        }

        ImageUtils.ConvertDepthToColorData(obDepthFrame.data, depthScale, ref colorData);
        depthTexture.LoadRawTextureData(colorData);
        depthTexture.Apply();
    }

    
}
