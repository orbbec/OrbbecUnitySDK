using System.Collections;
using Orbbec;
using UnityEngine;

namespace OrbbecUnity
{
    public class OrbbecSensor : MonoBehaviour
    {
        public OrbbecDevice orbbecDevice;
        public OrbbecProfile orbbecProfile;

        private Sensor sensor;
        private StreamProfile streamProfile;

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

        private void ConfigProfile()
        {
            var profileList = sensor.GetStreamProfileList();
            streamProfile = profileList.GetVideoStreamProfile(orbbecProfile.width, orbbecProfile.height, orbbecProfile.format, orbbecProfile.fps);
            if(streamProfile != null)
            {
                Debug.Log(string.Format("Profile found: {0}x{1}@{2} {3}", 
                        streamProfile.GetWidth(), streamProfile.GetHeight(), streamProfile.GetFPS(), streamProfile.GetFormat()));
            }
            else
            {
                Debug.LogWarning("Profile not found");
            }
        }

        public void StartStream(FrameCallback callback)
        {
            sensor.Start(streamProfile, callback);
        }
        
        public void StopStream()
        {
            sensor.Stop();
        }

        private void InitSensor(Device device)
        {
            sensor = device.GetSensor(orbbecProfile.sensorType);
            Debug.Log("Sensor found: " + orbbecProfile.sensorType);
            ConfigProfile();     
        }
    }
}