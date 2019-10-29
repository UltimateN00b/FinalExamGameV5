using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasBasic : MonoBehaviour
{
    private bool _hiding;
    private bool _showing;

    // Start is called before the first frame update
    void Start()
    {
        ResetObjects();
        _hiding = false;
        _showing = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject popupHeading = Utilities.SearchChild("PopupHeading", this.gameObject);

        if (popupHeading.GetComponent<Text>().color.a >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HideTutorialPopup();
            }
        }

        if (_showing)
        {
            if (popupHeading.GetComponent<Text>().color.a >= 1)
            {
                Time.timeScale = 0;
                _showing = false;
            }
        } else if (_hiding)
        {
            if (HiddenCheck())
            {
                ResetObjects();

                Time.timeScale = 1;
                this.transform.GetChild(0).gameObject.SetActive(false);

                _hiding = false;
            }
        }
    }

    public void MakeTutorialPopup(string heading, string body)
    {
       this.transform.GetChild(0).gameObject.SetActive(true);

        GameObject faderBG = Utilities.SearchChild("FaderBG", this.gameObject);
        GameObject popupBG = Utilities.SearchChild("PopupBG", this.gameObject);
        GameObject popupHeading = Utilities.SearchChild("PopupHeading", this.gameObject);
        GameObject popupBody = Utilities.SearchChild("PopupBody", this.gameObject);

        faderBG.GetComponent<MyUIFade>().FadeIn();
        popupBG.GetComponent<EntryTransitions>().GrowFromCenter();

        popupHeading.GetComponent<Text>().text = heading;
        popupBody.GetComponent<Text>().text = body;

        popupHeading.GetComponent<MyUIFade>().FadeIn();
        popupBody.GetComponent<MyUIFade>().FadeIn();

        _hiding = false;
        _showing = true;
    }

    public void HideTutorialPopup()
    {
        Time.timeScale = 1;

        GameObject faderBG = Utilities.SearchChild("FaderBG", this.gameObject);
        GameObject popupBG = Utilities.SearchChild("PopupBG", this.gameObject);
        GameObject popupHeading = Utilities.SearchChild("PopupHeading", this.gameObject);
        GameObject popupBody = Utilities.SearchChild("PopupBody", this.gameObject);

        faderBG.GetComponent<MyUIFade>().FadeOut();
        popupBG.GetComponent<EntryTransitions>().ShrinkFromCenter();

        popupHeading.GetComponent<MyUIFade>().FadeOut();
        popupBody.GetComponent<MyUIFade>().FadeOut();

        _showing = false;
        _hiding = true;
    }

    private void ResetObjects()
    {
        GameObject faderBG = Utilities.SearchChild("FaderBG", this.gameObject);
        GameObject popupBG = Utilities.SearchChild("PopupBG", this.gameObject);
        GameObject popupHeading = Utilities.SearchChild("PopupHeading", this.gameObject);
        GameObject popupBody = Utilities.SearchChild("PopupBody", this.gameObject);

        Color faderBGColor = faderBG.GetComponent<Image>().color;
        faderBGColor.a = 0;
        faderBG.GetComponent<Image>().color = faderBGColor;

        popupBG.GetComponent<RectTransform>().localScale = Vector3.zero;

        Color popupHeadingColor = popupHeading.GetComponent<Text>().color;
        popupHeadingColor.a = 0;
        popupHeading.GetComponent<Text>().color = popupHeadingColor;

        Color popupBodyColor = popupBody.GetComponent<Text>().color;
        popupBodyColor.a = 0;
        popupBody.GetComponent<Text>().color = popupBodyColor;

        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private bool HiddenCheck()
    {

        bool hidden = true;

        GameObject faderBG = Utilities.SearchChild("FaderBG", this.gameObject);
        GameObject popupBG = Utilities.SearchChild("PopupBG", this.gameObject);
        GameObject popupHeading = Utilities.SearchChild("PopupHeading", this.gameObject);
        GameObject popupBody = Utilities.SearchChild("PopupBody", this.gameObject);

        Color faderBGColor = faderBG.GetComponent<Image>().color;
        Color popupHeadingColor = popupHeading.GetComponent<Text>().color;
        Color popupBodyColor = popupBody.GetComponent<Text>().color;

        if (faderBGColor.a + popupHeadingColor.a + popupBodyColor.a > 0) //if any colour has an alpha greater than zero, then the popup is not hidden
        {
            hidden = false;
        } else if (popupBG.GetComponent<RectTransform>().localScale.x > 0) //if the popupBG's scale is larger than zero, then the popup is not hidden either
        {
            hidden = false;
        }

        return hidden;
    }
}
