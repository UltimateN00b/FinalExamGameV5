﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonDice : MonoBehaviour
{

    public int damageDoneOnHit;

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

        if (diceRoll == 1)
        {
            if (DiceManager.GetNumPlayerRolls() <= 3)
            {
                diceRoll = 2;
            }

            if (!DiceManager.CanCurrentlyRollOne())
            {
                diceRoll = 2;
            }
        }

        if (TurnManager.GetCurrTurnCharacter().tag.Contains("Player"))
        {
            DiceManager.IncreaseNumPlayerRolls();
        }

        FreezeOnRoll(diceRoll);

        if (DiceManager.PPFound() == false)
        {

            GameObject pp = DiceManager.FindEmptyTypeTotal();
            pp.GetComponent<Text>().text = "PP: ";
            pp.transform.GetChild(0).GetComponent<Text>().enabled = true;

            if (!TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
            {
                GameObject.Find("ConfirmAttackButton").GetComponent<CustomButton>().Enable();
            }

            DiceManager.SetPPFound(true);
        }

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

        int poisonTotal = int.Parse(DiceManager.FindTypeTotalGameObject("PP").transform.GetChild(0).GetComponent<Text>().text);
        poisonTotal += diceRoll;
        DiceManager.FindTypeTotalGameObject("PP").transform.GetChild(0).GetComponent<Text>().text = poisonTotal.ToString();

        int attackTotal = int.Parse(DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text);
        attackTotal += damageDoneOnHit;
        DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text = attackTotal.ToString();
        DiceManager.FindTypeTotalGameObject("AP").GetComponent<TextPulseTypeTotals>().Pulse();

        if (diceRoll == 1)
        {
            DiceManager.DisableAllButtons();
            GameObject.Find("ConfirmAttackButton").GetComponent<CustomButton>().Disable();
            GameObject.Find("AttackMissed").GetComponent<BillboardMessage>().ShowMessage();
        }

        GameObject.Find("AttackHolder").GetComponent<AttackHolder>().AddAttack(diceRoll, "Poison", damageDoneOnHit, diceRoll);
    }

    public void OnAttack()
    {
        if (DiceManager.FindTypeTotalGameObject("PP") != null)
        {
            foreach (Character c in DiceManager.GetCurrTargets())
            {
                GameObject poisonCanvas = Utilities.SearchChild("PoisonCanvas", c.gameObject);
                GameObject poison = Utilities.SearchChild("PoisonImage", poisonCanvas);

                int poisonTotal = int.Parse(DiceManager.FindTypeTotalGameObject("PP").transform.GetChild(0).GetComponent<Text>().text);

                poison.GetComponent<Poison>().AddPoison(poisonTotal);
            }
        }
    }

    public void OnIndividualAttack(int pP)
    {
        if (DiceManager.FindTypeTotalGameObject("PP") != null)
        {
            foreach (Character c in DiceManager.GetCurrTargets())
            {
                GameObject poisonCanvas = Utilities.SearchChild("PoisonCanvas", c.gameObject);
                GameObject poison = Utilities.SearchChild("PoisonImage", poisonCanvas);

                int poisonTotal = pP;

                poison.GetComponent<Poison>().AddPoison(poisonTotal);
            }
        }
    }

    private void FreezeOnRoll(int num)
    {
        Dice.LastDiceClicked().GetComponent<Animator>().enabled = false;

        DiceType myDiceType = DiceManager.SearchDiceType("Poison");

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
