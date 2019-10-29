using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenuCharacterPortrait : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCharacterPortrait(Sprite newPortrait)
    {
        this.GetComponent<Image>().sprite = newPortrait;
    }
}
