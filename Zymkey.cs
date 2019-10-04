using System;

namespace ZymkeySharp
{
    /// <summary>
    /// Represents an instance of a Zymkey context.
    /// </summary>
    public class Zymkey : IDisposable
    {
        private IntPtr _ref;

        /// <summary>
        /// Opens a new Zymkey context.
        /// </summary>
        public Zymkey() {
            NativeHelper.Validate(NativeMethods.zkOpen(out _ref));
        }

        /// <summary>
        /// Disposes the Zymkey context.
        /// </summary>
        public void Dispose()
        {
            NativeHelper.Validate(NativeMethods.zkClose(_ref));
        }

        /// <summary>
        /// Gets an array of random bytes.
        /// </summary>
        /// <param name="length">The number of bytes to generate.</param>
        public byte[] GetRandomBytes(int length) {
            var bytes = new byte[length];
            NativeHelper.Validate(NativeMethods.zkGetRandBytes(_ref, bytes, length));
            return bytes;
        }

        /// <summary>
        /// Encrypt data from a file with the specified key, and store the result in a file.
        /// </summary>
        /// <param name="sourceFile">The source filename.</param>
        /// <param name="destFile">The destination filename.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        public void LockData(string sourceFile, string destFile, bool useSharedKey = false)
            => NativeHelper.Validate(NativeMethods.zkLockDataF2F(_ref, sourceFile, destFile, useSharedKey));

        /// <summary>
        /// Encrypt data with the specified key, and store the result in a file.
        /// </summary>
        /// <param name="sourceData">A byte array with the source data.</param>
        /// <param name="destFile">The destination filename.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        public void LockData(byte[] sourceData, string destFile, bool useSharedKey = false)
            => NativeHelper.Validate(NativeMethods.zkLockDataB2F(_ref, sourceData, sourceData.Length, destFile, useSharedKey));

        /// <summary>
        /// Encrypt data from a file with the specified key, and return the result.
        /// </summary>
        /// <param name="sourceFile">The source filename.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        /// <returns>The encrypted data.</returns>
        public byte[] LockData(string sourceFile, bool useSharedKey = false) {
            NativeHelper.Validate(NativeMethods.zkLockDataF2B(_ref, sourceFile, out var output, out var outputLen, useSharedKey));
            return NativeHelper.PtrToArray(output, outputLen);
        }

        /// <summary>
        /// Encrypt data with the specified key, and return the result.
        /// </summary>
        /// <param name="sourceData">A byte array with the source data.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        /// <returns>The encrypted data.</returns>
        public byte[] LockData(byte[] sourceData, bool useSharedKey = false) {
            NativeHelper.Validate(NativeMethods.zkLockDataB2B(_ref, sourceData, sourceData.Length, out var output, out var outputLen, useSharedKey));
            return NativeHelper.PtrToArray(output, outputLen);
        }

        /// <summary>
        /// Decrypt data from a file with the specified key, and store the result in a file.
        /// </summary>
        /// <param name="sourceFile">The source filename.</param>
        /// <param name="destFile">The destination filename.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        public void UnlockData(string sourceFile, string destFile, bool useSharedKey = false)
            => NativeHelper.Validate(NativeMethods.zkUnlockDataF2F(_ref, sourceFile, destFile, useSharedKey));

        /// <summary>
        /// Decrypt data with the specified key, and store the result in a file.
        /// </summary>
        /// <param name="sourceData">A byte array with the source data.</param>
        /// <param name="destFile">The destination filename.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        public void UnlockData(byte[] sourceData, string destFile, bool useSharedKey = false)
            => NativeHelper.Validate(NativeMethods.zkUnlockDataB2F(_ref, sourceData, sourceData.Length, destFile, useSharedKey));

        /// <summary>
        /// Decrypt data from a file with the specified key, and return the result.
        /// </summary>
        /// <param name="sourceFile">The source filename.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        /// <returns>The decrypted data.</returns>
        public byte[] UnlockData(string sourceFile, bool useSharedKey = false) {
            NativeHelper.Validate(NativeMethods.zkUnlockDataF2B(_ref, sourceFile, out var output, out var outputLen, useSharedKey));
            return NativeHelper.PtrToArray(output, outputLen);
        }

