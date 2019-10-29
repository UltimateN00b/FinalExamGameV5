using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDiceHolder : MonoBehaviour
{

    private static string dice1 = "Common";
    private static string dice2 = "Empty";
    private static string dice3 = "Empty";

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
        if (SceneManager.GetActiveScene().name.Contains("GetDiceScene"))
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    public static List<string> GetDiceList()
    {
        List<string> diceList = new List<string>();

        diceList.Add(dice1);
        diceList.Add(dice2);
        diceList.Add(dice3);

        return diceList;
    }

    public static void ChangeDice(int diceNum, string name)
    {
        switch (diceNum)
        {
            case 1:
                dice1 = name;
                break;
            case 2:
                dice2 = name;
                break;
            case 3:
                dice3 = name;
                break;
            default:
                dice1 = name;
                break;
        }
    }

    public static string GetDiceType(int diceNum)
    {
        switch (diceNum)
        {
            case 1:
                return dice1;
            case 2:
                return dice2;
            default:
                return dice3;
        }
    }

    private void Hide()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Show()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
