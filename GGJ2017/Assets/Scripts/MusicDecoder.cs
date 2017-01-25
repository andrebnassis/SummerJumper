using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicDecoder : MonoBehaviour
{

    public new AudioSource audio;

    private int qSamples = 1024;  // array size
    private float refValue = 0.1f; // RMS value for 0 dB
    private float threshold = 0.02f;      // minimum amplitude to extract pitch
    private float rmsValue;   // sound level - RMS
    public float dbValue;    // sound level - dB
    public float min_dbValue;
    public float max_dbValue;
    //public float avg_dbValue; //average of sound level - dB
    //private float total_dbValue; //sum of sound level - dB
    public float pitchValue; // sound pitch - Hz
    public float max_pitchValue;
    public float min_pitchValue;
    //public float avg_pitchValue; //average of sound pitch - Hz
    //private float total_pitchValue; //sum of sound pitch - Hz
    private int count;

    private float[] samples;
    private float[] spectrum;
    private float fSample;

    public float period;
    private float currentPitchValue;
    private float lastPitchValue;

    void Start()
    {
        period = 0.0f;
        count = 0;
        //total_pitchValue = 0;
        //total_dbValue = 0;
        samples = new float[qSamples];
        spectrum = new float[qSamples];
        fSample = AudioSettings.outputSampleRate;
        audio = GetComponent<AudioSource>();
        audio.Play();
        //display = GameObject.Find("Canvas/DB-Value");
        AnalyzeSound();

    }

    void CalculateMeasures(int count)
    {
        //try
        //{
        //    total_dbValue += dbValue;
        //    total_pitchValue += pitchValue;
        //}
        //catch (Exception e)
        //{
        //    total_dbValue = 0;
        //    total_pitchValue = 0;
        //    count = 0;

        //}
        if (count > 0)
        {
            //avg_dbValue = total_dbValue / count;
            //avg_pitchValue = total_pitchValue / count;

            if (max_dbValue < dbValue)
            {
                max_dbValue = dbValue;
            }

            if (min_dbValue > dbValue)
            {
                min_dbValue = dbValue;
            }

            if (max_pitchValue < pitchValue)
            {
                max_pitchValue = pitchValue;
            }

            if (min_pitchValue > pitchValue)
            {
                min_pitchValue = pitchValue;
            }
        }


    }

    // Update is called once per frame
    void AnalyzeSound()
    {
        audio.GetOutputData(samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < qSamples; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        rmsValue = Mathf.Sqrt(sum / qSamples); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
                                            // get sound spectrum
        audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        int maxN = 0;
        for (i = 0; i < qSamples; i++)
        { // find max 
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
            }
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < qSamples - 1)
        { // interpolate index using neighbours
            float dL = spectrum[maxN - 1] / spectrum[maxN];
            float dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchValue = freqN * (fSample / 2) / qSamples; // convert index to frequency
    }



    void Update()
    {
        if (audio.isPlaying)
        {
            AnalyzeSound();
            count++;
            CalculateMeasures(count);

            if (count == 1)
            {
                max_dbValue = dbValue;
                min_dbValue = dbValue;
                max_pitchValue = pitchValue;
                min_pitchValue = pitchValue;
            }
                
            if(pitchValue >= lastPitchValue)
            {
                lastPitchValue = pitchValue;
            }
        
        }
    }

    public float GetPitchValue()
    {
        if (pitchValue >= lastPitchValue)
        {
            return 0;
        }

        var returnValue = lastPitchValue;
        lastPitchValue = 0;
        return returnValue;
    }
}
