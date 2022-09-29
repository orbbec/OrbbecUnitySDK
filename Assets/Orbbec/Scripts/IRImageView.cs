using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class IRImageView : MonoBehaviour
{
    public FramesetProcessor framesetProcessor;

    private Texture2D irTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        irTexture = new Texture2D(2, 2, TextureFormat.RG16, false);
        GetComponent<Renderer>().material.mainTexture = irTexture;
    }

    // Update is called once per frame
    void Update()
    {
        OrbbecFrame obIrFrame = framesetProcessor.GetIrFrame();

        if(obIrFrame == null || obIrFrame.width == 0 || obIrFrame.height == 0 || obIrFrame.data == null || obIrFrame.data.Length == 0)
        {
            return;
        }
        if(irTexture.width != obIrFrame.width || irTexture.height != obIrFrame.height)
        {
            irTexture.Resize(obIrFrame.width, obIrFrame.height);
        }
        irTexture.LoadRawTextureData(obIrFrame.data);
        irTexture.Apply();
    }
}