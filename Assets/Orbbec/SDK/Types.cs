using System;
using System.Runtime.InteropServices;

namespace Orbbec
{
    /**
    * @brief send data or receive data return status type
    */
    public enum HPStatusCode {
        HP_STATUS_OK                      = 0,      /**< success*/
        HP_STATUS_NO_DEVICE_FOUND         = 1,      /**< No device found*/
        HP_STATUS_CONTROL_TRANSFER_FAILED = 2,      /**< Transfer failed*/
        HP_STATUS_UNKNOWN_ERROR           = 0xffff, /**< Unknown error*/
    }

    /**
    * \if English
    * @brief the permission type of api or property
    * \else
    * @brief 接口/属性的访问权限类型
    * \endif
    */
    public enum PermissionType
    {
        OB_PERMISSION_DENY       = 0,   /**< no permission */
        OB_PERMISSION_READ       = 1,   /**< can read */
        OB_PERMISSION_WRITE      = 2,   /**< can write */
        OB_PERMISSION_READ_WRITE = 3,   /**< can read and write */
        OB_PERMISSION_ANY        = 255, /**< any situation above */
    }

    /**
     * \if English
     * @brief error code
     * \else
     * @brief 错误码
     * \endif
     */
    public enum Status
    {
        OB_STATUS_OK    = 0, /**< status ok */
        OB_STATUS_ERROR = 1, /**< status error */
    }

    /**
     * \if English
     * @brief log level, the higher the level, the stronger the log filter
     * \else
     * @brief log等级, 等级越高Log过滤力度越大
     * \endif
     */
    public enum LogSeverity
    {
        OB_LOG_SEVERITY_DEBUG, /**< debug */
        OB_LOG_SEVERITY_INFO,  /**< information */
        OB_LOG_SEVERITY_WARN,  /**< warning */
        OB_LOG_SEVERITY_ERROR, /**< error */
        OB_LOG_SEVERITY_FATAL, /**< fatal error */
        OB_LOG_SEVERITY_OFF    /**< off (close LOG) */
    }

    /**
    * \if English
    * @brief The exception types in the SDK, through the exception type, you can easily determine the specific type of error.
    * For detailed error API interface functions and error logs, please refer to the information of ob_error
    * \else
    * @brief SDK内部的异常类型，通过异常类型，可以简单判断具体哪个类型的错误
    * 详细的错误API接口函数、错误日志请参考ob_error的信息
    * \endif
    */
    public enum ExceptionType
    {
        OB_EXCEPTION_TYPE_UNKNOWN,                 /**< Unknown error, an error not clearly defined by the SDK */
        OB_EXCEPTION_TYPE_CAMERA_DISCONNECTED,     /**< SDK device disconnection exception */
        OB_EXCEPTION_TYPE_PLATFORM,                /**< An error in the SDK adaptation platform layer means an error in the implementation of a specific system
                                                    platform */
        OB_EXCEPTION_TYPE_INVALID_VALUE,           /**< Invalid parameter type exception, need to check input parameter */
        OB_EXCEPTION_TYPE_WRONG_API_CALL_SEQUENCE, /**< Exception caused by API version mismatch */
        OB_EXCEPTION_TYPE_NOT_IMPLEMENTED,         /**< SDK and firmware have not yet implemented functions */
        OB_EXCEPTION_TYPE_IO,                      /**< SDK access IO exception error */
        OB_EXCEPTION_TYPE_MEMORY,                  /**< SDK access and use memory errors, which means that the frame fails to allocate memory */
        OB_EXCEPTION_TYPE_UNSUPPORTED_OPERATION,   /**< Unsupported operation type error by SDK or RGBD device */
    }

    /**
     * \if English
     * @brief Enumeration value describing the sensor type
     * \else
     * @brief 描述传感器类型的枚举值
     * \endif
     */
    public enum SensorType
    {
        OB_SENSOR_UNKNOWN   = 0, /**< Unknown type sensor */
        OB_SENSOR_IR        = 1, /**< IR */
        OB_SENSOR_COLOR     = 2, /**< Color */
        OB_SENSOR_DEPTH     = 3, /**< Depth */
        OB_SENSOR_ACCEL     = 4, /**< Accel */
        OB_SENSOR_GYRO      = 5, /**< Gyro */
        OB_SENSOR_IR_LEFT   = 6, /**< left IR */
        OB_SENSOR_IR_RIGHT  = 7, /**< Right IR */
        OB_SENSOR_RAW_PHASE = 8, /**< Raw Phase */
        OB_SENSOR_COUNT,
    }

    /**
    * \if English
    * @brief Enumeration value describing the type of data stream
    * \else
    * @brief 描述数据流类型的枚举值
    * \endif
    */
    public enum StreamType
    {
        OB_STREAM_UNKNOWN   = -1, /**< Unknown type stream */
        OB_STREAM_VIDEO     = 0,  /**< Video stream (infrared, color, depth streams are all video streams) */
        OB_STREAM_IR        = 1,  /**< IR stream */
        OB_STREAM_COLOR     = 2,  /**< color stream */
        OB_STREAM_DEPTH     = 3,  /**< depth stream */
        OB_STREAM_ACCEL     = 4,  /**< Accelerometer data stream */
        OB_STREAM_GYRO      = 5,  /**< Gyroscope data stream */
        OB_STREAM_IR_LEFT   = 6,  /**< Left IR stream */
        OB_STREAM_IR_RIGHT  = 7,  /**< Right IR stream */
        OB_STREAM_RAW_PHASE = 8,  /**< RawPhase Stream */
    }

    /**
     * \if English
     * @brief Describe the Frame type enumeration value
     * \else
     * @brief 描述Frame类型枚举值
     * \endif
     */
    public enum FrameType
    {
        OB_FRAME_UNKNOWN   = -1, /**< Unknown frame type */
        OB_FRAME_VIDEO     = 0,  /**< Video frame */
        OB_FRAME_IR        = 1,  /**< IR frame */
        OB_FRAME_COLOR     = 2,  /**< Color frame */
        OB_FRAME_DEPTH     = 3,  /**< Depth frame */
        OB_FRAME_ACCEL     = 4,  /**< Accelerometer data frame */
        OB_FRAME_SET       = 5,  /**< Frame collection (internally contains a variety of data frames) */
        OB_FRAME_POINTS    = 6,  /**< Point cloud frame */
        OB_FRAME_GYRO      = 7,  /**< Gyroscope data frame */
        OB_FRAME_IR_LEFT   = 8,  /**< Left IR frame */
        OB_FRAME_IR_RIGHT  = 9,  /**< Right IR frame */
        OB_FRAME_RAW_PHASE = 10, /**< Rawphase frame*/
    }

    /**
    * \if English
    * @brief Enumeration value describing the pixel format
    * \else
    * @brief 描述像素格式的枚举值
    * \endif
    */
    public enum Format
    {
        OB_FORMAT_YUYV       = 0,    /**< YUYV format */
        OB_FORMAT_YUY2       = 1,    /**< YUY2 format (the actual format is the same as YUYV) */
        OB_FORMAT_UYVY       = 2,    /**< UYVY format */
        OB_FORMAT_NV12       = 3,    /**< NV12 format */
        OB_FORMAT_NV21       = 4,    /**< NV21 format */
        OB_FORMAT_MJPG       = 5,    /**< MJPEG encoding format */
        OB_FORMAT_H264       = 6,    /**< H.264 encoding format */
        OB_FORMAT_H265       = 7,    /**< H.265 encoding format */
        OB_FORMAT_Y16        = 8,    /**< Y16 format, 16-bit per pixel, single-channel*/
        OB_FORMAT_Y8         = 9,    /**< Y8 format, 8-bit per pixel, single-channel */
        OB_FORMAT_Y10        = 10,   /**< Y10 format, 10-bit per pixel, single-channel(SDK will unpack into Y16 by default) */
        OB_FORMAT_Y11        = 11,   /**< Y11 format, 11-bit per pixel, single-channel (SDK will unpack into Y16 by default) */
        OB_FORMAT_Y12        = 12,   /**< Y12 format, 12-bit per pixel, single-channel(SDK will unpack into Y16 by default) */
        OB_FORMAT_GRAY       = 13,   /**< GRAY (the actual format is the same as YUYV) */
        OB_FORMAT_HEVC       = 14,   /**< HEVC encoding format (the actual format is the same as H265) */
        OB_FORMAT_I420       = 15,   /**< I420 format */
        OB_FORMAT_ACCEL      = 16,   /**< Acceleration data format */
        OB_FORMAT_GYRO       = 17,   /**< Gyroscope data format */
        OB_FORMAT_POINT      = 19,   /**< XYZ 3D coordinate point format */
        OB_FORMAT_RGB_POINT  = 20,   /**< XYZ 3D coordinate point format with RGB information */
        OB_FORMAT_RLE        = 21,   /**< RLE pressure test format (SDK will be unpacked into Y16 by default) */
        OB_FORMAT_RGB        = 22,   /**< RGB format (actual RGB888)  */
        OB_FORMAT_BGR        = 23,   /**< BGR format (actual BGR888) */
        OB_FORMAT_Y14        = 24,   /**< Y14 format, 14-bit per pixel, single-channel (SDK will unpack into Y16 by default) */
        OB_FORMAT_BGRA       = 25,   /**< BGRA format */
        OB_FORMAT_COMPRESSED = 26,   /**< Compression format */
        OB_FORMAT_RVL        = 27,   /**< RVL pressure test format (SDK will be unpacked into Y16 by default) */
        OB_FORMAT_Z16        = 28,   /**< Is same as Y16*/
        OB_FORMAT_YV12       = 29,   /**< Is same as Y12, using for right ir stream*/
        OB_FORMAT_BA81       = 30,   /**< Is same as Y8, using for right ir stream*/
        OB_FORMAT_RGBA       = 31,   /**< RGBA format */
        OB_FORMAT_BYR2       = 32,   /**< byr2 format */
        OB_FORMAT_RW16       = 33,   /**< RAW16 format */
        OB_FORMAT_DISP16     = 34,   /**< Y16 format for disparity map*/
        OB_FORMAT_UNKNOWN    = 0xff, /**< unknown format */
    }

