using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relationship : MonoBehaviour
{
    public string characterName;
    public Sprite characterSprite;
    public Color characterColour;
    private float _currLevel;

    public bool _discovered;

    // Start is called before the first frame update
    void Start()
    {
       _currLevel = 0;
       _discovered = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string GetCharacterName()
    {
        return characterName;
    }

    public int GetCurrLevel()
    {
        if (_currLevel >= 0)
        {
            return Mathf.FloorToInt(_currLevel);
        } else
        {
            return (int)(_currLevel);
        }
    }

    public float GetCurrLevelSpecific()
    {
       return _currLevel;
    }

    public Sprite GetCharacterSprite()
    {
        return characterSprite;
    }

    public Color GetCharacterColour()
    {
        return characterColour;
    }

    public void SetCurrLevel(float newLevel)
    {
        _currLevel = newLevel;
    }

    public void SetDiscovered()
    {
        if (!_discovered)
        {
            _discovered = true;
            GameObject.Find("OverallController").GetComponent<OverallGameController>().GetInstructionsCanvas().GetComponent<EscapeMenuManager>().UpdateAllRelationships();
            GameObject.Find("CharacterInfoUpdated").GetComponent<BillboardMessage>().ShowMessage();
        }
    }

    public bool HasBeenDiscovered()
    {
        return _discovered;
    }

}
