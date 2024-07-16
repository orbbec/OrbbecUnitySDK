using System;
using System.Runtime.InteropServices;

namespace Orbbec
{
    public class Error : IDisposable
    {
        private NativeHandle _handle;

        internal Error(IntPtr handle)
        {
            _handle = new NativeHandle(handle, Delete);
        }

        /**
        * \if English
        * @brief Obtain detailed error logs of SDK internal exceptions.
        * \else
        * @brief 获取SDK内部异常的详细错误日志。
        * \endif
        */
        public String GetMessage()
        {
            IntPtr ptr = obNative.ob_error_message(_handle.Ptr);
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the name of the error function.
        * \else
        * @brief 获取SDK内部异常的错误接口函数名称。
        * \endif
        */
        public String GetFunction()
        {
            IntPtr ptr = obNative.ob_error_function(_handle.Ptr);
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the parameter passed in to the error interface.
        * \else
        * @brief 获取SDK内部异常的错误接口函数传入参数。
        * \endif
        */
        public String GetArgs()
        {
            IntPtr ptr = obNative.ob_error_args(_handle.Ptr);
            return Marshal.PtrToStringAnsi(ptr);
        }

        /**
        * \if English
        * @brief Get the exception type of the error, and determine which module is abnormal.
        * @return ExceptionType
        * \else
        * @brief 获取该错误的异常类型，判断是具体哪个模块异常。
        * @return ExceptionType
        */
        public ExceptionType GetExceptionType()
        {
            return obNative.ob_error_exception_type(_handle.Ptr);
        }

        internal void Delete(IntPtr handle)
        {
            IntPtr error = IntPtr.Zero;
            obNative.ob_delete_error(handle);
        }

        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}