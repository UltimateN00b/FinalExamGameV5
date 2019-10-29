using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmDiceSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name.Contains("GetDiceScene"))
        {
           for (int i = 1; i <= 3; i++)
            {
                PlayerDiceHolder.ChangeDice(i, "Common");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
