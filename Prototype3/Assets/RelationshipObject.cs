using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipObject : MonoBehaviour
{

    private Relationship _currRelationship;
    public GameObject happyParticles;
    public GameObject upsetParticles;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _currRelationship = null;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            float tempLevel = _currRelationship.GetCurrLevel();

            _currRelationship.SetCurrLevel(_currRelationship.GetCurrLevelSpecific()+changeAmount);

            Vector3 particlePos = GameObject.Find(_currRelationship.GetCharacterName() + "_ChatBot").transform.position;
            particlePos.z -= 3f;

            if (changeAmount > 0)
            {
                Instantiate(happyParticles, particlePos, Quaternion.identity);
                AudioManager.PlaySound(Resources.Load("Reward") as AudioClip);
            }
            else
            {
                Instantiate(upsetParticles, particlePos, Quaternion.identity);
                AudioManager.PlaySound(Resources.Load("Rolling a1") as AudioClip);
            }

            GameObject.Find("OverallController").GetComponent<OverallGameController>().GetInstructionsCanvas().GetComponent<EscapeMenuManager>().UpdateAllRelationships();

            if (Mathf.FloorToInt(tempLevel) < _currRelationship.GetCurrLevel())
            {
                GameObject.Find("CharacterInfoUpdated").GetComponent<BillboardMessage>().ShowMessage();
            }
        }
    }
}
