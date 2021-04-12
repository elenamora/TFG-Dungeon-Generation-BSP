using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject hint;

    public void Enable()
    {
        hint.SetActive(true);
    }

    public void Disable()
    {
        hint.SetActive(false);

    }

}
