using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static MoneyManager Instance;
    public static MoneyManager GetInstance() {
        return Instance;
    }
    public PlayerInfoManager playerInfoManager;
    public GameConfig game_config;
    public TopUIController topUIController;
    public float income_rate;
    public float click_rate;
    public float timer;

    public GameObject ClickMoneyPrefab;
    public Transform MoneyParent;
    // Start is called before the first frame update
    void Start()
    {
        topUIController = TopUIController.GetInstance();
    }

    public PlayerInfoManager GetPlayerInfoManager()
    {
        if (playerInfoManager == null)
        {
            playerInfoManager = PlayerInfoManager.GetInstance();
        }
        return playerInfoManager;
    }

    public GameConfig GetGameConfig()
    {
        if (game_config == null)
        {
            game_config = GameConfig.GetInstance();
        }
        return game_config;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer>=1) {
            timer = 0;
            UpdateRate();
        }
    }

    public float BuildingProduction() {
        float building_production_rate = 0;
        var info = GetPlayerInfoManager().player_info;
        if (info.building_assets != null)
        {
            for (int i =0;i< info.building_assets.Count;i++) {
                if (info.building_assets[i].count>0) {
                    var rate = GetGameConfig().GetBuildingItemByID(info.building_assets[i].id).base_production_rate* info.building_assets[i].Upgrade_buff;
                    building_production_rate += (rate * info.building_assets[i].count);
                }
            }
        }
        if (info.assets.chip>=1) {
            building_production_rate = building_production_rate * (1+ info.assets.chip*0.5f);
        }
       

        return building_production_rate;
    }

    public void OnMapClick() {
        var money_obj = Instantiate(ClickMoneyPrefab,Input.mousePosition,Quaternion.identity, MoneyParent);
        money_obj.GetComponent<ClickMoneyController>().ShowCoins(click_rate);
        GetPlayerInfoManager().AddMoney(click_rate);
    }



    public void UpdateRate() {
        income_rate = 0;
        income_rate += BuildingProduction();
        GetPlayerInfoManager().AddMoney(income_rate);
        UpdateUI();
    }

    public void UpdateUI() {
        topUIController.MoneyRateUpdate(income_rate);
    }
}
