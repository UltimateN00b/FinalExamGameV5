using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIcon(string spriteName)
    {
        Sprite searchIcon = null;

       List<Sprite> choiceIcons = GameObject.Find("DialogueCanvasDecorated").GetComponent<DialogueBox>().GetChoiceIcons();

        foreach (Sprite s in choiceIcons)
        {
            if (s.name.Contains(spriteName))
            {
                searchIcon = s;
            }
        }

        if (searchIcon != null)
        {
            if (spriteName.ToUpper().Contains("BAD"))
            {
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<Image>().color = Color.white;
            } else
            {
                this.GetComponent<Animator>().enabled = true;
                this.GetComponent<Image>().color = Color.black;
            }

            this.GetComponent<Image>().sprite = searchIcon;

        } else
        {
            Debug.Log("Search Icon Equals Null");
        }
    }
}
