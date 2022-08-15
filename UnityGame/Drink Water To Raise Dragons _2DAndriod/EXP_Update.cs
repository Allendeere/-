using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXP_Update : MonoBehaviour
{
    [SerializeField] EXP exp;
    [SerializeField] Slider exp_slider;
    [SerializeField] Text text;
    [SerializeField] GameObject FX;
    [SerializeField] Transform atransform;
    [SerializeField] GameObject[] DragonsPlant;
    public Animator[] anim;
    [SerializeField] Animator expanimation;
    int DragonState;
    private void Start()
    {
        Refresh();
        State();
    }
    public void Exp_Add()
    {
        exp.Experience++;
        Refresh();
        expanimation.SetTrigger("add");
    }
    void State()
    {
        for (int i = 0; i < DragonsPlant.Length; i++)
        {
            DragonsPlant[i].SetActive(false);
        }

        if (exp.Level < 5)                          //³J LV  0
        {            DragonState = 0;        }
        else if (exp.Level >= 5&& exp.Level<20)      //ÀsÄ_Ä_   LV  5
        {            DragonState = 1;        }
        else if (exp.Level >= 20 && exp.Level < 40)  //¤pÀs    LV  20
        {            DragonState = 2;        }
        else if (exp.Level >= 40 && exp.Level < 60)  //Às     LV  40
        {            DragonState = 3;        }
        else if (exp.Level >= 60 && exp.Level < 80)  //¤jÀs     LV  60
        {            DragonState = 4;         }
        DragonsPlant[DragonState].SetActive(true);
    }
    void Refresh()
    {
        if (exp.Experience >= 10)
        {
            LevelUP();
            State();
        }
        text.text = exp.Level.ToString();
        exp_slider.value = exp.Experience;
    }

    void LevelUP()
    {
        exp.Experience = 0;
        exp.Level++;
        Instantiate(FX, atransform.position, atransform.rotation);

        Invoke(nameof(Delay), .1f);
    }
    void Delay()
    {
        anim[DragonState].SetTrigger("t");
        expanimation.SetTrigger("lvup");
    }

    public void ClickScreen()
    {
        anim[DragonState].SetTrigger("c");
    }
}
