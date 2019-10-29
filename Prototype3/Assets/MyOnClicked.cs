using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyOnClicked : MonoBehaviour
{
    public UnityEvent m_OnEnterPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_OnEnterPressed.Invoke();
        }
    }
}
