using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceButtonDiceSelection : MonoBehaviour
{
    string _myDiceName = "Common";

    // Start is called before the first frame update
    void Start()
    {
        //_myDiceName = "Common";
    }

    // Update is called once per frame
    void Update()
    {
        if (_myDiceName != "Common")
        {
            if (PlayerDiceHolder.GetDiceList().Contains(_myDiceName))
            {
                Color newColour = Color.grey;
                newColour.a = 0.5f;

                this.GetComponent<Button>().enabled = false;
                this.GetComponent<Image>().color = newColour;
                this.transform.GetChild(0).GetComponent<Text>().color = newColour;
                this.transform.GetChild(1).GetComponent<Image>().color = newColour;
                this.transform.GetChild(2).GetComponent<Image>().color = newColour;
            } else
            {
                Color newColour = Color.white;
                newColour.a = 1;

                this.GetComponent<Button>().enabled = true;
                this.GetComponent<Image>().color = newColour;

                this.transform.GetChild(0).GetComponent<Text>().color = newColour;
                this.transform.GetChild(1).GetComponent<Image>().color = newColour;
                this.transform.GetChild(2).GetComponent<Image>().color = newColour;
            }
        }
    }

    public void ChangeButton(Sprite myImage, string myName, Sprite lockImage, Color myColour)
    {
        //Change the information
        Utilities.SearchChild("DiceName", this.gameObject).GetComponent<Text>().text = myName;
        Utilities.SearchChild("DiceProfile", this.gameObject).GetComponent<Image>().sprite = myImage;
        Utilities.SearchChild("LockMode", this.gameObject).GetComponent<Image>().sprite = lockImage;

        //Change the button colour
        this.GetComponent<Image>().color = myColour;

        _myDiceName = myName;
    }

    public void ChangeDiceName(string myName)
    {
        Utilities.SearchChild("DiceName", this.gameObject).GetComponent<Text>().text = myName;

        _myDiceName = myName;
    }

    public void ShowPopupInfo()
    {
        if (_myDiceName != null && FindDiceSkin(_myDiceName) != null)
        {

            DiceSkin dS = FindDiceSkin(_myDiceName);
            GameObject.Find("PopupCanvas").GetComponent<PopupCanvasDiceSelection>().Popup(dS.GetDiceInfoTitle(), dS.GetDiceInfoBody());
        } else if (this.gameObject.name.Contains("DefaultDice"))
        {
            GameObject.Find("PopupCanvas").GetComponent<PopupCanvasDiceSelection>().Popup("Default Dice", "Dice for scrubs");
        } else
        {
            GameObject.Find("PopupCanvas").GetComponent<PopupCanvasDiceSelection>().Popup("DICE LOCKED", "You need a higher relationship level to unlock this dice");
        }
    }

    public void HidePopupInfo()
    {
        GameObject.Find("PopupCanvas").GetComponent<PopupCanvasDiceSelection>().Hide();
    }

    private DiceSkin FindDiceSkin(string name)
    {
        DiceSkin newDiceSkin = null;

        foreach (DiceSkin dS in GameObject.Find("DiceSkinHolder").GetComponents<DiceSkin>())
        {
            if (dS.GetDiceName().ToUpper().Contains(name.ToUpper()))
            {
                newDiceSkin = dS;
            }
        }

        return newDiceSkin;
    }

    public void SwitchDice()
    {
        GameObject currDice = DiceIndicatorDiceSelection.GetSelectedDice();
        currDice.GetComponent<Image>().sprite = FindDiceSkin(_myDiceName).GetDiceImage();

        int diceNum = int.Parse(currDice.gameObject.name.Substring(currDice.gameObject.name.Length - 1));

        PlayerDiceHolder.ChangeDice(diceNum, _myDiceName);
    }

    public string GetDiceName()
    {
        return _myDiceName;
    }
}
