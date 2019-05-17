using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public BuildingManager building_manager;
    public GameConfig game_config;
    public float addtest_money;
    public GameObject PolityPage;

    public int current_chips;
    public float total_chips;
    public float chip_rate;

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

    public BuildingManager GetBuildingManager()
    {
        if (building_manager == null)
        {
            building_manager = BuildingManager.GetInstance();
        }
        return building_manager;
    }

    public void TestAddMoney()
    {
        AddMoney(addtest_money);
    }

    public void TestUnlock()
    {

    }

    public void Reborn() {
        if (current_chips>= 1)
        {
            player_info.assets.chip += current_chips;
            SaveChipInfo((int)player_info.assets.chip);
        }
        CleanPlayerInfo();
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void AddUnlockUpgrade(UnLockUpgradeClass data) {
        foreach (var obj in player_info.unlock_upgrade) {
            if (data.id==obj.id) {
                obj.level = data.level;
                SavePlayerInfo();
                MessageCenter.Dispatcher(EventConst.Unlock_upgrade, data);
                return;
            }
        }
        player_info.unlock_upgrade.Add(data);
        SavePlayerInfo();
        MessageCenter.Dispatcher(EventConst.Unlock_upgrade, data);
    }

    public void AddOwnUpgrade(UnLockUpgradeClass data)
    {
        foreach (var obj in player_info.own_upgrade)
        {
            if (data.id == obj.id)
            {
                obj.level = data.level;
                SavePlayerInfo();
                MessageCenter.Dispatcher(EventConst.Own_upgrade, data);
                return;
            }
        }
        player_info.own_upgrade.Add(data);
        SavePlayerInfo();
        MessageCenter.Dispatcher(EventConst.Own_upgrade, data);
    }

    public void ShowPolity() {
        PolityPage.SetActive(true);
    }

    public void ChooseFree() {
        player_info.technology.polity = 2;
        InitSuperBuilding();
        PolityPage.SetActive(false);
    }

    public void ChoosePlan()
    {
        player_info.technology.polity = 3;
        InitSuperBuilding();
        PolityPage.SetActive(false);
    }

    public void InitSuperBuilding() {
        var b3 = new BuildingAssetClass();
        b3.count = 0;
        var b4 = new BuildingAssetClass();
        b4.count = 0;
        var b5 = new BuildingAssetClass();
        b5.count = 0;
        var b6 = new BuildingAssetClass();
        b6.count = 0;
        if (player_info.technology.polity == 3)
        {
            b3.id = 10013;
            b4.id = 10014;
            b5.id = 10015;
            b6.id = 10016;
        }
        if (player_info.technology.polity == 2)
        {
            b3.id = 10023;
            b4.id = 10024;
            b5.id = 10025;
            b6.id = 10026;
        }

        player_info.building_assets.Add(b3);
        player_info.building_assets.Add(b4);
        player_info.building_assets.Add(b5);
        player_info.building_assets.Add(b6);
        SavePlayerInfo();
        UpdateBuildings();
     
    }

    public void AddBuilding(int id,int count,float spend_price,string name) {
        bool had = false;
        player_info.statics.totalBuilding += count;
        if (player_info.technology.polity ==1) {
            if (player_info.statics.totalBuilding >10)
            {
                ShowPolity();
            }
        }
        foreach (var obj in player_info.building_assets) {
            if (id==obj.id) {
                had = true;
                if (obj.count==0&& count>0) {
                    GetBuildingManager().ShowHouse(id%10, name);
                }
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
        SavePlayerInfo();
        UpdateBuildings();
      
    }

    public void SpendMoney(float m) {
        player_info.assets.money -= m;
        UpdateTopUI();
        UpdateBuildings();
        SavePlayerInfo();
    }

    public void AddUpgradeBuff(UnLockUpgradeClass data) {
        for (int i =0;i<player_info.building_assets.Count;i++) {
            if (data.id == player_info.building_assets[i].id) {
                player_info.building_assets[i].Upgrade_buff = ((float)data.level * 0.5f)+1;

               
                break;
            }
        }
        player_info.unlock_upgrade.Remove(data);
        AddOwnUpgrade(data);
    }

    public void AddMoney(float m)
    {
        player_info.statics.totalMoney += m;
        current_chips = (int)player_info.statics.totalMoney/1000000;

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
        player_info.assets.chip = LoadChipInfo();


        player_info.technology = new TechnologyClass();
        player_info.technology.polity = 1;

        player_info.excavate = new ExcavateClass();
        player_info.excavate.is_excavate = false;
        player_info.ruin = new ruinClass();
        player_info.ruin.is_ruin = false;
        player_info.statics = new StaticClass();

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
        player_info.own_upgrade = new List<UnLockUpgradeClass>();
    }
    public void CleanPlayerInfo()
    {
        player_info = null;
        PlayerPrefs.DeleteKey("PlayerInfo");
    }

    public void SaveChipInfo(int chip) {
        PlayerPrefs.SetInt("TotalChips", chip);
    }

    public int LoadChipInfo()
    {
       return  PlayerPrefs.GetInt("TotalChips");
    }

    public void DeletedChipInfo()
    {
        PlayerPrefs.DeleteKey("TotalChips");
    }

    public void CleanCache() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("GameScene");
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
        InitUpgrades();
        InitOwnUpgrades();
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

    public void InitUpgrades()
    {
        for (int i = 0;i<player_info.unlock_upgrade.Count;i++ ) {
            MessageCenter.Dispatcher(EventConst.Unlock_upgrade, player_info.unlock_upgrade[i]);
        }
    }


    public void InitOwnUpgrades()
    {
        for (int i = 0; i < player_info.own_upgrade.Count; i++)
        {
            MessageCenter.Dispatcher(EventConst.Own_upgrade, player_info.own_upgrade[i]);
        }
    }
}
