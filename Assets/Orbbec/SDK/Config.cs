using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public class Config : IDisposable
    {
        private NativeHandle _handle;

        public Config()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_config(ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
        }

        internal Config(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        internal NativeHandle GetNativeHandle()
        {
            return _handle;
        }

        /**
        * \if English
        * @brief Configure the stream to be enabled
        *
        * @param streamProfile Stream  configuration
        * \else
        * @brief 设置要打开的流配置
        *
        * @param streamProfile 流的配置
        * \endif
        */
        public void EnableStream(StreamProfile streamProfile)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_config_enable_stream(_handle.Ptr, streamProfile.GetNativeHandle().Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Configure all streams to be enabled
        * \else
        * @brief 设置打开所有的流
        * \endif
        */
        public void EnableAllStream()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_config_enable_all_stream(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Configure the stream to be disabled
        *
        * @param streamType Stream configuration
        * \else
        * @brief 设置要关闭的流配置
        *
        * @param streamType 流的配置
        * \endif
        */
        public void DisableStream(StreamType streamType)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_config_disable_stream(_handle.Ptr, streamType, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Configure all streams to be disabled
        * \else
        * @brief 设置关闭所有的流
        * \endif
        */
        public void DisableAllStream()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_config_disable_all_stream(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Set the alignment mode
        *
        * @param mode Align State Mode
        * \else
        * @brief 设置对齐模式
        *
        * @param mode 对齐状态模式
        * \endif
        */
        public void SetAlignMode(AlignMode mode)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_config_set_align_mode(_handle.Ptr, mode, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Whether the depth needs to be scaled after setting D2C
        *
        * @param enable Whether scaling is required
        * \else
        * @brief 设置D2C后是否需要缩放深度
        *
        * @param enable 是否需要缩放
        * \endif
        */
        public void SetDepthScaleRequire(bool enable)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_config_set_depth_scale_require(_handle.Ptr, enable, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Set the D2C target resolution, which is applicable to cases where the color stream is not enabled using the OrbbecSDK and the depth needs to be
        * D2C Note: When you use OrbbecSDK to enable the color stream, you also use this interface to set the D2C target resolution. The configuration of the
        * enabled Color stream is preferred for D2C.
        * @param d2cTargetWidth  The D2C target has a wide resolution
        * @param d2cTargetHeight The D2C target has a high resolutio
        * \else
        * @brief 设置D2C目标分辨率，适用于未使用OrbbecSDK开启Color流，且需要对深度进行D2C的情况
        * 注意:当使用OrbbecSDK开启Color流时，同时使用了此接口设置了D2C目标分辨率时。优先使用开启的Color流的配置进行D2C。
        *
        * @param d2cTargetWidth  D2C目标分辨率宽
        * @param d2cTargetHeight D2C目标分辨率高
        * \endif
        */
        public void SetD2CTargetResolution(UInt32 d2cTargetWidth, UInt32 d2cTargetHeight)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_config_set_d2c_target_resolution(_handle.Ptr, d2cTargetWidth, d2cTargetHeight, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_config(handle, ref error);
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