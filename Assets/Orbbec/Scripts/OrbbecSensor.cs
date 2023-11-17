using System.Collections;
using Orbbec;
using UnityEngine;
using UnityEngine.Events;

namespace OrbbecUnity
{
    [System.Serializable]
    public class SensorInitEvent : UnityEvent { }

    public class OrbbecSensor : MonoBehaviour
    {
        public OrbbecDevice orbbecDevice;
        public SensorType sensorType;
        public OrbbecProfile[] orbbecProfiles;
        public SensorInitEvent onSensorInit;

        private Sensor sensor;
        private VideoStreamProfile streamProfile;
        private FrameCallback frameCallback;

        public Sensor Sensor
        {
            get
            {
                return sensor;
            }
        }

        void Start()
        {
            orbbecDevice.onDeviceFound.AddListener(InitSensor);
        }

        void OnDestroy()
        {
            if(sensor != null)
            {
                sensor.Dispose();
            }
        }

        private VideoStreamProfile FindProfile(OrbbecProfile obProfile)
        {
            try
            {
                var profileList = sensor.GetStreamProfileList();
                VideoStreamProfile streamProfile = profileList.GetVideoStreamProfile(obProfile.width, obProfile.height, obProfile.format, obProfile.fps);
                if (streamProfile != null && obProfile.sensorType == sensor.GetSensorType())
                {
                    Debug.LogFormat("Profile found: {0}x{1}@{2} {3}",
                            streamProfile.GetWidth(),
                            streamProfile.GetHeight(),
                            streamProfile.GetFPS(),
                            streamProfile.GetFormat());
                    return streamProfile;
                }
                else
                {
                    Debug.LogWarning("Profile not found");
                }
            }
            catch (NativeException e)
            {
                Debug.Log(e.Message);
            }

            return null;
        }

        public void SetFrameCallback(FrameCallback callback)
        {
            frameCallback = callback;
        }

        public void StartStream()
        {
            sensor.Start(streamProfile, frameCallback);
        }
        
        public void StopStream()
        {
            sensor.Stop();
        }

        private void InitSensor(Device device)
        {
            sensor = device.GetSensor(sensorType);
            if(sensor == null)
            {
                Debug.LogError("Sensor not found: " + sensorType);
                return;
            }

            for (int i = 0; i < orbbecProfiles.Length - 1; i++)
            {
                streamProfile = FindProfile(orbbecProfiles[i]);
                if (streamProfile != null)
                {
                    break;
                }
            }
            onSensorInit?.Invoke();
        }
    }
}