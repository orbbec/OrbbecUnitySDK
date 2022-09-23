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

        public void ConfigProfile(int width, int height, int fps, Format format)
        {
            this.orbbecProfile.width = width;
            this.orbbecProfile.height = height;
            this.orbbecProfile.fps = fps;
            this.orbbecProfile.format = format;
            ConfigProfile();
        }

        private void ConfigProfile()
        {
            streamProfile = null;
            var profileList = sensor.GetStreamProfileList();
            for (int i = 0; i < profileList.ProfileCount(); i++)
            {
                var profile = profileList.GetProfile(i);
                if(profile.GetWidth() == orbbecProfile.width && 
                    profile.GetHeight() == orbbecProfile.height && 
                    profile.GetFPS() == orbbecProfile.fps && 
                    profile.GetFormat() == orbbecProfile.format)
                {
                    Debug.Log(string.Format("Profile found: {0}x{1}@{2} {3}", 
                        orbbecProfile.width, orbbecProfile.height, orbbecProfile.fps, orbbecProfile.format));
                    streamProfile = profile;
                    break;
                }
            }
        }

        public void StartStream(FrameCallback callback)
        {
            if(streamProfile == null)
            {
                ConfigProfile();
            }
            if(streamProfile != null)
            {
                sensor.Start(streamProfile, callback);
            }
        }
        
        public void StopStream()
        {
            sensor.Stop();
        }

        private void InitSensor(Device device)
        {
            sensor = device.GetSensor(orbbecProfile.sensorType);
            Debug.Log("Sensor found: " + orbbecProfile.sensorType);            
        }
    }
}