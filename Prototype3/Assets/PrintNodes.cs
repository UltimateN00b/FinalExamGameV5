using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintNodes : MonoBehaviour
{
    public bool printProofCopy;
    public bool printNodeNumber;
    public int startFrom;

    // Start is called before the first frame update
    void Start()
    {
        if (printProofCopy)
        {
            string proofCopy = "";

            for (int i = startFrom; i < this.transform.childCount; i++)
            {
                GameObject currChild = this.transform.GetChild(i).gameObject;

                proofCopy += currChild.GetComponent<MainText>().GetCurrCharacter() + ": ";
                proofCopy += currChild.GetComponent<MainText>().GetMainText() + "*";

                for (int j = 0; j < currChild.transform.childCount; j++)
                {
                    if (currChild.transform.GetChild(j).name.Contains("NodeNum"))
                    {
                        if (printNodeNumber)
                        {
                            proofCopy += "\nNode " + currChild.transform.GetChild(j).GetComponent<Text>().text + ":";
                        }

                    }
                    else
                    {
                        proofCopy += "\n";
                        proofCopy += "Choice " + j + ": ";
                        proofCopy += currChild.transform.GetChild(j).GetComponent<Choice>().GetChoiceText();
                    }
                }

                proofCopy += "\n";
            }

            Debug.Log(proofCopy);
        }

    }
}
