using System;
using System.Runtime.InteropServices;

namespace Orbbec
{
    internal delegate void NativeDeviceChangedCallback(IntPtr removedPtr, IntPtr addedPtr, IntPtr userData);
    internal delegate void NativeDeviceStateCallback(UInt64 state, String message, IntPtr userData);
    internal delegate void NativeSetDataCallback(DataTranState state, uint percent, IntPtr userData);
    internal delegate void NativeGetDataCallback(DataTranState state, DataChunk dataChunk, IntPtr userData);
    internal delegate void NativeDeviceUpgradeCallback(UpgradeState state, String message, byte percent, IntPtr userData);
    internal delegate void NativeSendFileCallback(FileTranState state, String message, byte percent, IntPtr userData);
    internal delegate void NativeFilterCallback(IntPtr framePtr, IntPtr userData);
    internal delegate void NativeFramesetCallback(IntPtr framesetPtr, IntPtr userData);
    internal delegate void NativeFrameDestroyCallback(IntPtr bufferPtr, IntPtr userData);
    internal delegate void NativePlaybackCallback(IntPtr framePtr, IntPtr userData);
    internal delegate void NativeMediaStateCallback(MediaState state, IntPtr userData);
    internal delegate void NativeFrameCallback(IntPtr framePtr, IntPtr userData);
    internal delegate void NativeLogCallback(LogSeverity logSeverity, String message, IntPtr userData);

    internal class obNative
    {
#if ORBBEC_DEBUG
        public const string obsdk = "OrbbecSDK_d";
#else
        public const string obsdk = "OrbbecSDK";
#endif

        #region Context
        //ob_context *ob_create_context(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_context")]
        public static extern IntPtr ob_create_context(ref IntPtr error);

        //ob_context *ob_create_context_with_config(const char *config_path, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_context_with_config")]
        public static extern IntPtr ob_create_context_with_config(String configPath, ref IntPtr error);

        //void ob_delete_context(ob_context *context, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_context")]
        public static extern void ob_delete_context(IntPtr context, ref IntPtr error);

        //ob_device_list *ob_query_device_list(ob_context *context, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_query_device_list")]
        public static extern IntPtr ob_query_device_list(IntPtr context, ref IntPtr error);

        //void ob_enable_net_device_enumeration(ob_context *context, bool enable, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_enable_net_device_enumeration")]
        public static extern void ob_enable_net_device_enumeration(IntPtr context, bool enable, ref IntPtr error);

        //ob_device *ob_create_net_device(ob_context *context, const char *address, uint16_t port, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_net_device")]
        public static extern IntPtr ob_create_net_device(IntPtr context, String address, UInt16 port, ref IntPtr error);

