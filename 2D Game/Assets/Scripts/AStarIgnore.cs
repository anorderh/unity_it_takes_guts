using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarIgnore : MonoBehaviour
{
    private Collider2D[] colliders;
    // Start is called before the first frame update
    void Start()
    {
       colliders = GetComponentsInChildren<Collider2D>(); 
       foreach(Collider2D collider in colliders) {
        collider.enabled = false;
       }
    }
}
