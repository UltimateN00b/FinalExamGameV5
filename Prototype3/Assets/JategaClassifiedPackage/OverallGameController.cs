using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallGameController : MonoBehaviour {

    private GameObject _instructionsCanvas;
	// Use this for initialization
	void Start () {
        _instructionsCanvas = GameObject.Find("InstructionsCanvas");
        _instructionsCanvas.GetComponent<EscapeMenuManager>().UpdateAyandaSleep();
        _instructionsCanvas.GetComponent<EscapeMenuManager>().UpdateAllRelationships();
        _instructionsCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_instructionsCanvas.activeInHierarchy)
            {
                _instructionsCanvas.SetActive(true);
                GameObject.Find("CharacterInfoUpdatedCanvas").GetComponent<AudioFader>().FadeIn();
                Camera.main.GetComponents<AudioFader>()[1].FadeOut();
            } else
            {
                _instructionsCanvas.SetActive(false);
                GameObject.Find("CharacterInfoUpdatedCanvas").GetComponent<AudioFader>().FadeOut();
                Camera.main.GetComponents<AudioFader>()[1].FadeIn();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
        {
           // GameObject.Find("TextLog").GetComponent<TextLog>().ControlVisibility();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!GameObject.Find("DialogueSystemWright").GetComponent<DialogueSystemWright>().IsVisible())
            {
                if (!GameObject.Find("TextLog").GetComponent<TextLog>().CheckHidDialogueBoxFromHere())
                {
                    GameObject.Find("SatchelBG").GetComponent<SatchelScreen>().ControlVisibilityForTabAndXButton();
                }
            }
        }
    }

    public GameObject GetInstructionsCanvas()
    {
        return _instructionsCanvas;
    }
}
