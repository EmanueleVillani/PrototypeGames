using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRandomPoint : MonoBehaviour
{

    List<Vector3> VerticeList = new List<Vector3>(); //List of local vertices on the plane
    List<Vector3> Corners = new List<Vector3>();
    public int sphereSize = 1;
    Vector3 RandomPoint;
    List<Vector3> EdgeVectors = new List<Vector3>();

    void OnDrawGizmos()
    {
        if (VerticeList.Count > 0)
            for (int a = 0; a < Corners.Count; a++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(Corners[a], sphereSize); //show coordinate as a sphere on editor
            }

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(RandomPoint, sphereSize); //show coordinate of the random point as a sphere on editor

    }


    // Start is called before the first frame update
    void Start()
    {
        VerticeList = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices); //get vertice points from the mesh of the object
        CalculateCornerPoints();
    }

    void Update()
    {
        CalculateCornerPoints(); //To show corner points with transform change
    }

    void CalculateEdgeVectors(int VectorCorner)
    {
        EdgeVectors.Clear();

        EdgeVectors.Add(Corners[3] - Corners[VectorCorner]);
        EdgeVectors.Add(Corners[1] - Corners[VectorCorner]);
    }

    public void CalculateRandomPoint()
    {
        int randomCornerIdx = Random.Range(0, 2) == 0 ? 0 : 2; //there is two triangles in a plane, which tirangle contains the random point is chosen
                                                               //corner point is chosen for triangles as the variable
        CalculateEdgeVectors(randomCornerIdx); //in case of transform changes edge vectors change too

        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);

        if (v + u > 1) //sum of coordinates should be smaller than 1 for the point be inside the triangle
        {
            v = 1 - v;
            u = 1 - u;
        }

        RandomPoint = Corners[randomCornerIdx] + u * EdgeVectors[0] + v * EdgeVectors[1];
    }
    public void CalculateCornerPoints()
    {
        Corners.Clear(); //in case of transform changes corner points are reset

        Corners.Add(transform.TransformPoint(VerticeList[0])); //corner points are added to show  on the editor
        Corners.Add(transform.TransformPoint(VerticeList[10]));
        Corners.Add(transform.TransformPoint(VerticeList[110]));
        Corners.Add(transform.TransformPoint(VerticeList[120]));
    }
}