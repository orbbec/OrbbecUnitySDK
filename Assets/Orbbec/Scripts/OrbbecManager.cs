using System.Collections;
using Orbbec;
using UnityEngine;

namespace OrbbecUnity
{
    [System.Serializable]
    public struct ImageMode
    {
        public int width;
        public int height;
        public int fps;
        public Orbbec.Format format;
    }

    public delegate void OrbbecInitHandle();

    public class OrbbecManager : MonoBehaviour
    {
        public int deviceIndex;
        private bool hasInit;
        private Context context;
        private DeviceList deviceList;
        private Device device;

        public Device GetCurrentDevice()
        {
            return device;
        }

        void Start()
        {
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
            context = new Context();
#if !UNITY_EDITOR && UNITY_ANDROID
            AndroidDeviceManager.Init();
#endif
            StartCoroutine(WaitForDevice());
        }

        private IEnumerator WaitForDevice()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                deviceList = context.QueryDeviceList();
                if (deviceList.DeviceCount() > deviceIndex)
                {
                    device = deviceList.GetDevice((uint)deviceIndex);
                    DeviceInfo deviceInfo = device.GetDeviceInfo();
                    Debug.Log(string.Format(
                        "Device found: {0} {1} {2:X} {3:X}", 
                        deviceInfo.Name(), 
                        deviceInfo.SerialNumber(),
                        deviceInfo.Vid(),
                        deviceInfo.Pid()));
                    break;
                }
                else
                {
                    deviceList.Dispose();
                }
            }
        }
    }
}