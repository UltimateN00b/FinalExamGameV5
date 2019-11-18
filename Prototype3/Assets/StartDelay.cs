using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartDelay : MonoBehaviour
{
    public float time;
    public UnityEvent m_OnDelayFinished;

    private float _timer;
    private bool _stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (m_OnDelayFinished == null)
        {
            m_OnDelayFinished = new UnityEvent();
        }

        _timer = 0.0f;

        _stopTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stopTimer)
        {
            _timer += Time.deltaTime;

            if (_timer >= time)
            {
                m_OnDelayFinished.Invoke();
                _stopTimer = true;
            }
        }
    }
}
