using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPointChecker : MonoBehaviour
{

    public string poisonPopupHeading;
    public string poisonPopupMessage;

    public string lifeStealPopupHeading;
    public string lifeStealPopupMessage;

    private static bool _hasShownPoisonDicePopup;
    private static bool _hasShownLifeStealPopup;

    private static int _roundNum;

    // Start is called before the first frame update
    void Start()
    {
        _hasShownPoisonDicePopup = false;
        _hasShownLifeStealPopup = false;

        _roundNum = 0;

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name.Contains("TinyDiceDungeonCombat"))
        {
            _roundNum++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NextDaySceneStarter.GetDayNum() == 1)
        {
            if (!_hasShownPoisonDicePopup)
            {
                if (SceneManager.GetActiveScene().name.Contains("TinyDiceDungeonCombat"))
                {
                    if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
                    {
                        GameObject.Find("TutorialCanvas_Basic").GetComponent<TutorialCanvasBasic>().MakeTutorialPopup(poisonPopupHeading, poisonPopupMessage);
                        _hasShownPoisonDicePopup = true;
                    }
                }
            }
        }
        else if (NextDaySceneStarter.GetDayNum() == 2|| NextDaySceneStarter.GetDayNum() == 3)
        {
            if (!_hasShownLifeStealPopup)
            {
                if (SceneManager.GetActiveScene().name.Contains("TinyDiceDungeonCombat"))
                {
                    if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
                    {
                        GameObject.Find("TutorialCanvas_Basic").GetComponent<TutorialCanvasBasic>().MakeTutorialPopup(lifeStealPopupHeading, lifeStealPopupMessage);
                        _hasShownLifeStealPopup = true;
                    }
                }
            }
        }
    }
}
