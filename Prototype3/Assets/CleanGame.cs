using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CleanGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject g in allObjects)
        {
            if (!g.Equals(this.gameObject))
            {
                Destroy(g.gameObject);
            }
        }

        SceneManager.LoadScene("Menu");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
