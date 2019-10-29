using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCanvasDiceSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Popup(string name, string info)
    {
        Utilities.SearchChild("DiceName", this.gameObject).GetComponent<Text>().text = name;
        Utilities.SearchChild("DiceInfo", this.gameObject).GetComponent<Text>().text = info;
        Show();
    }

    public void Show()
    {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<MyUIFade>().FadeIn();
            }
    }

    public void Hide()
    {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<MyUIFade>().FadeOut();
            }
    }
}
