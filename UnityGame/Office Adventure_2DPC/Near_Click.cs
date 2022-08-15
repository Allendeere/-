using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Near_Click : MonoBehaviour
{
    bool onclickpoint;
    [SerializeField] BoxCollider2D collider2D_;
    [SerializeField] GetComponent getcom;
    [Header("凍結 : 當你開啟物件時會自動幫你背景凍結")]
    [SerializeField] PauseMenu menu;
    [SerializeField] bool ForzeTime;

    [Header("使用AlwayTrigger將無視碰撞框，默認允許點擊")]
    [SerializeField] bool AlwayTrigger;

    [Header ("Teleporter : (1)去控制室 / (2)去主廳 / (3)出公司")]
    [SerializeField] int point;

    [Header("Trigger : OpenOBJ- 點即時觸發  /  Toucing- 碰撞時觸發")]
    public GameObject OpenOBJ;
    [SerializeField] GameObject Toucing;

    [Header("LevelPortal : otherPortal:隱藏其他傳送洞")]
    [SerializeField] bool IsLevelPortal;
    [SerializeField] GameObject[] otherPortal;

    [SerializeField] bool LevelPortalUpArror;
    [SerializeField] bool LevelPortalDownArror;
    [SerializeField] Portal_Select Portal_Select;
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (AlwayTrigger)
            return;
        if (collider.tag == "PlayerTrigger" && collider.gameObject.transform.parent.gameObject == getcom.Playerobject)
        {
            onclickpoint = true;

            if (Toucing == null) return;
            Toucing.SetActive(true);
        }
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        if (AlwayTrigger)
            return;
        if (collision.tag == "PlayerTrigger" && collision.gameObject.transform.parent.gameObject == getcom.Playerobject)
        {
            onclickpoint = false;

            if (Toucing == null) return;
            Toucing.SetActive(false);
        }
    }
    void OnMouseDown()
    {
        if (onclickpoint == true||AlwayTrigger)
        {
            if (gameObject.tag == "clickNPC")
            {
                //______________________________________LevelPortalArror______________________________________LevelPortalArror
                if (LevelPortalUpArror)
                {
                    Portal_Select.ButtonClick(true);
                }
                if (LevelPortalDownArror)
                {
                    Portal_Select.ButtonClick(false);
                }
                //______________________________________LevelPortalArror______________________________________LevelPortalArror

                if (OpenOBJ == null)
                    return;
                if (ForzeTime) { menu.Pause(OpenOBJ); }
                else {OpenOBJ.SetActive(true); }
                if (!IsLevelPortal)
                {
                    collider2D_.enabled = false;
                }

                if (IsLevelPortal)
                {
                    if (otherPortal != null)
                    {

                        foreach (GameObject portal in otherPortal)
                        {
                            portal.SetActive(false);
                        }
                    }
                }
            }
            else if (gameObject.tag == "Portal")//連接 M_Vote
            {
                M_Vote.click_tp(point);
            }
        }
    }


}
