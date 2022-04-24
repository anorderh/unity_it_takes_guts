using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpdate : MonoBehaviour
{
    private float maxScale;
   
    void Start() {
        maxScale = transform.localScale.x;
    }

    public void UpdateBar(float curHealth, float maxHealth) {
        if (curHealth < 0) {
            curHealth = 0;
        }

        transform.localScale = new Vector3((curHealth / maxHealth)*maxScale, transform.localScale.y, transform.localScale.z);
    }

}
