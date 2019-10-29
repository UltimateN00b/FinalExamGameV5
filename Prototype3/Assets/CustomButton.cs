using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour
{
    public UnityEvent m_OnButtonDown;

    public Color onMouseOver;
    public Color onMouseDown;
    public Color disabled;

    private Color _originalColor;
    private bool _disabled;

    // Start is called before the first frame update
    void Awake()
    {
        _originalColor = this.GetComponent<SpriteRenderer>().color;

        if (m_OnButtonDown == null)
        {
            m_OnButtonDown = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        if (!_disabled)
        {
            this.GetComponent<SpriteRenderer>().color = onMouseOver;
        }
    }

    private void OnMouseExit()
    {
        if (!_disabled)
        {
            this.GetComponent<SpriteRenderer>().color = _originalColor;
        }
    }

    private void OnMouseDown()
    {
        if (!_disabled)
        {
            this.GetComponent<SpriteRenderer>().color = onMouseDown;
            m_OnButtonDown.Invoke();
        }
    }

    public void Disable()
    {
        _disabled = true;
        this.GetComponent<SpriteRenderer>().color = disabled;
    }

    public void Enable()
    {
        _disabled = false;
        this.GetComponent<SpriteRenderer>().color = _originalColor;
    }
}
