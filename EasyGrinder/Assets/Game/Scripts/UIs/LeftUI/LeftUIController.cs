using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LeftUIController : MonoBehaviour
{
    private static LeftUIController Instance;
    public static LeftUIController GetInstance() {
        return Instance;
    }

    public CanvasGroup canvas_group;

    //左侧按钮
    public GameObject upgrade_button;
    public GameObject trophies_button;
    public GameObject event_button;
    public GameObject excavate_button;
    public GameObject ruin_button;

    //左侧面板
    public bool is_showed;
    public GameObject LeftPopParent;
    public GameObject upgrade_page;
    public GameObject trophies_page;
    public GameObject event_page;
    public GameObject excavate_page;
    public GameObject ruin_page;
 


    public PlayerInfoManager playerInfoManager;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    public void InitLeftButtons()
    {
        var info = GetPlayerInfoManager().player_info;
        if (info.excavate.is_excavate == true)
        {
            excavate_button.SetActive(true);
        }
        else
        {
            excavate_button.SetActive(false);
        }

        if (info.ruin.is_ruin == true)
        {
            ruin_button.SetActive(true);
        }
        else
        {
            ruin_button.SetActive(false);
        }
    }

    public PlayerInfoManager GetPlayerInfoManager()
    {
        if (playerInfoManager == null)
        {
            playerInfoManager = PlayerInfoManager.GetInstance();
        }
        return playerInfoManager;
    }

    public void CloseOtherPage() {
        if (upgrade_page.activeSelf == true) {
            upgrade_page.SetActive(false);
        }

        if (trophies_page.activeSelf == true)
        {
            trophies_page.SetActive(false);
        }

        if (event_page.activeSelf == true)
        {
            event_page.SetActive(false);
        }

        if (excavate_page.activeSelf == true)
        {
            excavate_page.SetActive(false);
        }

        if (ruin_page.activeSelf == true)
        {
            ruin_page.SetActive(false);
        }
    }

    public void On_upgrade_button() {
        if (is_showed)
        {
            if (upgrade_page.activeSelf == true)
            {
                HideLeftPop();
                upgrade_page.SetActive(false);
            }
            else {
                CloseOtherPage();
                upgrade_page.SetActive(true);
            }
        }
        else {
            upgrade_page.SetActive(true);
            showLeftPop();
        }
    }

    public void On_trophies_button()
    {
        if (is_showed)
        {
            if (trophies_page.activeSelf == true)
            {
                HideLeftPop();
                trophies_page.SetActive(false);
            }
            else
            {
                CloseOtherPage();
                trophies_page.SetActive(true);
            }
        }
        else
        {
            trophies_page.SetActive(true);
            showLeftPop();
        }
    }

    public void On_event_button()
    {

    }

    public void On_excavate_button()
    {

    }

    public void On_ruin_button()
    {

    }

    public void showLeftPop() {
        LeftPopParent.transform.DOLocalMoveX(-80,0.2f).OnComplete(()=> {
            is_showed = true;
        });
    }

    public void HideLeftPop()
    {
        LeftPopParent.transform.DOLocalMoveX(-750, 0.2f).OnComplete(()=> {
            is_showed = false;
        });
    }
}
