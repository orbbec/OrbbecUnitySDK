using System;
using System.Runtime.InteropServices;

namespace Orbbec
{    
    public class DeviceList : IDisposable
    {
        private NativeHandle _handle;

        internal DeviceList(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief Get device count
        *
        * @return UInt32 returns the number of devices
        * \else
        * @brief 获取设备数量
        *
        * @return UInt32 返回设备的数量
        * \endif
        */
        public UInt32 DeviceCount()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 count = obNative.ob_device_list_count(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return count;
        }

        /**
        * \if English
        * @brief Get the name of the specified device (DEPRECATED)
        *
        * @param index Device index
        * @return String returns the name of the device
        * \else
        * @brief 获取指定设备的名称 (废弃接口)
        *
        * @param index 设备索引
        * @return String 返回设备的名称
        * \endif
        */
        public String Name(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_list_get_device_name(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the pid of the specified device
        *
        * @param index Device index
        * @return int returns the pid of the device
        * \else
        * @brief 获取指定设备的pid
        *
        * @param index 设备索引
        * @return int 返回设备的pid
        * \endif
        */
        public int Pid(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            int pid = obNative.ob_device_list_get_device_pid(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return pid;
        }

        /**
        * \if English
        * @brief Get the vid of the specified device
        *
        * @param index Device index
        * @return int returns the vid of the device
        * \else
        * @brief 获取指定设备的vid
        *
        * @param index 设备索引
        * @return int 返回设备的vid
        * \endif
        */
        public int Vid(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            int vid = obNative.ob_device_list_get_device_vid(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return vid;
        }

        /**
        * \if English
        * @brief Get the uid of the specified device
        *
        * @param index Device index
        * @return String returns the uid of the device
        * \else
        * @brief 获取指定设备的uid
        *
        * @param index 设备索引
        * @return String 返回设备的uid
        * \endif
        */
        public String Uid(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_list_get_device_uid(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the serial number of the specified device
        *
        * @param index device index
        * @return String returns the serial number of the device
        * \else
        * @brief 获取指定设备的序列号
        *
        * @param index 设备索引
        * @return String 返回设备的序列号
        * \endif
        */
        public String SerialNumber(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_list_get_device_serial_number(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(handle);
        }

        public String ConnectionType(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_list_get_device_connection_type(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(handle);
        }

        public String IPAddress(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_list_get_device_ip_address(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(handle);
        }

        public String ExtensionInfo(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_list_get_extension_info(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return Marshal.PtrToStringAnsi(handle);
        }

        /**
        * \if English
        * @brief Get the specified device object from the device list
        * @attention If the device has been acquired and created elsewhere, repeated acquisition will throw an exception
        * @param index index of the device to create
        * @return Device returns the device object
        * \else
        * @brief 从设备列表中获取指定设备对象,
        * @attention 如果设备有在其他地方被获取创建，重复获取将会抛异常
        * @param index 要创建设备的索引
        * @return Device 返回设备对象
        * \endif
        */
        public Device GetDevice(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_list_get_device(_handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Device(handle);
        }

        /**
        * \if English
        * @brief Get the specified device object from the device list
        * @attention If the device has been acquired and created elsewhere, repeated acquisition will throw an exception
        * @param serialNumber The serial number of the device to be created
        * @return Device returns the device object
        * \else
        * @brief 从设备列表中获取指定设备对象
        * @attention 如果设备有在其他地方被获取创建，重复获取将会抛异常
        * @param serialNumber 要创建设备的序列号
        * @return Device 返回设备对象
        * \endif
        */
        public Device GetDeviceBySN(String serialNumber)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_list_get_device_by_serial_number(_handle.Ptr, serialNumber, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Device(handle);
        }

        /**
        * \if English
        * @brief Get the specified device object from the device list
        * @attention If the device has been acquired and created elsewhere, repeated acquisition will throw an exception
        * @param uid If the device has been acquired and created elsewhere, repeated acquisition will throw an exception
        * @return Device If the device has been acquired and created elsewhere, repeated acquisition will throw an exception
        * \else
        * @brief 从设备列表中获取指定设备对象
        * @attention 如果设备有在其他地方被获取创建，重复获取将会抛异常
        * @param uid 要创建设备的uid
        * @return Device 返回设备对象
        * \endif
        */
        public Device GetDeviceByUid(String uid)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_list_get_device_by_uid(_handle.Ptr, uid, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Device(handle);
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_device_list(handle, ref error);
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