using System.Collections;
using Orbbec;
using UnityEngine;

namespace OrbbecUnity
{
    public class OrbbecContext : Singleton<OrbbecContext>
    {
        private bool hasInit;
        private Context context;

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