using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveName : MonoBehaviour
{
    [SerializeField]
    private NameSO savedName;

    public void Save(string newName) {
        savedName.playerName = newName;
    }
}
