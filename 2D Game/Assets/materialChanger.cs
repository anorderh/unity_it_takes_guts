using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialChanger : MonoBehaviour
{
    [SerializeField] public PhysicsMaterial2D stuck;
    [SerializeField] public PhysicsMaterial2D slippery;
    [SerializeField] public Collider2D inputCollider;

    private Rigidbody2D rb;
    private float horMove;
    private float verMove;
    private bool moving;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horMove = Mathf.Abs(Input.GetAxis("Horizontal"));
        verMove = Mathf.Abs(Input.GetAxis("Vertical"));

        if ((horMove > 0 || verMove > 0)) {
            inputCollider.sharedMaterial = slippery;
        } else {
            inputCollider.sharedMaterial = stuck;
        }
    }
}
