using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class ColorImageView : MonoBehaviour
{
    public OrbbecFrameSource frameSource;

    private Texture2D colorTexture;
    

    void Update()
    {
        var obColorFrame = frameSource.GetColorFrame();

        if(obColorFrame == null || obColorFrame.width == 0 || obColorFrame.height == 0 || obColorFrame.data == null || obColorFrame.data.Length == 0)
        {
            return;
        }
        if(obColorFrame.frameType != FrameType.OB_FRAME_COLOR || obColorFrame.format != Format.OB_FORMAT_RGB)
        {
            return;
        }
        if(colorTexture == null)
        {
            colorTexture = new Texture2D(obColorFrame.width, obColorFrame.height, TextureFormat.RGB24, false);
            GetComponent<Renderer>().material.mainTexture = colorTexture;
        }
        if(colorTexture.width != obColorFrame.width || colorTexture.height != obColorFrame.height)
        {
            colorTexture.Resize(obColorFrame.width, obColorFrame.height);
        }
        colorTexture.LoadRawTextureData(obColorFrame.data);
        colorTexture.Apply();
    }
}
