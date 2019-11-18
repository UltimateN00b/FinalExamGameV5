using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLanaSound(AudioClip a_clip)
    {
            Utilities.SearchChild("L_Sound", this.gameObject).GetComponent<AudioSource>().clip = a_clip;
            Utilities.SearchChild("L_Sound", this.gameObject).GetComponent<AudioSource>().Play();
    }

    public void PlayJoeySound(AudioClip a_clip)
    {
        Utilities.SearchChild("J_Sound", this.gameObject).GetComponent<AudioSource>().clip = a_clip;
        Utilities.SearchChild("J_Sound", this.gameObject).GetComponent<AudioSource>().Play();
    }

    public void PlayNtandoSound(AudioClip a_clip)
    {
        Utilities.SearchChild("N_Sound", this.gameObject).GetComponent<AudioSource>().clip = a_clip;
        Utilities.SearchChild("N_Sound", this.gameObject).GetComponent<AudioSource>().Play();
    }

    public void PlayEngelsSound(AudioClip a_clip)
    {
        Utilities.SearchChild("E_Sound", this.gameObject).GetComponent<AudioSource>().clip = a_clip;
        Utilities.SearchChild("E_Sound", this.gameObject).GetComponent<AudioSource>().Play();
    }
}
