using System;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public class DeviceInfo : IDisposable
    {
        private NativeHandle _handle;

        internal DeviceInfo(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief Get device name
        *
        * @return String returns the device name
        * \else
        * @brief 获取设备名称
        *
        * @return String 返回设备名称
        * \endif
        */
        public String Name()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_name(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the pid of the device
        *
        * @return int returns the pid of the device
        * \else
        * @brief 获取设备的pid
        *
        * @return int 返回设备的pid
        * \endif
        */
        public int Pid()
        {
            IntPtr error = IntPtr.Zero;
            int pid = obNative.ob_device_info_pid(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return pid;
        }

        /**
        * \if English
        * @brief Get the vid of the device
        *
        * @return int returns the vid of the device
        * \else
        * @brief 获取设备的vid
        *
        * @return int 返回设备的vid
        * \endif
        */
        public int Vid()
        {
            IntPtr error = IntPtr.Zero;
            int vid = obNative.ob_device_info_vid(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return vid;
        }

        /**
        * \if English
        * @brief Get system assigned uid for distinguishing between different devices
        *
        * @return String returns the uid of the device
        * \else
        * @brief 获取设备的uid，该uid标识设备接入os操作系统时，给当前设备分派的唯一id，用来区分不同的设备
        *
        * @return String 返回设备的uid
        * \endif
        */
        public String Uid()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_uid(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the serial number of the device
        *
        * @return String returns the serial number of the device
        * \else
        * @brief 获取设备的序列号
        *
        * @return String 返回设备的序列号
        * \endif
        */
        public String SerialNumber()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_serial_number(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the version number of the firmware
        *
        * @return String returns the version number of the firmware
        * \else
        * @brief 获取固件的版本号
        *
        * @return String 返回固件的版本号
        * \endif
        */
        public String FirmwareVersion()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_firmware_version(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get usb connection type (DEPRECATED)
        *
        * @return String returns usb connection type
        * \else
        * @brief 获取usb连接类型 (废弃接口)
        *
        * @return String 返回usb连接类型
        * \endif
        */
        public String UsbType()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_usb_type(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get device connection type
        *
        * @return String returns connection type
        * \else
        * @brief 获取设备连接类型
        *
        * @return String 返回连接类型
        * \endif
        */
        public String ConnectionType()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_connection_type(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        public String IPAddress()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_ip_address(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        public String ExtensionInfo()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_get_extension_info(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the version number of the hardware
        *
        * @return String returns the version number of the hardware
        * \else
        * @brief 获取硬件的版本号
        *
        * @return String 返回硬件的版本号
        * \endif
        */
        public String HardwareVersion()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_hardware_version(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the minimum version number of the SDK supported by the device
        *
        * @return String returns the minimum SDK version number supported by the device
        * \else
        * @brief 获取设备支持的SDK最小版本号
        *
        * @return String 返回设备支持的SDK最小版本号
        * \endif
        */
        public String SupportedMinSdkVersion()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_supported_min_sdk_version(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get chip type name
        *
        * @return String returns the chip type name
        * \else
        * @brief 获取芯片类型名称
        *
        * @return String 返回芯片类型名称
        * \endif
        */
        public String AsicName()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_info_asicName(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get device type
        *
        * @return DeviceType returns the device type
        * \else
        * @brief 获取设备类型
        *
        * @return DeviceType 返回设备类型
        * \endif
        */
        public DeviceType DeviceType()
        {
            IntPtr error = IntPtr.Zero;
            DeviceType deviceType = obNative.ob_device_info_device_type(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return deviceType;
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_device_info(handle, ref error);
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