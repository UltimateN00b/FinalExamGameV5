using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    private static bool _death;
    private float _changeSceneTimer;

    private static bool _hasUpdatedSleepValue;

    // Start is called before the first frame update

    void Start()
    {
        _death = false;
        _changeSceneTimer = 0.0f;

    }
    private void OnLevelWasLoaded(int level)
    {
        _death = false;
        _hasUpdatedSleepValue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent.transform.parent.gameObject.tag.Contains("Enemy"))
        {
            if (TurnManager.GetCurrTurnCharacter().GetComponent<Character>().GetCurrHP() <= 0 || SurrenderButton.HasSurrendered())
            {
                GameObject.Find("Surrender").GetComponent<CustomButton>().Disable();

                _death = true;


                if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
                {
                    TurnManager.GetCurrTurnCharacter().GetComponent<EnemyAI>().CeaseEnemyAI();
                }
                else
                {
                    DiceManager.DisableAllButtons();
                }

                if ((int)(TurnManager.GetCurrTurnCharacter().GetComponent<Character>().GetCurrHP()) <= 0)
                {
                    GameObject overkillCanvas = Utilities.SearchChild("OverkillCanvas", TurnManager.GetCurrTurnCharacter());
                    GameObject overkillIndicator = Utilities.SearchChild("OverkillIndicator", overkillCanvas);
                    overkillIndicator.GetComponent<OverkillIndicator>().ShowOverkill((int)(TurnManager.GetCurrTurnCharacter().GetComponent<Character>().GetCurrHP()));

                    if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
                    {
                        GameObject.Find("Ayanda").GetComponent<Animator>().SetBool("winning", true);
                        if (!TutorialManager.IsTutorial())
                        {
                            if (GameObject.Find("AyandaMonster") != null)
                            {
                                GameObject.Find("AyandaMonster").GetComponent<Animator>().SetBool("dying", true);
                            }
                        }
                    } else
                    {
                        GameObject.Find("Ayanda").GetComponent<Animator>().SetBool("dying", true);
                    }
                }

                if (!TutorialManager.IsTutorial())
                {
                    _changeSceneTimer += Time.deltaTime;

                    if (_changeSceneTimer >= 3f)
                    {
                        if (!_hasUpdatedSleepValue)
                        {
                            if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
                            {
                                GameObject.Find("SleepValueHolder").GetComponent<SleepValueHolder>().UpdateSleepValue();
                                SceneManager.LoadScene("YouWin");
                            }
                            else
                            {
                                GameObject.Find("SleepValueHolder").GetComponent<SleepValueHolder>().UpdateSleepValue();
                                SceneManager.LoadScene("YouLose");
                            }

                            Debug.Log("CHANGED SCENES");
                            _changeSceneTimer = 0.0f;
                            _hasUpdatedSleepValue = true;
                        }

                    }

                    TutorialManager.MarkTutorialPlayed();
                }
                else
                {
                    GameObject.Find("ShadowEnemy").GetComponent<MyImage>().FadeOut();
                    Destroy(GameObject.Find("TutorialCase"));
                    TutorialManager.SetTutorialOver();
                }
            }
        }
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

    private void LoadWinScene()
    {

    }

    private void LoadLoseScene()
    {
    }

    public static bool DeathOccurred()
    {
        return _death;
    }
}
