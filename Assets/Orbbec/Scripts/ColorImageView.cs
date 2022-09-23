using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class ColorImageView : MonoBehaviour
{
    private OrbbecPipeline pipeline;
    private Texture2D colorTexture;
    private int colorWidth;
    private int colorHeight;
    private byte[] colorData;
    
    // Start is called before the first frame update
    void Start()
    {
        pipeline = OrbbecPipeline.Instance;
        pipeline.onPipelineInit.AddListener(()=>{
            pipeline.StartPipeline((frameset)=>{
                var colorFrame = frameset.GetColorFrame();
                if(colorFrame != null)
                {
                    colorWidth = (int)colorFrame.GetWidth();
                    colorHeight = (int)colorFrame.GetHeight();
                    var dataSize = colorFrame.GetDataSize();
                    if(colorData == null || colorData.Length != dataSize)
                    {
                        colorData = new byte[dataSize];
                    }
                    colorFrame.CopyData(ref colorData);
                    colorFrame.Dispose();
                }
                frameset.Dispose();
            });
        });
        
        colorTexture = new Texture2D(2, 2, TextureFormat.RGB24, false);
        // GetComponent<RawImage>().texture = colorTexture;
        GetComponent<Renderer>().material.mainTexture = colorTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if(colorWidth == 0 || colorHeight == 0 || colorData == null || colorData.Length == 0)
        {
            return;
        }
        if(colorTexture.width != colorWidth || colorTexture.height != colorHeight)
        {
            colorTexture.Resize(colorWidth, colorHeight);
        }
        colorTexture.LoadRawTextureData(colorData);
        colorTexture.Apply();
    }
}
