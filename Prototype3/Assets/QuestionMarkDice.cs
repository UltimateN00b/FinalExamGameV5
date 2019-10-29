using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionMarkDice : MonoBehaviour
{
    public Sprite questionMarkSprite;
    public Sprite paralysed;

    // Start is called before the first frame update
    void Start()
    {
        //questionMarkSprite = this.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToParalysed()
    {
        this.GetComponent<Image>().sprite = paralysed;
    }

    public void ChangeToQuestionMark()
    {
        this.GetComponent<Image>().sprite = questionMarkSprite;
    }
}
