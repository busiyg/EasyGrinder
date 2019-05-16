using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    private static PlayerInfoManager Instance;
    public static PlayerInfoManager GetInstance()
    {
        return Instance;
    }

    public PlayerInfoClass player_info = null;
    public TopUIController topUI_controller;
    public LeftUIController leftUI_controller;
    public BuildingUIController BuildingUI_controller;

    public void Awake()
    {
        Instance = this;
        LoadPlayerInfo();
    }
    // Start is called before the first frame update
    void Start()
    {
       
        BuildingUI_controller = BuildingUIController.GetInstance();
        topUI_controller = TopUIController.GetInstance();
        leftUI_controller = LeftUIController.GetInstance();
        InitUIs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TestAddMoney()
    {
        AddMoney(50000);
    }

    public void TestUnlock()
    {
        player_info.ruin.is_ruin = true;
        InitLeftButtons();
        SavePlayerInfo();
    }

    public void AddBuilding(int id,int count,float spend_price) {
        bool had = false;

        foreach (var obj in player_info.building_assets) {
            if (id==obj.id) {
                had = true;
                obj.count += count;
                SpendMoney(spend_price);
                break;
            }
        }

        if (!had) {
            var b = new BuildingAssetClass();
            b.id = id;
            b.count = count;
            player_info.building_assets.Add(b);
        }
        UpdateBuildings();
        SavePlayerInfo();
    }

    public void SpendMoney(float m) {
        player_info.assets.money -= m;
        UpdateTopUI();
        UpdateBuildings();
        SavePlayerInfo();
    }

    public void AddMoney(float m)
    {
        player_info.assets.money += m;
        UpdateTopUI();
        UpdateBuildings();
        SavePlayerInfo();
    }

    //初始化玩家资产信息
    public void InitPlayerInfo()
    {
        player_info = new PlayerInfoClass();
        player_info.assets = new AssetsClass();
        player_info.assets.money = 100;


        player_info.technology = new TechnologyClass();
        player_info.technology.polity = 1;

        player_info.excavate = new ExcavateClass();
        player_info.excavate.is_excavate = false;
        player_info.ruin = new ruinClass();
        player_info.ruin.is_ruin = false;


        player_info.building_assets = new List<BuildingAssetClass>();

        var b1 = new BuildingAssetClass();
        b1.id = 10001;
        b1.count = 0;
        player_info.building_assets.Add(b1);

        var b2 = new BuildingAssetClass();
        b2.id = 10002;
        b2.count = 0;
        player_info.building_assets.Add(b2);

        player_info.unlock_upgrade = new List<UnLockUpgradeClass>();
    }
    public void CleanPlayerInfo()
    {
        player_info = null;
        PlayerPrefs.DeleteKey("PlayerInfo");
    }

    public void LoadPlayerInfo()
    {
        var info_str = PlayerPrefs.GetString("PlayerInfo");
        if (!string.IsNullOrEmpty(info_str))
        {
            print("有数据:" + info_str);
            player_info = JsonUtility.FromJson<PlayerInfoClass>(info_str); 
        }
        else
        {
            print("没数据");
            InitPlayerInfo();
        }
        SavePlayerInfo();
    }

    public void SavePlayerInfo()
    {
        if (player_info != null)
        {
            string info_str = JsonUtility.ToJson(player_info);
            //print("save data :" + info_str);
            PlayerPrefs.SetString("PlayerInfo", info_str);
        }

    }

    public void InitUIs() {
        UpdateTopUI();
        InitLeftButtons();
        UpdateBuildings();
    }

    public void UpdateTopUI()
    {
        topUI_controller.UpdateUI();
     
    }

    public void InitLeftButtons()
    {
        leftUI_controller.InitLeftButtons();
    }

    public void UpdateBuildings()
    {
        BuildingUI_controller.UpdateBuildingItem();
    }
}
