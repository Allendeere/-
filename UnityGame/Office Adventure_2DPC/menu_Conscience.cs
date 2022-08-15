using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_Conscience : MonoBehaviour
{
    //���� : Conscience_count �A Conscience
    [Header("���~���y")]
    [SerializeField] GameObject[] souls;

    [SerializeField] GetComponent getcomp;
    [SerializeField] Conscience_count _count;
    [Header("���c�ϼ�")]
    [SerializeField] Image conscience_image;
    [SerializeField] Sprite[] consimage;
    [Header("�w�� - Kubo")]
    [SerializeField] Slider Kubo_Kill;
    [SerializeField] Slider Kubo_Tame;
    [Header("�w�w�Q - BubblePing")]
    [SerializeField] Slider BubblePing_kill;
    [SerializeField] Slider BubblePing_tame;


    void Start()
    {
        UI_Conscience();
        Kubo_Kill.value = _count.Kubo_Kill;
        Kubo_Tame.value = _count.Kubo_Tame;

        BubblePing_kill.value = _count.BubblePing_Kill;
        BubblePing_tame.value = _count.BubblePing_Tame;


    }
    void UI_Conscience() //���c�ϼ�
    {
        // ��
        if (_count.Conscience > 2)
        {
            //conscience_image.color = Color.yellow;
            
            if (_count.Conscience > 2 && _count.Conscience < 4)
            {
                conscience_image.sprite = consimage[1];
            }
            else if (_count.Conscience >= 4 && _count.Conscience < 6)
            {
                conscience_image.sprite = consimage[2];
            }//�i�ק�
        }
        
        // �c
        else if (_count.Conscience < -2)
        {
            //conscience_image.color = Color.cyan;

            if (_count.Conscience < -2 && _count.Conscience > -4)
            {
                conscience_image.sprite = consimage[3];
            }
            else if (_count.Conscience <= -4 && _count.Conscience > -6)
            {
                conscience_image.sprite = consimage[4];
            }
            //�i�ק�
        }
        Invoke(nameof(Conscience_Reward),1);
        Conscience_Reward();
    }

    void Conscience_Reward()
    {

        if (getcomp.Playerobject != null)
        {

            // ���c���y�t��
            // ��kill��tame��F�@�w���q��
            // �N�o�e���y

            if (_count.Kubo_Kill >= Kubo_Kill.maxValue && !_count.Kubo_Soul) // KUBO - SOULS 0
            {
                _count.Kubo_Soul = true;
                //1. �o�쪱�a��m  2.�ͦ�souls�b���a�Ҧb��m
                Instantiate(souls[0], getcomp.Playerobject.transform.position, Quaternion.identity);
            }
            if (_count.BubblePing_Kill >= BubblePing_kill.maxValue && !_count.BubblePing_Soul)// BUBBLEPING - SOULS 1
            {
                _count.BubblePing_Soul = true;
                Instantiate(souls[1], getcomp.Playerobject.transform.position, Quaternion.identity);
            }
        }

    }
}
