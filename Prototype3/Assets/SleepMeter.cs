using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SleepMeter : MonoBehaviour
{
    private static float _amountChanged;
    private static string _fightOutcomeString;
    private static float _initialValue;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.SetSleepMeter(this.gameObject);

        _fightOutcomeString = "";
        _initialValue = this.GetComponent<Slider>().value;
        DontDestroyOnLoad(this.transform.parent.gameObject);

        Hide();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name.Contains("YouWin")|| SceneManager.GetActiveScene().name.Contains("YouLose"))
        {
            ChangeFightOutputString();
        }

        if (SceneManager.GetActiveScene().name.Contains("Narrative"))
        {
            _initialValue = this.GetComponent<Slider>().value;
            //Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetSleepValue()
    {
        return this.GetComponent<Slider>().value;
    }

    public void Hide()
    {
        this.transform.parent.GetChild(0).gameObject.SetActive(false);

       for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        this.transform.parent.GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void UpdateSleepValue()
    {
        //Calculate sleep meter change according to player and enemy health

        _amountChanged = 0;

        Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Character enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Character>();

        float winnersHealth = 0;
        float losersHealth = 0;

        if (player.GetCurrHP() > 0)
        {
            winnersHealth = player.GetCurrHP() / player.hp;  
        } else
        {
            winnersHealth = enemy.GetCurrHP() / enemy.hp;
        }

        _amountChanged = Mathf.Abs(winnersHealth) + Mathf.Abs(losersHealth);
        _amountChanged = _amountChanged * 0.2f;

        //Change the slider value and output string.
        if (player.GetCurrHP() > 0)
        {
            Debug.Log("AMOUNT CHANGED " + _amountChanged);

            _fightOutcomeString = "Your sleep quality has improved!\nAyanda is feeling REPLACETHIS";
        }
        else
        {
            Debug.Log("AMOUNT CHANGED " + _amountChanged);

            _fightOutcomeString = "Your sleep quality has declined.\nAyanda is feeling REPLACETHIS";
        }
           
      }

    private void ChangeFightOutputString()
    {
        Debug.Log("SHOULD SHOW WIN TEXT");

        if (NextDaySceneStarter.GetDayNum() == 0)
        {
            GameObject.Find("SM_Superficial").GetComponent<SleepMeterSuperficial>().PlaySliderAnimation(_initialValue, this.GetComponent<Slider>().value + 0);
            Utilities.SearchChild("SleepMeter", GameObject.Find("SleepMeterCanvas")).GetComponent<Slider>().value = 0.5f;
        }else if (SceneManager.GetActiveScene().name.Contains("Win"))
        {
            GameObject.Find("SM_Superficial").GetComponent<SleepMeterSuperficial>().PlaySliderAnimation(_initialValue, this.GetComponent<Slider>().value + _amountChanged);
            Utilities.SearchChild("SleepMeter", GameObject.Find("SleepMeterCanvas")).GetComponent<Slider>().value = this.GetComponent<Slider>().value + _amountChanged;
        } else
        {
            GameObject.Find("SM_Superficial").GetComponent<SleepMeterSuperficial>().PlaySliderAnimation(_initialValue, this.GetComponent<Slider>().value - _amountChanged);
            Utilities.SearchChild("SleepMeter", GameObject.Find("SleepMeterCanvas")).GetComponent<Slider>().value = this.GetComponent<Slider>().value - _amountChanged;
        }

        _fightOutcomeString = _fightOutcomeString.Replace("REPLACETHIS", CalculateAyandaFeeling(Utilities.SearchChild("SleepMeter", GameObject.Find("SleepMeterCanvas")).GetComponent<Slider>().value));
        GameObject.Find("SleepText").GetComponent<Text>().text = _fightOutcomeString;
    }

    private string CalculateAyandaFeeling(float sleepMeterValue)
    {
        string sleepString = "EXHAUSTED";

        if (sleepMeterValue <= 1 && sleepMeterValue >= 0.8f)
        {
            sleepString = "ENERGISED";
        }
        else if (sleepMeterValue <= 0.79f && sleepMeterValue >= 0.6f)
        {
            sleepString = "RESTED";
        }
        else if (sleepMeterValue <= 0.59f && sleepMeterValue >= 0.4f)
        {
            sleepString = "AWAKE...ish";
        }
        else if (sleepMeterValue <= 0.39f && sleepMeterValue >= 0.2f)
        {
            sleepString = "TIRED";
        }
        
        return sleepString;
    }

}
