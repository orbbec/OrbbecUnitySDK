using System.Collections;
using System.Collections.Generic;
using System.Text;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

public class SensorControl : MonoBehaviour {

	public Dropdown deviceSelector;
	public Dropdown sensorSelector;
	public Dropdown propertySelector;
	public Button getButton;
	public Button setButton;
	public Text getText;
	public InputField setText;
	public Text logText;

	private Context context;
    private DeviceList deviceList;
    private List<Device> devices;
    private Device currentDevice;
	private SensorList sensorList;
    private List<Sensor> sensors;
	private List<PropertyId> propertyIds;

	private Device curDevice;
	private Sensor curSensor;
	private PropertyId curProperty;

	private bool hasInit = false;

	private StringBuilder stringBuilder;

	// Use this for initialization
	void Start () {
		if (!hasInit)
        {
            InitSDK();
        }
	}

    private void InitSDK()
    {
		Debug.Log(string.Format("Orbbec SDK version: {0}.{1}.{2}",
                                    Version.GetMajorVersion(),
                                    Version.GetMinorVersion(),
                                    Version.GetPatchVersion()));
        devices = new List<Device>();
		sensors = new List<Sensor>();
		propertyIds = new List<PropertyId>();
        context = new Context();
        context.SetDeviceChangedCallback(OnDeviceChanged);
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidDeviceManager.Init();
#else
        deviceList = context.QueryDeviceList();
        if (deviceList.DeviceCount() > 0)
        {
            for(uint i = 0; i < deviceList.DeviceCount(); i++)
            {
                Device device = deviceList.GetDevice(i);
                devices.Add(device);
            }
			curDevice = devices[0];
            UpdateSensors(0);
			UpdateProperties(0);
            hasInit = true;
			InitUI();
            // if(initHandle != null)
            // {
            //     initHandle.Invoke();
            // }
        }
#endif
    }

	private void OnDeviceChanged(DeviceList removed, DeviceList added)
    {
        Debug.Log(string.Format("on device changed: removed count {0}, added count {1}", removed.DeviceCount(), added.DeviceCount()));
        for(uint i = 0; i < added.DeviceCount(); i++)
        {
            Device device = added.GetDevice(i);
            devices.Add(device);
            Debug.Log(string.Format("added device: {0} {1} {2} {3}", added.Name(i), added.Vid(i), added.Pid(i), added.Uid(i)));
        }
        for(uint i = 0; i < removed.DeviceCount(); i++)
        {
            Debug.Log(string.Format("removed device: {0} {1} {2} {3}", removed.Name(i), removed.Vid(i), removed.Pid(i), removed.Uid(i)));
            if(removed.Uid(i) == currentDevice.GetDeviceInfo().Uid())
            {
                ReleaseSensors();
                devices.Remove(currentDevice);
                hasInit = false;
            }
            else
            {
                for(int j = devices.Count - 1; j >= 0; j--)
                {
                    if(removed.Uid(i) == devices[j].GetDeviceInfo().Uid())
                    {
                        devices.RemoveAt(j);
                    }
                }
            }
        }

        if(!hasInit)
        {
			curDevice = devices[0];
            UpdateSensors(0);
			UpdateProperties(0);
            hasInit = true;
			InitUI();
            // if(initHandle != null)
            // {
            //     initHandle.Invoke();
            // }
        }
        
        removed.Dispose();
        added.Dispose();
    }

	private void InitUI()
	{
		List<string> deviceNames = new List<string>();
		foreach(var device in devices)
		{
			deviceNames.Add(device.GetDeviceInfo().Name());
		}
		deviceSelector.ClearOptions();
		deviceSelector.AddOptions(deviceNames);
		deviceSelector.onValueChanged.AddListener(OnDeviceSelect);

		List<string> sensorNames = new List<string>(){"OB_DEVICE"};
		foreach (var sensor in sensors)
		{
			sensorNames.Add(sensor.GetSensorType().ToString());
		}
		sensorSelector.ClearOptions();
		sensorSelector.AddOptions(sensorNames);
		sensorSelector.onValueChanged.AddListener(OnSensorSelect);

		List<string> propertyNames = new List<string>();
		foreach (var property in propertyIds)
		{
			propertyNames.Add(property.ToString());
		}
		propertySelector.ClearOptions();
		propertySelector.AddOptions(propertyNames);
		propertySelector.onValueChanged.AddListener(OnPropertySelect);

		getButton.onClick.AddListener(OnGetProperty);
		setButton.onClick.AddListener(OnSetProperty);
	}

