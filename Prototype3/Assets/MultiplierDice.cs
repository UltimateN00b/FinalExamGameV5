using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierDice : MonoBehaviour
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
        int diceRoll = DiceRollCalculator.CalculateDiceRollThree();

        GameObject.Find("ClickTheDice").GetComponent<Text>().text = "DICE VALUES: ";

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

        int rollAdd = 0;

        for (int i = 1; i <= 3; i++)
        {
            string multiplyValueName = "RollValue"+i;

            if (GameObject.Find(multiplyValueName) != null)
            {
                if (!multiplyValueName.Equals("RollValue" + Dice.LastDiceClicked().name.ToCharArray()[Dice.LastDiceClicked().name.Length - 1]))
                {
                    int rollToMultiply = 0;
                    int isNumberCheck;

                    if (int.TryParse(GameObject.Find(multiplyValueName).GetComponent<Text>().text, out isNumberCheck))
                    {
                        rollToMultiply = int.Parse(GameObject.Find(multiplyValueName).GetComponent<Text>().text);
                    }

                    rollAdd += rollToMultiply * diceRoll - rollToMultiply;

                    rollToMultiply *= diceRoll;

                    GameObject.Find(multiplyValueName).GetComponent<Text>().text = rollToMultiply.ToString();
                }
            }
        }

        int attackTotal = int.Parse(DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text);
        attackTotal += rollAdd;

        DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text = attackTotal.ToString();
        GameObject.Find("AttackHolder").GetComponent<AttackHolder>().AddAttack(diceRoll, "Multiplier", rollAdd);
    }

    private void FreezeOnRoll(int num)
    {
        Dice.LastDiceClicked().GetComponent<Animator>().enabled = false;

        DiceType myDiceType = DiceManager.SearchDiceType("Multiplier");

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
