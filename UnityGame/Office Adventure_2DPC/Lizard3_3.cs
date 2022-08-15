using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Lizard3_3 : MonoBehaviour
{
    [Header(" 攝 影 機 ")]
    [SerializeField] CinemachineVirtualCamera _camera;
    [SerializeField] Transform _camPos;

    [Header(" 動 畫 : 2500HP ")]
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

        //_camera.m_Lens.FieldOfView = 51; // FOV 設 為 51
        BossHPMax = BossHp.health;
    }
    void DataClear()
    {
        if (Attack >= 6) { an.SetInteger("Trigger", 6); } //二階段
        else { an.SetInteger("Trigger", 0); }

        
    }
    void State1()
    {
        switch(Attack)
            {
            case 1:  //上殺
                TimeDelay = 6.08f;
                an.SetInteger("Trigger", 1);
                Attack = 2;
                break;

            case 2:  //傘狀
                TimeDelay = 6.08f;
                an.SetInteger("Trigger", 2);
                Attack = 3;
                break;

            case 3:  //下殺
                TimeDelay = 6.32f;
                an.SetInteger("Trigger", 3);
                Attack = 1;
                break;

                //------------------------------------------

            case 4:  //收割
                TimeDelay = 23f;
                an.SetInteger("Trigger", 4);
                Attack = 5;
                break;

            case 5:  //震波
                TimeDelay = 8.4f;
                an.SetInteger("Trigger", 5);
                Attack = 4;
                break;

            //------------------------------------------

            case 6:  //二轉(IDLE)
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
            if (BossHp.health > BossHPMax * .75f) // 1 階 7 5 %
            {
                State1(); Debug.Log("L33 - 1");
            }
            else if (BossHp.health < BossHPMax * .75f && BossHp.health > BossHPMax * .50f)// 2 階 5 0 %
            {
                State2();
                Debug.Log("L33 - 2");

            }
            else if (BossHp.health < BossHPMax * .50f && BossHp.health > BossHPMax * .25f) // 3 階 2 5 %
            {
                State3();
                Debug.Log("L33 - 3");
            }
            else { Debug.Log("L33 - 4"); }// 4 階 2 5 % 以 下 直 到 死 亡
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
