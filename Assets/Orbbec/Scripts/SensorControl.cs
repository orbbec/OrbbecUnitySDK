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
	private SensorList sensorList;
    private List<Device> devices;
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
        // context.SetDeviceChangedCallback(OnDeviceChanged);
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidDeviceManager.Init();
#endif
		StartCoroutine(WaitForDevice());
    }

	private IEnumerator WaitForDevice()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            deviceList = context.QueryDeviceList();
            if (deviceList.DeviceCount() > 0)
            {
				yield return new WaitForEndOfFrame();
                for(uint i = 0; i < deviceList.DeviceCount(); i++)
				{
					Device device = deviceList.GetDevice(i);
					devices.Add(device);
				}
				hasInit = true;
				OnDeviceInit();
                break;
            }
            else
            {
                deviceList.Dispose();
            }
        }
    }

	private void OnDeviceInit()
	{
		curDevice = devices[0];
		curSensor = null;
		curProperty = (PropertyId)(-1);
		UpdateSensors();
		UpdateProperties();

		InitUI();
		UpdateDeviceSelector();
	}

	private void OnDeviceAdd()
	{
		UpdateDeviceSelector();
	}

	private void UpdateSensors()
	{
		sensors.Clear();
		sensorList = curDevice.GetSensorList();
		for(uint i = 0; i < sensorList.SensorCount(); i++)
		{
			Sensor sensor = sensorList.GetSensor(i);
			sensors.Add(sensor);
			curSensor = null;
		}
	}

	private void UpdateProperties()
	{
		propertyIds.Clear();
		if(curSensor == null)
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
		if(propertyIds.Count > 0)
		{
			curProperty = propertyIds[0];
		}
	}

	private void InitUI()
	{
		deviceSelector.onValueChanged.AddListener(OnDeviceSelect);
		sensorSelector.onValueChanged.AddListener(OnSensorSelect);
		propertySelector.onValueChanged.AddListener(OnPropertySelect);

		getButton.onClick.AddListener(OnGetProperty);
		setButton.onClick.AddListener(OnSetProperty);
	}

	private void OnDeviceSelect(int index)
	{
		curDevice = devices[index];
		UpdateSensors();
		UpdateProperties();
		UpdateSensorSelector();
		Debug.Log("DeviceSelect: " + curDevice.GetDeviceInfo().Name());
		PrintLog("DeviceSelect: " + curDevice.GetDeviceInfo().Name());
	}

	private void OnSensorSelect(int index)
	{
		if(index == 0)
		{
			curSensor = null;
			Debug.Log("SensorSelect: " + "OB_DEVICE");
			PrintLog("SensorSelect: " + "OB_DEVICE");
		}
		else
		{
			curSensor = sensors[index - 1];
			Debug.Log("SensorSelect: " + curSensor.GetSensorType().ToString());
			PrintLog("SensorSelect: " + curSensor.GetSensorType().ToString());
		}
		UpdateProperties();
		UpdatePropertySelector();
	}

	private void OnPropertySelect(int index)
	{
		curProperty = propertyIds[index];

		string propertyStr = curProperty.ToString();
		Debug.Log("PropertySelect: " + propertyStr);
		PrintLog("PropertySelect: " + propertyStr);
	}

	private void UpdateDeviceSelector()
	{
		List<string> deviceNames = new List<string>();
		foreach (var device in devices)
		{
			deviceNames.Add(device.GetDeviceInfo().Name());
		}
		deviceSelector.ClearOptions();
		deviceSelector.AddOptions(deviceNames);
		deviceSelector.value = 0;

		UpdateSensorSelector();
	}

	private void UpdateSensorSelector()
	{
		List<string> sensorNames = new List<string>(){"OB_DEVICE"};
		foreach (var sensor in sensors)
		{
			sensorNames.Add(sensor.GetSensorType().ToString());
		}
		sensorSelector.ClearOptions();
		sensorSelector.AddOptions(sensorNames);
		sensorSelector.value = 0;

		UpdatePropertySelector();
	}

	private void UpdatePropertySelector()
	{
		List<string> propertyNames = new List<string>();
		foreach (var property in propertyIds)
		{
			propertyNames.Add(property.ToString());
		}
		propertySelector.ClearOptions();
		propertySelector.AddOptions(propertyNames);
		propertySelector.value = 0;
	}

	private void OnGetProperty()
	{
		if((int)curProperty == -1)
		{
			return;
		}
		string propertyStr = curProperty.ToString();
		Debug.Log("GetProperty: " + propertyStr);
		PrintLog("GetProperty: " + propertyStr);

		setText.text = null;

		if(curSensor == null)
		{
			if(propertyStr.EndsWith("BOOL"))
			{
				try
				{
					bool value = curDevice.GetBoolProperty(curProperty);
					getText.text = (value ? 1 : 0).ToString();
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
					int value = curDevice.GetIntProperty(curProperty);
					getText.text = value.ToString();
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
					float value = curDevice.GetFloatProperty(curProperty);
					getText.text = value.ToString();
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
					bool value = curSensor.GetBoolProperty(curProperty);
					getText.text = (value ? 1 : 0).ToString();
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
					int value = curSensor.GetIntProperty(curProperty);
					getText.text = value.ToString();
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
					float value = curSensor.GetFloatProperty(curProperty);
					getText.text = value.ToString();
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

	private void OnSetProperty()
	{
		if((int)curProperty == -1)
		{
			return;
		}
		string propertyStr = curProperty.ToString();
		Debug.Log("SetProperty: " + propertyStr);
		PrintLog("SetProperty: " + propertyStr);

		if(curSensor == null)
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
