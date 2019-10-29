using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poison : MonoBehaviour
{
    private bool _showing;

    private bool _hasDealtDamage;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        _hasDealtDamage = false;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void Show()
    {
        this.gameObject.GetComponent<Image>().enabled = true;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<Text>().enabled = true; ;
        }

        _showing = true;
    }

    public void Hide()
    {
        this.gameObject.GetComponent<Image>().enabled = false;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<Text>().enabled = false; ;
        }

        _showing = false;
    }

    public void AddPoison(int numPoison)
    {
        Show();
        GameObject poisonCountObject = Utilities.SearchChild("PoisonCount", this.gameObject);
        int poisonCount = int.Parse(poisonCountObject.GetComponent<Text>().text);
        poisonCount += numPoison;
        poisonCountObject.GetComponent<Text>().text = poisonCount.ToString();
    }

    public void DealPoisonDamage()
    {
        GameObject poisonCountObject = Utilities.SearchChild("PoisonCount", this.gameObject);
        int poisonCount = int.Parse(poisonCountObject.GetComponent<Text>().text);


        if (_showing)
        {
            //Change health bar
            GameObject currCharacter = this.transform.parent.transform.parent.gameObject;
            GameObject healthCanvas = Utilities.SearchChild("HealthCanvas", currCharacter.gameObject);
            GameObject healthBar = Utilities.SearchChild("HealthBar", healthCanvas);

            healthBar.GetComponent<HealthBar>().ChangeHealth(-poisonCount);

            Utilities.SearchChild("PoisonDamageIndicator", this.transform.parent.gameObject).GetComponent<PoisonDamageIndicator>().ShowPoisonDamage(-poisonCount);

            //TO RESTORE OLD POISON DAMAGE METHOD, UNCOMMENT THE LINES BELOW:
            //====================================================================================================
            //Divide poison damage by two, rounded down to smallest integer)

            //    float poisonCountFloat = (float)poisonCount;

            //    poisonCountFloat = Mathf.Floor(poisonCountFloat * 0.5f);

            //poisonCount = (int) poisonCountFloat;

            //=====================================================================================================

            //TO GET RID OF THE NEW POISON DAMAGE METHOD, COMMENT OUT THE LINES BELOW:
            //====================================================================================================
            if (!_hasDealtDamage)
            {
                float poisonCountFloat = (float)poisonCount;
                poisonCountFloat = Mathf.Floor(poisonCountFloat * 0.5f);
                poisonCount = (int)poisonCountFloat;
                _hasDealtDamage = true;
            }
            else
            {
                poisonCount = 0;
                Hide();
            }
            //====================================================================================================

            poisonCountObject.GetComponent<Text>().text = poisonCount.ToString();
        }

        //If poison count less than or equal to zero, hide it and thus stop poison damage from being dealt every turn
        if (poisonCount <= 0)
        {
            Hide();
            _hasDealtDamage = false;
        }
    }
}
