using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : BasicInventoryManager
{
    [SerializeField] private Text description;
    [SerializeField] private GameObject button;

    public void SetText(string description)
    {
        this.description.text = description;
    }
    public void SetButtonActive(bool buttonActive)
    {
        if (buttonActive) { button.SetActive(true); }
        else { button.SetActive(false); }
    }

    // OnEnable is called every time an object goes from being disabled to enabled
    void Start()
    {
        SetText("");
        SetButtonActive(false);
    }

    void Update()
    {
        SetText("");
    }

}
