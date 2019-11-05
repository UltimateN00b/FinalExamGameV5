using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int CalculateDiceRollSix()
    {
        int diceRoll = Random.Range(1, 138);

        if (diceRoll <= 12)
        {
            diceRoll = 1;
        }
        else if (diceRoll <= 27)
        {
            diceRoll = 2;
        }
        else if (diceRoll <= 47)
        {
            diceRoll = 3;
        }
        else if (diceRoll <= 77)
        {
            diceRoll = 4;
        }
        else if (diceRoll <= 107)
        {
            diceRoll = 5;
        }
        else //137
        {
            diceRoll = 6;
        }

        return diceRoll;
    }

    public static int CalculateDiceRollFour()
    {
        int diceRoll = Random.Range(1, 138);

        if (diceRoll <= 12)
        {
            diceRoll = 1;
        }
        else if (diceRoll <= 27)
        {
            diceRoll = 2;
        }
        else if (diceRoll <= 47)
        {
            diceRoll = 3;
        }
        else
        {
            diceRoll = 4;
        }

        return diceRoll;
    }

    public static int CalculateDiceRollThree()
    {
        int diceRoll = Random.Range(1, 138);

        if (diceRoll <= 12)
        {
            diceRoll = 1;
        }
        else if (diceRoll <= 27)
        {
            diceRoll = 2;
        }
        else
        {
            diceRoll = 3;
        }

        return diceRoll;
    }

    public static int CalculateDiceRollTen()
    {
        int diceRoll = Random.Range(1, 138);

        if (diceRoll <= 12)
        {
            int chanceOfTen = Random.Range(1, 3);

            if (chanceOfTen == 1)
            {
                diceRoll = 1;
            } else
            {
                diceRoll = 10;
            }
        }
        else if (diceRoll <= 27)
        {
            diceRoll = Random.Range(2, 4);
        }
        else if (diceRoll <= 47)
        {
            diceRoll = Random.Range(4, 6);
        }
        else if (diceRoll <= 77)
        {
            diceRoll = Random.Range(6, 8);
        }
        else
        {
            diceRoll = Random.Range(8, 10);
        }

        return diceRoll;
    }
}
