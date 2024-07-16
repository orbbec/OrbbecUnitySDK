using System;

namespace Orbbec
{    
    public class DepthWorkModeList : IDisposable
    {
        private NativeHandle _handle;

        internal DepthWorkModeList(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief Get the count of OBDepthWorkMode
        *
        * @return Count of OBDepthWorkMode
        * \else
        * @brief 获取相机深度模式的数量
        *
        * @return 列表中的相机深度模式数量
        * \endif
        */
        public UInt32 Count()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 count = obNative.ob_depth_work_mode_list_count(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return count;
        }

        /**
        * \if English
        * @brief Get DepthWorkMode at index in DepthWorkModeList
        *
        * @param[in] index Target DepthWorkMode's index
        *
        * @return DepthWorkMode at index
        * \else
        * @brief 根据下标获取相机深度模式
        *
        * @param index 对应模式列表的下标
        * @return 相机深度模式
        * \endif
        */
        public DepthWorkMode GetDepthWorkMode(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            DepthWorkMode workMode;
            obNative.ob_depth_work_mode_list_get_item(out workMode, _handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return workMode;
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