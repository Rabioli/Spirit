using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Collider collider;
    Vector3[] vertices;
    int[] triangles;
    public int xSize = 20, zSize=20;
    //Vector2[] uvs;
    Color[] colors;
    public Gradient gradient;
    float minTerrainHeight, maxTerrainHeight;
    public float scale = 10, offsetX=100, offsetZ=100;
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0f,999999f);
        offsetZ = Random.Range(0f, 999999f);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        StartCoroutine(CreateShape());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMesh();
        UpdateCollider();
        DeleteShape(car.transform.position);
    }
    IEnumerator CreateShape()
    {
        vertices = new Vector3[(xSize+1)*(zSize+1)];

        for(int z=0, i = 0; z<=zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float fx = (float)x / xSize*scale+offsetX;
                float fz = (float)z / zSize*scale+offsetZ;
                float y = Mathf.PerlinNoise(fx,fz)*2.5f;
                vertices[i] = new Vector3(x, y, z);
                if (y > maxTerrainHeight)
                {
                    maxTerrainHeight = y;
                }
                if (y < minTerrainHeight)
                {
                    minTerrainHeight = y;
                }
                i++;
            }
        }
        triangles = new int[xSize*zSize*6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert+0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris+=6;
                yield return new WaitForSeconds(.000005f);
            }
            vert++;
        }

        //uvs = new Vector2[vertices.Length];
        colors = new Color[vertices.Length];
        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                //uvs[i] = new Vector2((float)x/xSize, (float)z/zSize);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.colors = colors;
        //mesh.uv = uvs;
    }
    void DeleteShape(Vector3 carPosition){

    }

    void UpdateCollider(){

    }
}
