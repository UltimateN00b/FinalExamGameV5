using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SleepValueHolder : MonoBehaviour
{
    public static float _sleepValue;
    private static string _fightOutcomeString;
    private static float _amountChanged;

    public float viewSleepValue;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _sleepValue = 0.5f;
        _fightOutcomeString = "";

        _amountChanged = 0f;
    }

    private void Update()
    {
        viewSleepValue = _sleepValue;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name.Contains("YouWin") || SceneManager.GetActiveScene().name.Contains("YouLose"))
        {
            ChangeFightOutputString();
            SurrenderButton.SetSurrendered(false);
        }
    }

    public static float GetSleepValue()
    {
        return _sleepValue;
    }

    public void UpdateSleepValue()
    {
        //Calculate sleep meter change according to player and enemy health

        _amountChanged = 0;

        Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Character enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Character>();

        float winnersHealth = 0;
        float losersHealth = 0;

        if (SurrenderButton.HasSurrendered())
        {
            winnersHealth = enemy.GetCurrHP() / enemy.hp;
            losersHealth = player.GetCurrHP() / player.hp;
        }
        else if (player.GetCurrHP() > 0)
        {
            winnersHealth = player.GetCurrHP() / player.hp;
        }
        else if (enemy.GetCurrHP() > 0)
        {
            winnersHealth = enemy.GetCurrHP() / enemy.hp;
        }

        if (SurrenderButton.HasSurrendered())
        {
            _amountChanged = Mathf.Abs(winnersHealth)/Mathf.Abs(losersHealth);
        }
        else
        {
            _amountChanged = Mathf.Abs(winnersHealth) + Mathf.Abs(losersHealth);
        }

        _amountChanged = _amountChanged * 0.2f;

        //Change the output string.
        if (SurrenderButton.HasSurrendered() || enemy.GetCurrHP() > 0)
        {
            _fightOutcomeString = "Your sleep quality has declined.";
        }else if (player.GetCurrHP() > 0)
        {
            _fightOutcomeString = "Your sleep quality has improved!";
        }
    }

    private void ChangeFightOutputString()
    {
        //Update sleep meter value

        if (NextDaySceneStarter.GetDayNum() == 0)
        {
            _sleepValue = 0.5f;
        }else if (SceneManager.GetActiveScene().name.Contains("Win"))
        {
            ChangeSleepValueWithLimits(_amountChanged);
        }
        else
        {
            ChangeSleepValueWithLimits(-_amountChanged);
        }
        GameObject.Find("SleepText").GetComponent<Text>().text = _fightOutcomeString;
    }

    private string CalculateAyandaFeeling(float sleepValue)
    {
        string sleepString = "";

        if (sleepValue <= 1 && sleepValue >= 0.8f)
        {
            sleepString = "Energised";
        }
        else if (sleepValue <= 0.79f && sleepValue >= 0.6f)
        {
            sleepString = "Rested";
        }
        else if (sleepValue <= 0.59f && sleepValue >= 0.4f)
        {
            sleepString = "Awake...ish";
        }
        else if (sleepValue <= 0.39f && sleepValue >= 0.2f)
        {
            sleepString = "Tired";
        }
        else
        {
            sleepString = "Exhausted";
        }

        return sleepString;
    }

    public static void ChangeSleepValueWithLimits(float num)
    {
        if (_sleepValue + num > 1)
        {
            _sleepValue = 1;
        }
        else if (_sleepValue + num < 0)
        {
            _sleepValue = 0;
        }
        else
        {
            _sleepValue = _sleepValue + num;
        }
    }
}
