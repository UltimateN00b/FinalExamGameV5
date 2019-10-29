using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    private static GameObject sleepMeter;
    private static bool _death;

    // Start is called before the first frame update

    void Start()
    {
        sleepMeter = GameObject.Find("SleepMeter");
        _death = false;
    }

    private void OnLevelWasLoaded()
    {
        _death = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.GetCurrTurnCharacter().GetComponent<Character>().GetCurrHP() <= 0)
        {
            _death = true;
            if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
            {
                TurnManager.GetCurrTurnCharacter().GetComponent<EnemyAI>().CeaseEnemyAI();
            } else
            {
                DiceManager.DisableAllButtons();
            }

            if ((int)(TurnManager.GetCurrTurnCharacter().GetComponent<Character>().GetCurrHP()) < 0)
            {
                GameObject overkillCanvas = Utilities.SearchChild("OverkillCanvas", TurnManager.GetCurrTurnCharacter());
                GameObject overkillIndicator = Utilities.SearchChild("OverkillIndicator", overkillCanvas);
                overkillIndicator.GetComponent<OverkillIndicator>().ShowOverkill((int)(TurnManager.GetCurrTurnCharacter().GetComponent<Character>().GetCurrHP()));
            }

            if (!TutorialManager.IsTutorial())
            {
                sleepMeter.GetComponent<SleepMeter>().UpdateSleepValue();

                if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
                {
                    //SceneManager.LoadScene("YouWin");

                    Invoke("LoadWinScene", 3.0f);
                }
                else
                {
                    //SceneManager.LoadScene("YouLose");
                    Invoke("LoadLoseScene", 3.0f);
                }

                TutorialManager.MarkTutorialPlayed();
            } else
            {
                GameObject.Find("ShadowEnemy").GetComponent<MyImage>().FadeOut();
                Destroy(GameObject.Find("TutorialCase"));
                TutorialManager.SetTutorialOver();
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        //_updatedSleepValue = false;
    }

    public void ChangeHealth(float healthPoints)
    {
        GameObject character = this.transform.parent.transform.parent.gameObject;
        character.GetComponent<Character>().ChangeCurrHPPoints(healthPoints);

        float currHealth = character.GetComponent<Character>().GetCurrHP();
        float maxHealth = character.GetComponent<Character>().hp;

        if (DiceManager.GetCurrCharacter() == character)
        {
            GameObject statsCanvas = GameObject.Find("StatsCanvas");
            Utilities.SearchChild("Health", statsCanvas).GetComponent<Text>().text = currHealth + "/" + maxHealth.ToString();
        }

        this.GetComponent<Image>().fillAmount = currHealth / maxHealth;

        Utilities.SearchChild("HP", this.transform.parent.gameObject).GetComponent<Text>().text = character.GetComponent<Character>().GetCurrHP() + "/" + character.GetComponent<Character>().hp;
    }

    public static void SetSleepMeter(GameObject sM)
    {
        sleepMeter = sM;
    }

    private void LoadWinScene()
    {
        SceneManager.LoadScene("YouWin");
    }

    private void LoadLoseScene()
    {
        SceneManager.LoadScene("YouLose");
    }

    public static bool DeathOccurred()
    {
        return _death;
    }
}
