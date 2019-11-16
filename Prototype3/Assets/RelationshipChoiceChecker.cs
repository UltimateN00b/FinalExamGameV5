using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class RelationshipChoiceChecker : MonoBehaviour
{
    public string relationshipCharacter;

    public bool relationshipMoreThan; //more than or equal to
    public float relMoreThanNum;

    public bool relationshipLessThan; //less than or equal to
    public float relLessThanNum;

    public bool sleepValueLessThan; //less than or equal to
    public float sleepLessThanNum;

    public bool sleepValueMoreThan; //more than or equal to
    public float sleepMoreThanNum;

    public UnityEvent m_OnConditionsMet;
    public UnityEvent m_OnConditionsNotMet;


    // Start is called before the first frame update
    void Start()
    {
        if (m_OnConditionsMet == null)
        {
            m_OnConditionsMet = new UnityEvent();
        }

        if (m_OnConditionsNotMet == null)
        {
            m_OnConditionsNotMet = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InvokeConditionsMetEvent()
    {
        bool conditionsMet = true;
        Relationship currRelationship = null;

        GameObject relationshipHolder = GameObject.Find("RelationshipHolder");

        foreach (Relationship r in relationshipHolder.GetComponents<Relationship>())
        {
            if (r.GetCharacterName().ToUpper().Contains(relationshipCharacter.ToUpper()))
            {
                currRelationship = r;
            }
        }

        if (relationshipMoreThan && conditionsMet)
        {
            if (currRelationship.GetCurrLevel() < relMoreThanNum)
            {
                conditionsMet = false;
            }
        }

        if (relationshipLessThan && conditionsMet)
        {
            if (currRelationship.GetCurrLevel() > relMoreThanNum)
            {
                conditionsMet = false;
            }
        }

        if (sleepValueLessThan && conditionsMet)
        {
            if (SleepValueHolder.GetSleepValue() > sleepLessThanNum)
            {
                conditionsMet = false;
            }
        }

        if (sleepValueMoreThan && conditionsMet)
        {
            if (SleepValueHolder.GetSleepValue() < sleepMoreThanNum)
            {
                conditionsMet = false;
            }
        }

        if (conditionsMet)
        {
            Debug.Log("CONDITIONS MET!");
            m_OnConditionsMet.Invoke();
        } else
        {
            Debug.Log("CONDITIONS NOT MET!");
            m_OnConditionsNotMet.Invoke();
        }
    }
}
