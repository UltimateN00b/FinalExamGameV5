using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceIndicatorDiceSelection : MonoBehaviour
{
    private static GameObject _selectedGameObject;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name.Contains("2"))
        {
            _selectedGameObject = this.gameObject;
        }

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        if (this.gameObject != _selectedGameObject)
        {
            this.GetComponent<Image>().enabled = false;
        }
    }

    public void Show()
    {
        this.GetComponent<Image>().enabled = true;
    }

    public void SetSelected()
    {
        _selectedGameObject = this.gameObject;

        Utilities.SearchChild("Indicator_2", this.transform.parent.gameObject).GetComponent<DiceIndicatorDiceSelection>().Hide();
        Utilities.SearchChild("Indicator_3", this.transform.parent.gameObject).GetComponent<DiceIndicatorDiceSelection>().Hide();
    }

    public static GameObject GetSelectedDice()
    {
        GameObject currSelectedDice = null;

        if (_selectedGameObject.gameObject.name.Contains("1"))
        {
            currSelectedDice = Utilities.SearchChild("Dice_1", GameObject.Find("PlayerDiceCanvas"));
        }
        else if (_selectedGameObject.gameObject.name.Contains("2"))
        {
            currSelectedDice = Utilities.SearchChild("Dice_2", GameObject.Find("PlayerDiceCanvas"));
        }
        else
        {
            currSelectedDice = Utilities.SearchChild("Dice_3", GameObject.Find("PlayerDiceCanvas"));
        }

        return currSelectedDice;
    }
}
