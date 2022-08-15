using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Near_Click : MonoBehaviour
{
    bool onclickpoint;
    [SerializeField] BoxCollider2D collider2D_;
    [SerializeField] GetComponent getcom;
    [Header("�ᵲ : ��A�}�Ҫ���ɷ|�۰����A�I���ᵲ")]
    [SerializeField] PauseMenu menu;
    [SerializeField] bool ForzeTime;

    [Header("�ϥ�AlwayTrigger�N�L���I���ءA�q�{���\�I��")]
    [SerializeField] bool AlwayTrigger;

    [Header ("Teleporter : (1)�h����� / (2)�h�D�U / (3)�X���q")]
    [SerializeField] int point;

    [Header("Trigger : OpenOBJ- �I�Y��Ĳ�o  /  Toucing- �I����Ĳ�o")]
    public GameObject OpenOBJ;
    [SerializeField] GameObject Toucing;

    [Header("LevelPortal : otherPortal:���è�L�ǰe�}")]
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
            else if (gameObject.tag == "Portal")//�s�� M_Vote
            {
                M_Vote.click_tp(point);
            }
        }
    }


}
