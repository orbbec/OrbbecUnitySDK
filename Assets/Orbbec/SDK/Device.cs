using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public delegate void DeviceStateCallback(UInt64 state, String message);
    public delegate void SetDataCallback(DataTranState state, uint percent);
    public delegate void GetDataCallback(DataTranState state, DataChunk dataChunk);
    public delegate void DeviceUpgradeCallback(UpgradeState state, String message, byte percent);
    public delegate void SendFileCallback(FileTranState state, String message, byte percent);

    public class Device : IDisposable
    {
        private NativeHandle _handle;
        private static Dictionary<IntPtr, DeviceStateCallback> _deviceStateCallbacks = new Dictionary<IntPtr, DeviceStateCallback>();
        private NativeDeviceStateCallback _nativeDeviceStateCallback;
        private static Dictionary<IntPtr, SetDataCallback> _setDataCallbacks = new Dictionary<IntPtr, SetDataCallback>();
        private NativeSetDataCallback _nativeSetDataCallback;
        private static Dictionary<IntPtr, GetDataCallback> _getDataCallbacks = new Dictionary<IntPtr, GetDataCallback>();
        private NativeGetDataCallback _nativeGetDataCallback;
        private static Dictionary<IntPtr, DeviceUpgradeCallback> _deviceUpgradeCallbacks = new Dictionary<IntPtr, DeviceUpgradeCallback>();
        private NativeDeviceUpgradeCallback _nativeDeviceUpgradeCallback;
        private static Dictionary<IntPtr, SendFileCallback> _sendFileCallbacks = new Dictionary<IntPtr, SendFileCallback>();
        private NativeSendFileCallback _nativeSendFileCallback;

#if ORBBEC_UNITY
        [AOT.MonoPInvokeCallback(typeof(DeviceStateCallback))]
#endif
        private static void OnDeviceState(UInt64 state, String message, IntPtr userData)
        {
            _deviceStateCallbacks.TryGetValue(userData, out DeviceStateCallback callback);
            if(callback != null)
            {
                callback(state, message);
            }
        }

#if ORBBEC_UNITY
        [AOT.MonoPInvokeCallback(typeof(SetDataCallback))]
#endif
        private static void OnSetData(DataTranState state, uint percent, IntPtr userData)
        {
            _setDataCallbacks.TryGetValue(userData, out SetDataCallback callback);
            if(callback != null)
            {
                callback(state, percent);
            }
        }

#if ORBBEC_UNITY
        [AOT.MonoPInvokeCallback (typeof(GetDataCallback))]
#endif
        private static void OnGetData(DataTranState state, DataChunk dataChunk, IntPtr userData)
        {
            _getDataCallbacks.TryGetValue(userData, out GetDataCallback callback);
            if(callback != null)
            {
                callback(state, dataChunk);
            }
        }

#if ORBBEC_UNITY
        [AOT.MonoPInvokeCallback(typeof(DeviceUpgradeCallback))]
#endif
        private static void OnDeviceUpgrade(UpgradeState state, String message, byte percent, IntPtr userData)
        {
            _deviceUpgradeCallbacks.TryGetValue(userData, out DeviceUpgradeCallback callback);
            if(callback != null)
            {
                callback(state, message, percent);
            }
        }

#if ORBBEC_UNITY
        [AOT.MonoPInvokeCallback(typeof(SendFileCallback))]
#endif
        private static void OnSendFile(FileTranState state, String message, byte percent, IntPtr userData)
        {
            _sendFileCallbacks.TryGetValue(userData, out SendFileCallback callback);
            if(callback != null)
            {
                callback(state, message, percent);
            }
        }

        internal Device(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
            _nativeDeviceStateCallback = new NativeDeviceStateCallback(OnDeviceState);
            _nativeSetDataCallback = new NativeSetDataCallback(OnSetData);
            _nativeGetDataCallback = new NativeGetDataCallback(OnGetData);
            _nativeDeviceUpgradeCallback = new NativeDeviceUpgradeCallback(OnDeviceUpgrade);
            _nativeSendFileCallback = new NativeSendFileCallback(OnSendFile);
        }

        internal NativeHandle GetNativeHandle()
        {
            return _handle;
        }

        /**
        * \if English
        * @brief Get device information
        *
        * @return DeviceInfo returns device information
        * \else
        * @brief 获取设备信息
        *
        * @return DeviceInfo 返回设备的信息
        * \endif
        */
        public DeviceInfo GetDeviceInfo()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_get_device_info(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new DeviceInfo(handle);
        }

        /**
        * \if English
        * @brief Get device sensor list
        *
        * @return SensorList returns the sensor list
        * \else
        * @brief 获取设备传感器列表
        *
        * @return SensorList 返回传感器列表
        * \endif
        */
        public SensorList GetSensorList()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_get_sensor_list(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new SensorList(handle);
        }

        /**
        * \if English
        * @brief Get specific type of sensor
        * if device not open, SDK will automatically open the connected device and return to the instance
        *
        * @return Sensor eturns the sensor example, if the device does not have the device,returns nullptr
        * \else
        * @brief 获取指定类型传感器
        * 如果设备没有打开传感器，在SDK内部会自动打开设备并返回实例
        *
        * @return Sensor 返回传感器示例，如果设备没有该设备，返回nullptr
        * \endif
        */
        public Sensor GetSensor(SensorType sensorType)
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_get_sensor(_handle.Ptr, sensorType, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new Sensor(handle);
        }

        /**
        * \if English
        * @brief Set int type of device property
        *
        * @param propertyId Property id
        * @param property Property to be set
        * \else
        * @brief 设置int类型的设备属性
        *
        * @param propertyId 属性id
        * @param property 要设置的属性
        * \endif
        */
        public void SetIntProperty(PropertyId propertyId, Int32 property)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_set_int_property(_handle.Ptr, propertyId, property, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get int type of device property
        *
        * @param propertyId Property id
        * @return Int32 Property to get
        * \else
        * @brief 获取int类型的设备属性
        *
        * @param propertyId 属性id
        * @return Int32 获取的属性数据
        * \endif
        */
        public Int32 GetIntProperty(PropertyId propertyId)
        {
            IntPtr error = IntPtr.Zero;
            Int32 value = obNative.ob_device_get_int_property(_handle.Ptr, propertyId, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return value;
        }

        /**
        * \if English
        * @brief Set float type of device property
        *
        * @param propertyId Property id
        * @param property Property to be set
        * \else
        * @brief 设置float类型的设备属性
        *
        * @param propertyId 属性id
        * @param property 要设置的属性
        * \endif
        */
        public void SetFloatProperty(PropertyId propertyId, float property)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_set_float_property(_handle.Ptr, propertyId, property, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get float type of device property
        *
        * @param propertyId Property id
        * @return float Property to get
        * \else
        * @brief 获取float类型的设备属性
        *
        * @param propertyId 属性id
        * @return float 获取的属性数据
        * \endif
        */
        public float GetFloatProperty(PropertyId propertyId)
        {
            IntPtr error = IntPtr.Zero;
            float value = obNative.ob_device_get_float_property(_handle.Ptr, propertyId, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return value;
        }

        /**
        * \if English
        * @brief Set bool type of device property
        *
        * @param propertyId Property id
        * @param property Property to be set
        * \else
        * @brief 设置bool类型的设备属性
        *
        * @param propertyId 属性id
        * @param property 要设置的属性
        * \endif
        */
        public void SetBoolProperty(PropertyId propertyId, bool property)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_set_bool_property(_handle.Ptr, propertyId, property, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get bool type of device property
        *
        * @param propertyId Property id
        * @return bool Property to get
        * \else
        * @brief 获取bool类型的设备属性
        *
        * @param propertyId 属性id
        * @return bool 获取的属性数据
        * \endif
        */
        public bool GetBoolProperty(PropertyId propertyId)
        {
            IntPtr error = IntPtr.Zero;
            bool value = obNative.ob_device_get_bool_property(_handle.Ptr, propertyId, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return value;
        }

        /**
        * \if English
        * @brief Set structured data type of device property
        *
        * @param propertyId Property id
        * @param data Property data to be set
        * @param dataSize The size of the attribute to be set
        * \else
        * @brief 设置structured data类型的设备属性
        *
        * @param propertyId 属性id
        * @param data 要设置的属性数据
        * @param dataSize 要设置的属性大小
        * \endif
        */
        public void SetStructuredData(PropertyId propertyId, IntPtr data, UInt32 dataSize)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_set_structured_data(_handle.Ptr, propertyId, data, dataSize, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get structured data type of device property
        *
        * @param propertyId Property id
        * @param data Property data obtained
        * @param dataSize Get the size of the attribute
        * \else
        * @brief 获取structured data类型的设备属性
        *
        * @param propertyId 属性id
        * @param data 获取的属性数据
        * @param dataSize 获取的属性大小
        * \endif
        */
        public void GetStructuredData(PropertyId propertyId, IntPtr data, ref UInt32 dataSize)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_get_structured_data(_handle.Ptr, propertyId, data, ref dataSize, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Judge property permission support
        *
        * @param propertyId Property id
        * @param permission Types of read and write permissions that need to be interpreted
        * @return bool returns whether it is supported
        * \else
        * @brief 判断属性权限支持情况
        *
        * @param propertyId 属性id
        * @param permission 需要判读的读写权限类型
        * @return bool 返回是否支持
        * \endif
        */
        public bool IsPropertySupported(PropertyId propertyId, PermissionType permissionType)
        {
            IntPtr error = IntPtr.Zero;
            bool isSupported = obNative.ob_device_is_property_supported(_handle.Ptr, propertyId, permissionType, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return isSupported;
        }

        /**
        * \if English
        * @brief Get int type device property range (ncluding current valueand default value)
        *
        * @param propertyId Property id
        * @return IntPropertyRange Property range
        * \else
        * @brief 获取int类型的设备属性的范围(包括当前值和默认值)
        *
        * @param propertyId 属性id
        * @return IntPropertyRange 属性的范围
        * \endif
        */
        public IntPropertyRange GetIntPropertyRange (PropertyId propertyId)
        {
            IntPtr error = IntPtr.Zero;
            IntPropertyRange range;
            obNative.ob_device_get_int_property_range(out range, _handle.Ptr, propertyId, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return range;
        }

        /**
        * \if English
        * @brief Get float type device property range((including current valueand default value)
        *
        * @param propertyId Property id
        * @return FloatPropertyRange Property range
        * \else
        * @brief 获取float类型的设备属性的范围(包括当前值和默认值)
        *
        * @param propertyId 属性id
        * @return FloatPropertyRange 属性的范围
        * \endif
        */
        public FloatPropertyRange GetFloatPropertyRange (PropertyId propertyId)
        {
            IntPtr error = IntPtr.Zero;
            FloatPropertyRange range;
            obNative.ob_device_get_float_property_range(out range, _handle.Ptr, propertyId, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return range;
        }

        /**
        * \if English
        * @brief Get bool type device property range (including current value anddefault value)
        *
        * @param propertyId Property id
        * @return GetBoolPropertyRange Property range
        * \else
        * @brief 获取bool类型的设备属性的范围(包括当前值和默认值)
        *
        * @param propertyId 属性id
        * @return GetBoolPropertyRange 属性的范围
        * \endif
        */
        public BoolPropertyRange GetBoolPropertyRange(PropertyId propertyId)
        {
            IntPtr error = IntPtr.Zero;
            BoolPropertyRange range;
            obNative.ob_device_get_bool_property_range(out range, _handle.Ptr, propertyId, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return range;
        }

        /**
        * \if English
        * @brief AHB register write
        *
        * @param reg Register to be written
        * @param mask  The mask to be writen
        * @param value The value to be written
        * \else
        * @brief AHB写寄存器
        *
        * @param reg 要写入的寄存器
        * @param mask 要写入的掩码
        * @param value 要写入的值
        * \endif
        */
        public void WriteAHB(UInt32 reg, UInt32 mask, UInt32 value)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_write_ahb(_handle.Ptr, reg, mask, value, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief AHB AHB register read
        *
        * @param reg Register to be read
        * @param mask The mask to be read
        * @param value The value to be returned
        * \else
        * @brief AHB读寄存器
        *
        * @param reg 要读取的寄存器
        * @param mask 要读取的掩码
        * @param value 读取的值返回
        * \endif
        */
        public void ReadAHB(UInt32 reg, UInt32 mask, out UInt32 value)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_read_ahb(_handle.Ptr, reg, mask, out value, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief I2C register write
        *
        * @param reg I2C module ID to be written
        * @param reg Register to be written
        * @param mask The mask to be written
        * @param value he value to be written
        * \else
        * @brief I2C写寄存器
        *
        * @param reg 要写入的I2C模块ID
        * @param reg 要写入的寄存器
        * @param mask 要写入的掩码
        * @param value 要写入的值
        * \endif
        */
        public void WriteI2C(UInt32 moduleId, UInt32 reg, UInt32 mask, UInt32 value)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_write_i2c(_handle.Ptr, moduleId, reg, mask, value, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief I2C registers read
        *
        * @param reg I2C module ID to be read
        * @param reg Register to be read
        * @param mask The mask to be read
        * @param value The value to be returned
        * \else
        * @brief I2C读寄存器
        *
        * @param reg 要读取的I2C模块ID
        * @param reg 要读取的寄存器
        * @param mask 要读取的掩码
        * @param value 读取的值返回
        * \endif
        */
        public void ReadI2C(UInt32 moduleId, UInt32 reg, UInt32 mask, out UInt32 value)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_read_i2c(_handle.Ptr, moduleId, reg, mask, out value, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Set the properties of writing to Flash
        *
        * @param offset flash offset address
        * @param data Property data to be written
        * @param dataSize  The size of the property to be written
        * @param callback Write flash progress callback
        * @param async    Whether to execute asynchronously
        * \else
        * @brief 设置写入Flash的属性
        *
        * @param offset flash 偏移地址
        * @param data 要写入的属性数据
        * @param dataSize 要写入的属性大小
        * @param callback 写flash进度回调
        * @param async    是否异步执行
        * \endif
        */
        public void WriteFlash(UInt32 offset, byte[] data, UInt32 dataSize, SetDataCallback callback, bool async = false)
        {
            _setDataCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            GCHandle gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr intPtr = gcHandle.AddrOfPinnedObject();
            obNative.ob_device_write_flash(_handle.Ptr, offset, intPtr, dataSize, _nativeSetDataCallback, async, _handle.Ptr, ref error);
            gcHandle.Free();
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Read Flash property
        *
        * @param offset flash offset address
        * @param data Property data to be read
        * @param dataSize  The size of the property to get
        * @param callback Read data returned by flash and progress callback
        * @param async    Whether to execute asynchronously
        * \else
        * @brief 读取Flash的属性
        *
        * @param offset flash 偏移地址
        * @param data 读取的属性数据
        * @param dataSize 获取的属性大小
        * @param callback 读flash返回的数据及进度回调
        * @param async    是否异步执行
        * \endif
        */
        public void ReadFlash(UInt32 offset, UInt32 dataSize, GetDataCallback callback, bool async = false)
        {
            _getDataCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_read_flash(_handle.Ptr, offset, dataSize, _nativeGetDataCallback, async, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public void WriteCustomData(byte[] data, UInt32 dataSize)
        {
            IntPtr error = IntPtr.Zero;
            GCHandle gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr intPtr = gcHandle.AddrOfPinnedObject();
            obNative.ob_device_write_customer_data(_handle.Ptr, intPtr, dataSize, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public void ReadCustomData(IntPtr dataPtr, out UInt32 dataSize)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_read_customer_data(_handle.Ptr, dataPtr, out dataSize, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Set raw data type of device property [Asynchronous callback]
        *
        * @param propertyId Property id
        * @param data Property data to be set
        * @param dataSize The size of the property data to be set
        * @param callback rawdata set progress callback
        * @param async    Whether to execute asynchronously
        * \else
        * @brief 设置raw data类型的设备属性数据[异步回调]
        *
        * @param propertyId 属性id
        * @param data 要设置的属性数据
        * @param dataSize 要设置的属性数据大小
        * @param callback rawdata设置进度回调
        * @param async    是否异步执行
        * \endif
        */
        public void SetRawData(PropertyId propertyId, byte[] data, UInt32 dataSize, SetDataCallback callback, bool async = false)
        {
            _setDataCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            GCHandle gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr intPtr = gcHandle.AddrOfPinnedObject();
            obNative.ob_device_set_raw_data(_handle.Ptr, propertyId, intPtr, dataSize, _nativeSetDataCallback, async, _handle.Ptr, ref error);
            gcHandle.Free();
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get raw data type of device property [Asynchronous callback]
        *
        * @param propertyId Property id
        * @param data Property data obtained
        * @param dataSize Get the size of the property
        * @param callback  Get the returned data and progress callback
        * @param async    Whether to execute asynchronously
        * \else
        * @brief 获取raw data类型的设备属性数据[异步回调]
        *
        * @param propertyId 属性id
        * @param data 获取的属性数据
        * @param dataSize 获取的属性大小
        * @param callback 获取返回的数据及进度回调
        * @param async    是否异步执行
        * \endif
        */
        public void GetRawData(PropertyId propertyId, GetDataCallback callback, bool async = false)
        {
            _getDataCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_get_raw_data(_handle.Ptr, propertyId, _nativeGetDataCallback, async, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get the property protocol version
        *
        * @return ProtocolVersion
        * \else
        * @brief 获取设备的控制命令协议版本
        *
        * @return ProtocolVersion
        * \endif
        */
        public ProtocolVersion GetProtocolVersion()
        {
            IntPtr error = IntPtr.Zero;
            ProtocolVersion version = obNative.ob_device_get_protocol_version(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return version;
        }

        /**
        * \if English
        * @brief Get cmdVersion of property
        *
        * @param propertyId Property id
        * @return OBCmdVersion
        * \else
        * @brief 获取控制命令的版本号
        *
        * @param propertyId 属性id
        * @return OBCmdVersion
        * \endif
        */
        public CmdVersion GetCmdVersion(PropertyId propertyId)
        {
            IntPtr error = IntPtr.Zero;
            CmdVersion version = obNative.ob_device_get_cmd_version(_handle.Ptr, propertyId, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return version;
        }

        /**
        * \if English
        * @brief Get number of devices supported property
        *
        * @return UInt32 returns the number of supported attributes
        * \else
        * @brief 获取设备支持的属性的数量
        *
        * @return UInt32 返回支持的属性的数量
        * \endif
        */
        public UInt32 GetSupportedPropertyCount()
        {
            IntPtr error = IntPtr.Zero;
            UInt32 count = obNative.ob_device_get_supported_property_count(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return count;
        }

        /**
        * \if English
        * @brief Get device supported properties
        *
        * @param uint32_t Property index
        * @return PropertyItem returns the type of supported properties
        * \else
        * @brief 获取设备支持的属性
        *
        * @param uint32_t 属性的index
        * @return PropertyItem 返回支持的属性的类型
        * \endif
        */
        public PropertyItem GetSupportedProperty(UInt32 index)
        {
            IntPtr error = IntPtr.Zero;
            PropertyItem propertyItem;
            obNative.ob_device_get_supported_property(out propertyItem, _handle.Ptr, index, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return propertyItem;
        }

        /**
        * \if English
        * @brief Upgrade the device firmware
        *
        * @param filePath Firmware path
        * @param callback  Firmware upgrade progress and status callback
        * @param async    Whether to execute asynchronously
        * \else
        * @brief 升级设备固件
        *
        * @param filePath 固件的路径
        * @param callback 固件升级进度及状态回调
        * @param async    是否异步执行
        * \endif
        */
        public void DeviceUpgrade(String filePath, DeviceUpgradeCallback callback, bool async = true)
        {
            _deviceUpgradeCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_upgrade(_handle.Ptr, filePath, _nativeDeviceUpgradeCallback, async, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public void DeviceUpgradeFromData(byte[] fileData, DeviceUpgradeCallback callback, bool async = true)
        {
            _deviceUpgradeCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            GCHandle gcHandle = GCHandle.Alloc(fileData, GCHandleType.Pinned);
            IntPtr intPtr = gcHandle.AddrOfPinnedObject();
            obNative.ob_device_upgrade_from_data(_handle.Ptr, intPtr, (UInt32)fileData.Length, _nativeDeviceUpgradeCallback, async, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Send files to the specified path on the device side [Asynchronouscallback]
        *
        * @param filePath Original file path
        * @param dstPath  Accept the save path on the device side
        * @param callback File transfer callback
        * @param async    Whether to execute asynchronously
        * \else
        * @brief 发送文件到设备端指定路径[异步回调]
        *
        * @param filePath 原文件路径
        * @param dstPath 设备端接受保存路径
        * @param callback 文件传输回调
        * @param async    是否异步执行
        * \endif
        */
        public void SendFile(String filePath, String dstPath, SendFileCallback callback, bool async = true)
        {
            _sendFileCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_send_file_to_destination(_handle.Ptr, filePath, dstPath, _nativeSendFileCallback, async, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get the current state
        * @return UInt64 device state information
        * \else
        * @brief 获取当前设备状态
        * @return UInt64 设备状态信息
        * \endif
        */
        public UInt64 GetDeviceState()
        {
            IntPtr error = IntPtr.Zero;
            UInt64 state = obNative.ob_device_get_device_state(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return state;
        }

        /**
        * \if English
        * @brief Set the device state changed callbacks
        *
        * @param callback The callback function that is triggered when the device status changes (for example, the frame rate is automatically reduced or the
        * stream is closed due to high temperature, etc.) \else
        * @brief 设置设备状态改变回调函数
        *
        * @param callback 设备状态改变（如，由于温度过高自动降低帧率或关流等）时触发的回调函数
        * \endif
        */
        public void SetDeviceStateChangedCallback(DeviceStateCallback callback)
        {
            _deviceStateCallbacks[_handle.Ptr] = callback;
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_state_changed(_handle.Ptr, _nativeDeviceStateCallback, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Verify device authorization code
        * @param authCode Authorization code
        * @return bool whether the activation is successfu
        * \else
        * @brief 验证设备授权码
        * @param authCode 授权码
        * @return bool 激活是否成功
        * \endif
        */
        public bool ActivateAuthorization(String authCode)
        {
            IntPtr error = IntPtr.Zero;
            bool authorization = obNative.ob_device_activate_authorization(_handle.Ptr, authCode, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return authorization;
        }

        /**
        * \if English
        * @brief Write authorization code
        * @param authCode  Authorization code
        * \else
        * @brief 写入设备授权码
        * @param authCode 授权码
        * \endif
        */
        public void WriteAuthorizationCode(String authCode)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_write_authorization_code(_handle.Ptr, authCode, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Get the original parameter list of camera calibration saved in the device. The parameters in the list do not correspond to the current
        * open-current configuration. You need to select the parameters according to the actual situation, and may need to do scaling, mirroring and other
        * processing. Non-professional users are recommended to use the Pipeline::getCameraParam() interface.
        *
        * @return CameraParamList camera parameter list
        * \else
        * @brief 获取设备内保存的相机标定的原始参数列表，列表内参数不与当前开流配置相对应，
        * 需要自行根据实际情况选用参数并可能需要做缩放、镜像等处理。非专业用户建议使用Pipeline::getCameraParam()接口。
        *
        * @return CameraParamList 相机参数列表
        * \endif
        */
        public CameraParamList GetCalibrationCameraParamList()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr handle = obNative.ob_device_get_calibration_camera_param_list(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new CameraParamList(handle);
        }

        /**
        * \if English
        * @brief Get current depth work mode
        *
        * @return DepthWorkMode Current depth work mode
        * \else
        * @brief 查询当前的相机深度模式
        *
        * @return 返回当前的相机深度模式
        * \endif
        */
        public DepthWorkMode GetCurrentDepthWorkMode()
        {
            IntPtr error = IntPtr.Zero;
            DepthWorkMode workMode;
            obNative.ob_device_get_current_depth_work_mode(out workMode, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return workMode;
        }

        /**
        * \if English
        * @brief Switch depth work mode by DepthWorkMode. Prefer invoke switchDepthWorkMode(const char *modeName) to switch depth mode
        *        when known the complete name of depth work mode.
        * @param[in] workMode Depth work mode come from ob_depth_work_mode_list which return by ob_device_get_depth_work_mode_list
        * \else
        * @brief 切换相机深度模式（根据深度工作模式对象），如果知道设备支持的深度工作模式名称，那么推荐用switchDepthWorkMode(const char *modeName)
        *
        * @param workMode 要切换的相机深度模式
        *
        * \endif
        */
        public void SwitchDepthWorkMode(DepthWorkMode workMode)
        {
            IntPtr error = IntPtr.Zero;
            GCHandle gcHandle = GCHandle.Alloc(workMode, GCHandleType.Pinned);
            IntPtr ptr = gcHandle.AddrOfPinnedObject();
            obNative.ob_device_switch_depth_work_mode(_handle.Ptr, ptr, ref error);
            gcHandle.Free();
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Switch depth work mode by work mode name.
        *
        * @param[in] modeName Depth work mode name which equals to OBDepthWorkMode.name
        * \else
        * @brief 切换相机深度模式（根据深度工作模式名称）
        *
        * @param modeName 相机深度工作模式的名称，模式名称必须与OBDepthWorkMode.name一致
        *
        * \endif
        */
        public void SwitchDepthWorkMode(String modeName)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_switch_depth_work_mode_by_name(_handle.Ptr, modeName, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Request support depth work mode list
        * @return OBDepthWorkModeList list of ob_depth_work_mode
        * \else
        * @brief 查询相机深度模式列表
        *
        * @return 相机深度模式列表
        * \endif
        */
        public DepthWorkModeList GetDepthWorkModeList()
        {
            IntPtr error = IntPtr.Zero;
            IntPtr ptr = obNative.ob_device_get_depth_work_mode_list(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return new DepthWorkModeList(ptr);
        }

        /**
        * \if English
        * @brief Device restart
        * @attention The device will be disconnected and reconnected. After the device is disconnected, the access to the Device object interface may be abnormal.
        *   Please delete the object directly and obtain it again after the device is reconnected.
        * \else
        * @brief 设备重启
        * @attention 设备会掉线重连，设备掉线后对Device对象接口访问可能会发生异常，请直接删除该对象，
        *   待设备重连后可重新获取。
        * \endif
        */
        public void Reboot()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_reboot(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        /**
        * \if English
        * @brief Gets the current device synchronization configuration
        * @brief Device synchronization: including exposure synchronization function and multi-camera synchronization function of different sensors within a single
        * machine
        *
        * @return OBDeviceSyncConfig returns the device synchronization configuration
        * \else
        * @brief 获取当前设备同步配置
        * @brief 设备同步：包括单机内的不同 Sensor 的曝光同步功能 和 多机同步功能
        *
        * @return OBDeviceSyncConfig 返回设备同步配置
        * \endif
        *
        */
        public DeviceSyncConfig GetSyncConfig()
        {
            IntPtr error = IntPtr.Zero;
            DeviceSyncConfig config;
            obNative.ob_device_get_sync_config(_handle.Ptr, out config, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return config;
        }

        /**
        * \if English
        * @brief Set the device synchronization configuration
        * @brief Used to configure the exposure synchronization function and multi-camera synchronization function of different sensors in a single machine
        *
        * @attention Calling this function will directly write the configuration to the device Flash, and it will still take effect after the device restarts. To
        * avoid affecting the Flash lifespan, do not update the configuration frequently.
        *
        * @param deviceSyncConfig Device synchronization configuration
        * \else
        * @brief 设置设备同步配置
        * @brief 用于配置 单机内的不同 Sensor 的曝光同步功能 和 多机同步功能
        *
        * @attention 调用本函数会直接将配置写入设备Flash，设备重启后依然会生效。为了避免影响Flash寿命，不要频繁更新配置。
        *
        * @param deviceSyncConfig 设备同步配置
        * \endif
        *
        */
        public void SetSyncConfig(DeviceSyncConfig deviceSyncConfig)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_set_sync_config(_handle.Ptr, deviceSyncConfig, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public UInt16 GetSupportedMultiDeviceSyncModeBitmap()
        {
            IntPtr error = IntPtr.Zero;
            UInt16 result = obNative.ob_device_get_supported_multi_device_sync_mode_bitmap(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return result;
        }

        public void SetMultiDeviceSyncConfig(MultiDeviceSyncConfig config)
        {
            IntPtr error = IntPtr.Zero;
            GCHandle gcHandle = GCHandle.Alloc(config, GCHandleType.Pinned);
            IntPtr ptr = gcHandle.AddrOfPinnedObject();
            obNative.ob_device_set_multi_device_sync_config(_handle.Ptr, ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public MultiDeviceSyncConfig GetMultiDeviceSyncConfig()
        {
            IntPtr error = IntPtr.Zero;
            MultiDeviceSyncConfig config;
            obNative.ob_device_get_multi_device_sync_config(out config, _handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return config;
        }

        public void TriggerCapture()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_trigger_capture(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public void SetTimestampResetConfig(DeviceTimestampResetConfig config)
        {
            IntPtr error = IntPtr.Zero;
            GCHandle gcHandle = GCHandle.Alloc(config, GCHandleType.Pinned);
            IntPtr ptr = gcHandle.AddrOfPinnedObject();
            obNative.ob_device_set_timestamp_reset_config(_handle.Ptr, ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public DeviceTimestampResetConfig GetTimestampResetConfig()
        {
            IntPtr error = IntPtr.Zero;
            DeviceTimestampResetConfig config = obNative.ob_device_get_timestamp_reset_config(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
            return config;
        }

        public void TimestampReset()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_timestamp_reset(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        public void TimerSyncWithHost()
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_device_timer_sync_with_host(_handle.Ptr, ref error);
            if(error != IntPtr.Zero)
            {
                throw new NativeException(new Error(error));
            }
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_device(handle, ref error);
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