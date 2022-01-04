using UnityEngine;
using Orbbec;
using System;

public class ColorFrameRender : MonoBehaviour
{
    public OrbbecManager orbbecManager;
    public Texture2D colorTexture;

    private byte[] colorData;

    void Start()
    {
        colorTexture = new Texture2D(0, 0, TextureFormat.RGB24, false);
    }

    private void OnColorFrame(Frame frame)
    {
        ColorFrame colorFrame = frame as ColorFrame;
        Debug.Log(string.Format("{0}: {1}x{2} {4}", 
            colorFrame.GetFrameType(), 
            colorFrame.GetWidth(),
            colorFrame.GetHeight(),
            colorFrame.GetFormat()));
        if (colorFrame.GetFormat() == Format.OB_FORMAT_MJPG)
        {
            int dataSize = (int)colorFrame.GetDataSize();
            if(colorData == null || colorData.Length != dataSize)
            {
                colorData = new byte[dataSize];
            }
            colorFrame.CopyData(ref colorData);
            colorTexture.LoadImage(colorData);
            colorTexture.Apply();
        }
    }
}