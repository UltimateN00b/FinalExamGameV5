using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurrenderButton : MonoBehaviour
{
    private static bool _surrendered;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        //_surrendered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetSurrendered(bool surrender)
    {
        _surrendered = surrender;
    }

    public static bool HasSurrendered()
    {
        return _surrendered;
    }

    public void Surrender()
    {
        _surrendered = true;
        GameObject.Find("Ayanda").GetComponent<Animator>().SetBool("dying", true);
    }

    public void Show()
    {
        if (!TutorialManager.IsTutorial())
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