    /**
    * \if English
    * @brief Firmware upgrade status
    * \else
    * @brief 固件升级状态
    * \endif
    */
    public enum UpgradeState
    {
        STAT_VERIFY_SUCCESS = 5,  /**< Image file verifify success */
        STAT_FILE_TRANSFER  = 4,  /**< file transfer */
        STAT_DONE           = 3,  /**< update completed */
        STAT_IN_PROGRESS    = 2,  /**< upgrade in process */
        STAT_START          = 1,  /**< start the upgrade */
        STAT_VERIFY_IMAGE   = 0,  /**< Image file verification */
        ERR_VERIFY          = -1, /**< Verification failed */
        ERR_PROGRAM         = -2, /**< Program execution failed */
        ERR_ERASE           = -3, /**< Flash parameter failed */
        ERR_FLASH_TYPE      = -4, /**< Flash type error */
        ERR_IMAGE_SIZE      = -5, /**< Image file size error */
        ERR_OTHER           = -6, /**< other errors */
        ERR_DDR             = -7, /**< DDR access error */
        ERR_TIMEOUT         = -8  /**< timeout error */
    }

    /**
    * \if English
    * @brief file transfer status
    * \else
    * @brief 文件传输状态
    * \endif
    */
    public enum FileTranState
    {
        FILE_TRAN_STAT_TRANSFER         = 2,  /**< File transfer */
        FILE_TRAN_STAT_DONE             = 1,  /**< File transfer succeeded */
        FILE_TRAN_STAT_PREPAR           = 0,  /**< Preparing */
        FILE_TRAN_ERR_DDR               = -1, /**< DDR access failed */
        FILE_TRAN_ERR_NOT_ENOUGH_SPACE  = -2, /**< Insufficient target space error */
        FILE_TRAN_ERR_PATH_NOT_WRITABLE = -3, /**< Destination path is not writable */
        FILE_TRAN_ERR_MD5_ERROR         = -4, /**< MD5 checksum error */
        FILE_TRAN_ERR_WRITE_FLASH_ERROR = -5, /**< Write flash error */
        FILE_TRAN_ERR_TIMEOUT           = -6  /**< Timeout error */
    }

    /**
    * \if English
    * @brief data transfer status
    * \else
    * @brief 数据传输状态
    * \endif
    */
    public enum DataTranState
    {
        DATA_TRAN_STAT_VERIFY_DONE  = 4,  /**< data verify done */
        DATA_TRAN_STAT_STOPPED      = 3,  /**< data transfer stoped */
        DATA_TRAN_STAT_DONE         = 2,  /**< data transfer completed */
        DATA_TRAN_STAT_VERIFYING    = 1,  /**< data verifying */
        DATA_TRAN_STAT_TRANSFERRING = 0,  /**< data transferring */
        DATA_TRAN_ERR_BUSY          = -1, /**< Transmission is busy */
        DATA_TRAN_ERR_UNSUPPORTED   = -2, /**< Not supported */
        DATA_TRAN_ERR_TRAN_FAILED   = -3, /**< Transfer failed */
        DATA_TRAN_ERR_VERIFY_FAILED = -4, /**< Test failed */
        DATA_TRAN_ERR_OTHER         = -5  /**< Other errors */
    }

    /**
    * \if English
    * @brief Data block structure for data block transmission
    * \else
    * @brief 数据块结构体，用于数据分块传输
    * \endif
    */
    public struct DataChunk
    {
        public IntPtr data;    ///< \if English current block data pointer \else 当前块数据指针 \endif
        public UInt32 size;    ///< \if English Current block data length \else 当前块数据长度 \endif
        public UInt32 offset;  ///< \if English The offset of the current data block relative to the complete data \else 当前数据块相对完整数据的偏移 \endif
        public UInt32 fullDataSize;  ///< \if English full data size \else 完整数据大小 \endif
    }

    /**
    * \if English
    * @brief Int range structure
    * \else
    * @brief 整形范围的结构体
    * \endif
    */
    public struct IntPropertyRange
    {
        public Int32 cur;   ///< \if English current value \else 当前值 \endif
        public Int32 max;   ///< \if English maximum value \else 最大值 \endif
        public Int32 min;   ///< \if English minimum value \else 最小值 \endif
        public Int32 step;  ///< \if English step value \else 步进值 \endif
        public Int32 def;   ///< \if English Default \else 默认值 \endif
    }

    /**
    * \if English
    * @brief Float range structure
    * \else
    * @brief 浮点型范围的结构体
    * \endif
    */
    public struct FloatPropertyRange
    {
        public float cur;   ///< \if English current value \else 当前值 \endif
        public float max;   ///< \if English maximum value \else 最大值 \endif
        public float min;   ///< \if English minimum value \else 最小值 \endif
        public float step;  ///< \if English step value \else 步进值 \endif
        public float def;   ///< \if English default \else 默认值 \endif
    }

    /**
    * @brief Structure for float range
    */
    public struct UInt16PropertyRange 
    {
        public UInt16 cur;   ///< Current value
        public UInt16 max;   ///< Maximum value
        public UInt16 min;   ///< Minimum value
        public UInt16 step;  ///< Step value
        public UInt16 def;   ///< Default value
    } 

    /**
    * @brief Structure for float range
    */
    public struct UInt8PropertyRange 
    {
        public byte cur;   ///< Current value
        public byte max;   ///< Maximum value
        public byte min;   ///< Minimum value
        public byte step;  ///< Step value
        public byte def;   ///< Default value
    } 

