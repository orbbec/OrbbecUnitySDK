using System.Collections;
using Orbbec;
using UnityEngine;

namespace OrbbecUnity
{
    public class OrbbecContext : MonoBehaviour
    {
        private static OrbbecContext instance;
        private bool hasInit;
        private Context context;

        public static OrbbecContext Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<OrbbecContext>();
 
                    if (instance == null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<OrbbecContext>();
                        singletonObject.name = typeof(OrbbecContext).ToString();
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }

        public bool HasInit
        {
            get
            {
                return hasInit;
            }
        }

        public Context Context
        {
            get
            {
                return context;
            }
        }

        void Awake()
        {
            if (!hasInit)
            {
                InitSDK();
            }
        }

        void OnDestroy()
        {
            if(hasInit)
            {
                context.Dispose();
            }
            hasInit = false;
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
            hasInit = true;
        }
    }
}