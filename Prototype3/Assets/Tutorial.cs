using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public string tutorialName;
    public UnityEvent m_OnTutorialCalled;
    public UnityEvent m_OnTutorialClosed;

    // Start is called before the first frame update
    void Start()
    {
        if (m_OnTutorialCalled == null)
        {
            m_OnTutorialCalled = new UnityEvent();
        }

        if (m_OnTutorialClosed == null)
        {
            m_OnTutorialClosed = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallOpeningTutorialEvent()
    {
        m_OnTutorialCalled.Invoke();
    }

    public void CallClosingTutorialEvent()
    {
            m_OnTutorialClosed.Invoke();
    }

    public string GetTutorialName()
    {
        return tutorialName;
    }

}
