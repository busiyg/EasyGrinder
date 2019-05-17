using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUIController : MonoBehaviour
{
    private static BuildingUIController Instance;
    public static BuildingUIController GetInstance() {
        return Instance;
    }

    public PlayerInfoManager playerInfoManager;
    public GameConfig game_config;
    public BuildingManager building_manager;

    public Dictionary<int, BuildingUIItemController> building_items = new Dictionary<int, BuildingUIItemController>();
    public BuildingUIItemController buildingUIItem_prefab;
    public Transform content;
    public int buy_type;
    public Text buy_type_text;

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

    public GameConfig GetGameConfig()
    {
        if (game_config == null)
        {
            game_config = GameConfig.GetInstance();
        }
        return game_config;
    }

    public BuildingManager GetBuildingManager()
    {
        if (building_manager == null)
        {
            building_manager = BuildingManager.GetInstance();
        }
        return building_manager;
    }

    public void BuyTypeChange() {
        switch (buy_type) {
            case 1:
                buy_type = 10;
                buy_type_text.text = "BUY 10";
                break;
            case 10:
                buy_type = 100;
                buy_type_text.text = "BUY 100";
                break;
            case 100:
                buy_type = 1000;
                buy_type_text.text = "BUY MAX";
                break;
            case 1000:
                buy_type = 1;
                buy_type_text.text = "BUY 1";
                break;
        }
        UpdateBuildingItem();
    }

    public void UpdateBuildingItem()
    {  
        var info = GetPlayerInfoManager().player_info;
        float current_money = info.assets.money;
        float building_price_increase_rate = GetGameConfig().building_price_increase_rate;

        if (info!=null) {
            for (int i = 0;i<info.building_assets.Count;i++) {
                if (!building_items.ContainsKey(info.building_assets[i].id))
                {
                    var item = Instantiate(buildingUIItem_prefab, content);
                    var item_data = GetGameConfig().GetBuildingItemByID(info.building_assets[i].id);

                    var comp = item.GetComponent<BuildingUIItemController>();
                    var Prices = CalculatePrices(item_data.base_price,info.building_assets[i].count, current_money, building_price_increase_rate);
                    comp.InitItem(item_data.title, item_data.sprite, info.building_assets[i].id);
                    comp.UpdatePrice(Prices);
                    comp.SwitchPrice(buy_type, current_money);
                    comp.UpdateItem(info.building_assets[i].count, "0%", item_data.base_production_rate* info.building_assets[i].Upgrade_buff);

                    if (info.building_assets[i].next_upgrade_count==0) {
                        info.building_assets[i].next_upgrade_count += item_data.next_upgrade_count;
                    }

                    if (info.building_assets[i].count >= info.building_assets[i].next_upgrade_count)
                    {
                        var upgrade = new UnLockUpgradeClass();
                        upgrade.id = info.building_assets[i].id;
                        upgrade.level = (info.building_assets[i].next_upgrade_count / item_data.next_upgrade_count);
                        var data = GetGameConfig().GetUpGradeItemByID(info.building_assets[i].id);
                        upgrade.title = data.title;
                        upgrade.detail = data.describe;
                        info.building_assets[i].next_upgrade_count += item_data.next_upgrade_count;
                        GetPlayerInfoManager().AddUnlockUpgrade(upgrade);
                    }

                    building_items.Add(info.building_assets[i].id, item);
                }
                else {
                    var item = building_items[info.building_assets[i].id];
                    var item_data = GetGameConfig().GetBuildingItemByID(info.building_assets[i].id);
                    var Prices = CalculatePrices(item_data.base_price, info.building_assets[i].count, current_money, building_price_increase_rate);
                    item.UpdatePrice(Prices);
                    item.SwitchPrice(buy_type, current_money);
                    if (info.building_assets[i].count > 0)
                    {
                        GetBuildingManager().ShowHouse(info.building_assets[i].id % 10, item_data.title);
                    }
                    item.UpdateItem(info.building_assets[i].count, "0%", item_data.base_production_rate * info.building_assets[i].Upgrade_buff);

                    if (info.building_assets[i].next_upgrade_count == 0)
                    {
                        info.building_assets[i].next_upgrade_count += item_data.next_upgrade_count;
                    }

                    if (info.building_assets[i].count >= info.building_assets[i].next_upgrade_count)
                    {
                        var upgrade = new UnLockUpgradeClass();
                        upgrade.id = info.building_assets[i].id;
                        upgrade.level = (info.building_assets[i].next_upgrade_count / item_data.next_upgrade_count);
                        var data = GetGameConfig().GetUpGradeItemByID(info.building_assets[i].id);
                        upgrade.title = data.title;
                        upgrade.detail = data.describe;
                        info.building_assets[i].next_upgrade_count += item_data.next_upgrade_count;
                        GetPlayerInfoManager().AddUnlockUpgrade(upgrade);
                    }
                }
            }
        }
    }


    public List<float> CalculatePrices(float base_prize,int current_count,float current_money,float building_price_increase_rate) {
        List<float> prices = new List<float>();

        float p1 = base_prize * (Mathf.Pow(building_price_increase_rate, current_count));
        prices.Add(p1);

        float p10 = 0;
        for (int i=0;i<10;i++) {
            p10+= base_prize * (Mathf.Pow(building_price_increase_rate, current_count+i));
        }
        prices.Add(p10);

        float p100 = 0;
        for (int i = 0; i < 100; i++)
        {
            p100 += base_prize * (Mathf.Pow(building_price_increase_rate, current_count + i));
        }
        prices.Add(p100);

        float p_max = p1;
        int max_count = 1;
        for (int i = 1; p_max < current_money; i++)
        {
           
            var next_sum = p_max + base_prize * (Mathf.Pow(building_price_increase_rate, current_count + i));
            if (next_sum> current_money) {
                break;
            }
            else
            {
                p_max = next_sum;
                max_count += 1;
            }       
        }
        prices.Add(p_max);
        prices.Add(max_count);

        return prices;
    }
}
