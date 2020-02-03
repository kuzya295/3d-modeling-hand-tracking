using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    int curVertex = -1;
    Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices;

    void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
    }

    void Update()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            originalVertices[i] = displacedVertices[i];
        }
        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();

        gameObject.GetComponent<MeshCollider>().sharedMesh = null;
        gameObject.GetComponent<MeshCollider>().sharedMesh = deformingMesh;

    }

    public void AddDeformingForce(Vector3 point, Vector3 fingers, bool isOldPoint)
    {
        if (!isOldPoint)
        {
            bool flag = false;
            for (int i = 0; i < displacedVertices.Length; i++)
            {
                if (displacedVertices[i].ToString() == point.ToString())
                {
                    curVertex = i;
                    flag = true;
                }
            }

            if (!flag) {
                float minDistanceSqr = Mathf.Infinity;
                int nearestVertex = int.MaxValue;
                for (int i = 0; i < displacedVertices.Length; i++) {
                    float distSqr = (fingers - displacedVertices[i]).sqrMagnitude;
                    if (distSqr <= minDistanceSqr)
                    {
                        minDistanceSqr = distSqr;
                        nearestVertex = i;
                    }
                }
                curVertex = nearestVertex;
            }
        }


        if (curVertex != -1)
        {
            
            
            displacedVertices[curVertex] = fingers;
            Vector3 displacement = displacedVertices[curVertex] - originalVertices[curVertex];
            for (int i = 0; i < displacedVertices.Length; i++)
            {
                AddForceToVertex(i, displacement);
            }
        }
    }

    void AddForceToVertex(int i, Vector3 displacement)
    {
        float k = (float)(1.0 / (1.0 + 5 * Vector3.Distance(originalVertices[i], originalVertices[curVertex]) * Vector3.Distance(originalVertices[i], originalVertices[curVertex])));
        displacedVertices[i] = originalVertices[i] + displacement * k;
    }
}



