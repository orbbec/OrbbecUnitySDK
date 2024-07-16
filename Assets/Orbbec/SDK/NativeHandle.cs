using System;

namespace Orbbec
{
    public delegate void ReleaseAction(IntPtr ptr);

    /*
     * \internal
     */
    public sealed class NativeHandle : IDisposable
    {
        private readonly ReleaseAction _action;

        public IntPtr Ptr { get; private set; }
        public int ReferenceCount { get; private set; }
        public bool IsValid { get { return Ptr != IntPtr.Zero; } }

        public NativeHandle(IntPtr ptr, ReleaseAction releaseAction)
        {
            ThrowIfNull(ptr);

            Ptr = ptr;
            ReferenceCount = 1;
            _action = releaseAction;
        }

        private void ThrowIfNull(IntPtr ptr)
        {
            if(ptr == IntPtr.Zero)
            {
                throw new NullReferenceException("Handle is null");
            }
        }

        private void ThrowIfInvalid()
        {
            if (IsValid)
                return;

            throw new NullReferenceException("NativeHandle has previously been released");
        }

        public void Retain()
        {
            ++ReferenceCount;
        }

        public void Release()
        {
            if (--ReferenceCount > 0) 
                return;

            ThrowIfInvalid();
            if (_action != null)
            {
                _action(Ptr);
            }

            Ptr = IntPtr.Zero;
        }

        public void Dispose()
        {
            Release();
            GC.SuppressFinalize(this);
        }

        ~NativeHandle()
        {
            Release();
        }
    }
}
