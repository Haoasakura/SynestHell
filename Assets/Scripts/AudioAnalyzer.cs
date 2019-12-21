using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VisualizationMode {Ring, RingWithBeat}
public class AudioAnalyzer : MonoBehaviour
{
    public int m_bufferSampleSize;
    public float samplePercentage;
    public float emphasisMultiplier;
    public float retractionSpeed;
    public int amountOfSegments;
    public float radius;
    public float bufferSizeArea;
    public float maximumExtendLenght;
    public float[] samples;
    public float[] spectrum;
    public float[] extendLengths;
    private float sampleRate;

    public GameObject lineRenderer;
    public Material lineRendererMaterial;
    public VisualizationMode visualizationMode;

    public Gradient colorGradientA;
    public Gradient colorGradientB;
    private Gradient currentColor;
    private LineRenderer[] lineRenderers;
    private AudioSource audioSource;

    private void Awake() {
        audioSource=GetComponent<AudioSource>();
        sampleRate=AudioSettings.outputSampleRate;
        samples= new float[m_bufferSampleSize];
        spectrum = new float[m_bufferSampleSize];

        switch(visualizationMode) {
            case VisualizationMode.Ring:
            InitiateRing();
            break;
        }
    }

    private void InitiateRing() {
        extendLengths = new float[amountOfSegments+1];
        lineRenderers = new LineRenderer[extendLengths.Length];

        for (int i = 0; i < lineRenderers.Length; i++)
        {
            GameObject go = Instantiate(lineRenderer,transform.position,Quaternion.identity);

            LineRenderer lineRender =go.GetComponent<LineRenderer>();
            lineRender.sharedMaterial = lineRendererMaterial;

            lineRender.positionCount=2;
            lineRender.useWorldSpace=true;
            lineRenderers[i]=lineRender; 
        }
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrum,0,FFTWindow.BlackmanHarris);
         float sum=0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            sum+=spectrum[i];
        }
        sum/=spectrum.Length;
        print("spectrum  "+ sum);
        //samples=spectrum;
        UpdateExtends();

        if(visualizationMode==VisualizationMode.Ring)
            UpdateRing();
    }

    private void UpdateExtends(){
        int iteration = 0;
        int indexOnSpectrum = 0;
        int avarageValue=(int) (Mathf.Abs(samples.Length * samplePercentage)/amountOfSegments);

        if(avarageValue<1)
            avarageValue=1;
        
        while(iteration<amountOfSegments) {
            int iteraionIndex=0;
            float sumValueY = 0;

            while (iteraionIndex<avarageValue)
            {
                sumValueY += spectrum[indexOnSpectrum];
                indexOnSpectrum++;
                iteraionIndex++;
            }

            float y= sumValueY/avarageValue*emphasisMultiplier;
            extendLengths[iteration]-=retractionSpeed*Time.deltaTime;

            if(extendLengths[iteration]<y)
                extendLengths[iteration]=y;
            
            if(extendLengths[iteration]>maximumExtendLenght)
                extendLengths[iteration]=maximumExtendLenght;
            
            iteration++;
        }
        float sum=0;
        for (int i = 0; i < extendLengths.Length; i++)
        {
            sum+=extendLengths[i];
        }
        sum/=extendLengths.Length;
        print("extendend   "+sum);
    }

    private void UpdateRing() {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            float t = i/(lineRenderers.Length-2f);
            float a = t*Mathf.PI*2f;

            Vector2 direction = new Vector2(Mathf.Cos(a), Mathf.Sin(a));
            float maximumRadius=(radius+bufferSizeArea+extendLengths[i]);

            Vector2 pos=new Vector2(transform.position.x,transform.position.y);
            lineRenderers[i].SetPosition(0,pos+ direction*radius);
            lineRenderers[i].SetPosition(1,pos+direction*maximumRadius);

            lineRenderers[i].startWidth = Spacing(radius);
            lineRenderers[i].endWidth = Spacing(maximumRadius);

            lineRenderers[i].startColor = colorGradientA.Evaluate(0);
            lineRenderers[i].endColor = colorGradientB.Evaluate((extendLengths[i]-1)/(maximumExtendLenght-1f));
        }
    }

    private float Spacing(float radius) {
        float c=2f*Mathf.PI*radius;
        float n=lineRenderers.Length;
        return c/n;
    }
}
