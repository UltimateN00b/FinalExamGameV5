using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurrenderTut : MonoBehaviour
{
    private static bool _hasShown;

    // Start is called before the first frame update
    void Start()
    {
        if (!_hasShown)
        {
            if (!TutorialManager.IsTutorial())
            {
                this.GetComponent<MyImage>().FadeIn();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<SpriteRenderer>().color.a >= 1)
        {
            _hasShown = true;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                this.GetComponent<MyImage>().FadeOut();
            }
        }

        if (_hasShown)
        {
            if (this.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
