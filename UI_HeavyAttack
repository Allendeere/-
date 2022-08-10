using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeavyAttack : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField] Image HatkUI;
    [SerializeField] Sprite HAtk_Max;
    [SerializeField] Sprite HAtk_Max_rough;
    [SerializeField] Sprite HAtk_Max_damaged;

    [SerializeField] Sprite HAtk_Normal;
    [SerializeField] Sprite HAtk_Normal_rough;
    [SerializeField] Sprite HAtk_Normal_damaged;
    [SerializeField] GetComponent gcp;
    public void MaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetValue(int value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void TestforMaxValue()
    {
        if (gcp.hl.health == 1) { HatkUI.sprite = (slider.value >= slider.maxValue) ? HAtk_Max_damaged : HAtk_Normal_damaged; }
        else if (gcp.hl.health == 2) { HatkUI.sprite = (slider.value >= slider.maxValue) ? HAtk_Max_rough : HAtk_Normal_rough; }
        else if (gcp.hl.health == 3) { HatkUI.sprite = (slider.value >= slider.maxValue) ? HAtk_Max : HAtk_Normal; }
        
    }
}
