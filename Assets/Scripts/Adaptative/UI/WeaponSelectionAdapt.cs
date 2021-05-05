using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelectionAdapt : MonoBehaviour
{
    private int index = 0;

    public void ChooseWeapon()
    {
        PlayerPrefs.SetInt("WeaponSelected", index);
        SceneManager.LoadScene("BSPAdapt");
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
