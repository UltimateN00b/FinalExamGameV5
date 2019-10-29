using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDaySceneStarter : MonoBehaviour
{
    private static int _numDays = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("CURR NUM DAYS: " + _numDays);
        if (SceneManager.GetActiveScene().name.Contains("TheNextDay"))
        {
            if (_numDays < 3)
            {
                _numDays++;
            }
        }
    }

    public static void NextDay()
    {
        int upcomingNumDays = _numDays + 1;

        if (upcomingNumDays >= 3)
        {
            upcomingNumDays = 3;
        }

        GameObject.Find("FadeCanvas 1").GetComponent<FadeCanvasLegacy>().ChangeScene("TheNextDay" + upcomingNumDays);
    }

    public static int GetDayNum()
    {
        return _numDays;
    }
}
