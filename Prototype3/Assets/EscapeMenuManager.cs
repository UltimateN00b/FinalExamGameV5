using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite normalHeart;
    public Sprite greyHeart;
    public Sprite brokenHeart;

    public List<Sprite> characterPortraitSprites;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateDay()
    {
        Utilities.SearchChild("DayNum", this.gameObject).GetComponent<Text>().text = NextDaySceneStarter.GetDayNum().ToString();
    }

    public void UpdateAyandaSleep()
    {
        if (GameObject.Find("SleepValueHolder") != null)
        {
            UpdateDay();

            AyandaFeels ayandaFeels = Utilities.SearchChild("AyandaFeels", this.gameObject).GetComponent<AyandaFeels>();
            AyandaPortrait ayandaPortrait = Utilities.SearchChild("AyandaPortrait", this.gameObject).GetComponent<AyandaPortrait>();

            float sleepValue = SleepValueHolder.GetSleepValue();

            string sleepString = "";

            if (sleepValue <= 1 && sleepValue >= 0.8f)
            {
                sleepString = "Energised";
            }
            else if (sleepValue <= 0.79f && sleepValue >= 0.6f)
            {
                sleepString = "Rested";
            }
            else if (sleepValue <= 0.59f && sleepValue >= 0.4f)
            {
                sleepString = "Awake...ish";
            }
            else if (sleepValue <= 0.39f && sleepValue >= 0.2f)
            {
                sleepString = "Tired";
            }
            else
            {
                sleepString = "Exhausted";
            }

            ayandaFeels.ChangeFeeling(sleepString);

            if (!sleepString.Equals("Awake...ish")) //The sprite name probably shouldn't include an ellipses - so this check is just for safety purposes 
            {
                ayandaPortrait.ChangeSprite(sleepString);
            }
            else
            {
                ayandaPortrait.ChangeSprite("Awakeish");
            }
        }
        else
        {
            Debug.Log("SLEEP METER IS NOT IN THIS SCENE!");
        }
    }


    public void UpdateAllRelationships()
    {
        if (GameObject.Find("RelationshipHolder") != null)
        {
            foreach (Relationship r in GameObject.Find("RelationshipHolder").GetComponents<Relationship>())
            {
                if (r.HasBeenDiscovered())
                {
                    this.GetComponent<EscapeMenuManager>().UpdateRelationship(r.GetCharacterName(), r.GetCurrLevel());
                }
            }
        }
    }

    private void UpdateRelationship(string charName, int level)
    {
        GameObject relationships = Utilities.SearchChild("Relationships", this.gameObject);

        GameObject foundRelationship = null;

        for (int i = 0; i < relationships.transform.childCount; i++)
        {

            if (relationships.transform.GetChild(i).gameObject.name.ToUpper().Contains(charName.ToUpper()))
            {
                foundRelationship = relationships.transform.GetChild(i).gameObject;
            }
        }

        if (foundRelationship == null)
        {
            bool foundEmpty = false;

            for (int i = 0; i < relationships.transform.childCount; i++)
            {
                if (!foundEmpty)
                {
                    if (relationships.transform.GetChild(i).gameObject.name.Contains("Hearts_Relationship"))
                    {
                        foundRelationship = relationships.transform.GetChild(i).gameObject;
                        foundRelationship.gameObject.name = charName;
                        foundEmpty = true;
                    }
                }
            }
        }

        Sprite portraitSprite = null;

        foreach (Sprite s in characterPortraitSprites)
        {
            if (s.name.ToUpper().Contains(charName.ToUpper()))
            {
                portraitSprite = s;
            }
        }

        foundRelationship.GetComponent<EscapeMenuCharacterPortrait>().UpdateCharacterPortrait(portraitSprite);

        UpdateHeartLevel(foundRelationship, level);
    }

    private void UpdateHeartLevel(GameObject hearts_relationship, int level)
    {
        for (int i = 0; i < hearts_relationship.transform.childCount; i++)
        {
            hearts_relationship.transform.GetChild(i).GetComponent<Image>().sprite = greyHeart;
        }

        if (level >= 1)
        {
            for (int i = 0; i < level; i++)
            {
                hearts_relationship.transform.GetChild(i).GetComponent<Image>().sprite = normalHeart;
            }
        }

        if (level < 0)
        {
            for (int i = 0; i < Mathf.Abs(level); i++)
            {
                hearts_relationship.transform.GetChild(i).GetComponent<Image>().sprite = brokenHeart;
            }
        }
    }
}
