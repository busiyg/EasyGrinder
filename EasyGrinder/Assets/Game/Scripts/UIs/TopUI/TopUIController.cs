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
    public GameObject static_page;
    public GameObject SettingPage;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    public void ShowChipInfo() {
        TipsController.Show("每个芯片加速50%,总共加速"+ (int)GetPlayerInfoManager().player_info.assets.chip*50+"%");
    }

    public void HideInfo() {
        TipsController.hide();
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
            money.text = GameUtility.NumConversion(info.assets.money);
            chip.text = GameUtility.NumConversion(info.assets.chip); 
            battery.text = GameUtility.NumConversion(info.assets.battery); 
            robot.text = GameUtility.NumConversion(info.assets.robot);
        }
    }

    public void MoneyRateUpdate(float rate) {
        money_rate.text = GameUtility.NumConversion(rate) + "/s";
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
        if (static_page.activeSelf == true)
        {
            static_page.SetActive(false);
        }
        else {
            static_page.SetActive(true);
        }
    }
  
    public void OtherSetting() {
        if (SettingPage.activeSelf == true)
        {
            SettingPage.SetActive(false);
        }
        else
        {
            SettingPage.SetActive(true);
        }
    }
}
