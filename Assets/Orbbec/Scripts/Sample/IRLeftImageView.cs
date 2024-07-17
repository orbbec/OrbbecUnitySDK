using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class IRLeftImageView : MonoBehaviour
{
    public OrbbecFrameSource frameSource;
    
    private Texture2D irTexture;
    private byte[] colorData;

    void Update()
    {
        var obIrFrame = frameSource.GetIrLeftFrame();

        if(obIrFrame == null || obIrFrame.width == 0 || obIrFrame.height == 0 || obIrFrame.data == null || obIrFrame.data.Length == 0)
        {
            return;
        }
        if(obIrFrame.frameType != FrameType.OB_FRAME_IR_LEFT)
        {
            return;
        }
        if(irTexture == null)
        {
            irTexture = new Texture2D(obIrFrame.width, obIrFrame.height, TextureFormat.RGB24, false);
            GetComponent<Renderer>().material.mainTexture = irTexture;
        }
        if(irTexture.width != obIrFrame.width || irTexture.height != obIrFrame.height)
        {
            irTexture.Resize(obIrFrame.width, obIrFrame.height);
        }
        int colorDataLength = obIrFrame.format == Format.OB_FORMAT_Y8 ? obIrFrame.data.Length * 3 : (obIrFrame.data.Length / 2) * 3;
        if (colorData == null || colorData.Length != colorDataLength)
        {
            colorData = new byte[colorDataLength];
        }

        if(obIrFrame.format == Format.OB_FORMAT_Y8)
        {
            ImageUtils.Convert8BitIrToByteArray(obIrFrame.data, ref colorData);
        }
        else
        {
            ImageUtils.Convert16BitIrToColorData(obIrFrame.data, ref colorData);
        }

        irTexture.LoadRawTextureData(colorData);
        irTexture.Apply();
    }
}