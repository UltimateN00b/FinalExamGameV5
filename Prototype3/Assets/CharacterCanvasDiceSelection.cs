using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanvasDiceSelection : MonoBehaviour
{

    public GameObject buttonPrefab;
    public float buttonGap;

    // Start is called before the first frame update
    void Start()
    {
        GameObject baseButton = this.transform.GetChild(0).gameObject;
        Vector3 buttonPos = baseButton.GetComponent<RectTransform>().position;
        Destroy(baseButton.gameObject);

        GameObject relationshipHolder = GameObject.Find("RelationshipHolder");

        foreach (Relationship r in relationshipHolder.GetComponents<Relationship>())
        {
            if (r.HasBeenDiscovered())
            {
                MakeButton(buttonPos, r);
                buttonPos.y -= buttonGap;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeButton(Vector3 position, Relationship r)
    {
        GameObject newButton = Instantiate(buttonPrefab, this.GetComponent<RectTransform>(), false);
        newButton.GetComponent<RectTransform>().position = position;
        newButton.GetComponent<CharacterButtonDiceSelection>().ChangeButton(r.GetCharacterSprite(), r.GetCharacterName(), r.GetCurrLevel(), r.GetCharacterColour());
    }

    private void SetParentAndReset(RectTransform objectRect, Transform parent)
    {
        objectRect.SetParent(parent);
        objectRect.localScale = Vector3.one;
        objectRect.localPosition = Vector3.zero;
    }

    public void Hide()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
