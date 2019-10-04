using System;
using System.Runtime.InteropServices;

namespace ZymkeySharp
{
    internal static class NativeHelper
    {
        internal static byte[] PtrToArray(IntPtr array, int length) {
            var result = new byte[length];
            Marshal.Copy(array, result, 0, length);

            return result;
        }

        internal static int[] PtrToArrayInt(IntPtr array, int length) {
            var result = new int[length];
            Marshal.Copy(array, result, 0, length);

            return result;
        }

        internal static void Validate(int code) {
            if (code >= 0)
                return;

            throw new ZymkeyException(code);
        }
    }
}