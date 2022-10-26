using System;
using System.Collections;
using System.Collections.Generic;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class IMUReader : MonoBehaviour
{
    public OrbbecDevice orbbecDevice;
    public Text textDevice;
    public Text textAccel;
    public Text textGyro;

    private Sensor accelSensor;
    private Sensor gyroSensor;

    private OrbbecImuFrame obAccelFrame;
    private OrbbecImuFrame obGyroFrame;

    // Start is called before the first frame update
    void Start()
    {
        orbbecDevice.onDeviceFound.AddListener(OnDeviceFound);
    }

    private void OnDeviceFound(Device device)
    {
        try
        {
            accelSensor = device.GetSensor(SensorType.OB_SENSOR_ACCEL);
            var accelProfileList = accelSensor.GetStreamProfileList();
            var accelProfile = accelProfileList.GetProfile(0);
            accelSensor.Start(accelProfile, OnFrame);

            gyroSensor = device.GetSensor(SensorType.OB_SENSOR_GYRO);
            var gyroProfileList = gyroSensor.GetStreamProfileList();
            var gyroProfile = gyroProfileList.GetProfile(0);
            gyroSensor.Start(gyroProfile, OnFrame);

            accelProfileList.Dispose();
            accelProfile.Dispose();
            gyroProfileList.Dispose();
            gyroProfile.Dispose();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            textDevice.text = "Device has no imu sensor";
        }
    }

    private void OnFrame(Frame frame)
    {
        if(frame.GetFrameType() == FrameType.OB_FRAME_ACCEL)
        {
            var accelFrame = frame.As<AccelFrame>();
            if(accelFrame != null)
            {
                var accelValue = accelFrame.GetAccelValue();
                obAccelFrame = new OrbbecImuFrame();
                obAccelFrame.value = new Vector3(accelValue.x, accelValue.y, accelValue.z);
                obAccelFrame.format = Format.OB_FORMAT_ACCEL;
                obAccelFrame.frameType = FrameType.OB_FRAME_ACCEL;
                obAccelFrame.timestamp = accelFrame.GetTimeStamp();
                obAccelFrame.systemTimestamp = accelFrame.GetSystemTimeStamp();
                obAccelFrame.temperature = accelFrame.GetTemperature();
                // Debug.LogFormat("AccelFrame:({0},{1},{2})", accelValue.x, accelValue.y, accelValue.z);
                // accelFrame.Dispose();
            }
        }
        if(frame.GetFrameType() == FrameType.OB_FRAME_GYRO)
        {
            var gyroFrame = frame.As<GyroFrame>();
            if(gyroFrame != null)
            {
                var gyroValue = gyroFrame.GetGyroValue();
                obGyroFrame = new OrbbecImuFrame();
                obGyroFrame.value = new Vector3(gyroValue.x, gyroValue.y, gyroValue.z);
                obGyroFrame.format = Format.OB_FORMAT_GRAY;
                obGyroFrame.frameType = FrameType.OB_FRAME_GYRO;
                obGyroFrame.timestamp = gyroFrame.GetTimeStamp();
                obGyroFrame.systemTimestamp = gyroFrame.GetSystemTimeStamp();
                obGyroFrame.temperature = gyroFrame.GetTemperature();
                // Debug.LogFormat("GyroFrame:({0},{1},{2})", gyroValue.x, gyroValue.y, gyroValue.z);
                // gyroFrame.Dispose();
            }
        }
        frame.Dispose();
    }

    void Update()
    {
        if(obAccelFrame != null)
        {
            textAccel.text = string.Format("AccelTimestamp:{0}\nAccelTemperature:{1}\n\nAccel:({2},{3},{4})",
                                obAccelFrame.timestamp, obAccelFrame.temperature, 
                                obAccelFrame.value.x, obAccelFrame.value.y, obAccelFrame.value.z);
        }
        if(obGyroFrame != null)
        {
            textGyro.text = string.Format("GyroTimestamp:{0}\nGyroTemperature:{1}\n\nGyro:({2},{3},{4})",
                                obGyroFrame.timestamp, obGyroFrame.temperature, 
                                obGyroFrame.value.x, obGyroFrame.value.y, obGyroFrame.value.z);
        }
    }

    void OnDestroy()
    {
        if(accelSensor != null)
        {
            accelSensor.Stop();
            accelSensor.Dispose();
        }
        if(gyroSensor != null)
        {
            gyroSensor.Stop();
            gyroSensor.Dispose();
        }
    }
}
