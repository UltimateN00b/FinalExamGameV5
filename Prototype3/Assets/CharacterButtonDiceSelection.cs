using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonDiceSelection : MonoBehaviour
{
    private string _myCharacterName;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeButton(Sprite myImage, string myName, int myLevel, Color myColour)
    {
        //Change the ownership of this button
        _myCharacterName = myName;

        //Change the information
        Utilities.SearchChild("CharacterProfile", this.gameObject).GetComponent<Image>().sprite = myImage;
        Utilities.SearchChild("CharacterName", this.gameObject).GetComponent<Text>().text = myName;
        Utilities.SearchChild("CharacterLevel", this.gameObject).GetComponent<Text>().text = "Lvl "+myLevel;

        //Change the button colour
        this.GetComponent<Image>().color = myColour;
    }

    public void OnButtonClicked()
    {
        GameObject.Find("DiceCanvas").GetComponent<DiceCanvasDiceSelection>().Show();
        GameObject.Find("DiceCanvas").GetComponent<DiceCanvasDiceSelection>().SetupDiceCanvas(_myCharacterName);
        this.transform.parent.GetComponent<CharacterCanvasDiceSelection>().Hide();
    }
}
