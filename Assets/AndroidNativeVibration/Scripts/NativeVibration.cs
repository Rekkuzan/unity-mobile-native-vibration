using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Rekkuzan
{
    public class NativeVibration : Common.SingletonBehaviour<NativeVibration>
    {

        #region Private Attributes
        private bool CancelOnPause = true;
        #endregion

        #region Public Methods

        /// <summary>
        /// Is this device able to make vibration
        /// </summary>
        /// <returns>boolean</returns>
        public static bool IsVibrationAvailable()
        {
            if (!NativeVibration.Instance)
                return false;
#if UNITY_EDITOR
                return false;
#elif UNITY_IOS
			// not implemented
            return false;
#elif UNITY_ANDROID
            return NativeVibrationAndroid.HasVibrator();
#else
            return false;
#endif
        }

        /// <summary>
        /// Is this device able to control amplitude of vibration
        /// </summary>
        /// <returns>boolean</returns>
        public static bool IsAmplitudeAvailable()
        {
            if (!NativeVibration.Instance)
                return false;
#if UNITY_EDITOR
            return false;
#elif UNITY_IOS
			// not implemented
            return false;
#elif UNITY_ANDROID
            return NativeVibrationAndroid.HasAmplitudeControl();
#else
            return false;
#endif
        }

        /// <summary>
        /// Start a vibration of milliseconds lenght and amplitude strenght (-1 is default, up to 250)
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <param name="amplitude"></param>
        public static void Vibrate(long milliseconds = 100, int amplitude = -1)
        {
            if (!NativeVibration.Instance)
                return;
#if UNITY_EDITOR
#elif UNITY_IOS

#elif UNITY_ANDROID
            NativeVibrationAndroid.Vibrate(milliseconds, amplitude);
#endif
        }

        /// <summary>
        /// Start a vibration with a List<long> pattern (milliseconds) and the List<int> amplitudes (0 to 250)
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="amplitudes"></param>
        public static void Vibrate(List<long> pattern, List<int> amplitudes)
        {
            Vibrate(pattern.ToArray(), amplitudes.ToArray());
        }

        /// <summary>
        /// Start a vibration with a long[] pattern (milliseconds) and the int[] amplitudes (0 to 250)
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="amplitudes"></param>
        public static void Vibrate(long[] pattern, int[] amplitudes)
        {
            if (!NativeVibration.Instance)
                return;
#if UNITY_EDITOR
#elif UNITY_IOS

#elif UNITY_ANDROID
            NativeVibrationAndroid.Vibrate(pattern, amplitudes, -1);
#endif
        }

        /// <summary>
        /// Cancel any vibration
        /// </summary>
        public static void Cancel()
        {
            if (!NativeVibration.Instance)
                return;
#if UNITY_EDITOR
#elif UNITY_IOS

#elif UNITY_ANDROID
            NativeVibrationAndroid.Cancel();
#endif
        }

        /// <summary>
        /// Set the true/false cancel vibration when application pause
        /// </summary>
        /// <param name="enable"></param>
        public void SetCancelOnPause(bool enable)
        {
            CancelOnPause = enable;
        }

        #endregion

        private void OnApplicationPause(bool pause)
        {
            if (pause && CancelOnPause)
            {
                Cancel();
            }
        }

        private new void OnDestroy()
        {
            Cancel();
            base.OnDestroy();
        }
    }
}