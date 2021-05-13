using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private Image image;
    [SerializeField] private Text damage;
    [SerializeField] private Text energy;
    [SerializeField] private Text description;

    // Start is called before the first frame update
    private void Start()
    {
        CreateCard();
    }

    public void CreateCard()
    {
        image.sprite = data.image;
        damage.text = "" + data.damage;
        energy.text = "" + data.energy;
        description.text = "" + data.description;
       
    }

}
