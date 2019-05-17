using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeController : MonoBehaviour
{

    public GameConfig game_config;
    public ContentItemController item_prefab;
    public PlayerInfoManager playerInfoManager;

    public ContectController UnLock_content;
    public ContectController Own_content;

    public Dictionary<int, ContentItemController> Unlock_ItemMap = new Dictionary<int, ContentItemController>();
    public Dictionary<int, ContentItemController> Own_ItemMap = new Dictionary<int, ContentItemController>();
    // Start is called before the first frame update
    void Start()
    {
        MessageCenter.AddListener(EventConst.Unlock_upgrade,Unlock_upgrade);
        MessageCenter.AddListener(EventConst.Own_upgrade, Own_upgrade);
    }

    public GameConfig GetGameConfig()
    {
        if (game_config == null)
        {
            game_config = GameConfig.GetInstance();
        }
        return game_config;
    }

    private void OnDestroy()
    {
        MessageCenter.RemoveAlllistener();
    }

    public PlayerInfoManager GetPlayerInfoManager()
    {
        if (playerInfoManager == null)
        {
            playerInfoManager = PlayerInfoManager.GetInstance();
        }
        return playerInfoManager;
    }


    public void Unlock_upgrade(object data)
    {
        var Data = (UnLockUpgradeClass)data;
        ContentItemController item = null;
        if (!Unlock_ItemMap.ContainsKey(Data.id))
        {
            item = Instantiate(item_prefab);
            var comp = item.GetComponent<ContentItemController>();
            comp.UpdateItem(Data);
            comp.callback = (()=> {
                Unlock_ItemMap.Remove(Data.id);
            });
            Unlock_ItemMap.Add(Data.id, item);
        }
        else
        {

            item = Unlock_ItemMap[Data.id];
    
            item.UpdateItem(Data);
            item.callback = (() => {
                Unlock_ItemMap.Remove(Data.id);
            });
        }



        item.transform.SetParent(UnLock_content.main_content.transform);
        UnLock_content.InitHeight(UnLock_content.main_content.transform.childCount);
        UnLock_content.LayoutRebuild();
    }

    public void Own_upgrade(object data)
    {
        var Data = (UnLockUpgradeClass)data;
        ContentItemController item = null;
        if (!Own_ItemMap.ContainsKey(Data.id))
        {
            item = Instantiate(item_prefab);
            var comp = item.GetComponent<ContentItemController>();
            comp.image.gameObject.SetActive(true);
            comp.UpdateItem(Data);
            Own_ItemMap.Add(Data.id, item);
        }
        else
        {
            item = Own_ItemMap[Data.id];
            item.image.gameObject.SetActive(true);
            item.UpdateItem(Data);
        }

        item.transform.SetParent(Own_content.main_content.transform);
        UnLock_content.InitHeight(UnLock_content.main_content.transform.childCount);
        Own_content.InitHeight(Own_content.main_content.transform.childCount);
        Own_content.LayoutRebuild();
    }
}
