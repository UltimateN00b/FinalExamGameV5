using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeFader : MonoBehaviour
{
    private bool _isPlaying;
    private bool _stop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<AudioSource>().isPlaying || _stop)
        {
            if (this.GetComponent<AudioSource>().volume > 0 && GameObject.Find("InstructionsCanvas") == null)
            {
                _isPlaying = false;
                this.GetComponent<AudioSource>().loop = false;
                this.GetComponent<AudioFader>().FadeOut();
                Camera.main.GetComponents<AudioFader>()[1].FadeIn();
            }
        }
    }

    public void PlayTheme (AudioClip a_clip)
    {
        _stop = false;

        _isPlaying = true;
        this.GetComponent<AudioSource>().clip = a_clip;
        this.GetComponent<AudioSource>().Play();
       this.GetComponent<AudioFader>().FadeIn();

       Camera.main.GetComponents<AudioFader>()[1].FadeOut();
    }

    public void SetThemeToLoop()
    {
        this.GetComponent<AudioSource>().loop = true;
    }

    public bool IsPlaying()
    {
        return _isPlaying;
    }

    public void ManuallyStop()
    {
        _stop = true;
    }
}
