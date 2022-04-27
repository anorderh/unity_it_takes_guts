using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColor : MonoBehaviour
{
    [SerializeField]
    private Image playerPreview;

    public void Change(Color newColor)
    {
        playerPreview.color = newColor;
    }
}
