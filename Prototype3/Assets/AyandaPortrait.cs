using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AyandaPortrait : MonoBehaviour
{

    public List<Sprite> levelOfTiredPics;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite (string newSprite)
    {
        foreach (Sprite s in levelOfTiredPics)
        {
            if (s.name.ToUpper().Contains(newSprite.ToUpper()))
            {
                this.GetComponent<Image>().sprite = s;
            }
        }
    }
}
