using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class IRImageView : MonoBehaviour
{
    private OrbbecPipeline pipeline;
    private Texture2D irTexture;
    private int irWidth;
    private int irHeight;
    private byte[] irData;
    
    // Start is called before the first frame update
    void Start()
    {
        pipeline = OrbbecPipeline.Instance;
        pipeline.onPipelineInit.AddListener(()=>{
            pipeline.StartPipeline((frameset)=>{
                var irFrame = frameset.GetIRFrame();
                if(irFrame != null)
                {
                    irWidth = (int)irFrame.GetWidth();
                    irHeight = (int)irFrame.GetHeight();
                    var dataSize = irFrame.GetDataSize();
                    if(irData == null || irData.Length != dataSize)
                    {
                        irData = new byte[dataSize];
                    }
                    irFrame.CopyData(ref irData);
                    irFrame.Dispose();
                }
                frameset.Dispose();
            });
        });
        irTexture = new Texture2D(2, 2, TextureFormat.RG16, false);
        // GetComponent<RawImage>().texture = irTexture;
        GetComponent<Renderer>().material.mainTexture = irTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if(irWidth == 0 || irHeight == 0 || irData == null || irData.Length == 0)
        {
            return;
        }
        if(irTexture.width != irWidth || irTexture.height != irHeight)
        {
            irTexture.Resize(irWidth, irHeight);
        }
        irTexture.LoadRawTextureData(irData);
        irTexture.Apply();
    }
}