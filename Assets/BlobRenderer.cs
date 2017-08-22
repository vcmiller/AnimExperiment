using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobRenderer : MonoBehaviour {
    public LineRenderer line { get; private set; }
    public BlobMesh mesh { get; private set; }

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        mesh = GetComponent<BlobMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        line.positionCount = mesh.vertices.Length;
        line.SetPositions(mesh.vertices);
	}
}