    /**
     * \if English
     * @brief Boolean-scoped structure
     * \else
     * @brief 布尔型范围的结构体
     * \endif
     */
    public struct BoolPropertyRange
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool cur;    ///< \if English current value \else 当前值 \endif
        [MarshalAs(UnmanagedType.I1)]
        public bool max;    ///< \if English maximum value \else 最大值 \endif
        [MarshalAs(UnmanagedType.I1)]
        public bool min;    ///< \if English minimum value \else 最小值 \endif
        [MarshalAs(UnmanagedType.I1)]
        public bool step;   ///< \if English step value \else 步进值 \endif
        [MarshalAs(UnmanagedType.I1)]
        public bool def;    ///< \if English default \else 默认值 \endif
    }

    /**
     * \if English
     * @brief Camera intrinsic parameters
     * \else
     * @brief 相机内参
     * \endif
     */
    public struct CameraIntrinsic
    {
        public float fx;      ///< \if English focal length in x direction \else x方向焦距 \endif
        public float fy;      ///< \if English focal length in y direction \else y方向焦距 \endif
        public float cx;      ///< \if English Optical center abscissa \else 光心横坐标 \endif
        public float cy;      ///< \if English Optical center ordinate \else 光心纵坐标 \endif
        public Int16 width;   ///< \if English image width \else 图像宽度 \endif
        public Int16 height;  ///< \if English image height \else 图像高度 \endif
    }

    /**
    * @brief Structure for accelerometer intrinsic parameters
    */
    public struct AccelIntrinsic
    {
        public double noiseDensity;          ///< In-run bias instability
        public double randomWalk;            ///< random walk
        public double referenceTemp;         ///< reference temperature
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] bias;               ///< bias for x, y, z axis
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] gravity;            ///< gravity direction for x, y, z axis
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public double[] scaleMisalignment;  ///< scale factor and three-axis non-orthogonal error
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public double tempSlope;          ///< linear temperature drift coefficient
    } 

    /**
    * @brief Structure for gyroscope intrinsic parameters
    */
    public struct GyroIntrinsic{
        public double noiseDensity;          ///< In-run bias instability
        public double randomWalk;            ///< random walk
        public double referenceTemp;         ///< reference temperature
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] bias;               ///< bias for x, y, z axis
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public double[] scaleMisalignment;  ///< scale factor and three-axis non-orthogonal error
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public double[] tempSlope;          ///< linear temperature drift coefficient
    }

    /**
     * \if English
     * @brief Distortion Parameters
     * \else
     * @brief 畸变参数
     * \endif
     */
    public struct CameraDistortion
    {
        public float k1;    ///< \if English Radial distortion factor 1 \else 径向畸变系数1 \endif
        public float k2;    ///< \if English Radial distortion factor 2 \else 径向畸变系数2 \endif
        public float k3;    ///< \if English Radial distortion factor 3 \else 径向畸变系数3 \endif
        public float k4;    ///< \if English Radial distortion factor 4 \else 径向畸变系数4 \endif
        public float k5;    ///< \if English Radial distortion factor 5 \else 径向畸变系数5 \endif
        public float k6;    ///< \if English Radial distortion factor 6 \else 径向畸变系数6 \endif
        public float p1;    ///< \if English Tangential distortion factor 1 \else 切向畸变系数1 \endif
        public float p2;    ///< \if English Tangential distortion factor 2 \else 切向畸变系数2 \endif
    }

    /** \brief Distortion model: defines how pixel coordinates should be mapped to sensor coordinates. */
    public enum CameraDistortionModel{
        OB_DISTORTION_NONE,                   /**< Rectilinear images. No distortion compensation required. */
        OB_DISTORTION_MODIFIED_BROWN_CONRADY, /**< Equivalent to Brown-Conrady distortion, except that tangential distortion is applied to radially distorted points
                                            */
        OB_DISTORTION_INVERSE_BROWN_CONRADY,  /**< Equivalent to Brown-Conrady distortion, except undistorts image instead of distorting it */
        OB_DISTORTION_BROWN_CONRADY,          /**< Unmodified Brown-Conrady distortion model */
    } 

    /** \brief Video stream intrinsics. */
    public struct CameraAlignIntrinsic{
        public int                     width;  /**< Width of the image in pixels */
        public int                     height; /**< Height of the image in pixels */
        public float                   ppx;    /**< Horizontal coordinate of the principal point of the image, as a pixel offset from the left edge */
        public float                   ppy;    /**< Vertical coordinate of the principal point of the image, as a pixel offset from the top edge */
        public float                   fx;     /**< Focal length of the image plane, as a multiple of pixel width */
        public float                   fy;     /**< Focal length of the image plane, as a multiple of pixel height */
        public CameraDistortionModel model;  /**< Distortion model of the image */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public float[] coeffs; /**< Distortion coefficients. Order for Brown-Conrady: [k1, k2, p1, p2, k3]. Order for F-Theta Fish-eye: [k1, k2, k3, k4, 0]. Other models
                            are subject to their own interpretations */
    }


    /**
     * \if English
     * @brief Rotation/Transformation
     * \else
     * @brief 旋转/变换矩阵
     * \endif
     */
    public struct D2CTransform
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public float[] rot;  ///< \if English Rotation matrix \else 旋转矩阵，行优先\endif
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] trans;   ///< \if English transformation matrix \else 变化矩阵 \endif
    }

    public struct Extrinsic
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public float[] rot;  ///< \if English Rotation matrix \else 旋转矩阵，行优先\endif
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] trans;   ///< \if English transformation matrix \else 变化矩阵 \endif
    }

    /**
     * \if English
     * @brief Camera parameters
     * \else
     * @brief 相机参数
     * \endif
     */
    public struct CameraParam
    {
        public CameraIntrinsic depthIntrinsic;   ///< \if English Depth camera internal parameters \else 深度相机内参 \endif    
        public CameraIntrinsic rgbIntrinsic;     ///< \if English Color camera internal parameters \else 彩色相机内参 \endif    
        public CameraDistortion depthDistortion;  ///< \if English Depth camera distortion parameters \else 深度相机畸变参数 \endif   
        public CameraDistortion rgbDistortion;    ///< \if English Color camera distortion parameters 1 \else 彩色相机畸变参数 \endif   
        public D2CTransform transform;        ///< \if English rotation/transformation matrix \else 旋转/变换矩阵 \endif   
        public bool isMirrored;       ///< \if English Whether the image frame corresponding to this group of parameters is mirrored \else 本组参数对应的图像帧是否被镜像 \endif   
    }

    /**
    * @brief Camera parameters
    */
    public struct OBCameraParam_V0
    {
        public CameraIntrinsic  depthIntrinsic;   ///< Depth camera internal parameters
        public CameraIntrinsic  rgbIntrinsic;     ///< Color camera internal parameters
        public CameraDistortion depthDistortion;  ///< Depth camera distortion parameters

        public CameraDistortion rgbDistortion;  ///< Distortion parameters for color camera
        public D2CTransform     transform;      ///< Rotation/transformation matrix
    }

    /**
    * @brief calibration parameters
    */
    public struct CalibrationParam 
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public CameraIntrinsic[]  intrinsics;            ///< Sensor internal parameters
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public CameraDistortion[] distortion;            ///< Sensor distortion
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 81)]
        public Extrinsic[] extrinsics;  ///< The extrinsic parameters allow 3D coordinate conversions between sensor.To transform from a
                                                                ///< source to a target 3D coordinate system,under extrinsics[source][target].
    }

    /**
    * @brief Configuration for depth margin filter
    */
    public struct OBMarginFilterConfig
    {
        public int      margin_x_th;       ///< Horizontal threshold settings
        public int      margin_y_th;       ///< Vertical threshold settings
        public int      limit_x_th;        ///< Maximum horizontal threshold
        public int      limit_y_th;        ///< Maximum vertical threshold
        public UInt32 width;             ///< Image width
        public UInt32 height;            ///< Image height
        public bool     enable_direction;  ///< Set to true for horizontal and vertical, false for horizontal only
    }

    /**
    * @brief Configuration for mgc filter
    */
    public struct MGCFilterConfig
    {
        public UInt32 width;
        public UInt32 height;
        public int      max_width_left;
        public int      max_width_right;
        public int      max_radius;
        public int      margin_x_th;
        public int      margin_y_th;
        public int      limit_x_th;
        public int      limit_y_th;
    }

    /**
     * \if English
     * @brief alignment mode
     * \else
     * @brief 对齐模式
     * \endif
     */
    public enum AlignMode
    {
        ALIGN_DISABLE = 0,     /**< \if English turn off alignment \else 关闭对齐 \endif */
        ALIGN_D2C_HW_MODE = 1, /**< \if English Hardware D2C alignment mode \else 硬件D2C对齐模式 \endif */
        ALIGN_D2C_SW_MODE = 2, /**< \if English Software D2C alignment mode \else 软件D2C对齐模式 \endif */
    }

    /**
     * \if English
     * @brief rectangle
     * \else
     * @brief 矩形
     * \endif
     */
    public struct Rect
    {
        public UInt32 x;       ///< \if English origin coordinate x \else 原点坐标x \endif
        public UInt32 y;       ///< \if English origin coordinate y \else 原点坐标y \endif
        public UInt32 width;   ///< \if English rectangle width \else 矩形宽度 \endif
        public UInt32 height;  ///< \if English rectangle height \else 矩形高度 \endif
    }

    /**
     * \if English
     * @brief format conversion type enumeration
     * \else
     * @brief 格式转换类型枚举
     * \endif
     */
    public enum ConvertFormat
    {
        FORMAT_YUYV_TO_RGB = 0, /**< YUYV to RGB */
        FORMAT_I420_TO_RGB,     /**< I420 to RGB */
        FORMAT_NV21_TO_RGB,     /**< NV21 to RGB */
        FORMAT_NV12_TO_RGB,     /**< NV12 to RGB */
        FORMAT_MJPG_TO_I420,    /**< MJPG to I420 */
        FORMAT_RGB_TO_BGR,      /**< RGB888 to BGR */
        FORMAT_MJPG_TO_NV21,    /**< MJPG to NV21 */
        FORMAT_MJPG_TO_RGB,     /**< MJPG to RGB */
        FORMAT_MJPG_TO_BGR,     /**< MJPG to BGR */
        FORMAT_MJPG_TO_BGRA,    /**< MJPG to BGRA */
        FORMAT_UYVY_TO_RGB,     /**< UYVY to RGB */
        FORMAT_BGR_TO_RGB,      /**< BGR to RGB */
        FORMAT_MJPG_TO_NV12,    /**< MJPG to NV12 */
        FORMAT_YUYV_TO_BGR,     /**< YUYV to BGR */
        FORMAT_YUYV_TO_RGBA,    /**< YUYV to RGBA */
        FORMAT_YUYV_TO_BGRA,    /**< YUYV to BGRA */
        FORMAT_YUYV_TO_Y16,     /**< YUYV to Y16 */
        FORMAT_YUYV_TO_Y8,      /**< YUYV to Y8 */
    }

    /**
    * @brief Enumeration of gyroscope sample rate values
    */
    public enum GyroSampleRate
    {
        OB_SAMPLE_RATE_UNKNOWN   = 0, /**< Unknown sample rate */
        OB_SAMPLE_RATE_1_5625_HZ = 1, /**< 1.5625Hz */
        OB_SAMPLE_RATE_3_125_HZ,      /**< 3.125Hz */
        OB_SAMPLE_RATE_6_25_HZ,       /**< 6.25Hz */
        OB_SAMPLE_RATE_12_5_HZ,       /**< 12.5Hz */
        OB_SAMPLE_RATE_25_HZ,         /**< 25Hz */
        OB_SAMPLE_RATE_50_HZ,         /**< 50Hz */
        OB_SAMPLE_RATE_100_HZ,        /**< 100Hz */
        OB_SAMPLE_RATE_200_HZ,        /**< 200Hz */
        OB_SAMPLE_RATE_500_HZ,        /**< 500Hz */
        OB_SAMPLE_RATE_1_KHZ,         /**< 1KHz */
        OB_SAMPLE_RATE_2_KHZ,         /**< 2KHz */
        OB_SAMPLE_RATE_4_KHZ,         /**< 4KHz */
        OB_SAMPLE_RATE_8_KHZ,         /**< 8KHz */
        OB_SAMPLE_RATE_16_KHZ,        /**< 16KHz */
        OB_SAMPLE_RATE_32_KHZ,        /**< 32Hz */
    }

    public enum AccelSampleRate
    {
        OB_SAMPLE_RATE_UNKNOWN   = 0, /**< Unknown sample rate */
        OB_SAMPLE_RATE_1_5625_HZ = 1, /**< 1.5625Hz */
        OB_SAMPLE_RATE_3_125_HZ,      /**< 3.125Hz */
        OB_SAMPLE_RATE_6_25_HZ,       /**< 6.25Hz */
        OB_SAMPLE_RATE_12_5_HZ,       /**< 12.5Hz */
        OB_SAMPLE_RATE_25_HZ,         /**< 25Hz */
        OB_SAMPLE_RATE_50_HZ,         /**< 50Hz */
        OB_SAMPLE_RATE_100_HZ,        /**< 100Hz */
        OB_SAMPLE_RATE_200_HZ,        /**< 200Hz */
        OB_SAMPLE_RATE_500_HZ,        /**< 500Hz */
        OB_SAMPLE_RATE_1_KHZ,         /**< 1KHz */
        OB_SAMPLE_RATE_2_KHZ,         /**< 2KHz */
        OB_SAMPLE_RATE_4_KHZ,         /**< 4KHz */
        OB_SAMPLE_RATE_8_KHZ,         /**< 8KHz */
        OB_SAMPLE_RATE_16_KHZ,        /**< 16KHz */
        OB_SAMPLE_RATE_32_KHZ,        /**< 32Hz */
    }

    /**
     * \if English
     * @brief Enumeration of gyroscope ranges
     * \else
     * @brief 陀螺仪量程的枚举
     * \endif
     */
    public enum GyroFullScaleRange
    {
        OB_GYRO_FS_UNKNOWN = 0, /**< Unknown range */
        OB_GYRO_FS_16dps   = 1, /**< 16 degrees per second */
        OB_GYRO_FS_31dps,       /**< 31 degrees per second */
        OB_GYRO_FS_62dps,       /**< 62 degrees per second */
        OB_GYRO_FS_125dps,      /**< 125 degrees per second */
        OB_GYRO_FS_250dps,      /**< 250 degrees per second */
        OB_GYRO_FS_500dps,      /**< 500 degrees per second */
        OB_GYRO_FS_1000dps,     /**< 1000 degrees per second */
        OB_GYRO_FS_2000dps,     /**< 2000 degrees per second */
    }

    /**
     * \if English
     * @brief Accelerometer range enumeration
     * \else
     * @brief 加速度计量程枚举
     * \endif
     */
    public enum AccelFullScaleRange
    {
        OB_ACCEL_FS_UNKNOWN = 0, /**< Unknown range */
        OB_ACCEL_FS_2g      = 1, /**< 1x the acceleration of gravity */
        OB_ACCEL_FS_4g,          /**< 4x the acceleration of gravity */
        OB_ACCEL_FS_8g,          /**< 8x the acceleration of gravity */
        OB_ACCEL_FS_16g,         /**< 16x the acceleration of gravity */
    }

    /**
     * @brief Data structures for accelerometers
     */
    public struct AccelValue
    {
        public float x;  ///< X-direction component
        public float y;  ///< Y-direction component
        public float z;  ///< Z-direction component
    }

    /**
     * @brief Data structures for gyroscope
     */
    public struct GyroValue
    {
        public float x;  ///< X-direction component
        public float y;  ///< Y-direction component
        public float z;  ///< Z-direction component
    }

    /**
     * \if English
     * @brief Get the temperature parameters of the device (unit: Celsius)
     * \else
     * @brief 获取设备的温度参数（单位：摄氏度）
     * \endif
     */
    public struct DeviceTemperature
    {
        public float cpuTemp;         ///< CPU temperature
        public float irTemp;          ///< IR temperature
        public float ldmTemp;         ///< Laser temperature
        public float mainBoardTemp;   ///< Motherboard temperature
        public float tecTemp;         ///< TEC temperature
        public float imuTemp;         ///< IMU temperature
        public float rgbTemp;         ///< RGB temperature
        public float irLeftTemp;      ///< Left IR temperature
        public float irRightTemp;     ///< Right IR temperature
        public float chipTopTemp;     ///< MX6600 top temperature
        public float chipBottomTemp;  ///< MX6600 bottom temperature
    }

    /**
     * \if English
     * @brief Depth crop mode enumeration
     * \else
     * @brief 深度裁切模式枚举
     * \endif
     */
    public enum DepthCroppingMode
    {
        DEPTH_CROPPING_MODE_AUTO  = 0, /**< Automatic mode */
        DEPTH_CROPPING_MODE_CLOSE = 1, /**< Close crop */
        DEPTH_CROPPING_MODE_OPEN  = 2, /**< Open crop */
    }

    /**
     * \if English
     * @brief device type enumeration
     * \else
     * @brief 设备类型枚举
     * \endif
     */
    public enum DeviceType
    {
        OB_STRUCTURED_LIGHT_MONOCULAR_CAMERA = 0, /**< Monocular structured light camera */
        OB_STRUCTURED_LIGHT_BINOCULAR_CAMERA = 1, /**< Binocular structured light camera */
        OB_TOF_CAMERA                        = 2, /**< Time-of-flight camera */
    }

    /**
     * \if English
     * @brief record playback of the type of interest
     * \else
     * @brief 录制回放感兴趣数据类型
     * \endif
     */
    public enum MediaType
    {
        OB_MEDIA_COLOR_STREAM    = 1,   /**< Color stream */
        OB_MEDIA_DEPTH_STREAM    = 2,   /**< Depth stream */
        OB_MEDIA_IR_STREAM       = 4,   /**< Infrared stream */
        OB_MEDIA_GYRO_STREAM     = 8,   /**< Gyroscope stream */
        OB_MEDIA_ACCEL_STREAM    = 16,  /**< Accelerometer stream */
        OB_MEDIA_CAMERA_PARAM    = 32,  /**< Camera parameter */
        OB_MEDIA_DEVICE_INFO     = 64,  /**< Device information */
        OB_MEDIA_STREAM_INFO     = 128, /**< Stream information */
        OB_MEDIA_IR_LEFT_STREAM  = 256, /**< Left infrared stream */
        OB_MEDIA_IR_RIGHT_STREAM = 512, /**< Right infrared stream */

        OB_MEDIA_ALL = OB_MEDIA_COLOR_STREAM | OB_MEDIA_DEPTH_STREAM | OB_MEDIA_IR_STREAM | OB_MEDIA_GYRO_STREAM | OB_MEDIA_ACCEL_STREAM | OB_MEDIA_CAMERA_PARAM
                    | OB_MEDIA_DEVICE_INFO | OB_MEDIA_STREAM_INFO | OB_MEDIA_IR_LEFT_STREAM | OB_MEDIA_IR_RIGHT_STREAM, /**< All media data types */
    }

    /**
     * \if English
     * @brief Record playback status
     * \else
     * @brief 录制回放状态
     * \endif
     */
    public enum MediaState
    {
        OB_MEDIA_BEGIN = 0, /**< Begin */
        OB_MEDIA_PAUSE,     /**< Pause */
        OB_MEDIA_RESUME,    /**< Resume */
        OB_MEDIA_END,       /**< End */
    }

    /**
     * \if English
     * @brief depth accuracy class
     * @attention The depth accuracy level does not completely determine the depth unit and real accuracy, and the influence of the data packaging format needs to
     * be considered. The specific unit can be obtained through getValueScale() of DepthFrame \else
     * @brief 深度精度等级
     * @attention 深度精度等级并不完全决定深度的单位和真实精度，需要考虑数据打包格式的影响，
     * 具体单位可通过DepthFrame的getValueScale()获取
     * \endif
     */
    public enum DepthPrecisionLevel
    {
        OB_PRECISION_1MM,   /**< 1mm */
        OB_PRECISION_0MM8,  /**< 0.8mm */
        OB_PRECISION_0MM4,  /**< 0.4mm */
        OB_PRECISION_0MM1,  /**< 0.1mm */
        OB_PRECISION_0MM2,  /**< 0.2mm */
        OB_PRECISION_0MM5,  /**< 0.5mm */
        OB_PRECISION_0MM05, /**< 0.05mm */
        OB_PRECISION_UNKNOWN,
        OB_PRECISION_COUNT,
    }

    /**
    * \if English
    * @brief tof filter scene range
    * \else
    * @brief tof滤波场景范围
    * \endif
    */
    public enum TofFilterRange
    {
        OB_TOF_FILTER_RANGE_CLOSE  = 0,   /**< Close range */
        OB_TOF_FILTER_RANGE_MIDDLE = 1,   /**< Middle range */
        OB_TOF_FILTER_RANGE_LONG   = 2,   /**< Long range */
        OB_TOF_FILTER_RANGE_DEBUG  = 100, /**< Debug range */
    }

    /**
     * \if English
     * @brief 3D point structure in SDK
     * \else
     * @brief SDK中3D点结构体
     * \endif
     */
    public struct Point
    {
        public float x;  ///< X coordinate
        public float y;  ///< Y coordinate
        public float z;  ///< Z coordinate   
    }

    public struct Point3f
    {
        public float x;  ///< X coordinate
        public float y;  ///< Y coordinate
        public float z;  ///< Z coordinate   
    }

    /**
    * @brief 2D point structure in the SDK
    */
    public struct Point2f
    {
        public float x;  ///< X coordinate
        public float y;  ///< Y coordinate
    } 

    public struct XYTables
    {
        public IntPtr xTable;  ///< table used to compute X coordinate
        public IntPtr yTable;  ///< table used to compute Y coordinate
        public int    width;   ///< width of x and y tables
        public int    height;  ///< height of x and y tables
    } 

    /**
     * \if English
     * @brief 3D point structure with color information
     * \else
     * @brief 带有颜色信息的3D点结构体
     * \endif
     */
    public struct ColorPoint
    {
        public float x;  ///< X coordinate
        public float y;  ///< Y coordinate
        public float z;  ///< Z coordinate
        public float r;  ///< Red channel component
        public float g;  ///< Green channel component
        public float b;  ///< Blue channel component
    }

    public enum CompressionMode
    {
        OB_COMPRESSION_LOSSLESS = 0, /**< Lossless compression mode */
        OB_COMPRESSION_LOSSY    = 1, /**< Lossy compression mode */
    }

    
    
    public struct CompressionParams
    {
        /**
        * Lossy compression threshold, range [0~255], recommended value is 9, the higher the threshold, the higher the compression ratio.
        */
        public int threshold;
    }

    /**
    * \if English
    * @brief TOF Exposure Threshold
    * \else
    * @brief TOF 曝光阈值
    *\endif
    */
    
    
    public struct TofExposureThresholdControl
    {
        public Int32 upper;  ///< \if English Upper threshold, unit: ms \else 阈值上限， 单位：ms \endif
        public Int32 lower;  ///< \if English Lower threshold, unit: ms \else 阈值下限， 单位：ms \endif
    }

    /**
     * \if English
     * @brief Multi-device sync mode
     * \else
     * @brief 多设备同步模式
     * \endif
     */
    public enum SyncMode
    {
        /**
        * @brief Close synchronize mode
        * @brief Single device, neither process input trigger signal nor output trigger signal
        * @brief Each Sensor in a single device automatically triggers
        */
        OB_SYNC_MODE_CLOSE = 0x00,

        /**
        * @brief Standalone synchronize mode
        * @brief Single device, neither process input trigger signal nor output trigger signal
        * @brief Inside single device, RGB as Major sensor: RGB -> IR/Depth/TOF
        */
        OB_SYNC_MODE_STANDALONE = 0x01,

        /**
        * @brief Primary synchronize mode
        * @brief Primary device. Ignore process input trigger signal, only output trigger signal to secondary devices.
        * @brief Inside single device, RGB as Major sensor: RGB -> IR/Depth/TOF
        */
        OB_SYNC_MODE_PRIMARY = 0x02,

        /**
        * @brief Secondary synchronize mode
        * @brief Secondary device. Both process input trigger signal and output trigger signal to other devices.
        * @brief Different sensors in a single devices receive trigger signals respectively：ext trigger -> RGB && ext trigger -> IR/Depth/TOF
        *
        * @attention With the current Gemini 2 device set to this mode, each Sensor receives the first external trigger signal
        *     after the stream is turned on and starts timing self-triggering at the set frame rate until the stream is turned off
        */
        OB_SYNC_MODE_SECONDARY = 0x03,

        /**
        * @brief MCU Primary synchronize mode
        * @brief Primary device. Ignore process input trigger signal, only output trigger signal to secondary devices.
        * @brief Inside device, MCU is the primary signal source:  MCU -> RGB && MCU -> IR/Depth/TOF
        */
        OB_SYNC_MODE_PRIMARY_MCU_TRIGGER = 0x04,

        /**
        * @brief IR Primary synchronize mode
        * @brief Primary device. Ignore process input trigger signal, only output trigger signal to secondary devices.
        * @brief Inside device, IR is the primary signal source: IR/Depth/TOF -> RGB
        */
        OB_SYNC_MODE_PRIMARY_IR_TRIGGER = 0x05,

        /**
        * @brief Software trigger synchronize mode
        * @brief Host, triggered by software control (receive the upper computer command trigger), at the same time to the trunk output trigger signal
        * @brief Different sensors in a single machine receive trigger signals respectively: soft trigger -> RGB && soft trigger -> IR/Depth/TOF
        *
        * @attention Support product: Gemini2
        */
        OB_SYNC_MODE_PRIMARY_SOFT_TRIGGER = 0x06,

        /**
        * @brief Software trigger synchronize mode as secondary device
        * @brief The slave receives the external trigger signal (the external trigger signal comes from the soft trigger host) and outputs the trigger signal to
        * the external relay.
        * @brief Different sensors in a single machine receive trigger signals respectively：ext trigger -> RGB && ext  trigger -> IR/Depth/TOF
        */
        OB_SYNC_MODE_SECONDARY_SOFT_TRIGGER = 0x07,

        /**
        * @brief Unknown type
        */
        OB_SYNC_MODE_UNKNOWN = 0xff,
    }

    /**
    * \if English
    * @brief Device synchronization configuration
    * \else
    * @brief 设备同步配置
    *
    * @brief 单机内不同 Sensor 的同步 及 多机间同步 配置
    * \endif
    */
    
    
    public struct DeviceSyncConfig
    {
        /**
        * \if English
        * \else
        * @brief 同步模式
        * \endif
        *
        */
        public SyncMode syncMode;

        /**
        * \if English
        * @brief IR Trigger signal input delay: Used to configure the delay between the IR/Depth/TOF Sensor receiving the trigger signal and starting exposure,
        * Unit: microsecond
        *
        * @attention This parameter is invalid when the synchronization MODE is set to @ref OB SYNC MODE HOST IR TRIGGER
        * \else
        * @brief IR 触发信号输入延时，用于 IR/Depth/TOF Sensor 接收到触发信号后到开始曝光的延时配置，单位为微秒
        *
        * @attention 同步模式配置为  @ref OB_SYNC_MODE_HOST_IR_TRIGGER 时无效
        * \endif
        */
        public UInt16 irTriggerSignalInDelay;

        /**
        * \if English
        * @brief RGB trigger signal input delay is used to configure the delay from the time when an RGB Sensor receives the trigger signal to the time when the
        * exposure starts. Unit: microsecond
        *
        * @attention This parameter is invalid when the synchronization MODE is set to @ref OB SYNC MODE HOST
        * \else
        * @brief RGB 触发信号输入延时，用于 RGB Sensor 接收到触发信号后到开始曝光的延时配置，单位为微秒
        *
        * @attention 同步模式配置为  @ref OB_SYNC_MODE_HOST 时无效
        * \endif
        */
        public UInt16 rgbTriggerSignalInDelay;

        /**
        * \if English
        * @brief Device trigger signal output delay, used to control the delay configuration of the host device to output trigger signals or the slave device to
        * output trigger signals. Unit: microsecond
        *
        * @attention This parameter is invalid when the synchronization MODE is set to @ref OB SYNC MODE CLOSE or @ref OB SYNC Mode SINGLE
        * \else
        * @brief 设备触发信号输出延时，用于控制主机设备向外输 或 从机设备向外中继输出 触发信号的延时配置，单位：微秒
        *
        * @attention 同步模式配置为 @ref OB_SYNC_MODE_CLOSE 和  @ref OB_SYNC_MODE_SINGLE 时无效
        * \endif
        */
        public UInt16 deviceTriggerSignalOutDelay;

        /**
        * \if English
        * @brief The device trigger signal output polarity is used to control the polarity configuration of the trigger signal output from the host device or the
        * trigger signal output from the slave device
        * @brief 0: forward pulse; 1: negative pulse
        *
        * @attention This parameter is invalid when the synchronization MODE is set to @ref OB SYNC MODE CLOSE or @ref OB SYNC Mode SINGLE
        * \else
        * @brief 设备触发信号输出极性，用于控制主机设备向外输 或 从机设备向外中继输出 触发信号的极性配置
        * @brief 0: 正向脉冲；1: 负向脉冲
        *
        * @attention 同步模式配置为 @ref OB_SYNC_MODE_CLOSE 和  @ref OB_SYNC_MODE_SINGLE 时无效
        * \endif
        */
        public UInt16 deviceTriggerSignalOutPolarity;

        /**
        * \if English
        * @brief MCU trigger frequency, used to configure the output frequency of MCU trigger signal in MCU master mode, unit: Hz
        * @brief This configuration will directly affect the image output frame rate of the Sensor. Unit: FPS （frame pre second）
        *
        * @attention This parameter is invalid only when the synchronization MODE is set to @ref OB SYNC MODE HOST MCU TRIGGER
        * \else
        * @brief MCU 触发频率，用于 MCU 主模式下，MCU触发信号输出频率配置，单位：Hz
        * @brief 该配置会直接影响 Sensor 的图像输出帧率，即也可以认为单位为：FPS （frame pre second）
        *
        * @attention 仅当同步模式配置为 @ref OB_SYNC_MODE_HOST_MCU_TRIGGER 时无效
        * \endif
        */
        public UInt16 mcuTriggerFrequency;

        /**
        * \if English
        * @brief Device number. Users can mark the device with this number
        * \else
        * @brief 设备编号，用户可用该编号对设备进行标记
        * \endif
        */
        public UInt16 deviceId;
    }

    /**
    * \if English
    * @brief Depth work mode
    * \else
    * @brief 相机深度工作模式
    * \endif
    *
    */
    public struct DepthWorkMode {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] checksum;  ///< \if English Checksum of work mode \else 相机深度模式对应哈希二进制数组 \endif
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[]    name;      ///< \if English 名称 \else Name of work mode \endif
    }
    
    /**
    * @brief SequenceId fliter list item
    */
    public struct SequenceIdItem
    {
        public int  sequenceSelectId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] name;
    } 

    /**
    * @brief Hole fillig mode
    */
    public enum HoleFillingMode
    {
        OB_HOLE_FILL_TOP     = 0,
        OB_HOLE_FILL_NEAREST = 1,  // "max" means farest for depth, and nearest for disparity; FILL_NEAREST
        OB_HOLE_FILL_FAREST  = 2,  // FILL_FAREST
    }

    public struct SpatialAdvancedFilterParams
    {
        public byte magnitude;  // magnitude
        public float alpha;      // smooth_alpha
        public UInt16 disp_diff;  // smooth_delta
        public UInt16 radius;     // hole_fill
    } 

    public enum EdgeNoiseRemovalType {
        OB_MG_FILTER  = 0,
        OB_MGH_FILTER = 1,  // horizontal MG
        OB_MGA_FILTER = 2,  // asym MG
        OB_MGC_FILTER = 3,
    }

    public struct EdgeNoiseRemovalFilterParams
    {
        public EdgeNoiseRemovalType type;
        public UInt16               marginLeftTh;
        public UInt16               marginRightTh;
        public UInt16               marginTopTh;
        public UInt16               marginBottomTh;
    } 

    /**
    * @brief 去噪方式
    */
    public enum DDONoiseRemovalType 
    {
        OB_NR_LUT     = 0,  // SPLIT
        OB_NR_OVERALL = 1,  // NON_SPLIT
    } 

    public struct NoiseRemovalFilterParams
    {
        public UInt16              max_size;
        public UInt16              disp_diff;
        public DDONoiseRemovalType type;
    } 


    /**
    * @brief 控制命令协议版本号
    *
    */
    public struct ProtocolVersion {
        public byte major;  ///< 主版本号
        public byte minor;  ///< 次版本号
        public byte patch;  ///< 补丁版本
    }

    /**
    * \if English
    * Command version associate with property id
    * \else
    * 与属性ID关联的协议版本
    * \endif
    *
    */
    public enum CmdVersion : UInt16 {
        OB_CMD_VERSION_V0 = 0,  ///< \if English version 1.0 \else 版本1.0 \endif
        OB_CMD_VERSION_V1 = 1,  ///< \if English version 2.0 \else 版本2.0 \endif
        OB_CMD_VERSION_V2 = 2,  ///< \if English version 3.0 \else 版本3.0 \endif
        OB_CMD_VERSION_V3 = 3,  ///< \if English version 4.0 \else 版本4.0 \endif

        OB_CMD_VERSION_NOVERSION = 0xfffe,
        OB_CMD_VERSION_INVALID   = 0xffff,  ///< \if English Invalid version \else 无效版本 \endif
    }

    /**
    * @brief Internal API for future publication
    */
    public struct DataBundle {
        /**
        * @brief CmdVersion of propertyId
        */
        public CmdVersion cmdVersion;

        /**
        * @brief Data containing itemCount of elements
        */
        public IntPtr data;

        /**
        * @brief Data size in bytes
        */
        public UInt32 dataSize;

        /**
        * @brief Size of data item
        */
        public UInt32 itemTypeSize;

        /**
        * @brief Count of data item
        */
        public UInt32 itemCount;
    }

    /**
     * @brief 网络设备的IP地址配置（ipv4）
     *
     */
    public struct DeviceIpAddrConfig
    {
        public UInt16 dhcp;        ///< dhcp 动态ip配置开关; 0:关; 1: 开
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] address;  ///< ip地址(大端模式, 如192.168.1.1，则address[0]==192)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] mask;     ///< 子网掩码(大端模式)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] gateway;  ///< 网关(大端模式)
    }

    /**
     * @brief 设备通信模式
     *
     */
    public enum CommunicationType
    {
        OB_COMM_USB = 0x00,  ///< USB
        OB_COMM_NET = 0x01,  ///< Ethernet
    }

    /**
    * \if English
    * @brief USB power status
    * \else
    * @brief USB电源连接状态
    * \endif
    */
    public enum USBPowerState{
        OB_USB_POWER_NO_PLUGIN = 0,  ///< No plugin
        OB_USB_POWER_5V_0A9    = 1,  ///< 5V/0.9A
        OB_USB_POWER_5V_1A5    = 2,  ///< 5V/1.5A
        OB_USB_POWER_5V_3A0    = 3,  ///< 5V/3.0A
    }

    /**
    * \if English
    * @brief DC power status
    * \else
    * @brief DC电源连接状态
    * \endif
    */
    public enum DCPowerState {
        OB_DC_POWER_NO_PLUGIN = 0,  ///< No plugin
        OB_DC_POWER_PLUGIN    = 1,  ///< Plugin
    }

    /**
    * \if English
    * @brief Rotate degree
    * \else
    * @brief 旋转角度
    * \endif
    */
    public enum RotateDegreeType {
        OB_ROTATE_DEGREE_0   = 0,    ///< Rotate 0
        OB_ROTATE_DEGREE_90  = 90,   ///< Rotate 90
        OB_ROTATE_DEGREE_180 = 180,  ///< Rotate 180
        OB_ROTATE_DEGREE_270 = 270,  ///< Rotate 270
    }

    /**
    * \if English
    * @brief Power line frequency mode，for Color camera anti-flicker configuration
    * \else
    * @brief 电力线频率模式，用于Color相机防闪烁功能配置
    * \endif
    */
    public enum PowerLineFreqMode {
        OB_POWER_LINE_FREQ_MODE_CLOSE = 0,  ///< Close
        OB_POWER_LINE_FREQ_MODE_50HZ  = 1,  ///< 50Hz
        OB_POWER_LINE_FREQ_MODE_60HZ  = 2,  ///< 60Hz
    }

    /**
    * \if English
    * @brief Frame aggregate output mode
    * \else
    * @brief 帧汇聚输出模式
    * \endif
    */
    public enum FrameAggregateOutputMode {
        /**
        * @brief Only FrameSet that contains all types of data frames will be output
        */
        OB_FRAME_AGGREGATE_OUTPUT_FULL_FRAME_REQUIRE = 0,

        /**
        * @brief Color Frame Require output mode
        * @brief Suitable for Color using H264, H265 and other inter-frame encoding format open stream
        *
        * @attention In this mode, the user may return null when getting a non-Color type data frame from the acquired FrameSet
        */
        OB_FRAME_AGGREGATE_OUTPUT_COLOR_FRAME_REQUIRE,

        /**
        * @brief FrameSet for any case will be output
        *
        * @attention In this mode, the user may return null when getting the specified type of data frame from the acquired FrameSet
        */
        OB_FRAME_AGGREGATE_OUTPUT_ANY_SITUATION,
    } 

    /**
    * \if English
    * @brief Point cloud coordinate system type
    * \else
    * @brief 点云坐标系类型(左手坐标系,右手坐标系)
    * \endif
    */
    public enum CoordinateSystemType {
        OB_LEFT_HAND_COORDINATE_SYSTEM  = 0,
        OB_RIGHT_HAND_COORDINATE_SYSTEM = 1,
    } 

    /**
    * @brief Enumeration of device development modes
    */
    public enum DeviceDevelopmentMode {
        /**
        * @brief User mode (default mode), which provides full camera device functionality
        */
        OB_USER_MODE = 0,

        /**
        * @brief Developer mode, which allows developers to access the operating system and software/hardware resources on the device directly
        */
        OB_DEVELOPER_MODE = 1,
    }

    /**
    * @brief The synchronization mode of the device.
    */
    public enum MultiDeviceSyncMode{

        /**
        * @brief free run mode
        * @brief The device does not synchronize with other devices,
        * @brief The Color and Depth can be set to different frame rates.
        */
        OB_MULTI_DEVICE_SYNC_MODE_FREE_RUN = 1 << 0,

        /**
        * @brief standalone mode
        * @brief The device does not synchronize with other devices.
        * @brief The Color and Depth should be set to same frame rates, the Color and Depth will be synchronized.
        */
        OB_MULTI_DEVICE_SYNC_MODE_STANDALONE = 1 << 1,

        /**
        * @brief primary mode
        * @brief The device is the primary device in the multi-device system, it will output the trigger signal via VSYNC_OUT pin on synchronization port by
        * default.
        * @brief The Color and Depth should be set to same frame rates, the Color and Depth will be synchronized and can be adjusted by @ref colorDelayUs, @ref
        * depthDelayUs or @ref trigger2ImageDelayUs.
        */
        OB_MULTI_DEVICE_SYNC_MODE_PRIMARY = 1 << 2,

        /**
        * @brief secondary mode
        * @brief The device is the secondary device in the multi-device system, it will receive the trigger signal via VSYNC_IN pin on synchronization port. It
        * will out the trigger signal via VSYNC_OUT pin on synchronization port by default.
        * @brief The Color and Depth should be set to same frame rates, the Color and Depth will be synchronized and can be adjusted by @ref colorDelayUs, @ref
        * depthDelayUs or @ref trigger2ImageDelayUs.
        * @brief After starting the stream, the device will wait for the trigger signal to start capturing images, and will stop capturing images when the trigger
        * signal is stopped.
        *
        * @attention The frequency of the trigger signal should be same as the frame rate of the stream profile which is set when starting the stream.
        */
        OB_MULTI_DEVICE_SYNC_MODE_SECONDARY = 1 << 3,

        /**
        * @brief secondary synced mode
        * @brief The device is the secondary device in the multi-device system, it will receive the trigger signal via VSYNC_IN pin on synchronization port. It
        * will out the trigger signal via VSYNC_OUT pin on synchronization port by default.
        * @brief The Color and Depth should be set to same frame rates, the Color and Depth will be synchronized and can be adjusted by @ref colorDelayUs, @ref
        * depthDelayUs or @ref trigger2ImageDelayUs.
        * @brief After starting the stream, the device will be immediately start capturing images, and will adjust the capture time when the trigger signal is
        * received to synchronize with the primary device. If the trigger signal is stopped, the device will still capture images.
        *
        * @attention The frequency of the trigger signal should be same as the frame rate of the stream profile which is set when starting the stream.
        */
        OB_MULTI_DEVICE_SYNC_MODE_SECONDARY_SYNCED = 1 << 4,

        /**
        * @brief software triggering mode
        * @brief The device will start one time image capture after receiving the capture command and will output the trigger signal via VSYNC_OUT pin by default.
        * The capture command can be sent form host by call @ref ob_device_trigger_capture. The number of images captured each time can be set by @ref
        * framesPerTrigger.
        * @brief The Color and Depth should be set to same frame rates, the Color and Depth will be synchronized and can be adjusted by @ref colorDelayUs, @ref
        * depthDelayUs or @ref trigger2ImageDelayUs.
        *
        * @brief The frequency of the user call @ref ob_device_trigger_capture to send the capture command multiplied by the number of frames per trigger should be
        * less than the frame rate of the stream profile which is set when starting the stream.
        */
        OB_MULTI_DEVICE_SYNC_MODE_SOFTWARE_TRIGGERING = 1 << 5,

        /**
        * @brief hardware triggering mode
        * @brief The device will start one time image capture after receiving the trigger signal via VSYNC_IN pin on synchronization port and will output the
        * trigger signal via VSYNC_OUT pin by default. The number of images captured each time can be set by @ref framesPerTrigger.
        * @brief The Color and Depth should be set to same frame rates, the Color and Depth will be synchronized and can be adjusted by @ref colorDelayUs, @ref
        * depthDelayUs or @ref trigger2ImageDelayUs.
        *
        * @attention The frequency of the trigger signal multiplied by the number of frames per trigger should be less than the frame rate of the stream profile
        * which is set when starting the stream.
        * @attention The trigger signal input via VSYNC_IN pin on synchronization port should be ouput by other device via VSYNC_OUT pin in hardware triggering
        * mode or software triggering mode.
        * @attention Due to different models may have different signal input requirements, please do not use different models to output trigger
        * signal as input-trigger signal.
        */
        OB_MULTI_DEVICE_SYNC_MODE_HARDWARE_TRIGGERING = 1 << 6,

    }

    /**
    * @brief The synchronization configuration of the device.
    */
    public struct MultiDeviceSyncConfig{
        /**
        * @brief The sync mode of the device.
        */
        public MultiDeviceSyncMode syncMode;

        /**
        * @brief The delay time of the depth image capture after receiving the capture command or trigger signal in microseconds.
        *
        * @attention This parameter is only valid for some models， please refer to the product manual for details.
        */
        public int depthDelayUs;

        /**
        * @brief The delay time of the color image capture after receiving the capture command or trigger signal in microseconds.
        *
        * @attention This parameter is only valid for some models， please refer to the product manual for details.
        */
        public int colorDelayUs;

        /**
        * @brief The delay time of the image capture after receiving the capture command or trigger signal in microseconds.
        * @brief The depth and color images are captured synchronously as the product design and can not change the delay between the depth and color images.
        *
        * @attention For Orbbec Astra 2 device, this parameter is valid only when the @ref triggerOutDelayUs is set to 0.
        * @attention This parameter is only valid for some models to replace @ref depthDelayUs and @ref colorDelayUs, please refer to the product manual for
        * details.
        */
        public int trigger2ImageDelayUs;

        /**
        * @brief Trigger signal output enable flag.
        * @brief After the trigger signal output is enabled, the trigger signal will be output when the capture command or trigger signal is received. User can
        * adjust the delay time of the trigger signal output by @ref triggerOutDelayUs.
        *
        * @attention For some models, the trigger signal output is always enabled and cannot be disabled.
        * @attention If device is in the @ref OB_MULTI_DEVICE_SYNC_MODE_FREE_RUN or @ref OB_MULTI_DEVICE_SYNC_MODE_STANDALONE mode, the trigger signal output is
        * always disabled. Set this parameter to true will not take effect.
        */
        public bool triggerOutEnable;

        /**
        * @brief The delay time of the trigger signal output after receiving the capture command or trigger signal in microseconds.
        *
        * @attention For Orbbec Astra 2 device, only supported -1 and 0. -1 means the trigger signal output delay is automatically adjusted by the device, 0 means
        * the trigger signal output is disabled.
        */
        public int triggerOutDelayUs;

        /**
        * @brief The frame number of each stream after each trigger in triggering mode.
        *
        * @attention This parameter is only valid when the triggering mode is set to @ref OB_MULTI_DEVICE_SYNC_MODE_HARDWARE_TRIGGERING or @ref
        * OB_MULTI_DEVICE_SYNC_MODE_SOFTWARE_TRIGGERING.
        * @attention The trigger frequency multiplied by the number of frames per trigger cannot exceed the maximum frame rate of the stream profile which is set
        * when starting the stream.
        */
        public int framesPerTrigger;
    }

    /**
    * @brief The timestamp reset configuration of the device.
    *
    */
    public struct DeviceTimestampResetConfig{
        /**
        * @brief Whether to enable the timestamp reset function.
        * @brief If the timestamp reset function is enabled, the timer for calculating the timestamp for output frames will be reset to 0 when the timestamp reset
        * command or timestamp reset signal is received, and one timestamp reset signal will be output via TIMER_SYNC_OUT pin on synchronization port by default.
        * The timestamp reset signal is input via TIMER_SYNC_IN pin on the synchronization port.
        *
        * @attention For some models, the timestamp reset function is always enabled and cannot be disabled.
        */
        public bool enable;

        /**
        * @brief The delay time of executing the timestamp reset function after receiving the command or signal in microseconds.
        */
        public int timestamp_reset_delay_us;

        /**
        * @brief the timestamp reset signal output enable flag.
        *
        * @attention For some models, the timestamp reset signal output is always enabled and cannot be disabled.
        */
        public bool timestamp_reset_signal_output_enable;
    }

    /**
    * @brief Baseline calibration parameters
    */
    public struct BaselineCalibrationParam{
        /**
        * @brief Baseline length
        */
        public float baseline;
        /**
        * @brief Calibration distance
        */
        public float zpd;
    }
    
    /**
    * @brief HDR Configuration
    */
    public struct HdrConfig
    {

        /**
        * @brief Enable/disable HDR, after enabling HDR, the exposure_1 and gain_1 will be used as the first exposure and gain, and the exposure_2 and gain_2 will
        * be used as the second exposure and gain. The output image will be alternately exposed and gain between the first and second
        * exposure and gain.
        *
        * @attention After enabling HDR, the auto exposure will be disabled.
        */
        public byte  enable;
        public byte  sequence_name;  ///< Sequence name
        public UInt32 exposure_1;     ///< Exposure time 1
        public UInt32 gain_1;         ///< Gain 1
        public UInt32 exposure_2;     ///< Exposure time 2
        public UInt32 gain_2;         ///< Gain 2
    } 

    /**
    * @brief The rect of the region of interest
    */
    public struct RegionOfInterest
    {
        public Int16 x0_left;
        public Int16 y0_top;
        public Int16 x1_right;
        public Int16 y1_bottom;
    }

    /**
    * @brief Frame metadata types
    * @brief The frame metadata is a set of meta info generated by the device for current individual frame.
    */
    public enum FrameMetadataType
    {
        /**
        * @brief Timestamp when the frame is captured.
        * @attention Different device models may have different units. It is recommended to use the timestamp related functions to get the timestamp in the
        * correct units.
        */
        OB_FRAME_METADATA_TYPE_TIMESTAMP = 0,

        /**
        * @brief Timestamp in the middle of the capture.
        * @brief Usually is the middle of the exposure time.
        *
        * @attention Different device models may have different units.
        */
        OB_FRAME_METADATA_TYPE_SENSOR_TIMESTAMP = 1,

        /**
        * @brief The number of current frame.
        */
        OB_FRAME_METADATA_TYPE_FRAME_NUMBER = 2,

        /**
        * @brief Auto exposure status
        * @brief If the value is 0, it means the auto exposure is disabled. Otherwise, it means the auto exposure is enabled.
        */
        OB_FRAME_METADATA_TYPE_AUTO_EXPOSURE = 3,

        /**
        * @brief Exposure time
        *
        * @attention Different sensor may have different units. Usually, it is 100us for color sensor and 1us for depth/infrared sensor.
        */
        OB_FRAME_METADATA_TYPE_EXPOSURE = 4,

        /**
        * @brief Gain
        *
        * @attention For some device models, the gain value represents the gain level, not the multiplier.
        */
        OB_FRAME_METADATA_TYPE_GAIN = 5,

        /**
        * @brief Auto white balance status
        * @brief If the value is 0, it means the auto white balance is disabled. Otherwise, it means the auto white balance is enabled.
        */
        OB_FRAME_METADATA_TYPE_AUTO_WHITE_BALANCE = 6,

        /**
        * @brief White balance
        */
        OB_FRAME_METADATA_TYPE_WHITE_BALANCE = 7,

        /**
        * @brief Brightness
        */
        OB_FRAME_METADATA_TYPE_BRIGHTNESS = 8,

        /**
        * @brief Contrast
        */
        OB_FRAME_METADATA_TYPE_CONTRAST = 9,

        /**
        * @brief Saturation
        */
        OB_FRAME_METADATA_TYPE_SATURATION = 10,

        /**
        * @brief Sharpness
        */
        OB_FRAME_METADATA_TYPE_SHARPNESS = 11,

        /**
        * @brief Backlight compensation
        */
        OB_FRAME_METADATA_TYPE_BACKLIGHT_COMPENSATION = 12,

        /**
        * @brief Hue
        */
        OB_FRAME_METADATA_TYPE_HUE = 13,

        /**
        * @brief Gamma
        */
        OB_FRAME_METADATA_TYPE_GAMMA = 14,

        /**
        * @brief Power line frequency
        * @brief For anti-flickering， 0：Close， 1： 50Hz， 2： 60Hz， 3： Auto
        */
        OB_FRAME_METADATA_TYPE_POWER_LINE_FREQUENCY = 15,

        /**
        * @brief Low light compensation
        *
        * @attention The low light compensation is a feature inside the device，and can not manually control it.
        */
        OB_FRAME_METADATA_TYPE_LOW_LIGHT_COMPENSATION = 16,

        /**
        * @brief Manual white balance setting
        */
        OB_FRAME_METADATA_TYPE_MANUAL_WHITE_BALANCE = 17,

        /**
        * @brief Actual frame rate
        * @brief The actual frame rate will be calculated according to the exposure time and other parameters.
        */
        OB_FRAME_METADATA_TYPE_ACTUAL_FRAME_RATE = 18,

        /**
        * @brief Frame rate
        */
        OB_FRAME_METADATA_TYPE_FRAME_RATE = 19,

        /**
        * @brief Left region of interest for the auto exposure Algorithm.
        */
        OB_FRAME_METADATA_TYPE_AE_ROI_LEFT = 20,

        /**
        * @brief Top region of interest for the auto exposure Algorithm.
        */
        OB_FRAME_METADATA_TYPE_AE_ROI_TOP = 21,

        /**
        * @brief Right region of interest for the auto exposure Algorithm.
        */
        OB_FRAME_METADATA_TYPE_AE_ROI_RIGHT = 22,

        /**
        * @brief Bottom region of interest for the auto exposure Algorithm.
        */
        OB_FRAME_METADATA_TYPE_AE_ROI_BOTTOM = 23,

        /**
        * @brief Exposure priority
        */
        OB_FRAME_METADATA_TYPE_EXPOSURE_PRIORITY = 24,

        /**
        * @brief HDR sequence name
        */
        OB_FRAME_METADATA_TYPE_HDR_SEQUENCE_NAME = 25,

        /**
        * @brief HDR sequence size
        */
        OB_FRAME_METADATA_TYPE_HDR_SEQUENCE_SIZE = 26,

        /**
        * @brief HDR sequence index
        */
        OB_FRAME_METADATA_TYPE_HDR_SEQUENCE_INDEX = 27,

        /**
        * @brief Laser power value in mW
        *
        * @attention The laser power value is an approximate estimation.
        */
        OB_FRAME_METADATA_TYPE_LASER_POWER = 28,

        /**
        * @brief Laser power level
        */
        OB_FRAME_METADATA_TYPE_LASER_POWER_LEVEL = 29,

        /**
        * @brief Laser status
        * @brief 0: Laser off, 1: Laser on
        */
        OB_FRAME_METADATA_TYPE_LASER_STATUS = 30,

        /**
        * @brief GPIO input data
        */
        OB_FRAME_METADATA_TYPE_GPIO_INPUT_DATA = 31,

        /**
        * @brief The number of frame metadata types, using for types iterating
        * @attention It is not a valid frame metadata type
        */
        OB_FRAME_METADATA_TYPE_COUNT,
    }
}