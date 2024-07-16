using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public class Recorder : IDisposable
    {
        private NativeHandle _handle;

        /**
        * \if English
        * @brief Create a recorder for data recording
        * \else
        * @brief 创建录制器用于数据的录制
        * \endif
        */
        public Recorder()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_recorder(ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief Create a recorder for data recording
        * @param device Devices that need to record device information
        * \else
        * @brief 创建录制器用于数据的录制
        * @param device 需要录制设备信息的设备
        * \endif
        */
        public Recorder(Device device)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_recorder_with_device(device.GetNativeHandle().Ptr, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief start recording
        *
        * @param fileName Recorded file name
        * @param async Whether to record asynchronously
        * \else
        * @brief 开始录制
        *
        * @param fileName 录制的文件名称
        * @param async 是否异步录制
        * \endif
        */
        public void Start(String fileName, bool asycn = false)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_recorder_start(_handle.Ptr, fileName, asycn, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief stop recording
        * \else
        * @brief 停止录制
        * \endif
        */
        public void Stop()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_recorder_stop(_handle.Ptr, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Write frame data to the recorder
        *
        * @param frame Write frame data
        * \else
        * @brief 向录制器内写入帧数据
        *
        * @param frame 写入的帧数据
        * \endif
        */
        public void WriteFrame(Frame frame)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_recorder_write_frame(_handle.Ptr, frame.GetNativeHandle().Ptr, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_recorder(handle, ref error);
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