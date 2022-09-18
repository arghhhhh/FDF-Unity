using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joss.Helpers;

public class AudioEffector_5 : MonoBehaviour
{
    private AudioPeer audioPeer;
    public MapGenerator_5 mapGen;

    private float noiseScale;

    private int octaves; //needs to be >= 0
    private float persistance; //needs to be between 0 & 1
    [Range(0,7)]
    public int persistanceBand;

    private float lacunarity; // needs to be > 1
    [Range(0, 7)]
    public int lacunarityBand;

    private int seed;
    private Vector2 offset;

    public int noiseMin;
    public int noiseMax;

    [Range(0, 7)]
    public int noiseOffsetBand;
    public float noiseMult;
    [Range(0,0.99f)]
    public float freqVariance;

    public int songBPM;
    private float beatDuration;
    private float prevBeatTime = 0;
    private int beatCount = 1;
    private float tempOffset;
    [Range(0,1f)]
    public float beatOffset;

    private bool offsetAxis;
    private bool offsetDirection;

    [Range(0.0001f, 1f)]
    public float offsetIntensity;

    private void Awake()
    {
        audioPeer = GetComponent<AudioPeer>();
        beatDuration = 60f / songBPM;
    }
    void Update()
    {
        SetScale();
        mapGen.noiseScale = noiseScale;

        octaves = Mathf.RoundToInt(Mathf.Lerp(1f, 4f, audioPeer._AmplitudeBuffer));
        mapGen.octaves = octaves;

        persistance = audioPeer._audioBandBuffer[persistanceBand];
        mapGen.persistance = persistance;

        lacunarity = Toolbox.Remap(audioPeer._audioBandBuffer[lacunarityBand], 0, 1f, 1.75f, 5f);
        mapGen.lacunarity = lacunarity;


        SetOffsetAndSeed();
        mapGen.offset = offset;
        mapGen.seed = seed;

        mapGen.GenerateMap(); //update map

        SetColor();
    }

    void SetScale()
    {
        float noiseOffset = audioPeer._audioBand[noiseOffsetBand] / 10f;
        float scaleFreq = Toolbox.Remap(audioPeer._AmplitudeBuffer, 0, 1f, 1 - freqVariance, 1 + freqVariance);
        float tempScale = noiseMult * Mathf.Sin(scaleFreq * (Time.time - noiseOffset));
        noiseScale = Toolbox.Remap(tempScale, -1, 1, noiseMin, noiseMax);
        
    }


    void SetOffsetAndSeed()
    {
        float currTime = Time.time + beatOffset;
        if (currTime - prevBeatTime > beatDuration)
        {
            prevBeatTime = currTime;
            tempOffset = audioPeer._Amplitude;
            Debug.Log(tempOffset);
            offsetAxis = Toolbox.RandomBool();
            offsetDirection = Toolbox.RandomBool();
            beatCount++;
            if (beatCount == 4)
            {
                seed++;
                beatCount = 0;
            }
        }
        
        if (offsetAxis)
        {
            if (offsetDirection)
                offset.x += (tempOffset * offsetIntensity);
            else
                offset.x -= (tempOffset * offsetIntensity);
        }
            
        else
        {
            if (offsetDirection)
                offset.y += (tempOffset * offsetIntensity);
            else
                offset.y -= (tempOffset * offsetIntensity);
        }
    }

    void SetColor() 
    {
        Color newColor = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        int randomRegion = Random.Range(0, mapGen.regions.Length + 5);
        if (randomRegion < mapGen.regions.Length)
            mapGen.regions[randomRegion].color = newColor;
    }
}
