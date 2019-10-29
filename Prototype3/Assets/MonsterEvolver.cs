using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvolver : MonoBehaviour
{

    private int _evolutionIndex;
    private GameObject _currEvolution;

    // Start is called before the first frame update
    void Start()
    {
        GameObject evolutionHolder = GameObject.Find("EvolutionHolder");
        _evolutionIndex = 0;
        _currEvolution = evolutionHolder.transform.GetChild(_evolutionIndex).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Evolve()
    {
        _evolutionIndex++;
        GameObject evolutionHolder = GameObject.Find("EvolutionHolder");
        _currEvolution = evolutionHolder.transform.GetChild(_evolutionIndex).gameObject;

        this.GetComponent<SpriteRenderer>().sprite = _currEvolution.GetComponent<Evolution>().enemySprite;
        AudioManager.PlaySound(Resources.Load("Heartbeat") as AudioClip);
    }

    public void FinaliseEvolution()
    {
        GameObject evolutionHolder = GameObject.Find("EvolutionHolder");
        evolutionHolder.GetComponent<EvolutionHolder>().FinaliseEvolution(_currEvolution);
    }
}
