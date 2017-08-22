using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentControl : MonoBehaviour {
    public SegmentControl next;
    public SegmentControl prev;

    public Rigidbody2D rb { get; private set; }

    public float force;
    public float speed;
    public float dist = 1.2f;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {
        Vector3 targetPos;
        if (next) {
            targetPos = next.transform.position + (transform.position - next.transform.position).normalized * dist;
        } else {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Vector2 dir = targetPos - transform.position;
        if (dir.magnitude > 1) {
            dir = dir.normalized;
        }

        rb.AddForce((dir * speed - rb.velocity).normalized * force, ForceMode2D.Force);

        Vector2 right = transform.right;

        if (next && prev) {
            right = Vector3.Lerp(Vector3.Normalize(transform.position - prev.transform.position), Vector3.Normalize(next.transform.position - transform.position), 0.5f);
        } else if (next) {
            right = Vector3.Normalize(next.transform.position - transform.position);
        } else if (prev) {
            right = Vector3.Normalize(transform.position - prev.transform.position);
        }

        Vector3 up = Vector3.Cross(right, Vector3.forward);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, up);
    }
}
