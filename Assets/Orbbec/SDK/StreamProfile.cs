using System;

namespace Orbbec
{    
    public class StreamProfile : IDisposable
    {
        protected NativeHandle _handle;

        internal StreamProfile(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        internal NativeHandle GetNativeHandle()
        {
            return _handle;
        }

        public T As<T>() where T : StreamProfile {
            switch (GetStreamType())
            {
                case StreamType.OB_STREAM_VIDEO:
                case StreamType.OB_STREAM_IR:
                case StreamType.OB_STREAM_COLOR:
                case StreamType.OB_STREAM_DEPTH:
                    _handle.Retain();
                    return new VideoStreamProfile(_handle.Ptr) as T;
                case StreamType.OB_STREAM_ACCEL:
                    _handle.Retain();
                    return new AccelStreamProfile(_handle.Ptr) as T;
                case StreamType.OB_STREAM_GYRO:
                    _handle.Retain();
                    return new GyroStreamProfile(_handle.Ptr) as T;   
            }
            return null;
        }

        /**
        * \if English
        * @brief Get the format of the stream
        *
        * @return Format returns the format of the stream
        * \else
        * @brief 获取流的格式
        *
        * @return Format 返回流的格式
        * \endif
        */
        public Format GetFormat()
        {
            IntPtr error = IntPtr.Zero;
            Format format = obNative.ob_stream_profile_format(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return format;
        }

        /**
        * \if English
        * @brief Get the type of stream
        *
        * @return StreamType returns the type of the stream
        * \else
        * @brief 获取流的类型
        *
        * @return StreamType 返回流的类型
        * \endif
        */
        public StreamType GetStreamType()
        {
            IntPtr error = IntPtr.Zero;
            StreamType streamType = obNative.ob_stream_profile_type(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return streamType;
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_stream_profile(handle, ref error);
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

    public class VideoStreamProfile : StreamProfile
    {
        internal VideoStreamProfile(IntPtr handle) : base(handle)
        {   
        }

        /**
        * \if English
        * @brief Get stream frame rate
        *
        * @return UInt32 returns the frame rate of the stream
        * \else
        * @brief 获取流的帧率
        *
        * @return UInt32 返回流的帧率
        * \endif
        */
        public UInt32 GetFPS()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 fps = obNative.ob_video_stream_profile_fps(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return fps;
        }

        /**
        * \if English
        * @brief Get stream width
        *
        * @return UInt32 returns the width of the stream
        * \else
        * @brief 获取流的宽
        *
        * @return UInt32 返回流的宽
        * \endif
        */
        public UInt32 GetWidth()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 width = obNative.ob_video_stream_profile_width(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return width;
        }

        /**
        * \if English
        * @brief Get stream height
        *
        * @return UInt32 returns the high of the stream
        * \else
        * @brief 获取流的高
        *
        * @return UInt32 返回流的高
        * \endif
        */
        public UInt32 GetHeight()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 height = obNative.ob_video_stream_profile_height(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return height;
        } 
    }

    public class AccelStreamProfile : StreamProfile
    {
        internal AccelStreamProfile(IntPtr handle) : base(handle)
        {   
        }

        /**
        * \if English
        * @brief Get full scale range
        *
        * @return AccelFullScaleRange  returns the scale range value
        * \else
        * @brief 获取满量程范围
        *
        * @return AccelFullScaleRange  返回量程范围值
        * \endif
        */
        public AccelFullScaleRange GetFullScaleRange()
        {
            IntPtr error = IntPtr.Zero;
            AccelFullScaleRange accelFullScaleRange = obNative.ob_accel_stream_profile_full_scale_range(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return accelFullScaleRange;
        } 

        /**
        * \if English
        * @brief Get sampling frequency
        *
        * @return AccelSampleRate  returns the sampling frequency
        * \else
        * @brief 获取采样频率
        *
        * @return AccelSampleRate  返回采样频率
        * \endif
        */
        public AccelSampleRate GetSampleRate()
        {
            IntPtr error = IntPtr.Zero;
            AccelSampleRate accelSampleRate = obNative.ob_accel_stream_profile_sample_rate(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return accelSampleRate;
        } 
    }

    public class GyroStreamProfile : StreamProfile
    {
        internal GyroStreamProfile(IntPtr handle) : base(handle)
        {   
        }

        /**
        * \if English
        * @brief Get full scale range
        *
        * @return GyroFullScaleRange  returns the scale range value
        * \else
        * @brief 获取满量程范围
        *
        * @return GyroFullScaleRange  返回量程范围值
        * \endif
        */
        public GyroFullScaleRange GetFullScaleRange()
        {
            IntPtr error = IntPtr.Zero;
            GyroFullScaleRange gyroFullScaleRange = obNative.ob_gyro_stream_profile_full_scale_range(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return gyroFullScaleRange;
        } 

        /**
        * \if English
        * @brief Get sampling frequency
        *
        * @return GyroSampleRate  returns the sampling frequency
        * \else
        * @brief 获取采样频率
        *
        * @return GyroSampleRate  返回采样频率
        * \endif
        */
        public GyroSampleRate GetSampleRate()
        {
            IntPtr error = IntPtr.Zero;
            GyroSampleRate gyroSampleRate = obNative.ob_gyro_stream_profile_sample_rate(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return gyroSampleRate;
        } 
    }
}