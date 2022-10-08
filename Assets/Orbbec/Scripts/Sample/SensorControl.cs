﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using Orbbec;
using UnityEngine;
using UnityEngine.UI;

namespace OrbbecUnity
{
public class SensorControl : MonoBehaviour {

	public Dropdown deviceSelector;
	public Dropdown propertySelector;
	public Button getButton;
	public Button setButton;
	public Text getText;
	public InputField setText;
	public Text logText;
	public ScrollRect scrollView;

	private Context context;
    private List<Device> devices;
	private List<PropertyId> propertyIds;

	private Device curDevice;
	private PropertyId curProperty;

	private StringBuilder stringBuilder;

	// Use this for initialization
	void Start () {
		deviceSelector.onValueChanged.AddListener(OnDeviceSelect);
		propertySelector.onValueChanged.AddListener(OnPropertySelect);

		getButton.onClick.AddListener(OnGetProperty);
		setButton.onClick.AddListener(OnSetProperty);

		devices = new List<Device>();
		propertyIds = new List<PropertyId>();

		context = OrbbecContext.Instance.Context;
		if(OrbbecContext.Instance.HasInit)
		{
			StartCoroutine(WaitForDevice());
		}
	}

	void OnDestroy()
	{
		foreach (var device in devices)
		{
			device.Dispose();
		}
	}

	private IEnumerator WaitForDevice()
	{
		while (true)
		{
			yield return new WaitForEndOfFrame();
			DeviceList deviceList = context.QueryDeviceList();
			if (deviceList.DeviceCount() > 0)
			{
				for(uint i = 0; i < deviceList.DeviceCount(); i++)
				{
					Device device = deviceList.GetDevice(i);
					devices.Add(device);
				}
				deviceList.Dispose();
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
		UpdateDeviceSelector();
		OnDeviceSelect(0);
	}

	private void UpdateProperties()
	{
		propertyIds.Clear();

		for(int id = 0; id <= 98; id++)
		{
			PropertyId propertyId = (PropertyId)id;
			if(curDevice.IsPropertySupported(propertyId, PermissionType.OB_PERMISSION_READ_WRITE))
			{
				propertyIds.Add(propertyId);
			}
		}
		for(int id = 2000; id <= 2028; id++)
		{
			PropertyId propertyId = (PropertyId)id;
			if(curDevice.IsPropertySupported(propertyId, PermissionType.OB_PERMISSION_READ_WRITE))
			{
				propertyIds.Add(propertyId);
			}
		}
		for(int id = 3000; id <= 3008; id++)
		{
			PropertyId propertyId = (PropertyId)id;
			if(curDevice.IsPropertySupported(propertyId, PermissionType.OB_PERMISSION_READ_WRITE))
			{
				propertyIds.Add(propertyId);
			}
		}
		
		if(propertyIds.Count > 0)
		{
			curProperty = propertyIds[0];
		}
	}

	private void OnDeviceSelect(int index)
	{
		curDevice = devices[index];
		PrintLog("DeviceSelect: " + curDevice.GetDeviceInfo().Name());
		UpdateProperties();
		UpdatePropertySelector();
	}

	private void OnPropertySelect(int index)
	{
		curProperty = propertyIds[index];

		string propertyStr = curProperty.ToString();

		setText.text = null;
		getText.text = null;

		if(propertyStr.EndsWith("BOOL"))
		{
			BoolPropertyRange range = curDevice.GetBoolPropertyRange(curProperty);
			((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min ? 1 : 0, range.max ? 1 : 0);
		}
		else if(propertyStr.EndsWith("INT"))
		{
			IntPropertyRange range = curDevice.GetIntPropertyRange(curProperty);
			((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
		}
		else if(propertyStr.EndsWith("FLOAT"))
		{
			FloatPropertyRange range = curDevice.GetFloatPropertyRange(curProperty);
			((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
		}
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
		if(propertySelector.value == 0)
		{
			OnPropertySelect(0);
		}
		else
		{
			propertySelector.value = 0;
		}
	}

	private void OnGetProperty()
	{
		if(curDevice == null)
		{
			return;
		}

		string propertyStr = curProperty.ToString();

		setText.text = null;
		getText.text = null;

		if(propertyStr.EndsWith("BOOL"))
		{
			bool value = curDevice.GetBoolProperty(curProperty);
			getText.text = (value ? 1 : 0).ToString();
			BoolPropertyRange range = curDevice.GetBoolPropertyRange(curProperty);
			((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min ? 1 : 0, range.max ? 1 : 0);
			PrintLog("GetProperty: " + propertyStr + " " + getText.text);
		}
		else if(propertyStr.EndsWith("INT"))
		{
			int value = curDevice.GetIntProperty(curProperty);
			getText.text = value.ToString();
			IntPropertyRange range = curDevice.GetIntPropertyRange(curProperty);
			((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
			PrintLog("GetProperty: " + propertyStr + " " + getText.text);
		}
		else if(propertyStr.EndsWith("FLOAT"))
		{
			float value = curDevice.GetFloatProperty(curProperty);
			getText.text = value.ToString();
			FloatPropertyRange range = curDevice.GetFloatPropertyRange(curProperty);
			((Text)setText.placeholder).text = string.Format("[{0}-{1}]", range.min, range.max);
			PrintLog("GetProperty: " + propertyStr + " " + getText.text);
		}
	}

	private void OnSetProperty()
	{
		if(curDevice == null)
		{
			return;
		}
		
		string propertyStr = curProperty.ToString();

		if(propertyStr.EndsWith("BOOL"))
		{
			int value;
			if(int.TryParse(setText.text, out value))
			{
				curDevice.SetBoolProperty(curProperty, value == 1 ? true : false);
				PrintLog("SetProperty: " + propertyStr + " " + setText.text);
			}
		}
		else if(propertyStr.EndsWith("INT"))
		{
			int value;
			if(int.TryParse(setText.text, out value))
			{
				curDevice.SetIntProperty(curProperty, value);
				PrintLog("SetProperty: " + propertyStr + " " + setText.text);
			}
		}
		else if(propertyStr.EndsWith("FLOAT"))
		{
			float value;
			if(float.TryParse(setText.text, out value))
			{
				curDevice.SetFloatProperty(curProperty, value);
				PrintLog("SetProperty: " + propertyStr + " " + setText.text);
			}
		}
	}

	private void PrintLog(string msg)
	{
		Debug.Log(msg);

		if(stringBuilder == null)
		{
			stringBuilder = new StringBuilder();
		}
		stringBuilder.AppendLine(msg);

		logText.text = stringBuilder.ToString();

		scrollView.verticalNormalizedPosition = 0;
	}
}
}