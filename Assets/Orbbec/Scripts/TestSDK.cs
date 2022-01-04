using UnityEngine;
using System;
using System.Threading;

public class TestSDK : MonoBehaviour
{
    void Start()
    {
        Thread thread = new Thread(new ThreadStart(SDKTest.Test));
        thread.Start();
    }

    void OnDestroy()
    {
        SDKTest.Quit();
    }
}