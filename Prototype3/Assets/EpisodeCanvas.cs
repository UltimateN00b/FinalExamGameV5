using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayEpisodeEffect()
    {
        Camera.main.GetComponent<AudioFader>().FadeOut();

        GameObject anxietyBG = Utilities.SearchChild("AnxietyBG", this.gameObject);
        GameObject anxietyEffect = Utilities.SearchChild("AnxietyEffect", this.gameObject);
        anxietyBG.GetComponent<MyUIFade>().FadeIn();
        anxietyEffect.GetComponent<Image>().enabled = true;
        anxietyEffect.GetComponent<Animator>().Play("AnxietyAnim", -1, 0f);
        AudioManager.PlaySound(Resources.Load("Heartbeat") as AudioClip);
    }

    public void StopEpisodeEffect()
    {
        Camera.main.GetComponent<AudioFader>().FadeIn();

        GameObject anxietyBG = Utilities.SearchChild("AnxietyBG", this.gameObject);
        GameObject anxietyEffect = Utilities.SearchChild("AnxietyEffect", this.gameObject);
        anxietyBG.GetComponent<MyUIFade>().FadeOut();
        anxietyEffect.GetComponent<Image>().enabled = false;
        anxietyEffect.GetComponent<Animator>().StopPlayback();
    }
}
