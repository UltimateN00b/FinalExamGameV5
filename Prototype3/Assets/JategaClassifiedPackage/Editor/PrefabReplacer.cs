using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabReplacer : EditorWindow
{

    string findString = "";
    string replaceString = "";

    [MenuItem("Window/Game Object Replacer")]

    public static void ShowWindow()
    {
        GetWindow<PrefabReplacer>("Game Object Replacer");
    }

    private void OnGUI()
    {
        findString = EditorGUILayout.TextField("Find Objects:", findString);
        replaceString = EditorGUILayout.TextField("Replace With:", replaceString);

        if (GUILayout.Button("Find Objects"))
        {
            FindObjects();
        }

        if (GUILayout.Button("Replace Objects"))
        {
            ReplaceObjects();
        }
    }

    private void FindObjects()
    {
        GameObject searchObject = Selection.activeGameObject;

        List<GameObject> finalSelection = new List<GameObject>();
        finalSelection.Add(GameObject.Find(findString));
        Selection.objects = finalSelection.ToArray();
    }

    private void ReplaceObjects()
    {
        GameObject replacementObject = Resources.Load(replaceString) as GameObject;

        GameObject newObject = Instantiate(replacementObject, Selection.activeGameObject.transform.position, Quaternion.identity);
        newObject.transform.parent = Selection.activeGameObject.transform.parent;
        newObject.transform.localPosition = Selection.activeGameObject.transform.localPosition;

        DestroyImmediate(Selection.activeGameObject);
    }
}
