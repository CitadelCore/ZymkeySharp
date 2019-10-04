using System;
using System.Runtime.InteropServices;

namespace ZymkeySharp
{
    internal static class NativeMethods
    {
        private const string LIBRARY_NAME = "libzk_app_utils.so";

        internal const uint ZK_PERIMETER_EVENT_ACTION_NOTIFY = 1 << 0;
        internal const uint ZK_PERIMETER_EVENT_ACTION_SELF_DESTRUCT = 1 << 1;

        internal const int ETIMEDOUT = 110;

        [DllImport(LIBRARY_NAME)]
        internal static extern int zkOpen(out IntPtr ctx);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkClose(IntPtr ctx);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkGetRandBytes(IntPtr ctx, byte[] bytes, int bytesLen);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkLockDataF2F(IntPtr ctx, string sourceFile, string destFile, bool useSharedKey = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkLockDataB2F(IntPtr ctx, byte[] sourceData, int sourceDataLen, string destFile, bool useSharedKey = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkLockDataF2B(IntPtr ctx, string sourceFile, out IntPtr output, out int outputLen, bool useSharedKey = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkLockDataB2B(IntPtr ctx, byte[] sourceData, int sourceDataLen, out IntPtr output, out int outputLen, bool useSharedKey = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkUnlockDataF2F(IntPtr ctx, string sourceFile, string destFile, bool useSharedKey = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkUnlockDataB2F(IntPtr ctx, byte[] sourceData, int sourceDataLen, string destFile, bool useSharedKey = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkUnlockDataF2B(IntPtr ctx, string sourceFile, out IntPtr output, out int outputLen, bool useSharedKey = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkUnlockDataB2B(IntPtr ctx, byte[] sourceData, int sourceDataLen, out IntPtr output, out int outputLen, bool useSharedKey = false);

        [DllImport(LIBRARY_NAME)]
        internal static extern int zkGenECDSASigFromDigest(IntPtr ctx, byte[] digest, int slot, out IntPtr output, out int outputLen);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkVerifyECDSASigFromDigest(IntPtr ctx, byte[] digest, int slot, byte[] signature, int signatureLen);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkVerifyECDSASigFromDigestWithForeignKey(IntPtr ctx, byte[] digest, byte[] foreignPubkey, int foreignPubkeyLen, byte[] signature, int signatureLen, bool sigIsDer = false, ZkForeignPubkeyType foreignPubkeyType = ZkForeignPubkeyType.NistP256);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkSaveECDSAPubKey2File(IntPtr ctx, string destFile, int slot = 0);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkGetECDSAPubKey(IntPtr ctx, out IntPtr publicKey, out int publicKeyLen, int slot = 0);

        [DllImport(LIBRARY_NAME)]
        internal static extern int zkLEDOff(IntPtr ctx);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkLEDOn(IntPtr ctx);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkLEDFlash(IntPtr ctx, uint on, uint off = 0, uint numFlashes = 0);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkSetI2CAddr(IntPtr ctx, int addr);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkGetTime(IntPtr ctx, out uint time, bool preciseTime = false);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkSetTapSensitivity(IntPtr ctx, ZkAccelAxisType axisType, float sensitivity);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkWaitForTap(IntPtr ctx, uint timeout);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkGetAccelerometerData(IntPtr ctx, out ZkAccelAxisDataType x, out ZkAccelAxisDataType y, out ZkAccelAxisDataType z);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkWaitForPerimeterEvent(IntPtr ctx, uint timeout);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkGetPerimeterDetectInfo(IntPtr ctx, out IntPtr info, out uint infoCount);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkClearPerimeterDetectEvents(IntPtr ctx);
        [DllImport(LIBRARY_NAME)]
        internal static extern int zkSetPerimeterEventAction(IntPtr ctx, int channel, uint actionFlags);
    }
}