using System.Collections;
using System.Collections.Generic;
using OrbbecUnity;
using UnityEngine;

public class AutoStartSensor : MonoBehaviour
{
    public OrbbecSensor sensor;
    
    // Start is called before the first frame update
    void Start()
    {
        sensor.onSensorInit.AddListener(()=>{
            sensor.StartStream();
        });
    }
}
