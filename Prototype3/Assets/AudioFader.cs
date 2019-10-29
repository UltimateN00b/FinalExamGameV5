using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFader : MonoBehaviour
{

    public float fadeAmount;

    public bool useTimer = true;
    public float timeBeforeFadeIn = 5;

    private bool _fadeIn;
    private bool _fadeOut;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeIn)
        {
            if (useTimer)
            {
                _timer += Time.deltaTime;

                if (_timer >= timeBeforeFadeIn)
                {
                    if (this.GetComponent<AudioSource>().volume < 1)
                    {
                        this.GetComponent<AudioSource>().volume += fadeAmount;
                    }
                    else
                    {
                        _fadeIn = false;
                        _timer = 0.0f;
                    }
                }
            }
        } else if (_fadeOut)
        {
            if (this.GetComponent<AudioSource>().volume > 0)
            {
                this.GetComponent<AudioSource>().volume -= fadeAmount;
            } else
            {
                _fadeOut = false;
            }
        }
    }

    public void FadeIn()
    {
        _fadeIn = true;
    }

    public void FadeOut()
    {
        _fadeOut = true;
        _fadeIn = false;
    }
}
