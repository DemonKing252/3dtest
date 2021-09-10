using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public int[] indicies;
    public Vector3[] verticies;
    
    public Mesh mesh;

    private float scaleMultiplier = 0f;
    int seed = 0;

    public float heightMapY = 400f;
    public int sizeX = 20;
    public int sizeZ = 20;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        verticies = new Vector3[(sizeX + 1)* (sizeZ + 1)];

         
        for (int i = 0, z = 0; z <= sizeZ; z++)
        {

            for(int x = 0; x <= sizeX; x++)
            {
                float y = Mathf.PerlinNoise(x / 20f, z / 20f) * heightMapY;

                verticies[i] = new Vector3(x, y, z);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;
        indicies = new int[sizeX * sizeZ * 6];


        for (int z = 0; z < sizeZ; z++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                indicies[tris + 0] = vert + 0;
                indicies[tris + 1] = vert + sizeX + 1;
                indicies[tris + 2] = vert + 1;
                indicies[tris + 3] = vert + 1;
                indicies[tris + 4] = vert + sizeX + 1;
                indicies[tris + 5] = vert + sizeX + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        


        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = indicies;
        mesh.RecalculateNormals();


        float min = Mathf.Infinity;
        for (int i = 0; i < verticies.Length; i++)
        {
            if (verticies[i].y < min)
                min = verticies[i].y;
        }
        Debug.Log("Min y: " + min.ToString());



        print("dakota");


        float max = 0f;
        for (int i = 0; i < verticies.Length; i++)
        {
            if (verticies[i].y > max)
                max = verticies[i].y;
        }
        Debug.Log("Max y: " + max.ToString());



        //scaleMultiplier = 5f * transform.localScale.x;
        //seed = Random.Range(0, 100000);
        //mesh = GetComponent<MeshFilter>().mesh;
        //
        //verticies = mesh.vertices;
        //
        //for (int i = 0; i < verticies.Length; i++)
        //{
        //    //print("Noise: " + verticies[i].x.ToString("F1") + " " + verticies[i].z.ToString("F1"));
        //    verticies[i] = new Vector3(verticies[i].x, Mathf.PerlinNoise((float)seed + verticies[i].x * 0.15f, (float)seed + verticies[i].z * 0.15f) * 155f, verticies[i].z);
        //}
        //mesh.vertices = verticies;
        //GetComponent<MeshCollider>().sharedMesh.RecalculateBounds();
        //GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
        //GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();

    }
    //public float x = 0f, y = 0f;

    // Update is called once per frame
    void Update()
    {

        
    }
}
