using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    private static GameConfig Instance;

    public static GameConfig GetInstance() {
        return Instance;
    }

    public List<BuildingConfigItem> buildings;
    public List<SkillConfigItem> skills;
    public float building_price_increase_rate;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
    }

    public BuildingConfigItem GetBuildingItemByID(int id)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].id == id)
            {
                return buildings[i];
            }
        }
        return null;
    }

    public SkillConfigItem GetSkillItemByID(int id)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].id == id)
            {
                return skills[i];
            }
        }
        return null;
    }
}



[System.Serializable]
public class BuildingConfigItem
{
    public string title;
    public string describe;
    public float base_price;
    public float base_production_rate;
    public int id;
    public Sprite sprite;
}

[System.Serializable]
public class SkillConfigItem
{
    public int id;
    public string title;
    public Sprite sprite;
}
