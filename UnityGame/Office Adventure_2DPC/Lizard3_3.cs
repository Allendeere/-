using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Lizard3_3 : MonoBehaviour
{
    [Header(" �� �v �� ")]
    [SerializeField] CinemachineVirtualCamera _camera;
    [SerializeField] Transform _camPos;

    [Header(" �� �e : 2500HP ")]
    [SerializeField] Animator an;
    [SerializeField] Enemy BossHp;
    int BossHPMax;
    int Attack = 1;
    float TimeDelay;
    [SerializeField] GetComponent getcomp;

    float oldCamView;
    void Start()
    {
        _camera = getcomp.Playerobject.transform.GetChild(4).gameObject.GetComponent<CinemachineVirtualCamera>();
        _camera.Follow = _camPos;
        oldCamView = _camera.m_Lens.FieldOfView;

        StartCoroutine(CamZoom());

        //_camera.m_Lens.FieldOfView = 51; // FOV �] �� 51
        BossHPMax = BossHp.health;
    }
    void DataClear()
    {
        if (Attack >= 6) { an.SetInteger("Trigger", 6); } //�G���q
        else { an.SetInteger("Trigger", 0); }

        
    }
    void State1()
    {
        switch(Attack)
            {
            case 1:  //�W��
                TimeDelay = 6.08f;
                an.SetInteger("Trigger", 1);
                Attack = 2;
                break;

            case 2:  //�ʪ�
                TimeDelay = 6.08f;
                an.SetInteger("Trigger", 2);
                Attack = 3;
                break;

            case 3:  //�U��
                TimeDelay = 6.32f;
                an.SetInteger("Trigger", 3);
                Attack = 1;
                break;

                //------------------------------------------

            case 4:  //����
                TimeDelay = 23f;
                an.SetInteger("Trigger", 4);
                Attack = 5;
                break;

            case 5:  //�_�i
                TimeDelay = 8.4f;
                an.SetInteger("Trigger", 5);
                Attack = 4;
                break;

            //------------------------------------------

            case 6:  //�G��(IDLE)
                TimeDelay = 6.1f;
                an.SetInteger("Trigger", 6);
                Attack = 7;
                break;
        }
        Invoke(nameof(DataClear), 1);
    }
    void State2() {
        if (Attack < 4)
        {
            Attack = 4;
        }
        State1();
    }
    void State3()
    {
        if (Attack < 6)
        {
            Attack = 6;
        }
        State1();
    }
    void State4() { }
    private void Update()
    {
        if (TimeDelay > 0)
        {
            TimeDelay -= Time.deltaTime;
        }
        else
        {
            if (BossHp.health > BossHPMax * .75f) // 1 �� 7 5 %
            {
                State1(); Debug.Log("L33 - 1");
            }
            else if (BossHp.health < BossHPMax * .75f && BossHp.health > BossHPMax * .50f)// 2 �� 5 0 %
            {
                State2();
                Debug.Log("L33 - 2");

            }
            else if (BossHp.health < BossHPMax * .50f && BossHp.health > BossHPMax * .25f) // 3 �� 2 5 %
            {
                State3();
                Debug.Log("L33 - 3");
            }
            else { Debug.Log("L33 - 4"); }// 4 �� 2 5 % �H �U �� �� �� �`
        }
    }

    IEnumerator CamZoom()
    {
        for (float i = oldCamView; i < 51; i+=.2f)
        {
            _camera.m_Lens.FieldOfView = i;
        yield return new WaitForSeconds(.05f);
        }
    }
}
