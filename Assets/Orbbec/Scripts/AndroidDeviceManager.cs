
using UnityEngine;

public class AndroidDeviceManager
{
    private static AndroidJavaObject deviceWatcher;
    public static void Init()
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        deviceWatcher = new AndroidJavaObject("com.orbbec.obsensor.DeviceWatcher", currentActivity);
    }
}