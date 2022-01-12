
using UnityEngine;

public class AndroidDeviceManager
{
    private static AndroidJavaClass UsbPermissionUtil;
    public static void Init()
    {
        Debug.Log("init android device");
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        UsbPermissionUtil = new AndroidJavaClass("com.orbbec.obsensor.usbdevice.UsbPermissionUtil");
        UsbPermissionUtil.CallStatic("waitForUsbDevice", currentActivity);
        // UsbPermissionUtil.CallStatic("requestPermission", currentActivity);
        Debug.Log("android device has init");
    }
}