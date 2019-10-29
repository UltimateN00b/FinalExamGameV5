using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NegativeEventHolder : MonoBehaviour
{
    private List<NegativeEvent> _myNegativeEvents = new List<NegativeEvent>();
    private int _dialogueIndex;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueIndex = 0;

        foreach (NegativeEvent n in this.gameObject.GetComponents<NegativeEvent>())
        {
            _myNegativeEvents.Add(n);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name.Contains("TinyDiceDungeonCombat"))
        {
            Destroy(this.gameObject);
        }
    }

    public NegativeEvent SearchNegativeEvents(string searchString)
    {
        NegativeEvent returnNE = null;

        foreach (NegativeEvent n in this.gameObject.GetComponents<NegativeEvent>())
        {
            if (n.GetName().ToUpper().Contains(searchString.ToUpper()))
            {
                returnNE = n;
            }
        }

        return returnNE;
    }

    public void GoToNextNEDialogue()
    {
        string searchString = null;
        int nextNodeNum = -1;

        bool noMoreTriggeredEvents = true;

        if (_dialogueIndex < _myNegativeEvents.Count)
        {
            if (!_myNegativeEvents[_dialogueIndex].IsTriggered())
            {
                for (int i = _dialogueIndex; i < _myNegativeEvents.Count; i++)
                {
                    if (_myNegativeEvents[i].IsTriggered())
                    {
                        if (noMoreTriggeredEvents)
                        {
                            _dialogueIndex = i;
                            noMoreTriggeredEvents = false;
                        }
                    }
                }
            }
            else
            {
                noMoreTriggeredEvents = false;
            }
        } else
        {
            noMoreTriggeredEvents = true;
        }

        if (_dialogueIndex > _myNegativeEvents.Count-1 || noMoreTriggeredEvents)
        {
            searchString = "###SLEEPTIME###";
        }
        else
        {
            searchString = "###" + _myNegativeEvents[_dialogueIndex].GetName() + "###";
        }

        GameObject searchObject = GameObject.Find("NodesCanvas");

        // Search nodes for event
        for (int i = 0; i < searchObject.transform.childCount; i++)
        {
            GameObject currChild = searchObject.transform.GetChild(i).gameObject;

            if (currChild.GetComponent<Text>() != null)
            {
                if (currChild.GetComponent<Text>().text.ToUpper().Contains(searchString.ToUpper()))
                {
                    int nodeNum = int.Parse(Utilities.SearchChild("NodeNum", currChild).GetComponent<Text>().text);
                    nextNodeNum = nodeNum + 1;
                }
            }
        }

        _dialogueIndex++;

        if (nextNodeNum != -1)
        {
            GameObject.Find("DialogueCanvasDecorated").GetComponent<DialogueBox>().ChangeNode(nextNodeNum);
        }
        else
        {
            Debug.Log("NEGATIVE EVENT DIALOGUE NOT FOUND");
        }
    }
}
