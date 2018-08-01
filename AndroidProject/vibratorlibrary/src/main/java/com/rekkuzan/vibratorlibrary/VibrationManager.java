package com.rekkuzan.vibratorlibrary;

import android.content.Context;
import android.os.Build;
import android.os.VibrationEffect;
import android.os.Vibrator;

public class VibrationManager {

    private static volatile VibrationManager sInstance = new VibrationManager();

    //private constructor.
    private VibrationManager(){}

    public static VibrationManager Instance() {
        return sInstance;
    }

    private Vibrator mVibrator = null;

    private boolean IsOldAPI() {
        return android.os.Build.VERSION.SDK_INT < Build.VERSION_CODES.O;
    }

    /**
    * Public method
    */

    /**
     * Initialize the VibrationManager
     * @param context
     */
    public void Init(Context context)
    {
        mVibrator = (Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
    }

    /**
     * Return true/false whether the vibrator is available
     * @return boolean
     */
    public boolean HasVibrator()
    {
        return mVibrator.hasVibrator();
    }

    /**
     * Return true/false whether there is amplitude control
     * @return boolean
     */
    public boolean HasAmplitudeControl() {
        return !IsOldAPI() && mVibrator.hasAmplitudeControl();
    }

    /**
     * Cancel any vibration
     */
    public void CancelVibration()
    {
        mVibrator.cancel();
    }

    /**
     * Make a single Vibration for X milliseconds
     * @param milliseconds
     */
    public void VibrateOneShot(long milliseconds) {
        VibrateOneShot(milliseconds, -1);
    }

    /**
     * Make a single Vibration for X milliseconds and Y amplitude (strenght)
     * @param milliseconds
     * @param amplitude
     */
    public void VibrateOneShot(long milliseconds, int amplitude) {
        CancelVibration();
        if (IsOldAPI()) {
            mVibrator.vibrate(milliseconds);
        } else {
            // -1 is the default amplitude value
            amplitude = amplitude < 0 && amplitude != -1 ? 0 : amplitude;
            amplitude = amplitude > 255 ? 255 : amplitude;

            VibrationEffect vE = VibrationEffect.createOneShot(milliseconds, amplitude);
            mVibrator.vibrate(vE);
        }
    }

    /**
     * Make a pattern vibration with a pattern and amplitude
     * (Old API doesn't take amplitude into account)
     * @param pattern
     * @param amplitudes
     * @param repeat
     */
    public void VibratePattern(long[] pattern, int[] amplitudes, int repeat) {
        CancelVibration();
        if (IsOldAPI()) {
            mVibrator.vibrate(pattern, repeat);
        } else {

            for (int i = 0; i < amplitudes.length; ++i) {
                // -1 is the default amplitude value
                amplitudes[i] = amplitudes[i] < 0 && amplitudes[i] != -1 ? 0 : amplitudes[i];
                amplitudes[i] = amplitudes[i] > 255 ? 255 : amplitudes[i];
            }

            VibrationEffect vE = VibrationEffect.createWaveform(pattern, amplitudes, repeat);
            mVibrator.vibrate(vE);
        }
    }
}
