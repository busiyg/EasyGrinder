using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticController : MonoBehaviour
{
    public Text building;
    public Text money;
    public PlayerInfoManager playerInfoManager;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        var info = GetPlayerInfoManager().player_info;
        building.text ="总建筑数量："+ info.statics.totalBuilding.ToString();
        money.text = "总金钱数量：" + GameUtility.NumConversion(info.statics.totalMoney) ;
    }
}
