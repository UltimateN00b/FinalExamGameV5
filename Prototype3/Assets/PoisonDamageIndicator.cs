using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonDamageIndicator : MonoBehaviour
{
    private float _shrinkSpeed = 0.5f;
    private float _fadeSpeed = 0.01f;

    private bool _shrink;

    private Vector3 _originalScale;
    private Color _originalColor;

    // Start is called before the first frame update
    void Start()
    {
        _originalScale = this.transform.localScale;
        _originalColor = this.GetComponent<Text>().color;

        Color myColor = this.GetComponent<Text>().color;
        myColor.a = 0.0f;
        this.GetComponent<Text>().color = myColor;

        _shrink = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shrink)
        {
            if (this.GetComponent<Text>().color.a > 0)
            {
                Vector3 myScale = this.transform.localScale;
                myScale = Vector3.Lerp(myScale, Vector3.zero, _shrinkSpeed * Time.deltaTime);
                this.transform.localScale = myScale;

                Color myColor = this.GetComponent<Text>().color;
                myColor.a -= _fadeSpeed;
                this.GetComponent<Text>().color = myColor;
            } else
            {
                _shrink = false;
            }
        }
    }

    public void ShowPoisonDamage(int num)
    {

        AudioManager.PlaySound(Resources.Load("Heal") as AudioClip);

        this.GetComponent<Text>().text = "POISON DAMAGE\n" + num;
        this.GetComponent<Text>().color = _originalColor;
        this.transform.localScale = _originalScale;

        _shrink = true;
    }
}
