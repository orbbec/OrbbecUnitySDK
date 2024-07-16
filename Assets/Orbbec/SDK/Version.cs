using System;

namespace Orbbec
{
    public class Version
    {
        /**
        * \if English
        * @brief Get the SDK version number
        *
        * @return int returns the SDK version number
        * \else
        * @brief 获取SDK版本号
        *
        * @return int 返回SDK版本号
        * \endif
        */
        public static int GetVersion()
        {
            return obNative.ob_get_version();
        }

        /**
        * \if English
        * @brief Get the SDK minor version number
        *
        * @return int returns the SDK minor version number
        * \else
        * @brief 获取SDK副版本号
        *
        * @return int 返回SDK副版本号
        * \endif
        */
        public static int GetMajorVersion()
        {
            return obNative.ob_get_major_version();
        }

        /**
        * \if English
        * @brief Get the SDK minor version number
        *
        * @return int returns the SDK minor version number
        * \else
        * @brief 获取SDK副版本号
        *
        * @return int 返回SDK副版本号
        * \endif
        */
        public static int GetMinorVersion()
        {
            return obNative.ob_get_minor_version();
        }

        /**
        * \if English
        * @brief Get the SDK revision number
        *
        * @return int returns the SDK revision number
        * \else
        * @brief 获取SDK修订版本号
        *
        * @return int 返回SDK修订版本号
        * \endif
        */
        public static int GetPatchVersion()
        {
            return obNative.ob_get_patch_version();
        }
    }
}