using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public string myName;
    public Sprite characterImage;

    public float initiative;

    public float hp;

    private float _currHP;
    private float _currAtk;

    public string dice1Type;
    public string dice2Type;
    public string dice3Type;

    private bool _hadTurn;

    private bool _isSelected;

    private bool _diceChanged;

    private bool _paralysed;

    private void Awake()
    {

        if (this.gameObject.tag.Equals("Player"))
        {
            if (!TutorialManager.IsTutorial())
            {
                //IfShitBreaking change back to PlayerDiceHolder.GetDiceType(1), 2 and 3
                dice1Type = PlayerDiceHolder.GetDiceType(1);
                dice2Type = PlayerDiceHolder.GetDiceType(2);
                dice3Type = PlayerDiceHolder.GetDiceType(3);
            }
        }
    }

    void Start()
    {
        _hadTurn = false;

        _currHP = hp;

        _isSelected = false;

        _paralysed = false;

        GameObject healthCanvasChar = Utilities.SearchChild("HealthCanvas", this.gameObject);
        Debug.Log("CHARACTER: " + this.gameObject.name);
        Utilities.SearchChild("HP", healthCanvasChar).GetComponent<Text>().text = this.GetComponent<Character>().GetCurrHP() + "/" + this.GetComponent<Character>().hp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMyTurn()
    {
        _isSelected = false;

        GameObject.Find("ConfirmAttackButton").GetComponent<ConfirmAttackButton>().ShowUI();
        GameObject.Find("ConfirmAttackButton").GetComponent<CustomButton>().Enable();

        GameObject characterInfoCanvas = GameObject.Find("CharacterInfoCanvas");
        GameObject statsCanvas = GameObject.Find("StatsCanvas");

        Utilities.SearchChild("Name", characterInfoCanvas).GetComponent<Text>().text = myName;
        Utilities.SearchChild("Image", characterInfoCanvas).GetComponent<Image>().sprite = characterImage;

        //*where dice total clear was

        DiceManager.SetCurrCharacter(this);

        DiceManager.ClearAllDiceTotals();

        DiceManager.SetAllDiceUnclicked();

        DiceManager.ClearTargets();

        if (!TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
        {
            DiceManager.EnableAllButtons();
        }
        else
        {
            GameObject.Find("ClickTheDice").GetComponent<Text>().text = "";
            TurnManager.GetCurrTurnCharacter().gameObject.GetComponent<EnemyAI>().ExecuteEnemyAI();
        }

        GameObject.Find("ConfirmAttackButton").GetComponent<CustomButton>().Disable();

        if (DiceManager.GetCurrCharacter().tag.Contains("Enemy"))
        {
            DiceManager.DisableAllButtons();
        }
        else
        {
            DiceManager.EnableAllButtons();
        }

        Utilities.SearchChild("TurnArrow", this.gameObject).GetComponent<SpriteRenderer>().enabled = true;

        //DiceManager.SetCanReset(true);

        ChangeDiceType();
        DiceManager.ClearAllDiceTotals();

        if (!_paralysed)
        {
            GameObject diceCanvas = GameObject.Find("DiceCanvas");
            diceCanvas.transform.GetChild(2).GetComponent<Dice>().UnparalyseDice();
        }

        ApplyStatusEffects();
        DiceManager.CurrCombatStage = DiceManager.CombatStage.DiceAndTargets;

        if (TutorialManager.IsTutorial() && TutorialManager.FirstRoundFinished() && !TutorialManager.LastTutorialShown())
        {
            GameObject.Find("TutorialManager").GetComponent<TutorialManager>().ShowTutorial("TutorialWinning");
            TutorialManager.SetLastTutorialShown();
        }
    }

    public float GetInitiative()
    {
        return initiative;
    }

    private void OnMouseOver()
    {
        if (!TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
        {
            if (DiceManager.CurrCombatStage == DiceManager.CombatStage.DiceAndTargets)
            {
                if (TurnManager.GetCurrTurnCharacter() != this.gameObject)
                {
                    //this.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        if (!TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
        {
            if (DiceManager.CurrCombatStage == DiceManager.CombatStage.DiceAndTargets)
            {
                if (TurnManager.GetCurrTurnCharacter() != this.gameObject)
                {
                    if (_isSelected == false)
                    {
                        if (!DiceManager.GetCurrTargets().Contains(this.gameObject.GetComponent<Character>()))
                        {
                            //this.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
        {
            if (DiceManager.CurrCombatStage == DiceManager.CombatStage.DiceAndTargets)
            {
                if (TurnManager.GetCurrTurnCharacter() != this.gameObject)
                {
                    _isSelected = true;
                    DiceManager.ClearTargets();
                    DiceManager.AddTarget(this);
                }
            }
        }
    }

    public void SetTurnFinished()
    {
        _hadTurn = true;
        _paralysed = false;
    }

    public void SetTurnUnfinished()
    {
        _hadTurn = false;
    }

    public bool HasFinishedTurn()
    {
        return _hadTurn;
    }

    public void ChangeCurrHPPoints(float changeNum)
    {
        if (_currHP + changeNum > hp)
        {
            _currHP = hp;
        }
        else
        {
            _currHP += changeNum;
        }
    }

    public float GetCurrHP()
    {
        return _currHP;
    }

    public void ChangeDiceType()
    {
        GameObject diceCanvas = GameObject.Find("DiceCanvas");

        diceCanvas.transform.GetChild(0).GetComponent<Dice>().ChangeDice(dice1Type);
        diceCanvas.transform.GetChild(1).GetComponent<Dice>().ChangeDice(dice2Type);
        diceCanvas.transform.GetChild(2).GetComponent<Dice>().ChangeDice(dice3Type);
    }

    private void ApplyStatusEffects()
    {
        GameObject poisonCanvas = Utilities.SearchChild("PoisonCanvas", this.gameObject);
        GameObject poison = Utilities.SearchChild("PoisonImage", poisonCanvas);

        poison.GetComponent<Poison>().DealPoisonDamage();

        if (_paralysed)
        {
            GameObject diceCanvas = GameObject.Find("DiceCanvas");
            diceCanvas.transform.GetChild(2).GetComponent<Dice>().ParalyseDice();
        }
    }

    public void ChangeCharacter(CharacterTransfer newCharacter)
    {
        myName = newCharacter.myName;
        characterImage = newCharacter.characterImage;
        initiative = newCharacter.initiative;
        hp = newCharacter.hp;
        dice1Type = newCharacter.dice1Type;
        dice2Type = newCharacter.dice2Type;
        dice3Type = newCharacter.dice3Type;
    }

    public void SetParalysed(bool isParalysed)
    {
        _paralysed = isParalysed;
    }
}
