using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RelationshipCanvas : MonoBehaviour
{
    public float barFillAmount = 0.01f;
    public float waitTimeBeforeFade = 1f;

    private Relationship _currRelationship;

    private bool _increaseBar;
    private float _reachAmount;
    private bool _changeAmountIsPositive;

    private bool _startWaitBeforeFadeTimer;
    private float _fadeTimer;

    private int _levelStringLength;
    private bool _hasUpdatedLevelStringLength;

    public GameObject happyParticles;
    public GameObject upsetParticles;

    // Start is called before the first frame update
    private void Awake()
    {
        _levelStringLength = 1;
        _hasUpdatedLevelStringLength = false;
        Hide();
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _currRelationship = null;
        _increaseBar = false;

        Hide();

        _fadeTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        string levelString = Utilities.SearchChild("Level", this.gameObject).GetComponent<Text>().text;
        levelString = levelString.Substring(levelString.Length - 1);
        int level = int.Parse(levelString);

        if (_increaseBar)
        {
            GameObject relationshipBar = Utilities.SearchChild("RelationshipBar", this.gameObject);
            if (relationshipBar.GetComponent<Image>().fillAmount < _reachAmount && _changeAmountIsPositive)
            {

                    relationshipBar.GetComponent<Image>().fillAmount += barFillAmount*Time.deltaTime;

                if (_reachAmount >= 1)
                {
                    if (relationshipBar.GetComponent<Image>().fillAmount >= 1 || relationshipBar.GetComponent<Image>().fillAmount <= 0)
                    {
                        UpdateLevel();
                        _reachAmount -= 1;
                    }
                }
            } else if (relationshipBar.GetComponent<Image>().fillAmount > _reachAmount && !_changeAmountIsPositive)
            {

                relationshipBar.GetComponent<Image>().fillAmount -= barFillAmount * Time.deltaTime;

                if (_reachAmount <= 0)
                {
                    if (relationshipBar.GetComponent<Image>().fillAmount >= 1 || relationshipBar.GetComponent<Image>().fillAmount <= 0)
                    {
                        UpdateLevel();
                        _reachAmount += 1;
                    }
                }
            }
            else
            {
                //If the bar is full or empty, change current level and reset bar
                //UpdateLevel();


                //Update relationship holder
                levelString = Utilities.SearchChild("Level", this.gameObject).GetComponent<Text>().text;
                levelString = levelString.Substring(levelString.Length - 1);
                level = int.Parse(levelString);
                _currRelationship.SetCurrLevel(level);
                _currRelationship.SetProgress(relationshipBar.GetComponent<Image>().fillAmount);

                _startWaitBeforeFadeTimer = true;

                //Stop changing the bar
                _increaseBar = false;

                //Set curr relationship back to null
                _currRelationship = null;
            }
        }

        //Fade out after time
        if (_startWaitBeforeFadeTimer)
        {
            if (_fadeTimer < waitTimeBeforeFade)
            {
                _fadeTimer += Time.deltaTime;
            }
            else
            {
                Hide();

                _fadeTimer = 0.0f;
                _startWaitBeforeFadeTimer = false;
            }

        }

        GameObject.Find("OverallController").GetComponent<OverallGameController>().GetInstructionsCanvas().GetComponent<EscapeMenuManager>().UpdateAllRelationships();
    }

    public void SetCurrRelationship(string character)
    {
        GameObject relationshipHolder = Utilities.SearchChild("RelationshipHolder", this.gameObject);

        foreach (Relationship r in relationshipHolder.GetComponents<Relationship>())
        {
            if (r.GetCharacterName().ToUpper().Contains(character.ToUpper()))
            {
                _currRelationship = r;
            }
        }

        _currRelationship.SetDiscovered();
    }

    public void UpdateRelationship(float changeAmount)
    {
        if (_currRelationship == null)
        {
            Debug.Log("CURRENT RELATIONSHIP IS NULL!");
        }
        else
        {
            //Change relationship info
            Utilities.SearchChild("Name", this.gameObject).GetComponent<Text>().text = _currRelationship.GetCharacterName();
            Utilities.SearchChild("Level", this.gameObject).GetComponent<Text>().text = "Lvl: "+_currRelationship.GetCurrLevel().ToString();
            Utilities.SearchChild("RelationshipBar", this.gameObject).GetComponent<Image>().fillAmount = _currRelationship.GetProgress();
            _currRelationship.SetCurrLevel(_currRelationship.GetCurrLevel());

            //ShowRelationship
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Show();
            }

            //Change bar level on display
            GameObject relationshipBar = Utilities.SearchChild("RelationshipBar", this.gameObject);
            _reachAmount = relationshipBar.GetComponent<Image>().fillAmount + changeAmount;

            Vector3 particlePos = GameObject.Find(_currRelationship.GetCharacterName() + "_ChatBot").transform.position;
            particlePos.z -= 3f;

            if (changeAmount > 0)
            {
                _changeAmountIsPositive = true;
                Instantiate(happyParticles, particlePos, Quaternion.identity);
                AudioManager.PlaySound(Resources.Load("Reward") as AudioClip);
            } else
            {
                _changeAmountIsPositive = false;
                Instantiate(upsetParticles, particlePos, Quaternion.identity);
                AudioManager.PlaySound(Resources.Load("Rolling a1") as AudioClip);
            }

            _increaseBar = true;
        }

        GameObject.Find("OverallController").GetComponent<OverallGameController>().GetInstructionsCanvas().GetComponent<EscapeMenuManager>().UpdateAllRelationships();
    }

    private void UpdateLevel()
    {
        GameObject relationshipBar = Utilities.SearchChild("RelationshipBar", this.gameObject);

        string levelString = Utilities.SearchChild("Level", this.gameObject).GetComponent<Text>().text;

        if (int.Parse(levelString.Substring(levelString.Length - 1)) == 0)
        {
            if (!_hasUpdatedLevelStringLength)
            {
                _levelStringLength += 1;
                _hasUpdatedLevelStringLength = true;
            }
        } else
        {
            _hasUpdatedLevelStringLength = false;
        }

        levelString = levelString.Substring(levelString.Length - _levelStringLength);

        int level = int.Parse(levelString);

        if (relationshipBar.GetComponent<Image>().fillAmount >= 1)
        {
            level += 1;
            relationshipBar.GetComponent<Image>().fillAmount = 0;
        }
        else if (relationshipBar.GetComponent<Image>().fillAmount <= 0)
        {
            level -= 1;
            relationshipBar.GetComponent<Image>().fillAmount = 0;
        }

        Utilities.SearchChild("Level", this.gameObject).GetComponent<Text>().text = "Lvl: " + level.ToString();
        _currRelationship.SetCurrLevel(level);

        GameObject.Find("OverallController").GetComponent<OverallGameController>().GetInstructionsCanvas().GetComponent<EscapeMenuManager>().UpdateAllRelationships();
        GameObject.Find("CharacterInfoUpdated").GetComponent<BillboardMessage>().ShowMessage();
    }

    private void Hide()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<MyUIFade>().FadeOut();
            if (this.transform.GetChild(i).name.Contains("RelationshipBar"))
            {
                this.transform.GetChild(i).transform.GetChild(0).GetComponent<MyUIFade>().FadeOut();
            }
        }
    }

    private void Show()
    {
       // We keep the old UI hidden.


        //for (int i = 0; i < this.transform.childCount; i++)
        //{
        //    this.transform.GetChild(i).GetComponent<MyUIFade>().FadeIn();
        //    if (this.transform.GetChild(i).name.Contains("RelationshipBar"))
        //    {
        //        this.transform.GetChild(i).transform.GetChild(0).GetComponent<MyUIFade>().FadeIn();
        //    }
        //}
    }
}
