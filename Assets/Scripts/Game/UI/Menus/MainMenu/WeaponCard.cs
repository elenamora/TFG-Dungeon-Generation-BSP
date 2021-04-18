using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private Image image;
    [SerializeField] private Text damage;
    [SerializeField] private Text range;
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
        range.text = "" + data.range;
        description.text = "" + data.description;
       
    }

}
