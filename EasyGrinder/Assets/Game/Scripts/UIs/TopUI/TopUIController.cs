using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TopUIController : MonoBehaviour
{
    private static TopUIController Instance;

    public static TopUIController GetInstance() {
        return Instance;
    }
    public Text money;
    public Text money_rate;
    public Text chip;
    public Text battery;
    public Text robot;
    public PlayerInfoManager playerInfoManager;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    public PlayerInfoManager GetPlayerInfoManager()
    {
        if (playerInfoManager == null)
        {
            playerInfoManager = PlayerInfoManager.GetInstance();
        }
        return playerInfoManager;
    }

    public void UpdateUI() {
        var info = GetPlayerInfoManager().player_info;
        if (info.assets != null) {
            money.text = info.assets.money.ToString();
            chip.text = info.assets.chip.ToString();
            battery.text = info.assets.battery.ToString();
            robot.text = info.assets.robot.ToString();
        }
    }

    public void MoneyRateUpdate(float rate) {
        money_rate.text = rate.ToString() + "/s";
    }

    public CanvasGroup left_canvas_group;
    public CanvasGroup right_canvas_group;
    public bool canvas_changing = false;
    public void HideUI() {
        if (canvas_changing==false) {
            if (left_canvas_group.alpha == 1)
            {
                canvas_changing = true;
                left_canvas_group.blocksRaycasts = false;
                right_canvas_group.blocksRaycasts = false;
                left_canvas_group.DOFade(0, 0.2f).OnComplete(() =>
                {
                    canvas_changing = false;
                });
                right_canvas_group.DOFade(0, 0.2f).OnComplete(() =>
                {
                });
            }
            else
            {
                canvas_changing = true;
                left_canvas_group.blocksRaycasts = true;
                right_canvas_group.blocksRaycasts = true;
                left_canvas_group.DOFade(1, 0.2f).OnComplete(() =>
                {
                    canvas_changing = false;
                });
                right_canvas_group.DOFade(1, 0.2f).OnComplete(() =>
                {
                });
            }
        }    
    }

    public void HideBGM() {

    }

    public void ShowStatic()
    {

    }


  
    public void OtherSetting() {
     
    }
}
