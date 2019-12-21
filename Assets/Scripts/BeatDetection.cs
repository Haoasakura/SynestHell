using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetection : MonoBehaviour
{
    public int ringBufferSize = 120;
    public int bufferSize;
    public int samplingRate = 44100;
    public bool limitBeats;
    public int limitedAmmount;
    public float beatIndicationThreshold;


    private const int bands = 12;
    private const int maximumLag=100;
    private const float smoothDecay=0.997f;

    private AudioSource audioSource;
    private AudioDataLoadState audioData;
    private int framesSinceBeat;
    private float framePeriod;
    private int currentRingBufferPosition;

    private float[] spectrum;
    private float[] previousSpectrum;
    private float[] averagePowerBand;
    private float[] onsets;
    private float[] notation;
    private void Start() {
        
    }
}
