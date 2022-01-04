using System;
using System.Threading;
using Orbbec;
using UnityEngine;

public class SDKTest
{
    private static bool quit = false;

    public static void Quit()
    {
        quit = true;
    }

    public static void Test()
    {
        // Display the number of command line arguments.
        Context ctx = new Context();
        DeviceList devList = ctx.QueryDeviceList();
        Debug.Log(devList.DeviceCount());
        Device dev = devList.GetDevice(0);
        DeviceInfo devInfo = dev.GetDeviceInfo();
        Debug.Log(devInfo.Name());
        Debug.Log(devInfo.SerialNumber());
        Debug.Log(devInfo.Pid());
        Debug.Log(devInfo.Vid());
        Debug.Log(devInfo.Uid());

        SensorList senList = dev.GetSensorList();
        Debug.Log(senList.SensorCount());

        Sensor colorSen = dev.GetSensor(SensorType.OB_SENSOR_COLOR);
        Debug.Log(colorSen.GetSensorType());

        Sensor depthSen = dev.GetSensor(SensorType.OB_SENSOR_DEPTH);
        Debug.Log(depthSen.GetSensorType());

        Sensor irSen = dev.GetSensor(SensorType.OB_SENSOR_IR);
        Debug.Log(irSen.GetSensorType());

        StreamProfile[] profiles = colorSen.GetStreamProfiles();
        Debug.Log(profiles.Length);

        foreach (var profile in profiles)
        {
            Debug.Log(String.Format("Color {0} x {1} @ {2} {3}", profile.GetWidth(), profile.GetHeight(), profile.GetFPS(), profile.GetFormat()));
        }

        byte[] colorData = null;
        
        colorSen.Start(profiles[0], (frame)=>{
            if(frame == null) 
            {
                Debug.Log("empty color frame");
                return;
            }
            var vf = (frame as VideoFrame);
            // Debug.Log(vf.GetDataSize());
            Debug.Log(String.Format("Color {0} x {1} {2}", vf.GetWidth(), vf.GetHeight(), vf.GetDataSize()));
            if(colorData == null)
            {
                colorData = new byte[vf.GetDataSize()];
            }
            vf.CopyData(ref colorData);
            vf.Dispose();
            Debug.Log(String.Format("Color {0}-{1}", colorData[0], colorData[colorData.Length - 1]));
        });

        profiles = depthSen.GetStreamProfiles();
        Debug.Log(profiles.Length);

        foreach (var profile in profiles)
        {
            Debug.Log(String.Format("Depth {0} x {1} @ {2} {3}", profile.GetWidth(), profile.GetHeight(), profile.GetFPS(), profile.GetFormat()));
        }

        byte[] depthData = null;

        depthSen.Start(profiles[0], (frame) => {
            if(frame == null) 
            {
                Debug.Log("empty depth frame");
                return;
            }
            var vf = (frame as VideoFrame);
            // Debug.Log(vf.GetDataSize());
            Debug.Log(String.Format("Depth {0} x {1} {2}", vf.GetWidth(), vf.GetHeight(), vf.GetDataSize()));
            if (depthData == null)
            {
                depthData = new byte[vf.GetDataSize()];
            }
            vf.CopyData(ref depthData);
            vf.Dispose();
            Debug.Log(String.Format("Depth {0}-{1}", depthData[0], depthData[depthData.Length - 1]));
        });

        profiles = irSen.GetStreamProfiles();
        Debug.Log(profiles.Length);

        foreach (var profile in profiles)
        {
            Debug.Log(String.Format("IR {0} x {1} @ {2} {3}", profile.GetWidth(), profile.GetHeight(), profile.GetFPS(), profile.GetFormat()));
        }

        dev.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_EMITTER_BOOL, false);
        dev.SetBoolProperty(PropertyId.OB_DEVICE_PROPERTY_EMITTER_BOOL, true);

        while (!quit)
        {
            Thread.Sleep(100);
        }
        
        colorSen.Dispose();
        depthSen.Dispose();
        irSen.Dispose();
        dev.Dispose();
        devList.Dispose();
        ctx.Dispose();
    }
}