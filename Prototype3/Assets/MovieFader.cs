using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieFader : MonoBehaviour
{
    public float fadeAmount;
    public bool fadeOnStart = false;

    private bool _fadeIn;
    private bool _fadeOut;

    private void Start()
    {
        //_fadeIn = false;
        //_fadeOut = false;

        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    private void Update()
    {
        ControlFade();
    }

    public void ControlFade()
    {
        float myAlpha = this.GetComponent<VideoPlayer>().targetCameraAlpha;

        if (_fadeIn)
        {
            if (myAlpha <= 1)
            {
                myAlpha += fadeAmount;
            }
            else
            {
                _fadeIn = false;
            }
        }
        else if (_fadeOut)
        {
            if (myAlpha >= 0)
            {
                myAlpha -= fadeAmount;
            }
            else
            {
                _fadeOut = false;
            }
        }

        this.GetComponent<VideoPlayer>().targetCameraAlpha = myAlpha;
    }

    public void FadeIn()
    {
        _fadeIn = true;
    }

    public void FadeOut()
    {
        _fadeOut = true;
    }

}
