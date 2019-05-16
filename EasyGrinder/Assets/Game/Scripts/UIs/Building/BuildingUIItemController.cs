using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingUIItemController : MonoBehaviour
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
                price_text.text = "1x " + x1_p.ToString();
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
                price_text.text = "10x " + x10_p.ToString();
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
                price_text.text = "100x " + x100_p.ToString();
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
                price_text.text =  max_count.ToString()+"x " + max_p.ToString();
                break;
        }
    }

    public void UpdateItem(string count, string percentage) {
        percentage_text.text = percentage;
        count_text.text = count;
    }

    public void OnClickButton() {
        PlayerInfoManager.GetInstance().AddBuilding(ID, current_buy_count, current_buy_Price);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
