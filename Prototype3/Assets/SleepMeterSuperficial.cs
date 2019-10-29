using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepMeterSuperficial : MonoBehaviour
{
    private float _sliderSpeed = 0.3f;

    private float _startValue;
    private float _finalValue;

    private bool _playAnimation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_playAnimation)
        {
            if (_finalValue > _startValue)
            {
                if (this.GetComponent<Slider>().value < _finalValue)
                {
                    this.GetComponent<Slider>().value += _sliderSpeed * Time.deltaTime;
                }
            }
            else
            {
                if (this.GetComponent<Slider>().value > _finalValue)
                {
                    this.GetComponent<Slider>().value -= _sliderSpeed * Time.deltaTime;
                }
            }
        }
    }

    public void PlaySliderAnimation(float initialValue, float finValue)
    {
        this.GetComponent<Slider>().value = initialValue;

        _startValue = initialValue;
        _finalValue = finValue;

        _playAnimation = true;
    }
}
