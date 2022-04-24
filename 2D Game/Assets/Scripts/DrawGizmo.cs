using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public Transform point;
    public float radius;

    void Update() {
        OnDrawGizmosSelected();
    }

    void OnDrawGizmosSelected() {
        if (point == null) {
            return;
        }

        Gizmos.DrawWireSphere(point.position, radius);
    }
}
