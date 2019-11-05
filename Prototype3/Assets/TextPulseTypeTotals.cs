using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPulseTypeTotals : MonoBehaviour
{
    public float pulseAmount;
    public float pulseSpeed;
    public bool continous;

    public Color overTen;
    public Color overThirty;

    private bool _pulse;
    private Vector3 _originalScale;

    private bool _grow;

    private int _previousTypeTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        _pulse = true;
        _originalScale = this.GetComponent<RectTransform>().localScale;
        Pulse();
    }

    // Update is called once per frame
    void Update()
    {
       if (_pulse)
        {
            Vector3 myScale = this.GetComponent<RectTransform>().localScale;

            if (_grow)
            {
                myScale += Vector3.one * pulseSpeed * Time.deltaTime;

                if (myScale.x > _originalScale.x + pulseAmount)
                {
                    _grow = false;
                }
            } else
            {
                myScale -= Vector3.one * pulseSpeed * Time.deltaTime;

                if (myScale.x < _originalScale.x)
                {
                    _grow = true;
                    if (!continous)
                    {
                        _pulse = false;
                    }
                }
            }

            this.GetComponent<RectTransform>().localScale = myScale;
        }
    }

    public void Pulse()
    {
        if (DiceManager.FindTypeTotalGameObject("AP") != null)
        {
            int aP = int.Parse(DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text);

            if (_previousTypeTotal == 0)
            {
                DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().color = Color.white;
                _pulse = true;
            }
            else if (_previousTypeTotal < 10)
            {
                DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().color = Color.white;
                if (aP >= 10 & aP < 30)
                {
                    DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().color = overTen;
                    _pulse = true;
                }
            }
            else if (_previousTypeTotal >= 10 & _previousTypeTotal < 30)
            {
                if (aP > 30)
                {
                    DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().color = overThirty;
                    _pulse = true;
                }
            } else
            {
                _pulse = true;
            }

        _previousTypeTotal = aP;
        } else
        {
            _previousTypeTotal = 0;
        }
    }
}
