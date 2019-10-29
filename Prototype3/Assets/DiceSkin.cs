using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSkin : MonoBehaviour
{
    public string diceName;
    public Sprite diceImage;
    public Color diceButtonColor;

    public string diceInfoTitle;
    public string diceInfoBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDiceName()
    {
        return diceName;
    }

    public Sprite GetDiceImage()
    {
        return diceImage;
    }

    public Color GetDiceButtonColour()
    {
        return diceButtonColor;
    }

    public string GetDiceInfoTitle()
    {
        return diceInfoTitle;
    }

    public string GetDiceInfoBody()
    {
        return diceInfoBody;
    }
}
