using System.Collections;
using System.Collections.Generic;
using System.Text;
using Orbbec;
using OrbbecUnity;
using UnityEngine;
using UnityEngine.UI;

public class DeviceInfoView : MonoBehaviour
{
    public OrbbecDevice orbbecDevice;
    private Text infoText;

    // Use this for initialization
    void Start()
    {
        infoText = GetComponent<Text>();
        orbbecDevice.onDeviceFound.AddListener(OnDeviceFound);
    }

    private void OnDeviceFound(Device device)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("Orbbec SDK version: {0}.{1}.{2}",
                                Version.GetMajorVersion(),
                                Version.GetMinorVersion(),
                                Version.GetPatchVersion()));

        DeviceInfo devInfo = device.GetDeviceInfo();
        sb.AppendLine(string.Format("Device name: {0}", devInfo.Name()));
        sb.AppendLine(string.Format("Device pid: {0}", devInfo.Pid()));
        sb.AppendLine(string.Format("Device vid: {0}", devInfo.Vid()));
        sb.AppendLine(string.Format("Device uid: {0}", devInfo.Uid()));
        sb.AppendLine(string.Format("Device sn: {0}", devInfo.SerialNumber()));
        sb.AppendLine(string.Format("Device firmware: {0}", devInfo.FirmwareVersion()));
        sb.AppendLine(string.Format("Device connection type: {0}", devInfo.ConnectionType()));

        SensorList sensorList = device.GetSensorList();
        for (uint i = 0; i < sensorList.SensorCount(); i++)
        {
            Sensor sensor = sensorList.GetSensor(i);
            sb.AppendLine(string.Format("Sensor type: {0}", sensor.GetSensorType()));
        }

        infoText.text = sb.ToString();
    }
}