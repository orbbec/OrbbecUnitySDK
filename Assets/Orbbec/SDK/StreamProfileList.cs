using System;

namespace Orbbec
{    
    public class StreamProfileList : IDisposable
    {
        private NativeHandle _handle;

        internal StreamProfileList(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief Match the corresponding streamprofile through the passed in parameters. If there are multiple matches, the first one in the list will be returned
        * by default.
        *
        * @param width Width. If no matching condition is required, it can be passed to 0
        * @param height Height. If no matching condition is required, it can be passed to 0
        * @param format Type. If no matching condition is required, it can be passed to OB_FORMAT_UNKNOWN
        * @param fps Frame rate. If no matching condition is required, it can be passed to 0
        * @return VideoStreamProfile Returns the matching resolution
        * \else
        * @brief 通过传入的参数进行匹配对应的StreamProfile，若有多个匹配项默认返回列表中的第一个
        *
        * @param width 宽度，如不要求加入匹配条件，可传0
        * @param height 高度，如不要求加入匹配条件，可传0
        * @param format 类型，如不要求加入匹配条件，可传OB_FORMAT_UNKNOWN
        * @param fps 帧率，如不要求加入匹配条件，可传0
        * @return VideoStreamProfile 返回匹配的分辨率
        * \endif
        */
        public VideoStreamProfile GetVideoStreamProfile(int width, int height, Format format, int fps)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_stream_profile_list_get_video_stream_profile(_handle.Ptr, width, height, format, fps, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new VideoStreamProfile(handle);
        }

        public AccelStreamProfile GetAccelStreamProfile(AccelFullScaleRange fullScaleRange, AccelSampleRate sampleRate)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_stream_profile_list_get_accel_stream_profile(_handle.Ptr, fullScaleRange, sampleRate, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new AccelStreamProfile(handle);
        }

        public GyroStreamProfile GetGyroStreamProfile(GyroFullScaleRange fullScaleRange, GyroSampleRate sampleRate)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_stream_profile_list_get_gyro_stream_profile(_handle.Ptr, fullScaleRange, sampleRate, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new GyroStreamProfile(handle);
        }

        /**
        * \if English
        * @brief Get StreamProfile by index number
        *
        * @param index Device index to be created，the range is [0, count-1],if the index exceeds the range, an exception will be thrown
        * @return StreamProfile returns StreamProfile object
        * \else
        * @brief 通过索引号获取StreamProfile
        *
        * @param index 要创建设备的索，范围 [0, count-1]，如果index超出范围将抛异常
        * @return StreamProfile 返回StreamProfile对象
        * \endif
        */
        public StreamProfile GetProfile(int index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_stream_profile_list_get_profile(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new StreamProfile(handle);
        }

        /**
        * \if English
        * @brief Get stream profile count
        *
        * @return UInt32 returns the number of StreamProfile
        * \else
        * @brief 获取StreamProfile数量
        *
        * @return UInt32 返回StreamProfile的数量
        * \endif
        */
        public UInt32 ProfileCount()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 count = obNative.ob_stream_profile_list_count(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return count;
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_stream_profile_list(handle, ref error);
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