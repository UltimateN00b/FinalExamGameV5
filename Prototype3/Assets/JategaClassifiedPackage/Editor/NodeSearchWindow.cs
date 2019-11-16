﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class NodeSearchWindow : EditorWindow {

    string searchString = "";

    [MenuItem("Window/Search By Text Field")]

    public static void ShowWindow()
    {
        GetWindow<NodeSearchWindow>("Text Field Search");
    }

    private void OnGUI()
    {
        searchString = EditorGUILayout.TextField("Search:", searchString);

        //if (GUILayout.Button("Search Selected Objects"))
        //{
        //    SearchMultipleSelections();
        //}

        //if (GUILayout.Button("Search Child Objects"))
        //{
        //    SearchChildren();
        //}

        //if (GUILayout.Button("Search Children of Parents"))
        //{
        //    SearchChildrenOfParent();
        //}

        if (GUILayout.Button("Search Dialogue Nodes"))
        {
            SearchNodes();
        }

        if (GUILayout.Button("Search Game Objects"))
        {
            SearchAllObjects();
        }

        if (GUILayout.Button("Search Characters"))
        {
            SearchCharacters();
        }

        if (GUILayout.Button("Search Choices"))
        {
            SearchChoices();
        }

        if (GUILayout.Button("Select Only Choice Nodes"))
        {
            SelectOnlyChoiceNodes();
        }
    }

    private void SearchMultipleSelections()
    {
        GameObject[] searchMultipleObjects = Selection.gameObjects;

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchMultipleObjects.Length; i++)
        {
            GameObject currChild = searchMultipleObjects[i];

            if (currChild.GetComponent<Text>() != null)
            {
                if (currChild.GetComponent<Text>().text.ToUpper().Contains(searchString.ToUpper()))
                {
                    finalSelection.Add(currChild);
                }
            }
        }

        Selection.objects = finalSelection.ToArray();
    }

    private void SearchChildren()
    {
        GameObject searchObject = Selection.activeGameObject;

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchObject.transform.childCount; i++)
        {
            GameObject currChild = searchObject.transform.GetChild(i).gameObject;

            if (currChild.GetComponent<Text>()!= null)
            {
                if (currChild.GetComponent<Text>().text.ToUpper().Contains(searchString.ToUpper()))
                {
                   finalSelection.Add(currChild);
                }
            }
        }

        Selection.objects = finalSelection.ToArray();
    }

    private void SearchChildrenOfParent()
    {
        GameObject searchObject = Selection.activeGameObject.transform.parent.gameObject;

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchObject.transform.childCount; i++)
        {
            GameObject currChild = searchObject.transform.GetChild(i).gameObject;

            if (currChild.GetComponent<Text>().text.ToUpper().Contains(searchString.ToUpper()))
            {
                if (currChild.GetComponent<Text>().text.Contains(searchString))
                {
                    finalSelection.Add(currChild);
                }
            }
        }

        Selection.objects = finalSelection.ToArray();
    }

    private void SearchNodes()
    {
        GameObject searchObject = GameObject.Find("NodesCanvas");

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchObject.transform.childCount; i++)
        {
            GameObject currChild = searchObject.transform.GetChild(i).gameObject;

            if (currChild.GetComponent<Text>() != null)
            {
                if (currChild.GetComponent<Text>().text.ToUpper().Contains(searchString.ToUpper()))
                {
                    finalSelection.Add(currChild);
                }
            }
        }

        Selection.objects = finalSelection.ToArray();
    }

    private void SearchAllObjects()
    {
        GameObject[] searchAllObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchAllObjects.Length; i++)
        {
            GameObject currChild = searchAllObjects[i];

                if (currChild.name.ToUpper().Contains(searchString.ToUpper()))
                {
                    finalSelection.Add(currChild);
                }
        }

        Selection.objects = finalSelection.ToArray();
    }

    private void SearchCharacters()
    {
        GameObject searchObject = GameObject.Find("NodesCanvas");

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchObject.transform.childCount; i++)
        {
            GameObject currChild = searchObject.transform.GetChild(i).gameObject;
            string currCharacterName = currChild.GetComponent<MainText>().GetCurrCharacter();

            if (currCharacterName != null)
            {
                if (currCharacterName.ToUpper().Contains(searchString.ToUpper()))
                {
                    finalSelection.Add(currChild);
                }
            }
        }

        Selection.objects = finalSelection.ToArray();
    }

    private void SearchChoices()
    {
        GameObject searchObject = GameObject.Find("NodesCanvas");

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchObject.transform.childCount; i++)
        {
            GameObject currChild = searchObject.transform.GetChild(i).gameObject;

            if (currChild.GetComponent<Text>() != null)
            {
                for (int j = 0; j < currChild.transform.childCount; j++)
                {
                    if (currChild.transform.GetChild(j).name.Contains("Choice"))
                    {
                        string choice = currChild.transform.GetChild(j).GetComponent<Text>().text;

                        if (choice.ToUpper().Contains(searchString.ToUpper()))
                        {
                            finalSelection.Add(currChild);
                        }
                    }
                }
            }
        }

        Selection.objects = finalSelection.ToArray();
    }

    private void SelectOnlyChoiceNodes()
    {
        GameObject searchObject = GameObject.Find("NodesCanvas");

        List<GameObject> finalSelection = new List<GameObject>();

        // Do comparison here. For example
        for (int i = 0; i < searchObject.transform.childCount; i++)
        {
            GameObject currChild = searchObject.transform.GetChild(i).gameObject;

            if (currChild.GetComponent<Text>() != null)
            {
                if (currChild.transform.GetChild(0).gameObject.name.Contains("Choice"))
                {
                    finalSelection.Add(currChild);
                }
            }
        }

        Selection.objects = finalSelection.ToArray();
    }
}
