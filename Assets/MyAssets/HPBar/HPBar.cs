using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider slider;

    private float curValue;
    private float minValue;
    private float maxValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitHPBar(float maxHP, float initHP = -1, float minHP = 0)
    {
        minValue = minHP;
        maxValue = maxHP;
        if (initHP == -1) initHP = maxHP;
        SetHP(initHP);
    }

    public void SetHP(float hp)
    {
        if (hp < 0) hp = 0;
        curValue = hp;
        slider.value = curValue / maxValue * (slider.maxValue - slider.minValue);
    }

    public void AddValue(float value)
    {
        float oldValue = curValue;
        curValue += value;
        slider.value = curValue / maxValue * (slider.maxValue - slider.minValue);
    }
}