        //void ob_set_device_changed_callback(ob_context *context, ob_device_changed_callback callback, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_set_device_changed_callback")]
        public static extern void ob_set_device_changed_callback(IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)] NativeDeviceChangedCallback callback, IntPtr userData, ref IntPtr error);

        //void ob_enable_device_clock_sync(ob_context *context, uint64_t repeatInterval, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_enable_device_clock_sync")]
        public static extern void ob_enable_device_clock_sync(IntPtr context, UInt64 repeatInterval, ref IntPtr error);

        //void ob_set_logger_severity(ob_log_severity severity, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_set_logger_severity")]
        public static extern void ob_set_logger_severity(LogSeverity logSeverity, ref IntPtr error);

        //void ob_set_logger_to_file(ob_log_severity severity, const char *directory, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_set_logger_to_file")]
        public static extern void ob_set_logger_to_file(LogSeverity logSeverity, String directory, ref IntPtr error);

        //void ob_set_logger_to_console(ob_log_severity severity, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_set_logger_to_console")]
        public static extern void ob_set_logger_to_console(LogSeverity logSeverity, ref IntPtr error);

        //void ob_set_logger_callback(ob_log_severity severity, ob_log_callback callback, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_set_logger_callback")]
        public static extern void ob_set_logger_callback(LogSeverity logSeverity, NativeLogCallback callback, IntPtr userData, ref IntPtr error);
        #endregion

        #region Device
        //uint32_t ob_device_list_device_count(ob_device_list *list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_device_count")]
        public static extern UInt32 ob_device_list_count(IntPtr deviceList, ref IntPtr error);

        //const char *ob_device_list_get_device_name(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_name")]
        public static extern IntPtr ob_device_list_get_device_name(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //int ob_device_list_get_device_pid(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_pid")]
        public static extern int ob_device_list_get_device_pid(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //int ob_device_list_get_device_vid(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_vid")]
        public static extern int ob_device_list_get_device_vid(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //const char *ob_device_list_get_device_uid(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_uid")]
        public static extern IntPtr ob_device_list_get_device_uid(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //const char *ob_device_list_get_device_serial_number(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_serial_number")]
        public static extern IntPtr ob_device_list_get_device_serial_number(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //const char *ob_device_list_get_device_connection_type(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_connection_type")]
        public static extern IntPtr ob_device_list_get_device_connection_type(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //const char *ob_device_list_get_device_ip_address(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_ip_address")]
        public static extern IntPtr ob_device_list_get_device_ip_address(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //const char *ob_device_list_get_extension_info(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_extension_info")]
        public static extern IntPtr ob_device_list_get_extension_info(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //ob_device *ob_device_list_get_device(ob_device_list *list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device")]
        public static extern IntPtr ob_device_list_get_device(IntPtr deviceList, UInt32 index, ref IntPtr error);

        //ob_device *ob_device_list_get_device_by_serial_number(ob_device_list *list, const char *serial_number, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_by_serial_number")]
        public static extern IntPtr ob_device_list_get_device_by_serial_number(IntPtr deviceList, String serialNumber, ref IntPtr error);

        //ob_device *ob_device_list_get_device_by_uid(ob_device_list *list, const char *uid, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_list_get_device_by_uid")]
        public static extern IntPtr ob_device_list_get_device_by_uid(IntPtr deviceList, String uid, ref IntPtr error);

        //void ob_delete_device(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_device")]
        public static extern void ob_delete_device(IntPtr device, ref IntPtr error);

        //void ob_delete_device_info(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_device_info")]
        public static extern void ob_delete_device_info(IntPtr deviceInfo, ref IntPtr error);

        //void ob_delete_device_list(ob_device_list *list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_device_list")]
        public static extern void ob_delete_device_list(IntPtr deviceList, ref IntPtr error);

        //ob_device_info *ob_device_get_device_info(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_device_info")]
        public static extern IntPtr ob_device_get_device_info(IntPtr device, ref IntPtr error);

        //ob_sensor_list *ob_device_get_sensor_list(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_sensor_list")]
        public static extern IntPtr ob_device_get_sensor_list(IntPtr device, ref IntPtr error);

        //ob_sensor *ob_device_get_sensor(ob_device *device, ob_sensor_type type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_sensor")]
        public static extern IntPtr ob_device_get_sensor(IntPtr device, SensorType sensorType, ref IntPtr error);

        //void ob_device_set_int_property(ob_device *device, ob_property_id property_id, int32_t property, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_set_int_property")]
        public static extern void ob_device_set_int_property(IntPtr device, PropertyId propertyId, Int32 property, ref IntPtr error);

        //int32_t ob_device_get_int_property(ob_device *device, ob_property_id property_id, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_int_property")]
        public static extern Int32 ob_device_get_int_property(IntPtr device, PropertyId propertyId, ref IntPtr error);

        //void ob_device_set_float_property(ob_device *device, ob_property_id property_id, float property, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_set_float_property")]
        public static extern void ob_device_set_float_property(IntPtr device, PropertyId propertyId, float property, ref IntPtr error);

        //float ob_device_get_float_property(ob_device *device, ob_property_id property_id, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_float_property")]
        public static extern float ob_device_get_float_property(IntPtr device, PropertyId propertyId, ref IntPtr error);

        //void ob_device_set_bool_property(ob_device *device, ob_property_id property_id, bool property, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_set_bool_property")]
        public static extern void ob_device_set_bool_property(IntPtr device, PropertyId propertyId, bool property, ref IntPtr error);

        //bool ob_device_get_bool_property(ob_device *device, ob_property_id property_id, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_bool_property")]
        public static extern bool ob_device_get_bool_property(IntPtr device, PropertyId propertyId, ref IntPtr error);

        //void ob_device_set_structured_data( ob_device* device, ob_global_unified_property property_id, const void* data, uint32_t data_size, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_set_structured_data")]
        public static extern void ob_device_set_structured_data(IntPtr device, PropertyId propertyId, IntPtr data, UInt32 dataSize, ref IntPtr error);

        //void ob_device_get_structured_data( ob_device* device, ob_global_unified_property property_id, void* data, uint32_t* data_size, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_get_structured_data")]
        public static extern void ob_device_get_structured_data(IntPtr device, PropertyId propertyId, IntPtr data, ref UInt32 dataSize, ref IntPtr error);

        //void ob_device_set_raw_data(ob_device *device, ob_property_id property_id, void *data, uint32_t data_size, ob_set_data_callback cb, bool async, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_set_raw_data")]
        public static extern void ob_device_set_raw_data(IntPtr device, PropertyId propertyId, IntPtr data, UInt32 dataSize, [MarshalAs(UnmanagedType.FunctionPtr)] NativeSetDataCallback callback, bool async, IntPtr userData, ref IntPtr error);

        //void ob_device_get_raw_data(ob_device *device, ob_property_id property_id, ob_get_data_callback cb, bool async, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_raw_data")]
        public static extern void ob_device_get_raw_data(IntPtr device, PropertyId propertyId, [MarshalAs(UnmanagedType.FunctionPtr)] NativeGetDataCallback callback, bool async, IntPtr userData, ref IntPtr error);

        //ob_protocol_version ob_device_get_protocol_version(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_protocol_version")]
        public static extern ProtocolVersion ob_device_get_protocol_version(IntPtr device, ref IntPtr error);

        //ob_cmd_version ob_device_get_cmd_version(ob_device *device, ob_property_id property_id, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_cmd_version")]
        public static extern CmdVersion ob_device_get_cmd_version(IntPtr device, PropertyId propertyId, ref IntPtr error);

        //uint32_t ob_device_get_supported_property_count(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_supported_property_count")]
        public static extern UInt32 ob_device_get_supported_property_count(IntPtr device, ref IntPtr error);

        //ob_property_item ob_device_get_supported_property(ob_device *device, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_supported_property")]
        public static extern void ob_device_get_supported_property(out PropertyItem item, IntPtr device, UInt32 index, ref IntPtr error);

        //bool ob_device_is_property_supported(ob_device *device, ob_property_id property_id, ob_permission_type permission, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_is_property_supported")]
        public static extern bool ob_device_is_property_supported(IntPtr device, PropertyId propertyId, PermissionType permissionType, ref IntPtr error);

        //ob_int_property_range ob_device_get_int_property_range( ob_device* device, ob_property_id property_id, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_get_int_property_range")]
        public static extern void ob_device_get_int_property_range(out IntPropertyRange range, IntPtr device, PropertyId propertyId, ref IntPtr error);

        //ob_float_property_range ob_device_get_float_property_range( ob_device* device, ob_property_id property_id, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_get_float_property_range")]
        public static extern void ob_device_get_float_property_range(out FloatPropertyRange range, IntPtr device, PropertyId propertyId, ref IntPtr error);

        //ob_bool_property_range ob_device_get_bool_property_range( ob_device* device, ob_property_id property_id, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_get_bool_property_range")]
        public static extern void ob_device_get_bool_property_range(out BoolPropertyRange range, IntPtr device, PropertyId propertyId, ref IntPtr error);

        //void ob_device_write_ahb(ob_device *device, uint32_t reg, uint32_t mask, uint32_t value, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_write_ahb")]
        public static extern void ob_device_write_ahb(IntPtr device, UInt32 reg, UInt32 mask, UInt32 value, ref IntPtr error);

        //void ob_device_read_ahb(ob_device *device, uint32_t reg, uint32_t mask, uint32_t *value, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_read_ahb")]
        public static extern void ob_device_read_ahb(IntPtr device, UInt32 reg, UInt32 mask, out UInt32 value, ref IntPtr error);

        //void ob_device_write_i2c(ob_device *device, uint32_t module_id, uint32_t reg, uint32_t mask, uint32_t value, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_write_i2c")]
        public static extern void ob_device_write_i2c(IntPtr device, UInt32 moduleId, UInt32 reg, UInt32 mask, UInt32 value, ref IntPtr error);

        //void ob_device_read_i2c(ob_device *device, uint32_t module_id, uint32_t reg, uint32_t mask, uint32_t *value, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_read_i2c")]
        public static extern void ob_device_read_i2c(IntPtr device, UInt32 moduleId, UInt32 reg, UInt32 mask, out UInt32 value, ref IntPtr error);

        //void ob_device_write_flash(ob_device *device, uint32_t offset, const void *data, uint32_t data_size, ob_set_data_callback cb, bool async, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_write_flash")]
        public static extern void ob_device_write_flash(IntPtr device, UInt32 offset, IntPtr data, UInt32 dataSize, NativeSetDataCallback callback, bool asycn, IntPtr userData, ref IntPtr error);

        //void ob_device_read_flash(ob_device *device, uint32_t offset, uint32_t data_size, ob_get_data_callback cb, bool async, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_read_flash")]
        public static extern void ob_device_read_flash(IntPtr device, UInt32 offset, UInt32 dataSize, NativeGetDataCallback callback, bool async, IntPtr userData, ref IntPtr error);
        
        //void ob_device_write_customer_data(ob_device *device, const void *data, uint32_t data_size, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_write_customer_data")]
        public static extern void ob_device_write_customer_data(IntPtr device, IntPtr data, UInt32 dataSize, ref IntPtr error);
        
        //void ob_device_read_customer_data(ob_device *device, void *data, uint32_t *data_size, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_read_customer_data")]
        public static extern void ob_device_read_customer_data(IntPtr device, IntPtr data, out UInt32 dataSize, ref IntPtr error);
        
        //void ob_device_upgrade(ob_device *device, const char *path, ob_device_upgrade_callback callback, bool async, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_upgrade")]
        public static extern void ob_device_upgrade(IntPtr device, String path, [MarshalAs(UnmanagedType.FunctionPtr)] NativeDeviceUpgradeCallback callback, bool async, IntPtr userData, ref IntPtr error);

        //void ob_device_upgrade_from_data(ob_device *device, const char *file_data, uint32_t file_size, ob_device_upgrade_callback callback, bool async, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_upgrade_from_data")]
        public static extern void ob_device_upgrade_from_data(IntPtr device, IntPtr fileData, UInt32 fileSize, [MarshalAs(UnmanagedType.FunctionPtr)] NativeDeviceUpgradeCallback callback, bool asycn, IntPtr userData, ref IntPtr error);

        //ob_device_state ob_device_get_device_state( ob_device* device, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_get_device_state")]
        public static extern UInt64 ob_device_get_device_state(IntPtr device, ref IntPtr error);

        //void ob_device_state_changed( ob_device* device, ob_device_state_callback callback, void* user_data, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_state_changed")]
        public static extern void ob_device_state_changed(IntPtr device, [MarshalAs(UnmanagedType.FunctionPtr)] NativeDeviceStateCallback callback, IntPtr userData, ref IntPtr error);

        //void ob_device_send_file_to_destination(ob_device *device, const char *file_path, const char *dst_path, ob_file_send_callback callback, bool async, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_send_file_to_destination")]
        public static extern void ob_device_send_file_to_destination(IntPtr device, String filePath, String dstPath, NativeSendFileCallback callback, bool async, IntPtr user_data, ref IntPtr error);

        //bool ob_device_activate_authorization( ob_device* device, const char* auth_code, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_activate_authorization")]
        public static extern bool ob_device_activate_authorization(IntPtr device, String authCode, ref IntPtr error);

        //void ob_device_write_authorization_code( ob_device* device, const char* auth_code, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_write_authorization_code")]
        public static extern void ob_device_write_authorization_code(IntPtr device, String autoCode, ref IntPtr error);

        //ob_camera_param_list *ob_device_get_calibration_camera_param_list(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_calibration_camera_param_list")]
        public static extern IntPtr ob_device_get_calibration_camera_param_list(IntPtr device, ref IntPtr error);

        //ob_depth_work_mode ob_device_get_current_depth_work_mode(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_current_depth_work_mode")]
        public static extern void ob_device_get_current_depth_work_mode(out DepthWorkMode workMode, IntPtr device, ref IntPtr error);
        
        //ob_status ob_device_switch_depth_work_mode(ob_device *device, const ob_depth_work_mode *work_mode, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_switch_depth_work_mode")]
        public static extern void ob_device_switch_depth_work_mode(IntPtr device, IntPtr workMode, ref IntPtr error);

        //ob_status ob_device_switch_depth_work_mode_by_name(ob_device *device, const char *mode_name, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_switch_depth_work_mode_by_name")]
        public static extern void ob_device_switch_depth_work_mode_by_name(IntPtr device, String modeName, ref IntPtr error);

        //ob_depth_work_mode_list *ob_device_get_depth_work_mode_list(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_depth_work_mode_list")]
        public static extern IntPtr ob_device_get_depth_work_mode_list(IntPtr device, ref IntPtr error);

        //void ob_device_reboot(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_reboot")]
        public static extern void ob_device_reboot(IntPtr device, ref IntPtr error);

        //ob_device_sync_config ob_device_get_sync_config(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_sync_config")]
        public static extern void ob_device_get_sync_config(IntPtr device, out DeviceSyncConfig config, ref IntPtr error);

        //void ob_device_set_sync_config(ob_device *device, ob_device_sync_config device_sync_config, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_set_sync_config")]
        public static extern void ob_device_set_sync_config(IntPtr device, DeviceSyncConfig config, ref IntPtr error);

        //const char* ob_device_info_name( ob_device_info* info, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_info_name")]
        public static extern IntPtr ob_device_info_name(IntPtr deviceInfo, ref IntPtr error);

        //int ob_device_info_pid( ob_device_info* info, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_info_pid")]
        public static extern int ob_device_info_pid(IntPtr deviceInfo, ref IntPtr error);

        //int ob_device_info_vid( ob_device_info* info, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_info_vid")]
        public static extern int ob_device_info_vid(IntPtr deviceInfo, ref IntPtr error);

        //const char* ob_device_info_uid( ob_device_info* info, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_info_uid")]
        public static extern IntPtr ob_device_info_uid(IntPtr deviceInfo, ref IntPtr error);

        //const char* ob_device_info_serial_number( ob_device_info* info, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_info_serial_number")]
        public static extern IntPtr ob_device_info_serial_number(IntPtr deviceInfo, ref IntPtr error);

        //const char* ob_device_info_firmware_version( ob_device_info* info, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_info_firmware_version")]
        public static extern IntPtr ob_device_info_firmware_version(IntPtr deviceInfo, ref IntPtr error);

        //const char* ob_device_info_usb_type( ob_device_info* info, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_device_info_usb_type")]
        public static extern IntPtr ob_device_info_usb_type(IntPtr deviceInfo, ref IntPtr error);

        //const char *ob_device_info_connection_type(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_info_connection_type")]
        public static extern IntPtr ob_device_info_connection_type(IntPtr deviceInfo, ref IntPtr error);
        
        //const char *ob_device_info_ip_address(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_info_ip_address")]
        public static extern IntPtr ob_device_info_ip_address(IntPtr deviceInfo, ref IntPtr error);

        //const char *ob_device_info_get_extension_info(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_info_get_extension_info")]
        public static extern IntPtr ob_device_info_get_extension_info(IntPtr deviceInfo, ref IntPtr error);

        //const char *ob_device_info_hardware_version(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_info_hardware_version")]
        public static extern IntPtr ob_device_info_hardware_version(IntPtr deviceInfo, ref IntPtr error);

        //const char *ob_device_info_supported_min_sdk_version(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_info_supported_min_sdk_version")]
        public static extern IntPtr ob_device_info_supported_min_sdk_version(IntPtr deviceInfo, ref IntPtr error);

        //const char *ob_device_info_asicName(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_info_asicName")]
        public static extern IntPtr ob_device_info_asicName(IntPtr deviceInfo, ref IntPtr error);

        //ob_device_type ob_device_info_device_type(ob_device_info *info, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_info_device_type")]
        public static extern DeviceType ob_device_info_device_type(IntPtr deviceInfo, ref IntPtr error);

        //uint32_t ob_camera_param_list_count(ob_camera_param_list *param_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_camera_param_list_count")]
        public static extern UInt32 ob_camera_param_list_count(IntPtr paramList, ref IntPtr error);

        //ob_camera_param ob_camera_param_list_get_param(ob_camera_param_list *param_list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_camera_param_list_get_param")]
        public static extern void ob_camera_param_list_get_param(out CameraParam cameraParam, IntPtr paramList, UInt32 index, ref IntPtr error);

        //void ob_delete_camera_param_list(ob_camera_param_list *param_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_camera_param_list")]
        public static extern void ob_delete_camera_param_list(IntPtr paramList, ref IntPtr error);

        //uint32_t ob_depth_work_mode_list_count(ob_depth_work_mode_list *work_mode_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_depth_work_mode_list_count")]
        public static extern UInt32 ob_depth_work_mode_list_count(IntPtr workModeList, ref IntPtr error);

        //ob_depth_work_mode ob_depth_work_mode_list_get_item(ob_depth_work_mode_list *work_mode_list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_depth_work_mode_list_get_item")]
        public static extern void ob_depth_work_mode_list_get_item(out DepthWorkMode workMode, IntPtr workModeList, UInt32 index, ref IntPtr error);

        //void ob_delete_depth_work_mode_list(ob_depth_work_mode_list *work_mode_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_depth_work_mode_list")]
        public static extern void ob_delete_depth_work_mode_list(IntPtr workModeList, ref IntPtr error);

        //void ob_delete_data_bundle(ob_data_bundle *data_bundle, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_data_bundle")]
        public static extern void ob_delete_data_bundle(IntPtr dataBundle, ref IntPtr error);

        //bool ob_device_is_global_timestamp_supported(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_is_global_timestamp_supported")]
        public static extern bool ob_device_is_global_timestamp_supported(IntPtr device, ref IntPtr error);

        //void ob_device_load_depth_filter_config(ob_device *device, const char *file_path, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_load_depth_filter_config")]
        public static extern void ob_device_load_depth_filter_config(IntPtr device, string filePath, ref IntPtr error);

        //void ob_device_reset_default_depth_filter_config(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_reset_default_depth_filter_config")]
        public static extern void ob_device_reset_default_depth_filter_config(IntPtr device, ref IntPtr error);

        //const char *ob_device_get_current_preset_name(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_current_preset_name")]
        public static extern IntPtr ob_device_get_current_preset_name(IntPtr device, ref IntPtr error);

        //void ob_device_load_preset(ob_device *device, const char *preset_name, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_load_preset")]
        public static extern void ob_device_load_preset(IntPtr device, string presetName, ref IntPtr error);

        //void ob_device_load_preset_from_json_file(ob_device *device, const char *json_file_path, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_load_preset_from_json_file")]
        public static extern void ob_device_load_preset_from_json_file(IntPtr device, string jsonFilePath, ref IntPtr error);

        //void ob_device_export_current_settings_as_preset_json_file(ob_device *device, const char *json_file_path, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_export_current_settings_as_preset_json_file")]
        public static extern void ob_device_export_current_settings_as_preset_json_file(IntPtr device, string jsonFilePath, ref IntPtr error);

        //ob_device_preset_list *ob_device_get_available_preset_list(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_available_preset_list")]
        public static extern IntPtr ob_device_get_available_preset_list(IntPtr device, ref IntPtr error);

        //void ob_delete_preset_list(ob_device_preset_list *preset_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_preset_list")]
        public static extern void ob_delete_preset_list(IntPtr presetList, ref IntPtr error);

        //uint32_t ob_device_preset_list_count(ob_device_preset_list *preset_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_preset_list_count")]
        public static extern UInt32 ob_device_preset_list_count(IntPtr presetList, ref IntPtr error);

        //const char *ob_device_preset_list_get_name(ob_device_preset_list *preset_list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_preset_list_get_name")]
        public static extern IntPtr ob_device_preset_list_get_name(IntPtr presetList, UInt32 index, ref IntPtr error);

        //bool ob_device_preset_list_has_preset(ob_device_preset_list *preset_list, const char *preset_name, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_preset_list_has_preset")]
        public static extern bool ob_device_preset_list_has_preset(IntPtr presetList, string presetName, ref IntPtr error);
        #endregion

        #region MultipleDevice
        //uint16_t ob_device_get_supported_multi_device_sync_mode_bitmap(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_supported_multi_device_sync_mode_bitmap")]
        public static extern UInt16 ob_device_get_supported_multi_device_sync_mode_bitmap(IntPtr device, ref IntPtr error);
        
        //void ob_device_set_multi_device_sync_config(ob_device *device, const ob_multi_device_sync_config *config, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_set_multi_device_sync_config")]
        public static extern void ob_device_set_multi_device_sync_config(IntPtr device, IntPtr config, ref IntPtr error);

        //ob_multi_device_sync_config ob_device_get_multi_device_sync_config(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_multi_device_sync_config")]
        public static extern void ob_device_get_multi_device_sync_config(out MultiDeviceSyncConfig config, IntPtr device, ref IntPtr error);

        //void ob_device_trigger_capture(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_trigger_capture")]
        public static extern void ob_device_trigger_capture(IntPtr device, ref IntPtr error);

        //void ob_device_set_timestamp_reset_config(ob_device *device, const ob_device_timestamp_reset_config *config, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_set_timestamp_reset_config")]
        public static extern void ob_device_set_timestamp_reset_config(IntPtr device, IntPtr config, ref IntPtr error);

        //ob_device_timestamp_reset_config ob_device_get_timestamp_reset_config(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_get_timestamp_reset_config")]
        public static extern DeviceTimestampResetConfig ob_device_get_timestamp_reset_config(IntPtr device, ref IntPtr error);

        //void ob_device_timestamp_reset(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_timestamp_reset")]
        public static extern void ob_device_timestamp_reset(IntPtr device, ref IntPtr error);

        //void ob_device_timer_sync_with_host(ob_device *device, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_device_timer_sync_with_host")]
        public static extern void ob_device_timer_sync_with_host(IntPtr device, ref IntPtr error);
        #endregion

        #region Error
        //ob_status ob_error_status( ob_error* error );
        [DllImport(obsdk, EntryPoint = "ob_error_status")]
        public static extern Status ob_error_status(IntPtr error);

        //const char *ob_error_message(const ob_error *error);
        [DllImport(obsdk, EntryPoint = "ob_error_message")]
        public static extern IntPtr ob_error_message(IntPtr error);

        //const char* ob_error_function( ob_error* error );
        [DllImport(obsdk, EntryPoint = "ob_error_function")]
        public static extern IntPtr ob_error_function(IntPtr error);

        //const char* ob_error_args( ob_error* error );
        [DllImport(obsdk, EntryPoint = "ob_error_args")]
        public static extern IntPtr ob_error_args(IntPtr error);

        //ob_exception_type ob_error_exception_type( ob_error* error );
        [DllImport(obsdk, EntryPoint = "ob_error_exception_type")]
        public static extern ExceptionType ob_error_exception_type(IntPtr error);

        //void ob_delete_error( ob_error* error );
        [DllImport(obsdk, EntryPoint = "ob_delete_error")]
        public static extern void ob_delete_error(IntPtr error);
        #endregion

        #region Filter
        //ob_filter *ob_create_pointcloud_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_pointcloud_filter")]
        public static extern IntPtr ob_create_pointcloud_filter(ref IntPtr error);

        //void ob_pointcloud_filter_set_camera_param(ob_filter *filter, ob_camera_param param, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pointcloud_filter_set_camera_param")]
        public static extern void ob_pointcloud_filter_set_camera_param(IntPtr filter, CameraParam param, ref IntPtr error);

        //void ob_pointcloud_filter_set_point_format(ob_filter *filter, ob_format type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pointcloud_filter_set_point_format")]
        public static extern void ob_pointcloud_filter_set_point_format(IntPtr filter, Format format, ref IntPtr error);

        //void ob_pointcloud_filter_set_frame_align_state(ob_filter *filter, bool state, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pointcloud_filter_set_frame_align_state")]
        public static extern void ob_pointcloud_filter_set_frame_align_state(IntPtr filter, bool state, ref IntPtr error);

        //void ob_pointcloud_filter_set_position_data_scale(ob_filter *filter, float scale, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pointcloud_filter_set_position_data_scale")]
        public static extern void ob_pointcloud_filter_set_position_data_scale(IntPtr filter, float scale, ref IntPtr error);

        //void ob_pointcloud_filter_set_color_data_normalization(ob_filter *filter, bool state, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pointcloud_filter_set_color_data_normalization")]
        public static extern void ob_pointcloud_filter_set_color_data_normalization(IntPtr filter, bool state, ref IntPtr error);

        //void ob_pointcloud_filter_set_coordinate_system(ob_filter *filter, ob_coordinate_system_type type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pointcloud_filter_set_coordinate_system")]
        public static extern void ob_pointcloud_filter_set_coordinate_system(IntPtr filter, CoordinateSystemType type, ref IntPtr error);

        //ob_filter *ob_create_format_convert_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_format_convert_filter")]
        public static extern IntPtr ob_create_format_convert_filter(ref IntPtr error);

        //void ob_format_convert_filter_set_format(ob_filter *filter, ob_convert_format type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_format_convert_filter_set_format")]
        public static extern void ob_format_convert_filter_set_format(IntPtr filter, ConvertFormat format, ref IntPtr error);

        //ob_filter *ob_create_compression_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_compression_filter")]
        public static extern IntPtr ob_create_compression_filter(ref IntPtr error);

        //void ob_compression_filter_set_compression_params(ob_filter *filter, ob_compression_mode mode, void *params, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_compression_filter_set_compression_params")]
        public static extern void ob_compression_filter_set_compression_params(IntPtr filter, CompressionMode mode, IntPtr param, ref IntPtr error);

        //ob_filter *ob_create_decompression_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_decompression_filter")]
        public static extern IntPtr ob_create_decompression_filter(ref IntPtr error);

        //ob_filter *ob_create_holefilling_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_holefilling_filter")]
        public static extern IntPtr ob_create_holefilling_filter(ref IntPtr error);

        //void ob_holefilling_filter_set_mode(ob_filter *filter, ob_hole_filling_mode mode, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_holefilling_filter_set_mode")]
        public static extern void ob_holefilling_filter_set_mode(IntPtr filter, HoleFillingMode mode, ref IntPtr error);

        //ob_hole_filling_mode ob_holefilling_filter_get_mode(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_holefilling_filter_get_mode")]
        public static extern HoleFillingMode ob_holefilling_filter_get_mode(IntPtr filter, ref IntPtr error);

        //ob_filter *ob_create_temporal_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_temporal_filter")]
        public static extern IntPtr ob_create_temporal_filter(ref IntPtr error);

        //ob_float_property_range ob_temporal_filter_get_diffscale_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_temporal_filter_get_diffscale_range")]
        public static extern void ob_temporal_filter_get_diffscale_range(out FloatPropertyRange range, IntPtr filter, ref IntPtr error);

        //void ob_temporal_filter_set_diffscale_value(ob_filter *filter, float value, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_temporal_filter_set_diffscale_value")]
        public static extern void ob_temporal_filter_set_diffscale_value(IntPtr filter, float value, ref IntPtr error);

        //ob_float_property_range ob_temporal_filter_get_weight_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_temporal_filter_get_weight_range")]
        public static extern void ob_temporal_filter_get_weight_range(out FloatPropertyRange range, IntPtr filter, ref IntPtr error);

        //void ob_temporal_filter_set_weight_value(ob_filter *filter, float value, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_temporal_filter_set_weight_value")]
        public static extern void ob_temporal_filter_set_weight_value(IntPtr filter, float value, ref IntPtr error);

        //ob_filter *ob_create_spatial_advanced_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_spatial_advanced_filter")]
        public static extern IntPtr ob_create_spatial_advanced_filter(ref IntPtr error);

        //ob_float_property_range ob_spatial_advanced_filter_get_alpha_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_spatial_advanced_filter_get_alpha_range")]
        public static extern void ob_spatial_advanced_filter_get_alpha_range(out FloatPropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_uint16_property_range ob_spatial_advanced_filter_get_disp_diff_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_spatial_advanced_filter_get_disp_diff_range")]
        public static extern void ob_spatial_advanced_filter_get_disp_diff_range(out UInt16PropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_uint16_property_range ob_spatial_advanced_filter_get_radius_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_spatial_advanced_filter_get_radius_range")]
        public static extern void ob_spatial_advanced_filter_get_radius_range(out UInt16PropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_int_property_range ob_spatial_advanced_filter_get_magnitude_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_spatial_advanced_filter_get_magnitude_range")]
        public static extern void ob_spatial_advanced_filter_get_magnitude_range(out IntPropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_spatial_advanced_filter_params ob_spatial_advanced_filter_get_filter_params(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_spatial_advanced_filter_get_filter_params")]
        public static extern void ob_spatial_advanced_filter_get_filter_params(out SpatialAdvancedFilterParams filterParams, IntPtr filter, ref IntPtr error);

        //void ob_spatial_advanced_filter_set_filter_params(ob_filter *filter, ob_spatial_advanced_filter_params params, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_spatial_advanced_filter_set_filter_params")]
        public static extern void ob_spatial_advanced_filter_set_filter_params(IntPtr filter, SpatialAdvancedFilterParams filterParams, ref IntPtr error);

        //ob_filter *ob_create_noise_removal_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_noise_removal_filter")]
        public static extern IntPtr ob_create_noise_removal_filter(ref IntPtr error);

        //ob_uint16_property_range ob_noise_removal_filter_get_disp_diff_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_noise_removal_filter_get_disp_diff_range")]
        public static extern void ob_noise_removal_filter_get_disp_diff_range(out UInt16PropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_int_property_range ob_noise_removal_filter_get_max_size_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_noise_removal_filter_get_max_size_range")]
        public static extern void ob_noise_removal_filter_get_max_size_range(out IntPropertyRange range, IntPtr filter, ref IntPtr error);

        //void ob_noise_removal_filter_set_filter_params(ob_filter *filter, ob_noise_removal_filter_params params, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_noise_removal_filter_set_filter_params")]
        public static extern void ob_noise_removal_filter_set_filter_params(IntPtr filter, NoiseRemovalFilterParams filterParams, ref IntPtr error);

        //ob_noise_removal_filter_params ob_noise_removal_filter_get_filter_params(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_noise_removal_filter_get_filter_params")]
        public static extern void ob_noise_removal_filter_get_filter_params(out NoiseRemovalFilterParams filterParams, IntPtr filter, ref IntPtr error);

        //ob_filter *ob_create_edge_noise_removal_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_edge_noise_removal_filter")]
        public static extern IntPtr ob_create_edge_noise_removal_filter(ref IntPtr error);

        //void ob_edge_noise_removal_filter_set_filter_params(ob_filter *filter, ob_edge_noise_removal_filter_params params, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_edge_noise_removal_filter_set_filter_params")]
        public static extern void ob_edge_noise_removal_filter_set_filter_params(IntPtr filter, EdgeNoiseRemovalFilterParams filterParams, ref IntPtr error);

        //ob_edge_noise_removal_filter_params ob_edge_noise_removal_filter_get_filter_params(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_edge_noise_removal_filter_get_filter_params")]
        public static extern void ob_edge_noise_removal_filter_get_filter_params(out EdgeNoiseRemovalFilterParams filterParams, IntPtr filter, ref IntPtr error);

        //ob_uint16_property_range ob_edge_noise_removal_filter_get_margin_left_th_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_edge_noise_removal_filter_get_margin_left_th_range")]
        public static extern void ob_edge_noise_removal_filter_get_margin_left_th_range(out UInt16PropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_uint16_property_range ob_edge_noise_removal_filter_get_margin_right_th_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_edge_noise_removal_filter_get_margin_right_th_range")]
        public static extern void ob_edge_noise_removal_filter_get_margin_right_th_range(out UInt16PropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_uint16_property_range ob_edge_noise_removal_filter_get_margin_top_th_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_edge_noise_removal_filter_get_margin_top_th_range")]
        public static extern void ob_edge_noise_removal_filter_get_margin_top_th_range(out UInt16PropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_uint16_property_range ob_edge_noise_removal_filter_get_margin_bottom_th_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_edge_noise_removal_filter_get_margin_bottom_th_range")]
        public static extern void ob_edge_noise_removal_filter_get_margin_bottom_th_range(out UInt16PropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_filter *ob_create_decimation_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_decimation_filter")]
        public static extern IntPtr ob_create_decimation_filter(ref IntPtr error);

        //ob_uint8_property_range ob_decimation_filter_get_scale_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_decimation_filter_get_scale_range")]
        public static extern UInt8PropertyRange ob_decimation_filter_get_scale_range(IntPtr filter, ref IntPtr error);

        //void ob_decimation_filter_set_scale_value(ob_filter *filter, uint8_t value, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_decimation_filter_set_scale_value")]
        public static extern void ob_decimation_filter_set_scale_value(IntPtr filter, byte value, ref IntPtr error);

        //uint8_t ob_decimation_filter_get_scale_value(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_decimation_filter_get_scale_value")]
        public static extern byte ob_decimation_filter_get_scale_value(IntPtr filter, ref IntPtr error);

        //ob_filter *ob_create_threshold_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_threshold_filter")]
        public static extern IntPtr ob_create_threshold_filter(ref IntPtr error);

        //ob_int_property_range ob_threshold_filter_get_min_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_threshold_filter_get_min_range")]
        public static extern void ob_threshold_filter_get_min_range(out IntPropertyRange range, IntPtr filter, ref IntPtr error);

        //ob_int_property_range ob_threshold_filter_get_max_range(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_threshold_filter_get_max_range")]
        public static extern void ob_threshold_filter_get_max_range(out IntPropertyRange range, IntPtr filter, ref IntPtr error);

        //bool ob_threshold_filter_set_scale_value(ob_filter *filter, uint16_t min, uint16_t max, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_threshold_filter_set_scale_value")]
        public static extern bool ob_threshold_filter_set_scale_value(IntPtr filter, UInt16 min, UInt16 max, ref IntPtr error);

        //ob_filter *ob_create_sequenceId_filter(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_sequenceId_filter")]
        public static extern IntPtr ob_create_sequenceId_filter(ref IntPtr error);

        //void ob_sequence_id_filter_select_sequence_id(ob_filter *filter, int sequence_id, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_sequence_id_filter_select_sequence_id")]
        public static extern void ob_sequence_id_filter_select_sequence_id(IntPtr filter, int sequence_id, ref IntPtr error);

        //int ob_sequence_id_filter_get_sequence_id(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_sequence_id_filter_get_sequence_id")]
        public static extern int ob_sequence_id_filter_get_sequence_id(IntPtr filter, ref IntPtr error);

        //ob_sequence_id_item *ob_sequence_id_filter_get_sequence_id_list(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_sequence_id_filter_get_sequence_id_list")]
        public static extern IntPtr ob_sequence_id_filter_get_sequence_id_list(IntPtr filter, ref IntPtr error);

        //int ob_sequence_id_filter_get_sequence_id_list_size(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_sequence_id_filter_get_sequence_id_list_size")]
        public static extern int ob_sequence_id_filter_get_sequence_id_list_size(IntPtr filter, ref IntPtr error);

        //ob_filter *ob_create_hdr_merge(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_hdr_merge")]
        public static extern IntPtr ob_create_hdr_merge(ref IntPtr error);

        //ob_filter *ob_create_align(ob_error **error, ob_stream_type align_to_stream);
        [DllImport(obsdk, EntryPoint = "ob_create_align")]
        public static extern IntPtr ob_create_align(ref IntPtr error, StreamType alignToStream);

        //ob_stream_type ob_align_get_to_stream_type(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_align_get_to_stream_type")]
        public static extern StreamType ob_align_get_to_stream_type(IntPtr filter, ref IntPtr error);

        //ob_filter *ob_create_disparity_transform(ob_error **error, bool depth_to_disparity);
        [DllImport(obsdk, EntryPoint = "ob_create_disparity_transform")]
        public static extern IntPtr ob_create_disparity_transform(ref IntPtr error, bool depth_to_disparity);

        //void ob_filter_reset( ob_filter* filter, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_filter_reset")]
        public static extern void ob_filter_reset(IntPtr filter, ref IntPtr error);

        //ob_frame* ob_filter_process( ob_filter* filter, ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_filter_process")]
        public static extern IntPtr ob_filter_process(IntPtr filter, IntPtr frame, ref IntPtr error);

        //void ob_filter_enable(ob_filter *filter, bool enable, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_filter_enable")]
        public static extern void ob_filter_enable(IntPtr filter, bool enable, ref IntPtr error);

        //bool ob_filter_is_enable(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_filter_is_enable")]
        public static extern bool ob_filter_is_enable(IntPtr filter, ref IntPtr error);

        //void ob_filter_set_callback( ob_filter* filter, ob_filter_callback callback, void* user_data, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_filter_set_callback")]
        public static extern void ob_filter_set_callback(IntPtr filter, [MarshalAs(UnmanagedType.FunctionPtr)] NativeFilterCallback callback, IntPtr userData, ref IntPtr error);

        //void ob_filter_push_frame( ob_filter* filter, ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_filter_push_frame")]
        public static extern void ob_filter_push_frame(IntPtr filter, IntPtr frame, ref IntPtr error);
        
        //void ob_delete_filter(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_filter")]
        public static extern void ob_delete_filter(IntPtr filter, ref IntPtr error);
        #endregion

        #region Frame
        //uint64_t ob_frame_index( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frame_index")]
        public static extern UInt64 ob_frame_index(IntPtr frame, ref IntPtr error);

        //ob_format ob_frame_format( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frame_format")]
        public static extern Format ob_frame_format(IntPtr frame, ref IntPtr error);

        //ob_frame_type ob_frame_get_type( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frame_get_type")]
        public static extern FrameType ob_frame_get_type(IntPtr frame, ref IntPtr error);

        //uint64_t ob_frame_time_stamp( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frame_time_stamp")]
        public static extern UInt64 ob_frame_time_stamp(IntPtr frame, ref IntPtr error);

        //uint64_t ob_frame_time_stamp_us(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_time_stamp_us")]
        public static extern UInt64 ob_frame_time_stamp_us(IntPtr frame, ref IntPtr error);

        //uint64_t ob_frame_system_time_stamp( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frame_system_time_stamp")]
        public static extern UInt64 ob_frame_system_time_stamp(IntPtr frame, ref IntPtr error);

        //uint64_t ob_frame_system_time_stamp_us(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_system_time_stamp_us")]
        public static extern UInt64 ob_frame_system_time_stamp_us(IntPtr frame, ref IntPtr error);

        //uint64_t ob_frame_global_time_stamp_us(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_global_time_stamp_us")]
        public static extern UInt64 ob_frame_global_time_stamp_us(IntPtr frame, ref IntPtr error);

        //void* ob_frame_data( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frame_data")]
        public static extern IntPtr ob_frame_data(IntPtr frame, ref IntPtr error);

        //uint32_t ob_frame_data_size( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frame_data_size")]
        public static extern UInt32 ob_frame_data_size(IntPtr frame, ref IntPtr error);

        //void *ob_frame_metadata(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_metadata")]
        public static extern IntPtr ob_frame_metadata(IntPtr frame, ref IntPtr error);

        //uint32_t ob_frame_metadata_size(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_metadata_size")]
        public static extern UInt32 ob_frame_metadata_size(IntPtr frame, ref IntPtr error);

        //bool ob_frame_has_metadata(ob_frame *frame, ob_frame_metadata_type type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_has_metadata")]
        public static extern bool ob_frame_has_metadata(IntPtr frame, uint type, ref IntPtr error);

        //int64_t ob_frame_get_metadata_value(ob_frame *frame, ob_frame_metadata_type type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_get_metadata_value")]
        public static extern Int64 ob_frame_get_metadata_value(IntPtr frame, uint type, ref IntPtr error);

        //ob_stream_profile* ob_frame_get_stream_profile(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_get_stream_profile")]
        public static extern IntPtr ob_frame_get_stream_profile(IntPtr frame, ref IntPtr error);

        //ob_sensor* ob_frame_get_sensor(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_get_sensor")]
        public static extern IntPtr ob_frame_get_sensor(IntPtr frame, ref IntPtr error);

        //ob_device* ob_frame_get_device(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_get_device")]
        public static extern IntPtr ob_frame_get_device(IntPtr frame, ref IntPtr error);

        //uint32_t ob_video_frame_width( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_video_frame_width")]
        public static extern UInt32 ob_video_frame_width(IntPtr frame, ref IntPtr error);

        //uint32_t ob_video_frame_height( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_video_frame_height")]
        public static extern UInt32 ob_video_frame_height(IntPtr frame, ref IntPtr error);

        //uint8_t ob_video_frame_pixel_available_bit_size(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_video_frame_pixel_available_bit_size")]
        public static extern byte ob_video_frame_pixel_available_bit_size(IntPtr frame, ref IntPtr error);

        //ob_sensor_type ob_ir_frame_get_source_sensor_type(ob_frame *frame, ob_error **ob_error);
        [DllImport(obsdk, EntryPoint = "ob_ir_frame_get_source_sensor_type")]
        public static extern SensorType ob_ir_frame_get_source_sensor_type(IntPtr frame, ref IntPtr error);

        //float ob_depth_frame_get_value_scale( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_depth_frame_get_value_scale")]
        public static extern float ob_depth_frame_get_value_scale(IntPtr frame, ref IntPtr error);

        //float ob_points_frame_get_position_value_scale(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_points_frame_get_position_value_scale")]
        public static extern float ob_points_frame_get_position_value_scale(IntPtr frame, ref IntPtr error);

        //void ob_delete_frame( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_delete_frame")]
        public static extern void ob_delete_frame(IntPtr frame, ref IntPtr error);

        //uint32_t ob_frameset_frame_count( ob_frame* frameset, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frameset_frame_count")]
        public static extern UInt32 ob_frameset_frame_count(IntPtr frameset, ref IntPtr error);

        //ob_frame* ob_frameset_depth_frame( ob_frame* frame_set, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frameset_depth_frame")]
        public static extern IntPtr ob_frameset_depth_frame(IntPtr frameset, ref IntPtr error);

        //ob_frame* ob_frameset_color_frame( ob_frame* frame_set, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frameset_color_frame")]
        public static extern IntPtr ob_frameset_color_frame(IntPtr frameset, ref IntPtr error);

        //ob_frame* ob_frameset_ir_frame( ob_frame* frame_set, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frameset_ir_frame")]
        public static extern IntPtr ob_frameset_ir_frame(IntPtr frameset, ref IntPtr error);

        //ob_frame* ob_frameset_points_frame( ob_frame* frame_set, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_frameset_points_frame")]
        public static extern IntPtr ob_frameset_points_frame(IntPtr frameset, ref IntPtr error);

        //ob_frame *ob_frameset_get_frame(ob_frame *frameset, ob_frame_type frame_type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frameset_get_frame")]
        public static extern IntPtr ob_frameset_get_frame(IntPtr frameset, FrameType frameType, ref IntPtr error);

        //ob_frame *ob_frameset_get_frame_by_index(ob_frame *frameset, int index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frameset_get_frame_by_index")]
        public static extern IntPtr ob_frameset_get_frame_by_index(IntPtr frameset, int index, ref IntPtr error);

        //ob_accel_value ob_accel_frame_value( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_accel_frame_value")]
        public static extern void ob_accel_frame_value(out AccelValue accelValue, IntPtr frame, ref IntPtr error);

        //float ob_accel_frame_temperature( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_accel_frame_temperature")]
        public static extern float ob_accel_frame_temperature(IntPtr frame, ref IntPtr error);

        //ob_gyro_value ob_gyro_frame_value( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_gyro_frame_value")]
        public static extern void ob_gyro_frame_value(out GyroValue gyroValue, IntPtr frame, ref IntPtr error);

        //float ob_gyro_frame_temperature( ob_frame* frame, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_gyro_frame_temperature")]
        public static extern float ob_gyro_frame_temperature(IntPtr frame, ref IntPtr error);

        //void ob_frame_add_ref(ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_add_ref")]
        public static extern void ob_frame_add_ref(IntPtr frame, ref IntPtr error);

        //ob_frame *ob_create_frame(ob_format frame_format, int width, int height, int stride_bytes, ob_frame_type frame_type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_frame")]
        public static extern IntPtr ob_create_frame(Format format, int width, int height, int strideBytes, FrameType frameType, ref IntPtr error);

        //ob_frame *ob_create_frame_from_buffer(ob_format frame_format, uint32_t frame_width, uint32_t frame_height, uint8_t *buffer, uint32_t buffer_size, ob_frame_destroy_callback *buffer_destroy_cb, void *buffer_destroy_context, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_frame_from_buffer")]
        public static extern IntPtr ob_create_frame_from_buffer(Format format, UInt32 width, UInt32 height, IntPtr buffer, UInt32 bufferSize, NativeFrameDestroyCallback callback, IntPtr userData, ref IntPtr error);
        
        //ob_frame *ob_create_frameset(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_frameset")]
        public static extern IntPtr ob_create_frameset(ref IntPtr error);

        //void ob_frameset_push_frame(ob_frame *frameset, ob_frame_type type, ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frameset_push_frame")]
        public static extern void ob_frameset_push_frame(IntPtr frameset, FrameType frameType, IntPtr frame, ref IntPtr error);

        //void ob_frame_set_system_time_stamp(ob_frame *frame, uint64_t system_timestamp, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_set_system_time_stamp")]
        public static extern void ob_frame_set_system_time_stamp(IntPtr frame, UInt64 timestamp, ref IntPtr error);

        //void ob_frame_set_device_time_stamp(ob_frame *frame, uint64_t device_timestamp, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_set_device_time_stamp")]
        public static extern void ob_frame_set_device_time_stamp(IntPtr frame, UInt64 timestamp, ref IntPtr error);

        //void ob_frame_set_device_time_stamp_us(ob_frame *frame, uint64_t device_timestamp_us, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_frame_set_device_time_stamp_us")]
        public static extern void ob_frame_set_device_time_stamp_us(IntPtr frame, UInt64 timestamp, ref IntPtr error);
        #endregion

        #region Pipeline
        //ob_pipeline* ob_create_pipeline( ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_create_pipeline")]
        public static extern IntPtr ob_create_pipeline(ref IntPtr error);

        //ob_pipeline* ob_create_pipeline_with_device( ob_device* dev, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_create_pipeline_with_device")]
        public static extern IntPtr ob_create_pipeline_with_device(IntPtr device, ref IntPtr error);

        //ob_pipeline *ob_create_pipeline_with_playback_file(const char *file_name, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_pipeline_with_playback_file")]
        public static extern IntPtr ob_create_pipeline_with_playback_file(String fileName, ref IntPtr error);

        //void ob_delete_pipeline( ob_pipeline* pipeline, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_delete_pipeline")]
        public static extern void ob_delete_pipeline(IntPtr pipeline, ref IntPtr error);

        //void ob_pipeline_start( ob_pipeline* pipeline, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_start")]
        public static extern void ob_pipeline_start(IntPtr pipeline, ref IntPtr error);

        //void ob_pipeline_start_with_config( ob_pipeline* pipeline, ob_config* config, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_start_with_config")]
        public static extern void ob_pipeline_start_with_config(IntPtr pipeline, IntPtr config, ref IntPtr error);

        //void ob_pipeline_start_with_callback( ob_pipeline* pipeline, ob_config* config, ob_frame_set_callback callback, void* user_data, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_start_with_callback")]
        public static extern void ob_pipeline_start_with_callback(IntPtr pipeline, IntPtr config, [MarshalAs(UnmanagedType.FunctionPtr)] NativeFramesetCallback callback, IntPtr userData, ref IntPtr error);

        //void ob_pipeline_stop( ob_pipeline* pipeline, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_stop")]
        public static extern void ob_pipeline_stop(IntPtr pipeline, ref IntPtr error);

        //ob_config* ob_pipeline_get_config( ob_pipeline* pipeline, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_get_config")]
        public static extern IntPtr ob_pipeline_get_config(IntPtr pipeline, ref IntPtr error);

        //ob_frame* ob_pipeline_wait_for_frameset( ob_pipeline* pipeline, uint32_t timeout_ms, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_wait_for_frameset")]
        public static extern IntPtr ob_pipeline_wait_for_frameset(IntPtr pipeline, UInt32 timeoutMs, ref IntPtr error);

        //ob_device* ob_pipeline_get_device( ob_pipeline* pipeline, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_get_device")]
        public static extern IntPtr ob_pipeline_get_device(IntPtr pipeline, ref IntPtr error);

        //ob_playback *ob_pipeline_get_playback(ob_pipeline *pipeline, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pipeline_get_playback")]
        public static extern IntPtr ob_pipeline_get_playback(IntPtr pipeline, ref IntPtr error);

        //ob_stream_profile_list* ob_pipeline_get_stream_profile_list( ob_pipeline* pipeline, ob_sensor_type sensor_type, uint32_t* profile_count, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_get_stream_profile_list")]
        public static extern IntPtr ob_pipeline_get_stream_profile_list(IntPtr pipeline, SensorType sensorType, ref IntPtr error);

        //void ob_pipeline_enable_frame_sync( ob_pipeline* pipeline, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_enable_frame_sync")]
        public static extern void ob_pipeline_enable_frame_sync(IntPtr pipeline, ref IntPtr error);

        //void ob_pipeline_disable_frame_sync( ob_pipeline* pipeline, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_pipeline_disable_frame_sync")]
        public static extern void ob_pipeline_disable_frame_sync(IntPtr pipeline, ref IntPtr error);

        //void ob_pipeline_switch_config(ob_pipeline *pipeline, ob_config *config, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pipeline_switch_config")]
        public static extern void ob_pipeline_switch_config(IntPtr pipeline, IntPtr config, ref IntPtr error);

        //ob_camera_param ob_pipeline_get_camera_param_with_profile(ob_pipeline *pipeline, uint32_t colorWidth, uint32_t colorHeight, uint32_t depthWidth, uint32_t depthHeight, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pipeline_get_camera_param_with_profile")]
        public static extern void ob_pipeline_get_camera_param_with_profile(out CameraParam cameraParam, IntPtr pipeline, UInt32 colorWidth, UInt32 colorHeight, UInt32 depthWidth, UInt32 depthHeight, ref IntPtr error);

        //ob_camera_param ob_pipeline_get_camera_param(ob_pipeline *pipeline, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pipeline_get_camera_param")]
        public static extern void ob_pipeline_get_camera_param(out CameraParam cameraParam, IntPtr pipeline, ref IntPtr error);

        //ob_calibration_param ob_pipeline_get_calibration_param(ob_pipeline *pipeline, ob_config *config, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pipeline_get_calibration_param")]
        public static extern void ob_pipeline_get_calibration_param(out CalibrationParam calibrationParam, IntPtr pipeline, IntPtr config, ref IntPtr error);
        
        //ob_stream_profile_list *ob_get_d2c_depth_profile_list(ob_pipeline *pipeline, ob_stream_profile *color_profile,ob_align_mode align_mode, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_get_d2c_depth_profile_list")]
        public static extern IntPtr ob_get_d2c_depth_profile_list(IntPtr pipeline, IntPtr colorProfile, AlignMode alignMode, ref IntPtr error);
        
        //ob_rect ob_get_d2c_valid_area(ob_pipeline *pipeline, uint32_t distance, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_get_d2c_valid_area")]
        public static extern void ob_get_d2c_valid_area(out Rect rect, IntPtr pipeline, UInt32 distance, ref IntPtr error);

        //ob_rect ob_get_d2c_range_valid_area(ob_pipeline *pipeline, uint32_t minimum_distance,uint32_t maximum_distance, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_get_d2c_range_valid_area")]
        public static extern void ob_get_d2c_range_valid_area(out Rect rect, IntPtr pipeline, UInt32 minDistance, UInt32 maxDistance, ref IntPtr error);
        
        //void ob_pipeline_start_record(ob_pipeline *pipeline, const char *file_name, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pipeline_start_record")]
        public static extern void ob_pipeline_start_record(IntPtr pipeline, String fileName, ref IntPtr error);
        
        //void ob_pipeline_stop_record(ob_pipeline *pipeline, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_pipeline_stop_record")]
        public static extern void ob_pipeline_stop_record(IntPtr pipeline, ref IntPtr error);

        //ob_config* ob_create_config( ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_create_config")]
        public static extern IntPtr ob_create_config(ref IntPtr error);

        //void ob_delete_config( ob_config* config, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_delete_config")]
        public static extern void ob_delete_config(IntPtr config, ref IntPtr error);
        
        //void ob_config_enable_stream( ob_config* config, ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_config_enable_stream")]
        public static extern void ob_config_enable_stream(IntPtr config, IntPtr profile, ref IntPtr error);

        //void ob_config_enable_video_stream(ob_config *config, ob_stream_type type, int width, int height, int fps, ob_format format, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_enable_video_stream")]
        public static extern void ob_config_enable_video_stream(IntPtr config, StreamType streamType, int width, int height, int fps, Format format, ref IntPtr error);

        //void ob_config_enable_accel_stream(ob_config *config, ob_accel_full_scale_range full_scale_range, ob_accel_sample_rate sample_rate, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_enable_accel_stream")]
        public static extern void ob_config_enable_accel_stream(IntPtr config, AccelFullScaleRange fullScaleRange, AccelSampleRate sampleRate, ref IntPtr error);

        //void ob_config_enable_gyro_stream(ob_config *config, ob_gyro_full_scale_range full_scale_range, ob_gyro_sample_rate sample_rate, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_enable_gyro_stream")]
        public static extern void ob_config_enable_gyro_stream(IntPtr config, GyroFullScaleRange fullScaleRange, GyroSampleRate sampleRate, ref IntPtr error);

        //void ob_config_enable_all_stream( ob_config* config, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_config_enable_all_stream")]
        public static extern void ob_config_enable_all_stream(IntPtr config, ref IntPtr error);

        //ob_stream_profile_list *ob_config_get_enabled_stream_profile_list(ob_config *config, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_get_enabled_stream_profile_list")]
        public static extern IntPtr ob_config_get_enabled_stream_profile_list(IntPtr config, ref IntPtr error);

        //void ob_config_disable_stream( ob_config* config, ob_stream_type type, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_config_disable_stream")]
        public static extern void ob_config_disable_stream(IntPtr config, StreamType streamType, ref IntPtr error);

        //void ob_config_disable_all_stream( ob_config* config, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_config_disable_all_stream")]
        public static extern void ob_config_disable_all_stream(IntPtr config, ref IntPtr error);

        //void ob_config_set_align_mode(ob_config *config, ob_align_mode mode, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_set_align_mode")]
        public static extern void ob_config_set_align_mode(IntPtr config, AlignMode mode, ref IntPtr error);
        
        //void ob_config_set_depth_scale_require(ob_config *config,bool enable,ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_set_depth_scale_require")]
        public static extern void ob_config_set_depth_scale_require(IntPtr config, bool enable, ref IntPtr error);

        //void ob_config_set_d2c_target_resolution(ob_config *config,uint32_t d2c_target_width,uint32_t d2c_target_height,ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_set_d2c_target_resolution")]
        public static extern void ob_config_set_d2c_target_resolution(IntPtr config, UInt32 width, UInt32 height, ref IntPtr error);

        //void ob_config_set_frame_aggregate_output_mode(ob_config *config, ob_frame_aggregate_output_mode mode, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_config_set_frame_aggregate_output_mode")]
        public static extern void ob_config_set_frame_aggregate_output_mode(IntPtr config, FrameAggregateOutputMode mode, ref IntPtr error);
        #endregion

        #region Record
        //ob_recorder *ob_create_recorder(ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_recorder")]
        public static extern IntPtr ob_create_recorder(ref IntPtr error);

        //ob_recorder *ob_create_recorder_with_device(ob_device *dev, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_recorder_with_device")]
        public static extern IntPtr ob_create_recorder_with_device(IntPtr dev, ref IntPtr error);

        //void ob_delete_recorder(ob_recorder *recorder, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_recorder")]
        public static extern void ob_delete_recorder(IntPtr recorder, ref IntPtr error);
        
        //void ob_recorder_start(ob_recorder *recorder, const char *filename, bool async, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_recorder_start")]
        public static extern void ob_recorder_start(IntPtr recorder, String fileName, bool async, ref IntPtr error);

        //void ob_recorder_stop(ob_recorder *recorder, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_recorder_stop")]
        public static extern void ob_recorder_stop(IntPtr recorder, ref IntPtr error);

        //void ob_recorder_write_frame(ob_recorder *recorder, ob_frame *frame, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_recorder_write_frame")]
        public static extern void ob_recorder_write_frame(IntPtr recorder, IntPtr frame, ref IntPtr error);
        #endregion

        #region Playback
        //ob_playback *ob_create_playback(const char *filename, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_create_playback")]
        public static extern IntPtr ob_create_playback(String fileName, ref IntPtr error);

        //void ob_delete_playback(ob_playback *playback, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_playback")]
        public static extern void ob_delete_playback(IntPtr playback, ref IntPtr error);

        //void ob_playback_start(ob_playback *playback, ob_playback_callback callback, void *user_data, ob_media_type type, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_playback_start")]
        public static extern void ob_playback_start(IntPtr playback, [MarshalAs(UnmanagedType.FunctionPtr)] NativePlaybackCallback callback, IntPtr userData, MediaType mediaType, ref IntPtr error);

        //void ob_playback_stop(ob_playback *playback, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_playback_stop")]
        public static extern void ob_playback_stop(IntPtr playback, ref IntPtr error);

        //void ob_set_playback_state_callback(ob_playback *playback, ob_media_state_callback callback, void *user_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_set_playback_state_callback")]
        public static extern void ob_set_playback_state_callback(IntPtr playback, NativeMediaStateCallback callback, IntPtr userData, ref IntPtr error);
        
        //ob_device_info *ob_playback_get_device_info(ob_playback *playback, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_playback_get_device_info")]
        public static extern IntPtr ob_playback_get_device_info(IntPtr playback, ref IntPtr error);

        //ob_camera_param ob_playback_get_camera_param(ob_playback *playback, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_playback_get_camera_param")]
        public static extern void ob_playback_get_camera_param(out CameraParam cameraParam, IntPtr playback, ref IntPtr error);
        #endregion

        #region Sensor
        //ob_sensor_type ob_sensor_get_type( ob_sensor* sensor, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_sensor_get_type")]
        public static extern SensorType ob_sensor_get_type(IntPtr sensor, ref IntPtr error);

        //ob_stream_profile_list *ob_sensor_get_stream_profile_list(ob_sensor *sensor, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_sensor_get_stream_profile_list")]
        public static extern IntPtr ob_sensor_get_stream_profile_list(IntPtr sensor, ref IntPtr error);

        //ob_filter_list *ob_sensor_get_recommended_filter_list(ob_sensor *sensor, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_sensor_get_recommended_filter_list")]
        public static extern IntPtr ob_sensor_get_recommended_filter_list(IntPtr sensor, ref IntPtr error);

        //uint32_t ob_filter_list_get_count(ob_filter_list *filter_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_filter_list_get_count")]
        public static extern UInt32 ob_filter_list_get_count(IntPtr filterList, ref IntPtr error);

        //ob_filter *ob_get_filter(ob_filter_list *filter_list, uint32_t index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_get_filter")]
        public static extern IntPtr ob_get_filter(IntPtr filterList, UInt32 index, ref IntPtr error);

        //const char *ob_get_filter_name(ob_filter *filter, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_get_filter_name")]
        public static extern IntPtr ob_get_filter_name(IntPtr filter, ref IntPtr error);

        //void ob_delete_filter_list(ob_filter_list *filter_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_delete_filter_list")]
        public static extern void ob_delete_filter_list(IntPtr filterList, ref IntPtr error);

        //void ob_sensor_start( ob_sensor* sensor, ob_stream_profile* profile, ob_frame_callback callback, void* user_data, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_sensor_start")]
        public static extern void ob_sensor_start(IntPtr sensor, IntPtr profile, [MarshalAs(UnmanagedType.FunctionPtr)] NativeFrameCallback callback, IntPtr userData, ref IntPtr error);
        
        //void ob_sensor_stop( ob_sensor* sensor, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_sensor_stop")]
        public static extern void ob_sensor_stop(IntPtr sensor, ref IntPtr error);

        //void ob_sensor_switch_profile(ob_sensor *sensor, ob_stream_profile *profile, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_sensor_switch_profile")]
        public static extern void ob_sensor_switch_profile(IntPtr sensor, IntPtr profile, ref IntPtr error);

        //void ob_delete_sensor_list( ob_sensor_list* sensors, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_delete_sensor_list")]
        public static extern void ob_delete_sensor_list(IntPtr sensors, ref IntPtr error);

        //uint32_t ob_sensor_list_get_sensor_count( ob_sensor_list* sensors, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_sensor_list_get_sensor_count")]
        public static extern UInt32 ob_sensor_list_get_sensor_count(IntPtr sensors, ref IntPtr error);

        //ob_sensor_type ob_sensor_list_get_sensor_type( ob_sensor_list* sensors, uint32_t index, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_sensor_list_get_sensor_type")]
        public static extern SensorType ob_sensor_list_get_sensor_type(IntPtr sensors, UInt32 index, ref IntPtr error);

        //ob_sensor* ob_sensor_list_get_sensor_by_type( ob_sensor_list* sensors, ob_sensor_type sensorType, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_sensor_list_get_sensor_by_type")]
        public static extern IntPtr ob_sensor_list_get_sensor_by_type(IntPtr sensors, SensorType sensorType, ref IntPtr error);

        //ob_sensor* ob_sensor_list_get_sensor( ob_sensor_list* sensors, uint32_t index, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_sensor_list_get_sensor")]
        public static extern IntPtr ob_sensor_list_get_sensor(IntPtr sensors, UInt32 index, ref IntPtr error);

        //void ob_delete_sensor( ob_sensor* sensor, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_delete_sensor")]
        public static extern void ob_delete_sensor(IntPtr sensor, ref IntPtr error);
        #endregion

        #region StreamProfile
        //ob_format ob_stream_profile_format( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_format")]
        public static extern Format ob_stream_profile_format(IntPtr profile, ref IntPtr error);

        //ob_stream_type ob_stream_profile_type( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_type")]
        public static extern StreamType ob_stream_profile_type(IntPtr profile, ref IntPtr error);

        //ob_extrinsic ob_stream_profile_get_extrinsic_to(ob_stream_profile *source, ob_stream_profile *target, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_get_extrinsic_to")]
        public static extern void ob_stream_profile_get_extrinsic_to(out Extrinsic extrinsic, IntPtr source, IntPtr target, ref IntPtr error);

        //uint32_t ob_video_stream_profile_fps( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_video_stream_profile_fps")]
        public static extern UInt32 ob_video_stream_profile_fps(IntPtr profile, ref IntPtr error);

        //uint32_t ob_video_stream_profile_width( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_video_stream_profile_width")]
        public static extern UInt32 ob_video_stream_profile_width(IntPtr profile, ref IntPtr error);
        
        //uint32_t ob_video_stream_profile_height( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_video_stream_profile_height")]
        public static extern UInt32 ob_video_stream_profile_height(IntPtr profile, ref IntPtr error);

        //ob_camera_intrinsic ob_video_stream_get_intrinsic(ob_stream_profile *profile, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_video_stream_get_intrinsic")]
        public static extern void ob_video_stream_get_intrinsic(out CameraIntrinsic intrinsic, IntPtr profile, ref IntPtr error);

        //ob_camera_distortion ob_video_stream_get_distortion(ob_stream_profile *profile, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_video_stream_get_distortion")]
        public static extern void ob_video_stream_get_distortion(out CameraDistortion distortion, IntPtr profile, ref IntPtr error);

        //ob_accel_full_scale_range ob_accel_stream_profile_full_scale_range( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_accel_stream_profile_full_scale_range")]
        public static extern AccelFullScaleRange ob_accel_stream_profile_full_scale_range(IntPtr profile, ref IntPtr error);

        //ob_accel_sample_rate ob_accel_stream_profile_sample_rate( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_accel_stream_profile_sample_rate")]
        public static extern AccelSampleRate ob_accel_stream_profile_sample_rate(IntPtr profile, ref IntPtr error);

        //ob_accel_intrinsic ob_accel_stream_profile_get_intrinsic(ob_stream_profile *profile, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_accel_stream_profile_get_intrinsic")]
        public static extern void ob_accel_stream_profile_get_intrinsic(out AccelIntrinsic intrinsic, IntPtr profile, ref IntPtr error);

        //ob_gyro_full_scale_range ob_gyro_stream_profile_full_scale_range( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_gyro_stream_profile_full_scale_range")]
        public static extern GyroFullScaleRange ob_gyro_stream_profile_full_scale_range(IntPtr profile, ref IntPtr error);

        //ob_gyro_intrinsic ob_gyro_stream_get_intrinsic(ob_stream_profile *profile, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_gyro_stream_get_intrinsic")]
        public static extern void ob_gyro_stream_get_intrinsic(out GyroIntrinsic intrinsic, IntPtr profile, ref IntPtr error);

        //ob_gyro_sample_rate ob_gyro_stream_profile_sample_rate( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_gyro_stream_profile_sample_rate")]
        public static extern GyroSampleRate ob_gyro_stream_profile_sample_rate(IntPtr profile, ref IntPtr error);

        //ob_stream_profile *ob_stream_profile_list_get_video_stream_profile(ob_stream_profile_list *profile_list, int width, int height, ob_format format, int fps, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_list_get_video_stream_profile")]
        public static extern IntPtr ob_stream_profile_list_get_video_stream_profile(IntPtr profileList, int width, int height, Format format, int fps, ref IntPtr error);
        
        //ob_stream_profile *ob_stream_profile_list_get_accel_stream_profile(ob_stream_profile_list *profile_list, ob_accel_full_scale_range fullScaleRange, ob_accel_sample_rate sampleRate, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_list_get_accel_stream_profile")]
        public static extern IntPtr ob_stream_profile_list_get_accel_stream_profile(IntPtr profileList, AccelFullScaleRange fullScaleRange, AccelSampleRate sampleRate, ref IntPtr error);

        //ob_stream_profile *ob_stream_profile_list_get_gyro_stream_profile(ob_stream_profile_list *profile_list, ob_gyro_full_scale_range fullScaleRange, ob_gyro_sample_rate sampleRate, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_list_get_gyro_stream_profile")]
        public static extern IntPtr ob_stream_profile_list_get_gyro_stream_profile(IntPtr profileList, GyroFullScaleRange fullScaleRange, GyroSampleRate sampleRate, ref IntPtr error);

        //ob_stream_profile *ob_stream_profile_list_get_profile(ob_stream_profile_list *profile_list, int index, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_list_get_profile")]
        public static extern IntPtr ob_stream_profile_list_get_profile(IntPtr profileList, int index, ref IntPtr error);
        
        //uint32_t ob_stream_profile_list_count(ob_stream_profile_list *profile_list, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_stream_profile_list_count")]
        public static extern UInt32 ob_stream_profile_list_count(IntPtr profileList, ref IntPtr error);

        //void ob_delete_stream_profile_list( ob_stream_profile** profiles, uint32_t count, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_delete_stream_profile_list")]
        public static extern void ob_delete_stream_profile_list(IntPtr profiles, ref IntPtr error);

        //void ob_delete_stream_profile( ob_stream_profile* profile, ob_error** error );
        [DllImport(obsdk, EntryPoint = "ob_delete_stream_profile")]
        public static extern void ob_delete_stream_profile(IntPtr profile, ref IntPtr error);
        #endregion

        #region Utils
        //bool ob_calibration_3d_to_3d(const ob_calibration_param calibration_param, const ob_point3f source_point3f, const ob_sensor_type source_sensor_type, const ob_sensor_type target_sensor_type, ob_point3f *target_point3f, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_calibration_3d_to_3d")]
        public static extern bool ob_calibration_3d_to_3d(CalibrationParam calibrationParam, Point3f sourcePoint3f, SensorType sourceSensorType, SensorType targetSensorType, out Point3f targetPoint3f, ref IntPtr error);

        //bool ob_calibration_2d_to_3d(const ob_calibration_param calibration_param, const ob_point2f source_point2f, const float source_depth_pixel_value, const ob_sensor_type source_sensor_type, const ob_sensor_type target_sensor_type, ob_point3f *target_point3f, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_calibration_2d_to_3d")]
        public static extern bool ob_calibration_2d_to_3d(CalibrationParam calibrationParam, Point2f sourcePoint2f, float sourceDepthPixelValue, SensorType sourceSensorType, SensorType targetSensorType, out Point3f targetPoint3f, ref IntPtr error);

        //bool ob_calibration_2d_to_3d_undistortion(const ob_calibration_param calibration_param, const ob_point2f source_point2f, const float source_depth_pixel_value, const ob_sensor_type source_sensor_type, const ob_sensor_type target_sensor_type, ob_point3f *target_point3f, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_calibration_2d_to_3d_undistortion")]
        public static extern bool ob_calibration_2d_to_3d_undistortion(CalibrationParam calibrationParam, Point2f sourcePoint2f, float sourceDepthPixelValue, SensorType sourceSensorType, SensorType targetSensorType, out Point3f targetPoint3f, ref IntPtr error);

        //bool ob_calibration_3d_to_2d(const ob_calibration_param calibration_param, const ob_point3f source_point3f, const ob_sensor_type source_sensor_type, const ob_sensor_type target_sensor_type, ob_point2f *target_point2f, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_calibration_3d_to_2d")]
        public static extern bool ob_calibration_3d_to_2d(CalibrationParam calibrationParam, Point3f sourcePoint3f, SensorType sourceSensorType, SensorType targetSensorType, out Point2f targetPoint2f, ref IntPtr error);

        //bool ob_calibration_2d_to_2d(const ob_calibration_param calibration_param, const ob_point2f source_point2f, const float source_depth_pixel_value, const ob_sensor_type source_sensor_type, const ob_sensor_type target_sensor_type, ob_point2f *target_point2f, ob_error **error);
        [DllImport(obsdk, EntryPoint = "ob_calibration_2d_to_2d")]
        public static extern bool ob_calibration_2d_to_2d(CalibrationParam calibrationParam, Point2f sourcePoint2f, float sourceDepthPixelValue, SensorType sourceSensorType, SensorType targetSensorType, Point2f targetPoint2f, ref IntPtr error);

        //ob_frame *transformation_depth_frame_to_color_camera(ob_device *device, ob_frame *depth_frame, uint32_t target_color_camera_width, uint32_t target_color_camera_height, ob_error **error);
        [DllImport(obsdk, EntryPoint = "transformation_depth_frame_to_color_camera")]
        public static extern IntPtr transformation_depth_frame_to_color_camera(IntPtr device, IntPtr depthFrame, UInt32 targetColorCameraWidth, UInt32 targetColorCameraHeight, ref IntPtr error);

        //bool transformation_init_xy_tables(const ob_calibration_param calibration_param, const ob_sensor_type sensor_type, float *data, uint32_t *data_size, ob_xy_tables *xy_tables, ob_error **error);
        [DllImport(obsdk, EntryPoint = "transformation_init_xy_tables")]
        public static extern bool transformation_init_xy_tables(CalibrationParam calibrationParam, SensorType sensorType, IntPtr data, IntPtr dataSize, IntPtr xyTables, ref IntPtr error);

        //void transformation_depth_to_pointcloud(ob_xy_tables *xy_tables, const void *depth_image_data, void *pointcloud_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "transformation_depth_to_pointcloud")]
        public static extern void transformation_depth_to_pointcloud(IntPtr xyTables, IntPtr depthImageData, IntPtr pointcloudData, ref IntPtr error);

        //void transformation_depth_to_rgbd_pointcloud(ob_xy_tables *xy_tables, const void *depth_image_data, const void *color_image_data, void *pointcloud_data, ob_error **error);
        [DllImport(obsdk, EntryPoint = "transformation_depth_to_rgbd_pointcloud")]
        public static extern void transformation_depth_to_rgbd_pointcloud(IntPtr xyTables, IntPtr depthImageData, IntPtr colorImageData, IntPtr pointcloudData, ref IntPtr error);
        #endregion

        #region Version
        //int ob_get_version();
        [DllImport(obsdk, EntryPoint = "ob_get_version")]
        public static extern int ob_get_version();

        //int ob_get_major_version();
        [DllImport(obsdk, EntryPoint = "ob_get_major_version")]
        public static extern int ob_get_major_version();

        //int ob_get_minor_version();
        [DllImport(obsdk, EntryPoint = "ob_get_minor_version")]
        public static extern int ob_get_minor_version();

        //int ob_get_patch_version();
        [DllImport(obsdk, EntryPoint = "ob_get_patch_version")]
        public static extern int ob_get_patch_version();

        //const char *ob_get_stage_version();
        [DllImport(obsdk, EntryPoint = "ob_get_stage_version")]
        public static extern IntPtr ob_get_stage_version();
        #endregion
    }
}