using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContectController : MonoBehaviour
{
    public RectTransform rect_transfrom;
 

    public GameObject main_content;

    public Text title_text;

    //隐藏内容
    public bool is_hide;
    public Image switch_hide_button;
    public Sprite sprite_hide;
    public Sprite sprite_show;

    //图标按钮
    public GameObject item_prefab;
    public List<ContentItemController> buttons; 
    // Start is called before the first frame update
    void Start()
    {
        InitContent(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitContent(int count) {
        if (count > 0) {
            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(item_prefab, main_content.transform);
                var comp = obj.GetComponent<ContentItemController>();
                buttons.Add(comp);
            }
            InitHeight(count);
        }
    
    }

    public void SwitchHide() {
        if (is_hide)
        {
            main_content.SetActive(true);
            switch_hide_button.sprite = sprite_show;
            InitHeight(buttons.Count);
        }
        else {
            main_content.SetActive(false);
            switch_hide_button.sprite = sprite_hide;
            InitHeight(0);
        }
        is_hide = !is_hide;
    }

    public void InitHeight(int count) {
        if (count > 0)
        {
            float h = (float)count / 6;
            rect_transfrom.sizeDelta = new Vector2(rect_transfrom.sizeDelta.x, 5 + 50 + Mathf.CeilToInt(h) * 80);
        }
        else
        {
            rect_transfrom.sizeDelta = new Vector2(rect_transfrom.sizeDelta.x, 50);
        }
      
    }
}