        /// <summary>
        /// Decrypt data with the specified key, and return the result.
        /// </summary>
        /// <param name="sourceData">A byte array with the source data.</param>
        /// <param name="useSharedKey">Whether to use the Zymbit cloud key.</param>
        /// <returns>The decrypted data.</returns>
        public byte[] UnlockData(byte[] sourceData, bool useSharedKey = false) {
            NativeHelper.Validate(NativeMethods.zkUnlockDataB2B(_ref, sourceData, sourceData.Length, out var output, out var outputLen, useSharedKey));
            return NativeHelper.PtrToArray(output, outputLen);
        }

        /// <summary>
        /// Generate an ECDSA signature from a digest.
        /// </summary>
        /// <param name="digest">The digest to sign.</param>
        /// <param name="slot">The Zymkey key slot to use for the operation.</param>
        /// <returns>The ECDSA signature.</returns>
        public byte[] GenerateEcdsaSignatureFromDigest(byte[] digest, int slot = 0) {
            NativeHelper.Validate(NativeMethods.zkGenECDSASigFromDigest(_ref, digest, slot, out var output, out var outputLen));
            return NativeHelper.PtrToArray(output, outputLen);
        }

        /// <summary>
        /// Verify an ECDSA signature from a digest.
        /// </summary>
        /// <param name="digest">The digest to verify.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="slot">The Zymkey key slot to use for the operation.</param>
        /// <returns>Whether the signature is valid.</returns>
        public bool VerifyEcdsaSignatureFromDigest(byte[] digest, byte[] signature, int slot = 0) {
            var code = NativeMethods.zkVerifyECDSASigFromDigest(_ref, digest, slot, signature, signature.Length);
            NativeHelper.Validate(code);

            return code == 1 ? true : false;
        }

        /// <summary>
        /// Verify an ECDSA signature from a digest with a foreign key.
        /// </summary>
        /// <param name="digest">The digest to verify.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="foreignPubkey">The foreign key.</param>
        /// <param name="der">Whether the signature is DER.</param>
        /// <param name="pubkeyType">Type of curve to verify against.</param>
        /// <returns>Whether the signature is valid.</returns>
        public bool VerifyEcdsaSignatureFromDigest(byte[] digest, byte[] signature, byte[] foreignPubkey, bool der = false, ZkForeignPubkeyType pubkeyType = ZkForeignPubkeyType.NistP256) {
            var code = NativeMethods.zkVerifyECDSASigFromDigestWithForeignKey(_ref, digest, foreignPubkey, foreignPubkey.Length, signature, signature.Length, der, pubkeyType);
            NativeHelper.Validate(code);
            
            return code == 1 ? true : false;
        }

        /// <summary>
        /// Save the ECDSA public key at the specified slot to a file.
        /// </summary>
        /// <param name="destFile">The file to save to.</param>
        /// <param name="slot">The Zymkey slot.</param>
        public void SaveEcdsaPubkeyToFile(string destFile, int slot = 0)
            => NativeHelper.Validate(NativeMethods.zkSaveECDSAPubKey2File(_ref, destFile, slot));

        /// <summary>
        /// Return the ECDSA public key at the specified slot.
        /// </summary>
        /// <param name="slot">The Zymkey slot.</param>
        /// <returns>The ECDSA public key.</returns>
        public byte[] GetEcdsaPubkey(int slot = 0) {
            NativeHelper.Validate(NativeMethods.zkGetECDSAPubKey(_ref, out var publicKey, out var publicKeyLen, slot));
            return NativeHelper.PtrToArray(publicKey, publicKeyLen);
        }

        /// <summary>
        /// Turns the hardware LED off.
        /// </summary>
        public void LedOff() 
            => NativeHelper.Validate(NativeMethods.zkLEDOff(_ref));

        /// <summary>
        /// Turns the hardware LED on.
        /// </summary>
        public void LedOn()
            => NativeHelper.Validate(NativeMethods.zkLEDOn(_ref));

