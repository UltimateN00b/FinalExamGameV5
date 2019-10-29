using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDiceProfile : MonoBehaviour
{
    public string characterName;
    public Sprite characterImage;
    public List<int> unlockLevels;
    public List<string> diceTypes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetCharacterName()
    {
        return characterName;
    }

    public List<int> GetUnlockLevels()
    {
        return unlockLevels;
    }

    public List<string> GetDiceTypes()
    {
        return diceTypes;
    }

    public Sprite GetCharacterImage()
    {
        return characterImage;
    }
}