	private void OnGetProperty()
	{
		string propertyStr = curProperty.ToString();
		Debug.Log("OnGetProperty: " + propertyStr);
		PrintLog("OnGetProperty: " + propertyStr);

		if(sensorSelector.value == 0)
		{
			if(propertyStr.EndsWith("BOOL"))
			{
				try
				{
					bool value = curDevice.GetBoolProperty(curProperty);
					getText.text = (value ? 1 : 0).ToString();
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("INT"))
			{
				try
				{
					int value = curDevice.GetIntProperty(curProperty);
					getText.text = value.ToString();
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("FLOAT"))
			{
				try
				{
					float value = curDevice.GetFloatProperty(curProperty);
					getText.text = value.ToString();
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
		}
		else
		{
			if(propertyStr.EndsWith("BOOL"))
			{
				try
				{
					bool value = curSensor.GetBoolProperty(curProperty);
					getText.text = value.ToString();
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("INT"))
			{
				try
				{
					int value = curSensor.GetIntProperty(curProperty);
					getText.text = value.ToString();
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("FLOAT"))
			{
				try
				{
					float value = curSensor.GetFloatProperty(curProperty);
					getText.text = value.ToString();
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
		}
	}

	private void OnSetProperty()
	{
		string propertyStr = curProperty.ToString();
		Debug.Log("OnSetProperty: " + propertyStr);
		PrintLog("OnSetProperty: " + propertyStr);

		if(sensorSelector.value == 0)
		{
			if(propertyStr.EndsWith("BOOL"))
			{
				try
				{
					bool value = int.Parse(setText.text) == 1 ? true : false;
					curDevice.SetBoolProperty(curProperty, value);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("INT"))
			{
				try
				{
					int value = int.Parse(setText.text);
					curDevice.SetIntProperty(curProperty, value);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("FLOAT"))
			{
				try
				{
					float value = float.Parse(setText.text);
					curDevice.SetFloatProperty(curProperty, value);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
		}
		else
		{
			if(propertyStr.EndsWith("BOOL"))
			{
				try
				{
					bool value = int.Parse(setText.text) == 1 ? true : false;
					curSensor.SetBoolProperty(curProperty, value);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("INT"))
			{
				try
				{
					int value = int.Parse(setText.text);
					curSensor.SetIntProperty(curProperty, value);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("FLOAT"))
			{
				try
				{
					float value = float.Parse(setText.text);
					curSensor.SetFloatProperty(curProperty, value);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
		}
	}

	private void OnDeviceSelect(int value)
	{
		curDevice = devices[value];
		ReleaseSensors();
		UpdateSensors(value);
		UpdateUI();
		Debug.Log("OnDeviceSelect: " + curDevice.GetDeviceInfo().Name());
		PrintLog("OnDeviceSelect: " + curDevice.GetDeviceInfo().Name());
	}

	private void OnSensorSelect(int value)
	{
		if(value > 0)
		{
			curSensor = sensors[value - 1];
			Debug.Log("OnSensorSelect: " + curSensor.GetSensorType().ToString());
			PrintLog("OnSensorSelect: " + curSensor.GetSensorType().ToString());
		}
		else
		{
			Debug.Log("OnSensorSelect: " + "OB_DEVICE");
			PrintLog("OnSensorSelect: " + "OB_DEVICE");
		}
		UpdateProperties(value);
		UpdateUI();
	}

	private void OnPropertySelect(int index)
	{
		curProperty = propertyIds[index];

		string propertyStr = curProperty.ToString();
		Debug.Log("OnPropertySelect: " + propertyStr);
		PrintLog("OnPropertySelect: " + propertyStr);

		if(sensorSelector.value == 0)
		{
			if(propertyStr.EndsWith("BOOL"))
			{
				try
				{
			 		BoolPropertyRange range = curDevice.GetBoolPropertyRange(curProperty);
					((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min ? 1 : 0, range.max ? 1 : 0);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("INT"))
			{
				try
				{
					IntPropertyRange range = curDevice.GetIntPropertyRange(curProperty);
					((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("FLOAT"))
			{
				try
				{
					FloatPropertyRange range = curDevice.GetFloatPropertyRange(curProperty);
					((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
		}
		else
		{
			if(propertyStr.EndsWith("BOOL"))
			{
				try
				{
					BoolPropertyRange range = curSensor.GetBoolPropertyRange(curProperty);
					((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min ? 1 : 0, range.max ? 1 : 0);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("INT"))
			{
				try
				{
					IntPropertyRange range = curSensor.GetIntPropertyRange(curProperty);
					((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
			else if(propertyStr.EndsWith("FLOAT"))
			{
				try
				{
					FloatPropertyRange range = curSensor.GetFloatPropertyRange(curProperty);
					((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
				}
				catch(NativeException e)
				{
					Debug.Log(e.Message);
					PrintLog(e.Message);
				}
			}
		}
	}

	private void UpdateUI()
	{
		List<string> sensorNames = new List<string>(){"OB_DEVICE"};
		foreach (var sensor in sensors)
		{
			sensorNames.Add(sensor.GetSensorType().ToString());
		}
		sensorSelector.ClearOptions();
		sensorSelector.AddOptions(sensorNames);

		List<string> propertyNames = new List<string>();
		foreach (var property in propertyIds)
		{
			propertyNames.Add(property.ToString());
		}
		propertySelector.ClearOptions();
		propertySelector.AddOptions(propertyNames);
	}

	private void UpdateSensors(int index)
	{
		sensorList = devices[index].GetSensorList();
		for(uint i = 0; i < sensorList.SensorCount(); i++)
		{
			Sensor sensor = sensorList.GetSensor(i);
			sensors.Add(sensor);
			curSensor = sensors[0];
		}
	}

	private void UpdateProperties(int index)
	{
		propertyIds.Clear();
		if(index == 0)
		{
			for(int id = 0; id <= 85; id++)
			{
				PropertyId propertyId = (PropertyId)id;
				if(curDevice.IsPropertySupported(propertyId))
				{
					propertyIds.Add(propertyId);
				}
			}
			for(int id = 3000; id <= 3006; id++)
			{
				PropertyId propertyId = (PropertyId)id;
				if(curDevice.IsPropertySupported(propertyId))
				{
					propertyIds.Add(propertyId);
				}
			}
		}
		else
		{
			for(int id = 2000; id <= 2024; id++)
			{
				PropertyId propertyId = (PropertyId)id;
				if(curSensor.IsPropertySupported(propertyId))
				{
					propertyIds.Add(propertyId);
				}
			}
		}
	}

	private void ReleaseDevices()
	{
		for(int i = 0; i < devices.Count; i++)
		{
			if(devices[i] != null)
			{
				devices[i].Dispose();
				devices[i] = null;
			}
		}
		devices.Clear();
		deviceList.Dispose();
	}

	private void ReleaseSensors()
	{
		for(int i = 0; i < sensors.Count; i++)
		{
			if(sensors[i] != null)
			{
				sensors[i].Dispose();
				sensors[i] = null;
			}
		}
		sensors.Clear();
		sensorList.Dispose();
	}

	private void PrintLog(string msg)
	{
		if(stringBuilder == null)
		{
			stringBuilder = new StringBuilder();
		}
		stringBuilder.AppendLine(msg);

		logText.text = stringBuilder.ToString();
	}
}
