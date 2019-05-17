using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingUIItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public Image image;
    public Text title_text;
    public Text price_text;
    public Text count_text;
    public Text percentage_text;

    public float x1_p;
    public float x10_p;
    public float x100_p;
    public float max_p;
    public float max_count;
    public float production_rate;
    public int current_buy_count;
    public float current_buy_Price;
    public int Count;

    public int ID;

    void Start()
    {
        
    }

    public void InitItem(string title,Sprite sprite,int id) {
        title_text.text = title;
        image.sprite = sprite;
        ID = id;
    }

    public void UpdatePrice(List<float> prices) {
        x1_p = prices[0];
        x10_p = prices[1];
        x100_p = prices[2];
        max_p = prices[3];
        max_count = prices[4];
    }

    public void SwitchPrice(int type,float current_money) {
        switch (type)
        {
            case 1:
                if (current_money < x1_p)
                {
                    button.interactable = false;
                }
                else {
                    button.interactable = true;
                }
                current_buy_count = 1;
                current_buy_Price = x1_p;
                price_text.text = "1x " + GameUtility.NumConversion(x1_p);
                break;
            case 10:
                if (current_money < x10_p)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
                current_buy_count = 10;
                current_buy_Price = x10_p;
                price_text.text = "10x " + GameUtility.NumConversion(x10_p);
                break;
            case 100:
                if (current_money < x100_p)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
                current_buy_count = 100;
                current_buy_Price = x100_p;
                price_text.text = "100x " + GameUtility.NumConversion(x100_p);
                break;
            case 1000:
                if (current_money < max_p)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
                current_buy_count = (int)max_count;
                current_buy_Price = max_p;
                price_text.text = max_count.ToString() + "x "+ GameUtility.NumConversion(max_p); 
                break;
        }
    }

    public void UpdateItem(int count, string percentage,float rate) {
        production_rate = rate;
        percentage_text.text = percentage;
        Count = count;
        count_text.text = Count.ToString();
    }

    public void OnClickButton() {
        PlayerInfoManager.GetInstance().AddBuilding(ID, current_buy_count, current_buy_Price, title_text.text);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TipsController.Show("每个" + title_text.text + "增加" + GameUtility.NumConversion(production_rate)  + "金钱，一共增加"+ GameUtility.NumConversion(production_rate * Count));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TipsController.hide();
    }
}
