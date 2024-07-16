using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public delegate void FramesetCallback(Frameset frameset);

    public class Pipeline : IDisposable
    {
        private NativeHandle _handle;
        private Device _device;
        private static Dictionary<IntPtr, FramesetCallback> _framesetCallbacks = new Dictionary<IntPtr, FramesetCallback>();
        private NativeFramesetCallback _nativeCallback;
        
#if ORBBEC_UNITY
        [AOT.MonoPInvokeCallback(typeof(NativeFramesetCallback))]
#endif
        private static void OnFrameset(IntPtr framesetPtr, IntPtr userData)
        {
            Frameset frameset = new Frameset(framesetPtr);
            _framesetCallbacks.TryGetValue(userData, out FramesetCallback callback);
            if(callback != null)
            {
                callback(frameset);
            }
            else
            {
                frameset.Dispose();
            }
        }

        /**
        * \if English
        * @brief Pipeline is a high-level interface for applications, algorithms related RGBD data streams. Pipeline can provide alignment inside and synchronized
        * FrameSet. Pipeline() no parameter version, which opens the first device in the list of devices connected to the OS by default. If the application has
        * obtained the device through the DeviceList, opening the Pipeline() at this time will throw an exception that the device has been created. \else
        * @brief Pipeline 是SDK的高级接口，适用于应用，算法等重点关注RGBD数据流常见，Pipeline在SDK内部可以提供对齐，同步后的FrameSet桢集合
        * 直接方便客户使用。
        * Pipeline()无参数版本，默认打开连接到OS的设备列表中的第一个设备。若应用已经通过DeviceList获取设备，此时打开Pipeline()会抛出设备已经创建异常。
        * 需要开发者捕获异常处理。
        * \endif
        */
        public Pipeline()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_pipeline(ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
            _nativeCallback = new NativeFramesetCallback(OnFrameset);
        }

        /**
        * \if English
        * @brief
        * For multi-device operations. Multiple devices need to be obtained through DeviceList, and the device
        * and pipeline are bound through this interface. \else
        * @brief
        * 适用于多设备操作常见，此时需要通过DeviceList获取多个设备，通过该接口实现device和pipeline绑定。
        * \endif
        */
        public Pipeline(Device device)
        {
            _device = device;
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_pipeline_with_device(device.GetNativeHandle().Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
            _nativeCallback = new NativeFramesetCallback(OnFrameset);
        }

        /**
        * \if English
        * @brief Use the playback file to create a pipeline object
        *
        * @param fileName The playback file path used to create the pipeline
        * @return Pipeline returns the pipeline object
        * \else
        * @brief 使用回放文件来创建pipeline对象
        *
        * @param fileName 用于创建pipeline的回放文件路径
        * @return Pipeline 返回pipeline对象
        * \endif
        */
        public Pipeline(string fileName)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_pipeline_with_playback_file(fileName, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
            _nativeCallback = new NativeFramesetCallback(OnFrameset);
        }

        /**
        * \if English
        * @brief Start the pipeline with configuration parameters
        *
        * @param config Parameter configuration of pipeline
        * \else
        * @brief 启动pipeline并配置参数
        *
        * @param config pipeline的参数配置
        * \endif
        */
        public void Start(Config config)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_start_with_config(_handle.Ptr, config == null ? IntPtr.Zero : config.GetNativeHandle().Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Start the pipeline and set the frameset data callback
        *
        * @param config parameter configuration of pipeline
        * @param callback  Set the callback to be triggered when all frame data in the frame set arrives
        * \else
        * @brief 启动pipeline并设置帧集合数据回调
        *
        * @param config pipeline的参数配置
        * @param callback 设置帧集合中的所有帧数据都到达时触发回调
        * \endif
        */
        public void Start(Config config, FramesetCallback callback)
        {
            _framesetCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_start_with_callback(_handle.Ptr, config == null ? IntPtr.Zero : config.GetNativeHandle().Ptr, _nativeCallback, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Stop pipeline
        * \else
        * @brief 停止pipeline
        * \endif
        */
        public void Stop()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_stop(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get pipeline configuration parameters
        *
        * @return Config returns the configured parameters
        * \else
        * @brief 获取pipeline的配置参数
        *
        * @return Config 返回配置的参数
        * \endif
        */
        public Config GetConfig()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_pipeline_get_config(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Config(handle);
        }

        /**
        * \if English
        * @brief Waiting for frame set data
        *
        * @param timeout_ms  Waiting timeout (ms)
        * @return Frameset returns the waiting frame set data
        * \else
        * @brief 等待帧集合数据
        *
        * @param timeout_ms 等待超时时间(毫秒)
        * @return Frameset 返回等待的帧集合数据
        * \endif
        */
        public Frameset WaitForFrames(UInt32 timeoutMs)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_pipeline_wait_for_frameset(_handle.Ptr, timeoutMs, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            if(handle == IntPtr.Zero)
            {
                return null;
            }
            return new Frameset(handle);
        }

        /**
        * \if English
        * @brief Get device object
        *
        * @return Device returns the device object
        * \else
        * @brief 获取设备对象
        *
        * @return Device 返回设备对象
        * \endif
        */
        public Device GetDevice()
        {
            if(_device != null)
            {
                return _device;
            }
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_pipeline_get_device(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Device(handle);
        }

        /**
        * \if English
        * @brief Get playback object
        *
        * @return std::shared_ptr<Playback> returns the playback object
        * \else
        * @brief 获取回放对象
        *
        * @return std::shared_ptr<Playback> 返回回放对象
        * \endif
        */
        public Playback GetPlayback()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_pipeline_get_playback(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Playback(handle);
        }

        /**
        * \if English
        * @brief Get the stream profile of specified sensor
        *
        * @param sensorType Type of sensor

        * @return std::shared_ptr<StreamProfileList> returns the stream profile list
        * \else
        * @brief 获取指定传感器的流配置
        *
        * @param sensorType 传感器的类型
        * @return std::shared_ptr<StreamProfileList> 返回流配置列表
        * \endif
        */
        public StreamProfileList GetStreamProfileList(SensorType sensorType)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_pipeline_get_stream_profile_list(_handle.Ptr, sensorType, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            
            return new StreamProfileList(handle);
        }

        /**
        * \if English
        * @brief Turn on frame synchronization
        * \else
        * @brief 打开帧同步功能
        * \endif
        *
        */
        public void EnableFrameSync()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_enable_frame_sync(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Turn off frame synchronization
        * \else
        * @brief 关闭帧同步功能
        * \endif
        */
        public void DisableFrameSync()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_disable_frame_sync(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get camera parameters
        * @attention If D2C is enabled, it will return the camera parameters after D2C, if not, it will return to the default parameters
        *
        * @return  OBCameraParam returns camera parameters
        * \else
        * @brief 获取相机参数
        * @attention 如果开启了D2C将返回D2C后的相机参数，如果没有将返回默认参数
        *
        * @return  OBCameraParam返回相机参数
        * \endif
        */
        public CameraParam GetCameraParam()
        {
            IntPtr error = IntPtr.Zero;
            CameraParam cameraParam;
            obNative.ob_pipeline_get_camera_param(out cameraParam, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return cameraParam;
        }

        public CameraParam GetCameraParamWithProfile(UInt32 colorWidth, UInt32 colorHeight, UInt32 depthWidth, UInt32 depthHeight)
        {
            IntPtr error = IntPtr.Zero;
            CameraParam cameraParam;
            obNative.ob_pipeline_get_camera_param_with_profile(out cameraParam, _handle.Ptr, colorWidth, colorHeight, depthWidth, depthHeight, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return cameraParam;
        }

        /**
        * \if English
        * @brief Return a list of D2C-enabled depth sensor resolutions corresponding to the input color sensor resolution
        * @param colorProfile Input color sensor resolution
        * @param alignMode Input align mode
        *
        * @return StreamProfileList returns a list of depth sensor resolutions
        * \else
        * @brief 返回与输入的彩色传感器分辨率对应的支持D2C的深度传感器分辨率列表
        * @param colorProfile 输入的彩色传感器分辨率
        * @param alignMode 输入的对齐模式
        *
        * @return StreamProfileList 返回深度传感器分辨率列表
        * \endif
        */
        public StreamProfileList GetD2CDepthProfileList(StreamProfile colorProfile, AlignMode alignMode)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_get_d2c_depth_profile_list(_handle.Ptr, colorProfile.GetNativeHandle().Ptr, alignMode, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new StreamProfileList(ptr);
        }

        /**
        * \if English
        * @brief Get valid area between minimum distance and maximum distance after D2C
        *
        * @param minimumDistance minimum working distance
        * @param maximumDistance maximum working distance
        * @return Rect returns the area information valid after D2C at the working distance
        * \else
        * @brief 获取D2C后给定工作范围的有效区域
        * 如果需要获取指定距离D2C后的ROI区域，将minimum_distance与maximum_distance设置成一样或者将maximum_distance设置成0
        *
        * @param minimumDistance 最小工作距离
        * @param maximumDistance 最大工作距离
        * @return Rect 返回在工作距离下D2C后有效的区域信息
        * \endif
        */
        public Rect GetD2CValidArea(UInt32 minimumDistance, UInt32 maximumDistance = 0)
        {
            IntPtr error = IntPtr.Zero;
            Rect rect;
            obNative.ob_get_d2c_range_valid_area(out rect, _handle.Ptr, minimumDistance, maximumDistance, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return rect;
        }

        /**
        * \if English
        * @brief Dynamically switch the corresponding config configuration
        *
        * @param config Updated config configuration
        * \else
        * @brief 动态切换对应的config配置
        *
        * @param config 更新后的config配置
        * \endif
        */
        public void SwitchConfig(Config config)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_switch_config(_handle.Ptr, config.GetNativeHandle().Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief start recording
        *
        * @param filename Record file name
        * \else
        * @brief 开始录制
        *
        * @param filename 录制文件名
        * \endif
        */
        public void StartRecord(String fileName)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_start_record(_handle.Ptr, fileName, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Stop recording
        * \else
        * @brief 停止录制
        * \endif
        */
        public void StopRecord()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_pipeline_stop_record(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_pipeline(handle, ref error);
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