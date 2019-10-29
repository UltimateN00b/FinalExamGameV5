using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NegativeEvent : MonoBehaviour
{

    public string eventName;
    private bool _triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTriggered()
    {
        _triggered = true;
    }

    public string GetName()
    {
        return eventName;
    }

    public bool IsTriggered()
    {
        return _triggered;
    }
}
