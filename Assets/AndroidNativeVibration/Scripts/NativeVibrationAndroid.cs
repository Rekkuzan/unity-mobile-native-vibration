using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan
{
    class NativeVibrationAndroid {

#if UNITY_ANDROID

        #region Private Static Variables

		private static AndroidJavaObject mActivityContext;
		private static AndroidJavaObject mAndroidNativeVibratorJavaObject;
		private static bool mInitialized = false;
        private static bool mHasVibrator;
        private static bool mHasAmplitudeControl;
        #endregion

        #region Public Static Methods

        public static bool HasVibrator()
        {
            if (!mInitialized)
            {
                Initialize();
            }
            return mHasVibrator;
        }

        public static bool HasAmplitudeControl()
        {
            if (!mInitialized)
            {
                Initialize();
            }
            return mHasAmplitudeControl;
        }

        public static void Vibrate(long milliseconds, int amplitude)
        {
            if (!mInitialized)
            {
                Initialize();
            }
            mAndroidNativeVibratorJavaObject.Call("VibrateOneShot", milliseconds, amplitude);
        }

        public static void Vibrate (long[] pattern, int[] amplitudes, int repeat)
		{
			if (!mInitialized) {
				Initialize ();
			}
            mAndroidNativeVibratorJavaObject.Call ("VibratePattern", pattern, amplitudes, repeat);
		}

        public static void Cancel()
        {
            if (!mInitialized)
            {
                Initialize();
            }
            mAndroidNativeVibratorJavaObject.Call("CancelVibration");
        }

        #endregion

        #region Private Static Methods

		private static void Initialize ()
		{
			using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
				mActivityContext = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity");
			}
			using (AndroidJavaClass UnityAndroidPluginClass = new AndroidJavaClass ("com.rekkuzan.vibratorlibrary.VibrationManager")) {
                mAndroidNativeVibratorJavaObject = UnityAndroidPluginClass.CallStatic<AndroidJavaObject> ("Instance");
                mAndroidNativeVibratorJavaObject.Call ("Init", mActivityContext);
                mHasVibrator = mAndroidNativeVibratorJavaObject.Call<bool>("HasVibrator");
                mHasAmplitudeControl = mAndroidNativeVibratorJavaObject.Call<bool>("HasAmplitudeControl");
            }
			mInitialized = true;
		}

        #endregion

#endif
    }
}
