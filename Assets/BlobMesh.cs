using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMesh : MonoBehaviour {
    public int vertexCount = 32;
    
    public Vector3[] vertices { get; private set; }
    public SegmentCreator creator { get; private set; }

    private Spline spline;

    private void Start() {
        vertices = new Vector3[vertexCount];
        creator = GetComponent<SegmentCreator>();
    }

    // Update is called once per frame
    void Update () {
        List<Vector3> pointsTemp = new List<Vector3>();
        
        for (int i = 0; i < creator.radii.Length; i++) {
            pointsTemp.Add(creator.segments[i].transform.position + creator.segments[i].transform.up * creator.radii[i]);
        }

        for (int i = creator.radii.Length - 1; i >= 0; i--) {
            pointsTemp.Add(creator.segments[i].transform.position - creator.segments[i].transform.up * creator.radii[i]);
        }

        pointsTemp.Add(pointsTemp[0]);

        Vector3[] splinePoints = pointsTemp.ToArray();
        Vector3[] tagnents = Spline.CalculateTangents(splinePoints, 10, true);

        spline = new Spline(splinePoints, tagnents, 64);


        for (int i = 0; i < vertexCount; i++) {
            vertices[i] = spline.getLocation(i * 1.0f / vertexCount, false);
        }
    }
}
