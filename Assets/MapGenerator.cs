using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapGenerator : MonoBehaviour
{
    public Tilemap waterMap;
    public Tilemap groundMap;

    public List<Tile> water;
    public List<Tile> ground;

    //perlin noise algorithm
    public float[][] perlinNoise;
    public int width;
    public int height;
    public int octave;
    private System.Random sysRandom;
    // Start is called before the first frame update
    void Start()
    {
        sysRandom = new System.Random();
        FixArray();
        initialMap();
    }

    private void initialMap()
    {
        for (int i = 0; i < perlinNoise.Length; i++)
        {
            for (int j = 0; j < perlinNoise[i].Length; j++)
            {
                GetColorByFloat(perlinNoise[i][j], i, j);
            }
        }
    }

    private float[][] GeneratePerlinNoise(float[][] baseNoise, int octaveCount)
    {
        int width = baseNoise.Length;
        int height = baseNoise[0].Length;

        float[][][] smoothNoise = new float[octaveCount][][]; //an array of 2D arrays containing
        float persistance = 0.5f;
        //generate smooth noise
        for (int i = 0; i < octaveCount; i++)
        {
            smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);
        }

        float[][] perlinNoise = new float[width][];
        for (int i = 0; i < width; i++)
        {
            perlinNoise[i] = new float[height];
        }
        float amplitude = 1.0f;
        float totalAmplitude = 0.0f;

        //blend noise together
        for (int octave = octaveCount - 1; octave >= 0; octave--)
        {
            amplitude *= persistance;
            totalAmplitude += amplitude;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    perlinNoise[i][j] += smoothNoise[octave][i][j] * amplitude;
                }
            }
        }
        //normalisation
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                perlinNoise[i][j] /= totalAmplitude;
            }
        }

        return perlinNoise;

    }

    private float[][] GenerateSmoothNoise(float[][] baseNoise, int octave)
    {
        int width = baseNoise.Length;
        int height = baseNoise[0].Length;

        float[][] smoothNoise = new float[width][];
        for (int i = 0; i < width; i++)
        {
            smoothNoise[i] = new float[height];
        }
        int samplePeriod = 1 << octave;
        float sampleFrequency = 1.0f / samplePeriod;
        for (int i = 0; i < width; i++)
        {
            //calculate the horizontal sampling indices
            int sample_i0 = (i / samplePeriod) * samplePeriod;
            int sample_i1 = (sample_i0 + samplePeriod) % width; //wrap around
            float horizontal_blend = (i - sample_i0) * sampleFrequency;

            for (int j = 0; j < height; j++)
            {
                //calculate the vertical sampling indices
                int sample_j0 = (j / samplePeriod) * samplePeriod;
                int sample_j1 = (sample_j0 + samplePeriod) % height; //wrap around
                float vertical_blend = (j - sample_j0) * sampleFrequency;

                //blend the top two corners
                float top = Interpolate(baseNoise[sample_i0][sample_j0],
                   baseNoise[sample_i1][sample_j0], horizontal_blend);

                //blend the bottom two corners
                float bottom = Interpolate(baseNoise[sample_i0][sample_j1],
                   baseNoise[sample_i1][sample_j1], horizontal_blend);

                //final blend
                smoothNoise[i][j] = Interpolate(top, bottom, vertical_blend);
            }
        }

        return smoothNoise;
    }

    private float Interpolate(float x0, float x1, float alpha)
    {
        return x0 * (1 - alpha) + alpha * x1;
    }

    private float[][] GenerateWhiteNoise(int v1, int v2, int v3)
    {
        float[][] noise = new float[width][];
        for (int i = 0; i < width; i++)
        {
            noise[i] = new float[height];
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                noise[i][j] = (float)sysRandom.NextDouble() % 1;
            }
        }
        return noise;
    }

    private void FixArray()
    {
        float[][] whiteNoise = GenerateWhiteNoise(width, height, sysRandom.Next());
        perlinNoise = GeneratePerlinNoise(whiteNoise, 5);

    }

    public void GetColorByFloat(float noise, int i, int j)
    {
        noise = noise * 255;

        if (noise > 190)
        {
            waterMap.SetTile(new Vector3Int(i-width/2, j- height / 2, 0), water[sysRandom.Next(water.Count)]);
        }
        else {
            groundMap.SetTile(new Vector3Int(i - width / 2, j - height / 2, 0), ground[sysRandom.Next(ground.Count)]);
        }

    }
}
