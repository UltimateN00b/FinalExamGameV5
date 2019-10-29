using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public UnityEvent m_OnTutorialStart;

    public List<Sprite> tutorialImages;

    private bool _hasToShowTutorial;
    private float _checkTimer;
    private static bool _shouldFadeLastTutorial;

    //Tutorial condition checks
    private static bool _tutorial;
    private static bool _canRollMultiple;
    private static bool _firstDiceRolled;
    private static bool _attackButtonFirstClicked;
    private static bool _firstRoundFinished;


    private void OnLevelWasLoaded(int level)
    {
        if (NextDaySceneStarter.GetDayNum() == 0)
        {
            _tutorial = true;
        } else
        {
            _tutorial = false;
        }

        //Was on start

        _hasToShowTutorial = false;
        _checkTimer = 0;

        if (m_OnTutorialStart == null)
        {
            m_OnTutorialStart = new UnityEvent();
        }

        if (_tutorial)
        {
            m_OnTutorialStart.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldFadeLastTutorial)
        {
            if(this.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.GetComponent<MyImage>().FadeOut();
                    this.transform.GetChild(0).GetComponent<MyImage>().FadeOut();
                }
            }
        }
    }

    public static void MarkTutorialPlayed()
    {
        _tutorial = false;
    }

    public static bool IsTutorial()
    {
        return _tutorial;
    }

    public static void SetTutorialOver()
    {
        _tutorial = false;
    }


    public static bool CanRollMultiple()
    {
        return _canRollMultiple;
    }

    public static void SetCanRollMultipleTrue()
    {
        _canRollMultiple = true;
    }

    public static bool FirstDiceRolled()
    {
        return _firstDiceRolled;
    }

    public static void SetFirstDiceRolled()
    {
        _firstDiceRolled = true;
    }

    public static bool AttackButtonFirstClicked()
    {
        return _attackButtonFirstClicked;
    }

    public static void SetAttackButtonFirstClicked()
    {
        _attackButtonFirstClicked = true;
    }

    public static bool FirstRoundFinished()
    {
        return _firstRoundFinished;
    }

    public static void SetFirstRoundFinished()
    {
        _firstRoundFinished = true;
    }

    public static bool LastTutorialShown()
    {
        return _shouldFadeLastTutorial;
    }

    public static void SetLastTutorialShown()
    {
        _shouldFadeLastTutorial = true;
    }

    //NOTE: THIS TUTORIAL SYSTEM REQUIRES THAT THE SPRITE NAMES MATCH THE TUTORIAL NAMES!
    public void ShowTutorial(string tutorialName)
    {
        if (_tutorial)
        {
            if (this.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                foreach (Tutorial tut in this.gameObject.GetComponents<Tutorial>())
                {
                    if (tut.GetTutorialName().ToUpper().Contains(this.GetComponent<SpriteRenderer>().name.ToUpper()))
                    {
                        tut.CallClosingTutorialEvent();
                    }
                }
            }

            _hasToShowTutorial = true;
            _checkTimer = 0.0f;

            foreach (Sprite s in tutorialImages)
            {
                if (s.name.ToUpper().Contains(tutorialName.ToUpper()))
                {
                    this.transform.GetComponent<SpriteRenderer>().sprite = s;
                }
            }

            foreach (Tutorial tut in this.gameObject.GetComponents<Tutorial>())
            {
                if (tut.GetTutorialName().ToUpper().Contains(tutorialName.ToUpper()))
                {
                    tut.CallOpeningTutorialEvent();
                }
            }

            this.GetComponent<MyImage>().FadeIn();
            this.transform.GetChild(0).GetComponent<MyImage>().FadeIn();
        }
    }

    public void FadeTutorial()
    {
        foreach (Tutorial tut in this.gameObject.GetComponents<Tutorial>())
        {
            if (tut.GetTutorialName().ToUpper().Contains(this.GetComponent<SpriteRenderer>().sprite.name.ToUpper()))
            {
                tut.CallClosingTutorialEvent();
            }
        }

        this.GetComponent<MyImage>().FadeOut();
        this.transform.GetChild(0).GetComponent<MyImage>().FadeOut();
    }

}
