using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public void Start()
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        SceneManager.LoadScene("BSP");
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;

    }
}
