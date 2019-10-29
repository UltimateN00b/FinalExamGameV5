using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AyandaFeels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFeeling(string feeling)
    {
        this.GetComponent<Text>().text = "Ayanda feels " + feeling.ToUpper();
    }
}
