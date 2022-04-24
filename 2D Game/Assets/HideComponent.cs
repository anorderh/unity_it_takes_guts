using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideComponent : MonoBehaviour
{
    public bool hide;

    private CanvasGroup group;

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();

        if (hide) {
            group.alpha = 0f;
            group.blocksRaycasts = false;
        } else {
            group.alpha = 1f;
            group.blocksRaycasts = true;
        }
    }

    public void RevealComponent() {
        GetComponent<Animator>().SetTrigger("reveal");
        group.blocksRaycasts = true;
    }

    public void RevealText() {
        GetComponent<Animator>().SetTrigger("reveal");
    }
}
