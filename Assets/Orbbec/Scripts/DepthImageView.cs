using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class DepthImageView : MonoBehaviour
{
    private OrbbecPipeline pipeline;
    private Texture2D depthTexture;
    private int depthWidth;
    private int depthHeight;
    private byte[] depthData;
    
    // Start is called before the first frame update
    void Start()
    {
        pipeline = OrbbecPipeline.Instance;
        pipeline.onPipelineInit.AddListener(()=>{
            pipeline.StartPipeline((frameset)=>{
                var depthFrame = frameset.GetDepthFrame();
                if(depthFrame != null)
                {
                    depthWidth = (int)depthFrame.GetWidth();
                    depthHeight = (int)depthFrame.GetHeight();
                    var dataSize = depthFrame.GetDataSize();
                    if(depthData == null || depthData.Length != dataSize)
                    {
                        depthData = new byte[dataSize];
                    }
                    depthFrame.CopyData(ref depthData);
                    depthFrame.Dispose();
                }
                frameset.Dispose();
            });
        });
        depthTexture = new Texture2D(2, 2, TextureFormat.RG16, false);
        // GetComponent<RawImage>().texture = depthTexture;
        GetComponent<Renderer>().material.mainTexture = depthTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if(depthWidth == 0 || depthHeight == 0 || depthData == null || depthData.Length == 0)
        {
            return;
        }
        if(depthTexture.width != depthWidth || depthTexture.height != depthHeight)
        {
            depthTexture.Resize(depthWidth, depthHeight);
        }
        depthTexture.LoadRawTextureData(depthData);
        depthTexture.Apply();
    }
}
