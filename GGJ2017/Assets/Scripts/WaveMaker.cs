using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaker : MonoBehaviour {

    public float speed = 1.0f;
    public float Height = 1.0f;
    private Vector3[] baseMeshHeight = null;
    public bool useOriginal = false;
    public bool withCollider = false;

    public void Start()
    {
        
    }

    public void Update()
    {
        var mesh = (GetComponent(typeof(MeshFilter)) as MeshFilter).mesh;

        if (baseMeshHeight == null)
        {
            baseMeshHeight = mesh.vertices;
        }

        WaveMesh(baseMeshHeight, mesh);

        Destroy(GetComponent(typeof(MeshCollider)));

        if (withCollider)
        {

            var collider = GetComponent(typeof(MeshCollider)) as MeshCollider;
            if (collider == null)
            {
                collider = this.gameObject.AddComponent<MeshCollider>();
            }
        }

    }

    private void WaveMesh(Vector3[] baseHeight, Mesh mesh)
    {
        //

        var vertices = new Vector3[baseHeight.Length];
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = baseHeight[i];

            if (useOriginal)
            {
                vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * Height;
            }
            else
            {
                vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y) * (Height * .5f) + Mathf.Sin(Time.time * speed + baseHeight[i].z + baseHeight[i].y) * (Height * .5f);
            }

            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
