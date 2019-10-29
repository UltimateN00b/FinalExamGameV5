using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {

    private UnityEvent m_OnPressed;
    private GameObject _logContent;

    private Choice myChoice;
    private Font _originalFont;

    private bool _enabledButton;

    // Use this for initialization

    private void Awake()
    {
        Hide();
        _originalFont = this.transform.GetChild(0).GetComponent<Text>().font;
    }

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(InvokeOnPressed);
        _logContent = GameObject.Find("LogContent");
        _enabledButton = false;
    }

    public void SetOnPressedEvent(UnityEvent newPressEvent)
    {
        m_OnPressed = newPressEvent;
    }

    private void Update()
    {
     if (!_enabledButton)
        {
            if (this.GetComponent<Image>().color.a >= 1)
            {
                this.GetComponent<Button>().enabled = true;
                _enabledButton = true;
            }
        }
    }

    void InvokeOnPressed()
    {
        string logText = _logContent.GetComponent<Text>().text;
        logText += "\n" + this.transform.GetChild(0).GetComponent<Text>().text + "\n";
        _logContent.GetComponent<Text>().text = logText;

        if (myChoice!=null)
        {
            myChoice.SetSeen();
        }

        AudioManager.PlaySound(Resources.Load("ButtonPress") as AudioClip);
        Hide();
        m_OnPressed.Invoke();
    }

    public void PlaySound()
    {
        if (this.GetComponent<Button>().enabled)
        {
            AudioManager.PlaySound(Resources.Load("Select") as AudioClip, 0.8f);
        }
    }

    public void SetMyChoice(Choice choice)
    {
        myChoice = choice;
    }



    public void DisableChoice()
    {
        ResetChoice();

        this.transform.GetChild(0).GetComponent<Text>().font = GameObject.Find("DialogueCanvasDecorated").GetComponent<DialogueBox>().GetStrikethroughFont();
        this.GetComponent<Button>().interactable = false;

        int iconIndex = int.Parse(this.gameObject.name.Substring(this.gameObject.name.Length - 1));
        Utilities.SearchChild(("ChoiceIcon" + iconIndex), this.transform.parent.gameObject).SetActive(true);
        Utilities.SearchChild(("ChoiceIcon" + iconIndex), this.transform.parent.gameObject).GetComponent<ChoiceIcon>().SetIcon("BadSleep");
    }
    
    public void ResetChoice()
    {
        this.transform.GetChild(0).GetComponent<Text>().font = _originalFont;
        this.GetComponent<Button>().interactable = true;

        int iconIndex = int.Parse(this.gameObject.name.Substring(this.gameObject.name.Length - 1));
        Utilities.SearchChild(("ChoiceIcon" + iconIndex), this.transform.parent.gameObject).SetActive(false);
    }

    public void EnableChoiceWithIcon()
    {
        ResetChoice();
        int iconIndex = int.Parse(this.gameObject.name.Substring(this.gameObject.name.Length - 1));
        Utilities.SearchChild(("ChoiceIcon" + iconIndex), this.transform.parent.gameObject).SetActive(true);
        Utilities.SearchChild(("ChoiceIcon" + iconIndex), this.transform.parent.gameObject).GetComponent<ChoiceIcon>().SetIcon("GoodSleep");
    }

    public void Show()
    {
        this.GetComponent<MyUIFade>().FadeIn();
        this.transform.GetChild(0).GetComponent<MyUIFade>().FadeIn();
    }

    public void Hide()
    {
        Color imageColor = this.GetComponent<Image>().color;
        imageColor.a = 0;
        this.GetComponent<Image>().color = imageColor;

        Color textColour = this.transform.GetChild(0).GetComponent<Text>().color;
        textColour.a = 0;
        this.transform.GetChild(0).GetComponent<Text>().color = textColour;

        this.GetComponent<Button>().enabled = false;
        _enabledButton = false;
    }
}
