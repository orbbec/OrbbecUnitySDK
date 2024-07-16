using System;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public delegate void FrameDestroyCallback(byte[] buffer);

    public class Frame : IDisposable
    {
        protected NativeHandle _handle;

        internal Frame(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        internal NativeHandle GetNativeHandle()
        {
            return _handle;
        }

        public T As<T>() where T : Frame {
            switch (GetFrameType())
            {
                case FrameType.OB_FRAME_VIDEO:
                    _handle.Retain();
                    return new VideoFrame(_handle.Ptr) as T;
                case FrameType.OB_FRAME_IR:
                case FrameType.OB_FRAME_IR_LEFT:
                case FrameType.OB_FRAME_IR_RIGHT:
                    _handle.Retain();
                    return new IRFrame(_handle.Ptr) as T;
                case FrameType.OB_FRAME_COLOR:
                    _handle.Retain();
                    return new ColorFrame(_handle.Ptr) as T;
                case FrameType.OB_FRAME_DEPTH:
                    _handle.Retain();
                    return new DepthFrame(_handle.Ptr) as T;
                case FrameType.OB_FRAME_ACCEL:
                    _handle.Retain();
                    return new AccelFrame(_handle.Ptr) as T;
                case FrameType.OB_FRAME_SET:
                    _handle.Retain();
                    return new Frameset(_handle.Ptr) as T;
                case FrameType.OB_FRAME_POINTS:
                    _handle.Retain();
                    return new PointsFrame(_handle.Ptr) as T;
                case FrameType.OB_FRAME_GYRO:
                    _handle.Retain();
                    return new GyroFrame(_handle.Ptr) as T;
            }
            return null;
        }

        public Frame Copy()
        {
            _handle.Retain();
            return new Frame(_handle.Ptr);
        }

        /**
        * \if English
        * @brief Get the sequence number of the frame
        *
        * @return UInt64 returns the sequence number of the frame
        * \else
        * @brief 获取帧的序号
        *
        * @return UInt64 返回帧的序号
        * \endif
        */
        public UInt64 GetIndex()
        {
            IntPtr error = IntPtr.Zero;
            UInt64 index = obNative.ob_frame_index(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return index;
        }

        /**
        * \if English
        * @brief Get the format of the frame
        *
        * @return Format returns the format of the frame
        * \else
        * @brief 获取帧的格式
        *
        * @return Format 返回帧的格式
        * \endif
        */
        public Format GetFormat()
        {
            IntPtr error = IntPtr.Zero;
            Format format = obNative.ob_frame_format(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return format;
        }

        /**
        * \if English
        * @brief Get the type of frame
        *
        * @return FrameType returns the type of frame
        * \else
        * @brief 获取帧的类型
        *
        * @return FrameType 返回帧的类型
        * \endif
        */
        public FrameType GetFrameType()
        {
            IntPtr error = IntPtr.Zero;
            FrameType frameType = obNative.ob_frame_get_type(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return frameType;
        }

        /**
        * \if English
        * @brief Get the hardware timestamp of the frame
        *
        * @return UInt64 returns the time stamp of the frame hardware
        * \else
        * @brief 获取帧的硬件时间戳
        *
        * @return UInt64 返回帧硬件的时间戳
        * \endif
        */
        public UInt64 GetTimeStamp()
        {
            IntPtr error = IntPtr.Zero;
            UInt64 timestamp = obNative.ob_frame_time_stamp(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return timestamp;
        }

        /**
        * \if English
        * @brief Get the hardware timestamp of the frame us
        *
        * @return uint64_t returns the time stamp of the frame hardware, unit us
        * \else
        * @brief 获取帧的硬件时间戳
        *
        * @return uint64_t 返回帧硬件的时间戳
        * \endif
        */
        public UInt64 GetTimeStampUs()
        {
            IntPtr error = IntPtr.Zero;
            UInt64 timestamp = obNative.ob_frame_time_stamp_us(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return timestamp;
        }

        /**
        * \if English
        * @brief Get frame system timestamp
        *
        * @return UInt64 returns the time stamp of the frame hardware
        * \else
        * @brief 获取帧的系统时间戳
        *
        * @return UInt64 返回帧的系统时间戳
        * \endif
        */
        public UInt64 GetSystemTimeStamp()
        {
            IntPtr error = IntPtr.Zero;
            UInt64 sysTimestamp = obNative.ob_frame_system_time_stamp(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return sysTimestamp;
        }

        /**
        * @brief 获取帧数据
        * @param data 获取到的帧数据
        */
        public void CopyData(ref Byte[] data)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr dataPtr = obNative.ob_frame_data(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            Marshal.Copy(dataPtr, data, 0, data.Length);
        }

        /**
        * @brief 获取帧数据
        * @return IntPtr 获取数据在非托管内存的原始指针
        */
        public IntPtr GetDataPtr()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr dataPtr = obNative.ob_frame_data(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return dataPtr; 
        }

        /**
        * \if English
        * @brief Get the frame data size
        *
        * @return UInt32 returns the frame data size
        * If it is point cloud data, it returns the number of bytes occupied by all point sets. If you need to find the number of points, you need to divide the
        * dataSize by the structure size of the corresponding point type. \else
        * @brief 获取帧数据大小
        *
        * @return UInt32 返回帧数据的大小
        * 如果是点云数据返回的是所有点集合占的字节数，若需要求出点的个数需要将dataSize除以对应的点类型的结构体大小
        * \endif
        */
        public UInt32 GetDataSize()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 dataSize = obNative.ob_frame_data_size(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return dataSize;
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_frame(handle, ref error);
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

    public class VideoFrame : Frame
    {
        internal VideoFrame(IntPtr handle) : base(handle)
        {
        }

        /**
        * \if English
        * @brief Get frame width
        *
        * @return UInt32 returns the width of the frame
        * \else
        * @brief 获取帧的宽
        *
        * @return UInt32 返回帧的宽
        * \endif
        */
        public UInt32 GetWidth()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_video_frame_width(_handle.Ptr, ref error);
        }

        /**
        * \if English
        * @brief Get frame height
        *
        * @return UInt32 returns the height of the frame
        * \else
        * @brief 获取帧的高
        *
        * @return UInt32 返回帧的高
        * \endif
        */
        public UInt32 GetHeight()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_video_frame_height(_handle.Ptr, ref error);
        }

        /**
        * \if English
        * @brief Get the metadata of the frame
        *
        * @return Byte[] returns the metadata of the frame
        * \else
        * @brief 获取帧的元数据
        *
        * @return Byte[] 返回帧的元数据
        * \endif
        */
        public Byte[] GetMetadata()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr data = obNative.ob_frame_metadata(_handle.Ptr, ref error);
            UInt32 dataSize = obNative.ob_frame_metadata_size(_handle.Ptr, ref error);
            Byte[] buffer = new Byte[dataSize];
            Marshal.Copy(data, buffer, 0, (int)dataSize);
            return buffer;
        }

        /**
        * \if English
        * @brief Get the metadata size of the frame
        *
        * @return UInt32 returns the metadata size of the frame
        * \else
        * @brief 获取帧的元数据大小
        *
        * @return UInt32 返回帧的元数据大小
        * \endif
        */
        public UInt32 GetMetadataSize()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_frame_metadata_size(_handle.Ptr, ref error);
        }

        /**
        * \if English
        * @brief Get the effective number of pixels (such as Y16 format frame, but only the lower 10 bits are valid bits, and the upper 6 bits are filled with 0)
        * @attention Only valid for Y8/Y10/Y11/Y12/Y14/Y16 format
        *
        * @return uint8_t returns the effective number of pixels in the pixel, or 0 if it is an unsupported format
        * \else
        * @brief 获取像素有效位数（如Y16格式帧，每个像素占16bit，但实际只有低10位是有效位，高6位填充0）
        * @attention 仅对Y8/Y10/Y11/Y12/Y14/Y16格式有效
        *
        * @return uint8_t 返回像素有效位数，如果是不支持的格式，返回0
        * \endif
        */
        byte PixelAvailableBitSize()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_video_frame_pixel_available_bit_size(_handle.Ptr, ref error);
        }
    }

    public class ColorFrame : VideoFrame
    {
        internal ColorFrame(IntPtr handle) : base(handle)
        {
        }
    }

    public class DepthFrame : VideoFrame
    {
        internal DepthFrame(IntPtr handle) : base(handle)
        {
        }

        /**
        * \if English
        * @brief Get the value scale of the depth frame, the unit is mm/step,
        *        such as valueScale=0.1, and a certain coordinate pixel value is pixelValue=10000,
        *        then the depth value value = pixelValue*valueScale = 10000*0.1=1000mm.
        *
        * @return float
        * \else
        * @brief 获取深度帧的值刻度，单位为 mm/step，
        *      如valueScale=0.1, 某坐标像素值为pixelValue=10000，
        *     则表示深度值value = pixelValue*valueScale = 10000*0.1=1000mm。
        *
        * @return float
        * \endif
        */
        public float GetValueScale()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_depth_frame_get_value_scale(_handle.Ptr, ref error);
        }
    }

    public class IRFrame : VideoFrame
    {
        internal IRFrame(IntPtr handle) : base(handle)
        {
        }
    }

    public class PointsFrame : Frame
    {
        internal PointsFrame(IntPtr handle) : base(handle)
        {
        }

        /**
        * \if English
        * @brief Get the point position value scale of the points frame. the point position value of points frame is multiplied by the scale to give a position
        * value in millimeter. such as scale=0.1, The x-coordinate value of a point is x = 10000, which means that the actual x-coordinate value = x*scale =
        * 10000*0.1 = 1000mm.
        *
        * @param[in] frame Frame object
        * @param[out] error Log error messages
        * @return float position value scale
        * \else
        * @brief 获取点云帧的点坐标值缩放系数，点坐标值乘以缩放系数后，可以得到单位为毫米的坐标值； 如scale=0.1, 某个点的x坐标值为x=10000，
        *     则表示实际x坐标value = x*scale = 10000*0.1=1000mm。
        *
        * @return float 缩放系数
        * \endif
        */
        public float GetPositionValueScale()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_points_frame_get_position_value_scale(_handle.Ptr, ref error);
        }
    }

    public class AccelFrame : Frame
    {
        internal AccelFrame(IntPtr handle) : base(handle)
        {
        }

        /**
        * @brief 获取帧的加速度值
        * @return AccelValue 返回加速度值
        */
        public AccelValue GetAccelValue()
        {
            IntPtr error = IntPtr.Zero;
            AccelValue accelValue;
            obNative.ob_accel_frame_value(out accelValue, _handle.Ptr, ref error);
            return accelValue;
        }

        /**
        * @brief 获取帧采样时的温度
        * @return float 返回温度值
        */
        public float GetTemperature()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_accel_frame_temperature(_handle.Ptr, ref error);
        }
    }

    public class GyroFrame : Frame
    {
        internal GyroFrame(IntPtr handle) : base(handle)
        {
        }

        /**
        * @brief 获取陀螺仪帧数据
        * @return GyroValue 返回陀螺仪的值
        */
        public GyroValue GetGyroValue()
        {
            IntPtr error = IntPtr.Zero;
            GyroValue gyroValue;
            obNative.ob_gyro_frame_value(out gyroValue, _handle.Ptr, ref error);
            return gyroValue;
        }

        /**
        * @brief 获取帧采样时的温度
        * @return float 返回温度值
        */
        public float GetTemperature()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_gyro_frame_temperature(_handle.Ptr, ref error);
        }
    }

    public class Frameset : Frame
    {
        internal Frameset(IntPtr handle) : base(handle)
        {
        }

        /**
        * \if English
        * @brief Get frame count
        *
        * @return UInt32 returns the number of frames
        * \else
        * @brief 帧集合中包含的帧数量
        *
        * @return UInt32 返回帧的数量
        * \endif
        */
        public UInt32 GetFrameCount()
        {
            IntPtr error = IntPtr.Zero;
            return obNative.ob_frameset_frame_count(_handle.Ptr, ref error);
        }

        /**
        * \if English
        * @brief Get depth frame
        *
        * @return DepthFrame returns the depth frame
        * \else
        * @brief 获取深度帧
        *
        * @return DepthFrame 返回深度帧
        * \endif
        */
        public DepthFrame GetDepthFrame()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_frameset_depth_frame(_handle.Ptr, ref error);
            if(handle == IntPtr.Zero)
            {
                return null;
            }
            return new DepthFrame(handle);
        }

        /**
        * \if English
        * @brief Get color frame
        *
        * @return ColorFrame returns the color frame
        * \else
        * @brief 获取彩色帧
        *
        * @return ColorFrame 返回彩色帧
        * \endif
        */
        public ColorFrame GetColorFrame()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_frameset_color_frame(_handle.Ptr, ref error);
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            return new ColorFrame(handle);
        }

        /**
        * \if English
        * @brief Get infrared frame
        *
        * @return IRFrame returns infrared frame
        * \else
        * @brief 获取红外帧
        *
        * @return IRFrame 返回红外帧
        * \endif
        */
        public IRFrame GetIRFrame()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_frameset_ir_frame(_handle.Ptr, ref error);
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            return new IRFrame(handle);
        }

        /**
        * \if English
        * @brief Get point cloud frame
        *
        * @return  PointsFrame returns the point cloud data frame
        * \else
        * @brief 获取点云帧
        *
        * @return  PointsFrame 返回点云帧
        * \endif
        */
        public PointsFrame GetPointsFrame()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_frameset_points_frame(_handle.Ptr, ref error);
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            return new PointsFrame(handle);
        }

        /**
        * \if English
        * @brief Get frame by sensor type
        *
        * @param frameType  Type of sensor
        * @return std::shared_ptr<Frame> returns the corresponding type of frame
        * \else
        * @brief 通过传感器类型获取帧
        *
        * @param frameType 传感器的类型
        * @return std::shared_ptr<Frame> 返回相应类型的帧
        * \endif
        */
        public Frame GetFrame(FrameType frameType)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_frameset_get_frame(_handle.Ptr, frameType, ref error);
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            return new Frame(handle);
        }
    }
}