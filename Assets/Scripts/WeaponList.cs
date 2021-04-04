using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponList : MonoBehaviour
{
    private GameObject[] weapons;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("WeaponSelected");
        weapons = new GameObject[transform.childCount];

        // Fill the Array with our weapons
        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i).gameObject;
        }

        // Deactivate all weapons
        foreach (GameObject go in weapons) { go.SetActive(false); }
       
        if (weapons[index]) { weapons[index].SetActive(true); }
    }


    public void ChooseWeapon()
    {
        index = 0;
        PlayerPrefs.SetInt("WeaponSelected", index);
        SceneManager.LoadScene("BSP");
    }
}
