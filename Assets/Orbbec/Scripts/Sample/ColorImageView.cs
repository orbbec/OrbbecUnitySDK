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
    
    // Start is called before the first frame update
    void Start()
    {        
        colorTexture = new Texture2D(2, 2, TextureFormat.RGB24, false);
        GetComponent<Renderer>().material.mainTexture = colorTexture;
    }

    // Update is called once per frame
    void Update()
    {
        OrbbecFrame obColorFrame = frameSource.GetColorFrame();

        if(obColorFrame == null || obColorFrame.width == 0 || obColorFrame.height == 0 || obColorFrame.data == null || obColorFrame.data.Length == 0)
        {
            return;
        }
        if(obColorFrame.frameType != FrameType.OB_FRAME_COLOR || obColorFrame.format != Format.OB_FORMAT_RGB888)
        {
            return;
        }
        if(colorTexture.width != obColorFrame.width || colorTexture.height != obColorFrame.height)
        {
            colorTexture.Resize(obColorFrame.width, obColorFrame.height);
        }
        colorTexture.LoadRawTextureData(obColorFrame.data);
        colorTexture.Apply();
    }
}