        /// <summary>
        /// Flashes the hardware LED for the specified times.
        /// </summary>
        /// <param name="on">The time the LED will stay on.</param>
        /// <param name="off">The time the LED will stay off.</param>
        /// <param name="numFlashes">The number of times to flash. If 0, the LED will flash indefinitely.</param>
        public void LedFlash(uint on, uint off = 0, uint numFlashes = 0)
            => NativeHelper.Validate(NativeMethods.zkLEDFlash(_ref, on, off, numFlashes));

        /// <summary>
        /// Sets the Zymkey's I2C address.
        /// </summary>
        /// <param name="addr">The I2C address.</param>
        public void SetI2CAddr(int addr)
            => NativeHelper.Validate(NativeMethods.zkSetI2CAddr(_ref, addr));

        /// <summary>
        /// Gets the current GMT time.
        /// </summary>
        /// <param name="preciseTime">Whether to wait until the next second to return.</param>
        /// <returns>The time.</returns>
        public DateTime GetTime(bool preciseTime = false) {
            NativeHelper.Validate(NativeMethods.zkGetTime(_ref, out var time, preciseTime));
            
            var timeStamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return timeStamp.AddSeconds(time).ToLocalTime();
        }

        /// <summary>
        /// Sets the sensitivity of tap operations.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="sensitivity">The sensitivity, expressed as a percentage.</param>
        public void SetTapSensitivity(ZkAccelAxisType axis, float sensitivity)
            => NativeHelper.Validate(NativeMethods.zkSetTapSensitivity(_ref, axis, sensitivity));

        /// <summary>
        /// Blocks until a tap event is triggered or the timeout is reached.
        /// </summary>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns>Whether the event was triggered.</returns>
        public bool WaitForTap(uint timeout) {
            var code = NativeMethods.zkWaitForTap(_ref, timeout);
            if (code == -NativeMethods.ETIMEDOUT)
                return false;

            NativeHelper.Validate(code);
            return true;
        }

        /// <summary>
        /// Gets the accelerometer data and per-axis tap information.
        /// </summary>
        /// <returns>The accelerometer data.</returns>
        public ZkAccelData GetAccelerometerData() {
            NativeHelper.Validate(NativeMethods.zkGetAccelerometerData(_ref, out ZkAccelAxisDataType x, out ZkAccelAxisDataType y, out ZkAccelAxisDataType z));
            return new ZkAccelData() {
                X = x.g,
                Y = y.g,
                Z = z.g,
                TapDirX = x.tapDirection,
                TapDirY = y.tapDirection,
                TapDirZ = z.tapDirection
            };
        }

        /// <summary>
        /// Blocks until a perimeter event is triggered or the timeout is reached.
        /// </summary>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns>Whether the event was triggered.</returns>
        public bool WaitForPerimeterEvent(uint timeout) {
            var code = NativeMethods.zkWaitForPerimeterEvent(_ref, timeout);
            if (code == -NativeMethods.ETIMEDOUT)
                return false;

            NativeHelper.Validate(code);
            return true;
        }

        /// <summary>
        /// Gets the perimeter detection info.
        /// </summary>
        /// <returns>The info as an array of ints.</returns>
        public int[] GetPerimeterDetectInfo() {
            NativeHelper.Validate(NativeMethods.zkGetPerimeterDetectInfo(_ref, out IntPtr info, out var infoCount));
            return NativeHelper.PtrToArrayInt(info, (int)infoCount);
        }

        /// <summary>
        /// Clears the perimeter detection info and rearms all channels.
        /// </summary>
        public void ClearPerimeterDetectEvents()
            => NativeHelper.Validate(NativeMethods.zkClearPerimeterDetectEvents(_ref));

        /// <summary>
        /// Sets the action taken when a perimeter channel is tripped.
        /// </summary>
        /// <param name="channel">The channel to set.</param>
        /// <param name="actionFlags">Bitfield of the action(s) to take.</param>
        public void SetPerimeterEventAction(int channel, uint actionFlags)
            => NativeHelper.Validate(NativeMethods.zkSetPerimeterEventAction(_ref, channel, actionFlags));
    }
}
