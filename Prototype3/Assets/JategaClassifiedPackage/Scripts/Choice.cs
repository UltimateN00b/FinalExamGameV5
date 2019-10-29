using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Choice : MonoBehaviour {

    public UnityEvent m_OnChosen;
    public UnityEvent m_OnShown;

    private string _choiceText;
    private bool _seen;

	// Use this for initialization
	void Start () {
        Hide();

		if (m_OnChosen == null)
        {
            m_OnChosen = new UnityEvent();
        }

        if (m_OnShown == null)
        {
            m_OnShown = new UnityEvent();
        }
    }

    public string GetChoiceText()
    {
        return this.GetComponent<Text>().text;
    }

    public UnityEvent GetOnChosenEvent()
    {
        return m_OnChosen;
    }

    public UnityEvent GetOnShownEvent()
    {
        return m_OnShown;
    }

    private void Hide()
    {
        this.GetComponent<Text>().color = Color.clear;
    }

    public void SetSeen()
    {
        _seen = true;
        string myText = this.GetComponent<Text>().text;

        if (!myText.Contains(" [SEEN]"))
        {
            this.GetComponent<Text>().text = myText + " [SEEN]";
        }
    }

    public bool CheckSeen()
    {
        return _seen;
    }

    public void EnableChoice()
    {
        this.transform.parent.GetComponent<MainText>().AddToChoiceList(this);
    }

    public void DisableOnSleepMeterLessThan(float meterCheck)
    {
        if (GameObject.Find("SleepMeter").GetComponent<SleepMeter>().GetSleepValue() < meterCheck)
        {
            int childIndex = int.Parse(this.gameObject.name.Substring(this.gameObject.name.Length - 1));
            childIndex -= 1;
            GameObject.Find("ChoiceButton" + childIndex).GetComponent<ChoiceButton>().DisableChoice();
        }
    }

    public void EnableOnSleepMeterMoreThan(float meterCheck)
    {
        int childIndex = int.Parse(this.gameObject.name.Substring(this.gameObject.name.Length - 1));
        childIndex -= 1;

        if (GameObject.Find("SleepMeter").GetComponent<SleepMeter>().GetSleepValue() > meterCheck)
        {
            GameObject.Find("ChoiceButton" + childIndex).GetComponent<ChoiceButton>().EnableChoiceWithIcon();
        } else
        {
            GameObject.Find("ChoiceButton" + childIndex).SetActive(false);
        }
    }
}
