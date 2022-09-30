using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OrbbecUnity;

public class PlayInfoView : MonoBehaviour
{
    public OrbbecDevice device;

    // Start is called before the first frame update
    void Start()
    {
        var text = GetComponent<Text>();
        device.onDeviceFound.AddListener((device)=>{
            var devInfo = device.GetDeviceInfo();
            text.text = string.Format("Device: {0}\nSN: {1}\nPID{2}\nVID{3}", 
                devInfo.Name(), devInfo.SerialNumber(), devInfo.Pid(), devInfo.Vid());
            devInfo.Dispose();
        });
    }
}
