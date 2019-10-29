using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonDice : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStopped()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);



        int diceRoll = DiceRollCalculator.CalculateDiceRollSix();

        GameObject.Find("ClickTheDice").GetComponent<Text>().text = "DICE VALUES: ";

        if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy")&& TutorialManager.IsTutorial())
        {
                if (TutorialManager.LastTutorialShown())
                {
                    diceRoll = 1;
                }
                else
                {
                    diceRoll = 6;
                }
        }
        else if (diceRoll == 1)
        {
            if (DiceManager.GetNumPlayerRolls() <= 3)
            {
                diceRoll = 2;
            }
        }

        if (TutorialManager.IsTutorial() && TutorialManager.FirstDiceRolled())
        {
            if (DiceManager.GetNumPlayerRolls() == 2)
            {
                diceRoll = 1;
            }
        }

        if (TurnManager.GetCurrTurnCharacter().tag.Contains("Player"))
        {
            DiceManager.IncreaseNumPlayerRolls();
        }

        FreezeOnRoll(diceRoll);

        if (DiceManager.APFound() == false)
        {

            GameObject ap = DiceManager.FindEmptyTypeTotal();
            ap.GetComponent<Text>().text = "AP: ";
            ap.transform.GetChild(0).GetComponent<Text>().enabled = true;

            if (!TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
            {
                GameObject.Find("ConfirmAttackButton").GetComponent<CustomButton>().Enable();
            }

            DiceManager.SetAPFound(true);

        }

        string rollValueName = "RollValue" + Dice.LastDiceClicked().name.ToCharArray()[Dice.LastDiceClicked().name.Length - 1];
        GameObject.Find(rollValueName).GetComponent<Text>().text = diceRoll.ToString();

        //int rollTotal = int.Parse(GameObject.Find("RollTotal").GetComponent<Text>().text);
        //rollTotal += diceRoll;
        //GameObject.Find("RollTotal").GetComponent<Text>().text = rollTotal.ToString();
        //GameObject.Find("RollTotal").GetComponent<Text>().enabled = true;

        int attackTotal = int.Parse(DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text);
        attackTotal += diceRoll;
        DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text = attackTotal.ToString();

        if (diceRoll == 1)
        {
            DiceManager.DisableAllButtons();
            GameObject.Find("ConfirmAttackButton").GetComponent<CustomButton>().Disable();
            GameObject.Find("AttackMissed").GetComponent<BillboardMessage>().ShowMessage();
        }

        GameObject.Find("AttackHolder").GetComponent<AttackHolder>().AddAttack(diceRoll, "Common", diceRoll);
    }

    private void FreezeOnRoll(int num)
    {
        Dice.LastDiceClicked().GetComponent<Animator>().enabled = false;

        DiceType myDiceType = DiceManager.SearchDiceType("Common");

        List<Sprite> freezeSprites = myDiceType.GetFreezeSprites();

        Sprite freezeSprite = null;

        foreach (Sprite s in freezeSprites)
        {
            if (s.name.Contains(num.ToString()))
            {
                freezeSprite = s;
            }
        }

        Dice.LastDiceClicked().GetComponent<Image>().sprite = freezeSprite;
    }

}
