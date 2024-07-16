using System;

namespace Orbbec
{    
    public class CameraParamList : IDisposable
    {
        private NativeHandle _handle;

        internal CameraParamList(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief Number of camera parameter groups
        *
        * @return UInt32  returns the number of camera parameter groups
        * \else
        * @brief 相机参数组数
        *
        * @return UInt32  返回相机参数组数
        * \endif
        */
        public UInt32 Count()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 count = obNative.ob_camera_param_list_count(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return count;
        }

        /**
        * \if English
        * @brief Get camera parameters
        *
        * @param index parameter index
        * @return CameraParam returns the corresponding group parameters
        * \else
        * @brief 获取相机参数
        *
        * @param index 参数索引
        * @return CameraParam 返回对应组参数
        * \endif
        */
        public CameraParam GetCameraParam(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            CameraParam cameraParam;
            obNative.ob_camera_param_list_get_param(out cameraParam, _handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return cameraParam;
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_camera_param_list(handle, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}