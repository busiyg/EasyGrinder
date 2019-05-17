using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfoClass
{
    public AssetsClass assets;
    public TechnologyClass technology;
    public ExcavateClass excavate;
    public ruinClass ruin;
    public List<BuildingAssetClass> building_assets;
    public List<UnLockUpgradeClass> unlock_upgrade;
    public List<UnLockUpgradeClass> own_upgrade;
    public StaticClass statics;
}

[System.Serializable]
public class AssetsClass
{
    //金钱
    public float money;
    //芯片
    public float chip;
    //电池
    public int battery;
    //劳动力
    public int robot;
}

[System.Serializable]
public class TechnologyClass
{
    //政体：1基础，2自由，3集权
    public int polity;
    //科学：
    public int science;
}



[System.Serializable]
public class BuildingAssetClass
{
    public int id;
    public int count;
    public int next_upgrade_count;
    public float Upgrade_buff = 1;
}

[System.Serializable]
public class UnLockUpgradeClass
{
    public int id;
    public string title;
    public string detail;
    public int level;
    public float base_price;
}

[System.Serializable]
public class ExcavateClass
{
    public bool is_excavate;
    public int layer;
}

[System.Serializable]
public class StaticClass
{
    public float totalMoney;
    public int totalBuilding;
}

[System.Serializable]
public class ruinClass
{
    public bool is_ruin;
}




