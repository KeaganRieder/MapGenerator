using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* about PerlinNoiseMap
 * contains a function to generate a perlin noise map and return it
 * as a float[,]
 */
public class PerlinNoiseMap : NoiseMap
{
	public PerlinNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
		this.mapWidth = mapWidth;
		this.mapHeight = mapHeight;
		Generate(seed, scale, octaves, persistance, lacunarity, offset);
	}

	public void Generate(int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
		noiseMap = new float[mapWidth, mapHeight];

        //making map octaves have a different seed to allow for better 
        //layering of terrain
        System.Random randomSeed = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = randomSeed.Next(-100000, 100000) + offset.x;
            float offsetY = randomSeed.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{

				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				//layering noise maps based on octaves
				for (int i = 0; i < octaves; i++)
				{
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
					float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight)
				{
					maxNoiseHeight = noiseHeight;
				}
				else if (noiseHeight < minNoiseHeight)
				{
					minNoiseHeight = noiseHeight;
				}
				noiseMap[x, y] = noiseHeight;
			}
		}

		//normilizing noise map values to ensure they are within the range
		//0 and 1
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
			}
		}


		//return noiseMap;
    }
}

/* about NoiseMap
 * contains a functions that handling manipulating noise maps
 */
public class NoiseMap
{
	protected float[,] noiseMap;
	protected int mapWidth;
	protected int mapHeight;

	public float[,] GetNoiseMap()
    {
		return noiseMap;
    }
	public void ApplyCurve(AnimationCurve curve, float heightCurveMultipler)
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
				noiseMap[x, y] = curve.Evaluate(noiseMap[x, y] * heightCurveMultipler);

			}
        }
    }
}
