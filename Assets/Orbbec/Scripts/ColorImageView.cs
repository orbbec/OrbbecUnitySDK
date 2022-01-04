﻿using System.Collections;
using System.Collections.Generic;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

public class ColorImageView : MonoBehaviour
{
    public OrbbecManager orbbecManager;
    public Texture2D colorTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        colorTexture = new Texture2D(0, 0, TextureFormat.RGB24, false);
        GetComponent<RawImage>().texture = colorTexture;
    }

    // Update is called once per frame
    void Update()
    {
        ImageData imageData = orbbecManager.colorData;
        if (imageData.format == Format.OB_FORMAT_MJPG)
        {
            colorTexture.LoadImage(imageData.data);
            colorTexture.Apply();
        }
    }
}