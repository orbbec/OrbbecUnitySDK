using System;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public delegate void DeviceChangedCallback(DeviceList removed, DeviceList added);
    public delegate void LogCallback(LogSeverity logSeverity, String message);

    public class Context : IDisposable
    {
        private NativeHandle _handle;
        private DeviceChangedCallback _deviceChangedCallback;
        private NativeDeviceChangedCallback _nativeDeviceChangedCallback;
        private static LogCallback _logCallback;
        private static NativeLogCallback _nativeLogCallback;

        private void OnDeviceChanged(IntPtr removedPtr, IntPtr addedPtr, IntPtr userData)
        {
            DeviceList removed = new DeviceList(removedPtr);
            DeviceList added = new DeviceList(addedPtr);
            if (_deviceChangedCallback != null)
            {
                _deviceChangedCallback(removed, added);
            }
            else
            {
                removed.Dispose();
                added.Dispose();
            }
        }

        private void OnLogCallback(LogSeverity logSeverity, String message, IntPtr userData)
        {
            if(_logCallback != null)
            {
                _logCallback(logSeverity, message);
            }
        }

        /**
        * \if English
        * @brief Context is a management class that describes the runtime of the SDK. It is responsible for the applying and releasing of resources for the SDK.
        * The context has the ability to manage multiple devices, is responsible for enumerating devices, monitoring device callbacks, and enabling functions such
        * as multi-device synchronization. \else
        * @brief context是描述SDK的runtime一个管理类，负责SDK的资源申请与释放
        * context具备多设备的管理能力，负责枚举设备，监听设备回调，启用多设备同步等功能
        * \endif
        *
        */
        public Context()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_context(ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
            _nativeDeviceChangedCallback = new NativeDeviceChangedCallback(OnDeviceChanged);
            _nativeLogCallback = new NativeLogCallback(OnLogCallback);
        }

        /**
        * \if English
        * @brief Context is a management class that describes the runtime of the SDK. It is responsible for the applying and releasing of resources for the SDK.
        * The context has the ability to manage multiple devices, is responsible for enumerating devices, monitoring device callbacks, and enabling functions such
        * as multi-device synchronization. \else
        * @brief context是描述SDK的runtime一个管理类，负责SDK的资源申请与释放
        * context具备多设备的管理能力，负责枚举设备，监听设备回调，启用多设备同步等功能
        * \endif
        *
        */
        public Context(String configPath)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_context_with_config(configPath, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            _handle = new NativeHandle(handle, Delete);
            _nativeDeviceChangedCallback = new NativeDeviceChangedCallback(OnDeviceChanged);
        }

        /**
        *\if English
        * @brief Query enumerated device list
        *
        * @return DeviceList returns a pointer to the device list class
        * \else
        * @brief 获取枚举到设备列表
        *
        * @return DeviceList返回设备列表类的指针
        * \endif
        */
        public DeviceList QueryDeviceList()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_query_device_list(_handle.Ptr, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new DeviceList(handle);
        }

        public void EnableNetDeviceEnumeration(bool enable)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_enable_net_device_enumeration(_handle.Ptr, enable, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * @brief 创建网络设备
        *
        * @param address 设备ip地址
        * @param port 设备端口
        * @return Device 返回设备对象
        */
        public Device CreateNetDevice(string address, UInt16 port)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_create_net_device(_handle.Ptr, address, port, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Device(handle);
        }

        /**
        * \if English
        * @brief Set device plug-in callback function
        *
        * @param callback function triggered when the device is plugged and unplugged
        * \else
        * @brief 设置设备插拔回调函数
        *
        * @param callback 设备插拔时触发的回调函数
        * \endif
        */
        public void SetDeviceChangedCallback(DeviceChangedCallback callback)
        {
            _deviceChangedCallback = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_set_device_changed_callback(_handle.Ptr, _nativeDeviceChangedCallback, IntPtr.Zero, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Activate the multi-device synchronization function to synchronize the clock of the created device(the device needs support this function)
        *
        * @param repeatInterval  synchronization time interval (unit: ms; if repeatInterval=0, it means that it will only be synchronized once and will not be
        * executed regularly) \else
        * @brief 启动多设备同步功能，同步已创建设备的时钟(需要使用的设备支持该功能)
        *
        * @param repeatInterval 定时同步时间间隔（单位ms；如果repeatInterval=0，表示只同步一次，不再定时执行）
        * \endif
        */
        public void EnableDeviceClockSync(UInt64 repeatInterval)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_enable_device_clock_sync(_handle.Ptr, repeatInterval, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Set the level of the global log will affect both the log level output to the terminal and output to the file
        *
        * @param severity log output level
        * \else
        * @brief 设置全局日志的等级，会同时作用于输出到终端和输出到文件的日志等级
        *
        * @param severity 日志输出等级
        * \endif
        */
        public static void SetLoggerSeverity(LogSeverity logSeverity)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_set_logger_severity(logSeverity, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Set log output to file
        *
        * @param severity log level output to file
        * @param directory The log file output path. If the path is empty, the existing settings will continue to be used (if the existing configuration is also
        * empty, the log will not be output to the file) \else
        * @brief 设置日志输出到文件
        *
        * @param severity 输出到文件的日志等级
        * @param directory 日志文件输出路径，如果路径为空，则继续使用已有设置(已有配置也为空则不输出日志到文件)
        * \endif
        */
        public static void SetLoggerToFile(LogSeverity logSeverity, String directory)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_set_logger_to_file(logSeverity, directory, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Set log output to terminal
        *
        * @param severity 	log level output to the terminal
        * \else
        * @brief 设置日志输出到终端
        *
        * @param severity 输出到终端的日志等级
        * \endif
        */
        public static void SetLoggerToConsole(LogSeverity logSeverity)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_set_logger_to_console(logSeverity, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public static void SetLoggerCallback(LogSeverity logSeverity, LogCallback callback)
        {
            _logCallback = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_set_logger_callback(logSeverity, _nativeLogCallback, IntPtr.Zero, ref error);
            if (error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_context(handle, ref error);
            if (error != IntPtr.Zero)
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