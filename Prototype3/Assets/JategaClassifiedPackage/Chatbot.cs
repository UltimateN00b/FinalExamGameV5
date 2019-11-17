﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chatbot : MonoBehaviour {

    public List<Sprite> myExpressions;

    public float leftPos;
    public float centerPos;
    public float rightPos;

    private List<ChatPosSetter> _myChatPosSetters = new List<ChatPosSetter>();

    private bool _isShowing;

    private static bool _anyShowing;

    private bool _finishedHiding;

    // Use this for initialization
    private void Awake()
    {
    }

    void Start () {
		
	}

    private void LateUpdate()
    {
    }

    // Update is called once per frame
    void Update () {
        if (!_finishedHiding)
        {
            if (this.GetComponent<SpriteRenderer>() != null)
            {
                if (this.GetComponent<SpriteRenderer>().color.a <= 0)
                {
                    ChangeExpression("Default");
                    CheckAllAlone();
                    _finishedHiding = true;
                }
            }

            if (this.GetComponent<Image>() != null)
            {
                if (this.GetComponent<Image>().color.a <= 0)
                {
                    ChangeExpression("Default");
                    CheckAllAlone();
                    _finishedHiding = true;
                }
            }
        }
	}

    public void Show()
    {
        _isShowing = true;

        if (IsAlone())
        {
            this.transform.position = new Vector3(centerPos, this.transform.position.y, this.transform.position.z);
        }
        else
        {
            AdjustAllPositions();
        }

        this.GetComponent<MyImage>().FadeIn();
        GameObject.Find("TalkFader").GetComponent<MyImage>().FadeIn();
    }

    public void Hide()
    {
        _finishedHiding = false;
        _isShowing = false;

        this.GetComponent<MyImage>().FadeOut();

        bool tempShow = false;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Chatbot"))
        {
            if (g.GetComponent<Chatbot>().CheckShowing())
            {
                tempShow = true;
            }
        }

        if (tempShow == false)
        {
            GameObject.Find("TalkFader").GetComponent<MyImage>().FadeOut();
        }
    }

    public bool CheckShowing()
    {
        return _isShowing;
    }

    public void ChangeExpression(string expression)
    {
        if (expression != "")
        {
            Sprite changeSprite = null;

            if (this.GetComponent<SpriteRenderer>() != null)
            {
                changeSprite = this.GetComponent<SpriteRenderer>().sprite;
            }

            if (this.GetComponent<Image>() != null)
            {
                changeSprite = this.GetComponent<Image>().sprite;
            }

            foreach (Sprite s in myExpressions)
            {
                string expressionName = s.name;

                if (expressionName.ToUpper().Contains(expression.ToUpper()))
                {
                    changeSprite = s;
                }
            }

            if (this.GetComponent<SpriteRenderer>() != null)
            {
                this.GetComponent<SpriteRenderer>().sprite = changeSprite;
            }

            if (this.GetComponent<Image>() != null)
            {
                this.GetComponent<Image>().sprite = changeSprite;
            }
        }
    }

    public static bool CheckAnyShowing()
    {
        bool anyShowing = false;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Chatbot"))
        {
            if (g.GetComponent<Chatbot>().CheckShowing())
            {
                anyShowing = true;
            }
        }

        return anyShowing;
    }

    private void AdjustAllPositions()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ChatObject"))
        {
            if (GameObject.Find(g.name + "_ChatBot") != null)
            {
                GameObject currChatBot = GameObject.Find(g.name + "_ChatBot");

                foreach (ChatPosSetter c in currChatBot.GetComponent<Chatbot>().myListChatPosSetters())
                {
                    c.SetPosAccordingToPerson();
                }
            }
        }
    }

    private bool IsAlone()
    {
        bool alone = true;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Chatbot"))
        {
            if (g != this.gameObject)
            {
                if (g.GetComponent<Chatbot>().CheckShowing())
                {
                    alone = false;
                }
            }
        }

        return alone;
    }

    private void CheckAllAlone()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ChatObject"))
        {
            if (GameObject.Find(g + "_ChatBot") != null)
            {
                GameObject currChatBot = GameObject.Find(g + "_ChatBot");
                if (currChatBot.GetComponent<Chatbot>().IsAlone())
                {
                    currChatBot.transform.position = new Vector3(centerPos, this.transform.position.y, this.transform.position.z);
                }
            }
        }
    }

    public void AddChatBotPosSetterToList(ChatPosSetter cP)
    {
        _myChatPosSetters.Add(cP);
    }

    public List<ChatPosSetter> myListChatPosSetters()
    {
        return _myChatPosSetters;
    }
}
