using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relationship : MonoBehaviour
{
    public string characterName;
    public float startingProgressToNextLevel;
    public Sprite characterSprite;
    public Color characterColour;
    private int _currLevel;
    private float _currProgressToNextLevel;

    private bool _discovered;

    // Start is called before the first frame update
    void Start()
    {
        _currLevel = 0;
        _currProgressToNextLevel = startingProgressToNextLevel;
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
        return _currLevel;
    }

    public float GetProgress()
    {
        return _currProgressToNextLevel;
    }

    public Sprite GetCharacterSprite()
    {
        return characterSprite;
    }

    public Color GetCharacterColour()
    {
        return characterColour;
    }

    public void SetCurrLevel(int newLevel)
    {
        _currLevel = newLevel;
    }

    public void SetProgress(float newProgress)
    {
        _currProgressToNextLevel = newProgress;
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
