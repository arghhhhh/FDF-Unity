using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator_5 : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap, 
        ColorMap
    };
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;
    public void GenerateMap()
    {
        float[,] noiseMap = Noise_5.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y=0; y<mapHeight; y++) {
            for (int x=0; x<mapWidth; x++) {
                float currentHeight = noiseMap[x, y];
                for (int i=0; i<regions.Length; i++) {
                    if (currentHeight <= regions[i].height) {
                        if ((i != 0) && (i < regions.Length))
                        {
                            float blendRegion = Mathf.InverseLerp(regions[i - 1].height, regions[i].height, currentHeight);
                            colorMap[y * mapWidth + x] = Color.Lerp(regions[i - 1].color, regions[i].color, blendRegion);
                        }
                        else colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay_5 display = FindObjectOfType<MapDisplay_5>();
        if (drawMode == DrawMode.NoiseMap)
            display.DrawTexture(TextureGenerator_5.TextureFromHeightMap(noiseMap));
        else if (drawMode == DrawMode.ColorMap)
            display.DrawTexture(TextureGenerator_5.TextureFromColorMap(colorMap, mapWidth, mapHeight));
    }

    void OnValidate() //called whenever an inspector value is changed
    {
        if (mapWidth < 1)
            mapWidth = 1;
        if (mapHeight < 1)
            mapHeight = 1;
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}
