using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    private int index = 0;


    public void ChooseWeapon()
    {
        PlayerPrefs.SetInt("WeaponSelected", index);
        SceneManager.LoadScene("BSP");
    }

    public void SelectSword()
    {
        index = 0;
        ChooseWeapon();
    }

    public void SelectAxe()
    {
        index = 2;
        ChooseWeapon();
    }

    public void SelectHammer()
    {
        index = 1;
        ChooseWeapon();
    }

}
