using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContentItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public Image image;
    public Text text;
    public UnLockUpgradeClass data;
    public PlayerInfoManager playerInfoManager;
    public System.Action callback;
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

    public void OnClickItem()
    {
        var info = GetPlayerInfoManager();
        if (info.player_info.assets.money > data.base_price * data.level)
        {
            if (data.id / 10000 == 1)
            {
                GetPlayerInfoManager().AddUpgradeBuff(data);
                if (callback != null) {
                    callback.Invoke();
                }
                Destroy(gameObject);
            }
        }

    }

    public void UpdateItem(UnLockUpgradeClass info) {
        data = info;
        text.text = info.title+data.level+"级";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TipsController.Show(data.detail+" "+ (float)data.level*0.5f*100+"%");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TipsController.hide();
    }


    public void DestroyButton() {

    }
}
