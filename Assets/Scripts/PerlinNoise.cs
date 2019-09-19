using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256, height = 256;
    public float scale = 10, offsetX=100, offsetY=100;
    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0f,999999f);
        offsetY = Random.Range(0f, 999999f);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }
    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                Color color =CalculateColor(x,y);
                texture.SetPixel(x,y,color);
            }
        }
        texture.Apply();
        return texture;
    }
    Color CalculateColor(int x, int y)
    {
        float fx = (float)x / width*scale+offsetX;
        float fy = (float)y / height*scale+offsetY;
        float samplex = Mathf.PerlinNoise(fx, fy);
        float sampley = Mathf.PerlinNoise(fx, fy);
        float samplez= Mathf.PerlinNoise(fx, fy);
        return new Color(sampley, samplez, samplex);
    }
}
