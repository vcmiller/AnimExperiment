using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCreator : MonoBehaviour {
    public float[] radii;
    public GameObject baseObject;

    public SegmentControl[] segments { get; private set; }

    public float force = 8;
    public float headForce = 8;
    public float speed = 8;
    public float headSpeed = 4;

    private void Awake() {
        segments = new SegmentControl[radii.Length];

        float d = 0;

        for (int i = 0; i < radii.Length; i++) {
            d += radii[i];

            GameObject obj = Instantiate(baseObject);

            obj.transform.position = transform.position + Vector3.right * d;
            segments[i] = obj.AddComponent<SegmentControl>();
            obj.GetComponent<CircleCollider2D>().radius = radii[i];
            float f = radii[i] * 2f;

            segments[i].speed = i == radii.Length - 1 ? headSpeed : speed;
            segments[i].force = i == radii.Length - 1 ? headForce : force;

            if (i > 0) {
                segments[i].prev = segments[i - 1];
                segments[i - 1].next = segments[i];
                segments[i - 1].dist = radii[i - 1] + radii[i] + 0.1f;
            }


            d += radii[i];
        }
    }
}
