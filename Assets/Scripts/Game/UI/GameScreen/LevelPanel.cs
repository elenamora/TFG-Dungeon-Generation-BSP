using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    public GameState gameState;
    public GameEvent startLevel;
    public GameObject levelScreen;
    [SerializeField] private Text numLevel;

    public void Start()
    {
        Time.timeScale = 0f;
        numLevel.text = "Level " + gameState.level;
        
        

    }

    public void StartGame()
    {
        StartCoroutine(StartCo());
    }


    IEnumerator StartCo()
    {
        yield return new WaitForSeconds(0.5f);
        levelScreen.SetActive(false);
        Time.timeScale = 1f;

        //startLevel.Raise();
    }



}
