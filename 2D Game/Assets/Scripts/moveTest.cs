using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class moveTest : MonoBehaviour
{
    public AIPath path;
    public Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = path.destination;
    }
}
