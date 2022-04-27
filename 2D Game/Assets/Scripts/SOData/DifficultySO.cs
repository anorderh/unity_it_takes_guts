using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DifficultySO : ScriptableObject
{
    [SerializeField]
    private int[] enemyInfo = new int[]{10, 3};

    public int[] inputEnemies {
        set {enemyInfo = value;}
        get {return enemyInfo;}
    }

    public void Reset() {
        enemyInfo = new int[]{10, 3};
    }
}
