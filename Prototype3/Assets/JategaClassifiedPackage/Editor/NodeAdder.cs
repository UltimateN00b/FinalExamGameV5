using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class NodeAdder : EditorWindow

{
    string typeString = "";

    [MenuItem("Window/Node Adder")]

    public static void ShowWindow()
    {
        GetWindow<NodeAdder>("Node Adder");
    }

    Vector3 currNodePos = new Vector3();

    private void OnGUI()
    {

        //typeString = EditorGUILayout.TextField("Dialogue:", typeString);
        GUILayout.Label("Type your dialogue here:");

        typeString = EditorGUI.TextArea(new Rect(10, 50, 350, 100), typeString);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Add Nodes"))
        {
            GameObject nodeStem = Selection.activeGameObject;
            currNodePos = nodeStem.GetComponent<RectTransform>().localPosition;
            AddNodes(typeString);
        }
    }

    private void AddNodes(string input)
    {
        //string line = "Doggo: Keep this clean, 'ya hear?* Best Bro: Yes, master doggo.* Homewrecker: Glad that's understood.*";

        string line = input;
        line = line.Replace("\r", "").Replace("\n", "");

        char[] lineArray = line.ToCharArray();

        string character = "";
        string dialogue = "";

        bool onCharacter = true;

        foreach (char c in lineArray)
        {
            if (onCharacter)
            {
                if (c != ':')
                {
                    character += c;
                } else
                {
                    onCharacter = false;
                }
            } else
            {
                if (c != '*')
                {
                    dialogue += c;
                }
                else
                {

                    char[] charsToTrim = { ' ', '\t' };

                    character = character.TrimStart(charsToTrim);
                    dialogue = dialogue.TrimStart(charsToTrim);

                    character = character.TrimEnd(charsToTrim);
                    dialogue = dialogue.TrimEnd(charsToTrim);

                    character = character.Trim(charsToTrim);
                    dialogue = dialogue.Trim(charsToTrim);

                    MakeNode(character, dialogue);
                    character = "";
                    dialogue = "";
                    onCharacter = true;
                }
            }
        }
    }

    private void MakeNode(string character, string dialogue)
    {
        Debug.Log(character);

        currNodePos.x += 300;

        GameObject node = Resources.Load("NodeTest") as GameObject;

        GameObject newNode = Instantiate(node, currNodePos, Quaternion.identity);
        newNode.transform.parent = GameObject.Find("NodesCanvas").transform;
        newNode.transform.localPosition = currNodePos;

        newNode.GetComponent<Text>().text = dialogue;
        newNode.GetComponent<MainText>().currCharacter = character;
        newNode.GetComponent<MainText>().SetGoToConsecutiveNodeOnClick(true);
    }
}
