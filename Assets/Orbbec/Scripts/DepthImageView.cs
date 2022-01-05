using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

public class DepthImageView : MonoBehaviour
{
    public OrbbecPipelineManager orbbecManager;
    public Texture2D depthTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        depthTexture = new Texture2D(0, 0, TextureFormat.YUY2, false);
        // GetComponent<RawImage>().texture = depthTexture;
        GetComponent<Renderer>().material.mainTexture = depthTexture;
    }

    // Update is called once per frame
    void Update()
    {
        ImageData imageData = orbbecManager.depthData;
        if (imageData.format == Format.OB_FORMAT_Y16)
        {
            if(depthTexture.width != imageData.width || depthTexture.height != imageData.height)
            {
                depthTexture.Resize(imageData.width, imageData.height);
            }
            depthTexture.LoadRawTextureData(imageData.data);
            depthTexture.Apply();
        }
    }
}
