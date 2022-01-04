using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

public class IRImageView : MonoBehaviour
{
    public OrbbecManager orbbecManager;
    public Texture2D irTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        irTexture = new Texture2D(0, 0, TextureFormat.YUY2, false);
        // GetComponent<RawImage>().texture = irTexture;
        GetComponent<Renderer>().material.mainTexture = irTexture;
    }

    // Update is called once per frame
    void Update()
    {
        ImageData imageData = orbbecManager.irData;
        if (imageData.format == Format.OB_FORMAT_Y16)
        {
            if(irTexture.width != imageData.width || irTexture.height != imageData.height)
            {
                irTexture.Resize(imageData.width, imageData.height);
            }
            irTexture.LoadRawTextureData(imageData.data);
            irTexture.Apply();
        }
    }
}
