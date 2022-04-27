using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DifficultySO : ScriptableObject
{
    [SerializeField]
    private int[] enemyInfo;

    public int[] inputEnemies {
        set {enemyInfo = value;}
        get {return enemyInfo;}
    }
}