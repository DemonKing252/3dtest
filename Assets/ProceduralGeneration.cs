using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public Vector3[] verticies;
    public Mesh mesh;

    int seed = 0;

    // Start is called before the first frame update
    void Start()
    {
        seed = Random.Range(0, 100000);
        mesh = GetComponent<MeshFilter>().mesh;

        verticies = mesh.vertices;

        for (int i = 0; i < verticies.Length; i++)
        {
            //print("Noise: " + verticies[i].x.ToString("F1") + " " + verticies[i].z.ToString("F1"));
            verticies[i] = new Vector3(verticies[i].x, Mathf.PerlinNoise((float)seed + verticies[i].x * 0.15f, (float)seed + verticies[i].z * 0.15f) * 15f, verticies[i].z);
        }
        mesh.vertices = verticies;
        GetComponent<MeshCollider>().sharedMesh.RecalculateBounds();
        GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
        GetComponent<MeshFilter>().sharedMesh.RecalculateTangents();

    }
    public float x = 0f, y = 0f;

    // Update is called once per frame
    void Update()
    {

        
    }
}
