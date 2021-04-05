using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;

    public List<ScriptableObject> objects = new List<ScriptableObject>();

    private void Awake()
    {
        // We only want one gameSave object
        if ( gameSave == null) { gameSave = this; }

        else { Destroy(this.gameObject); }

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
