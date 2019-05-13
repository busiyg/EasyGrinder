using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LobbyAniController : MonoBehaviour
{

    private int frame_gap;
    public int frame_interval;
    //BG
    public float bg_move_speed;
    public List<GameObject> bg_objs;

    //Ship
    public List<Sprite> ship_sprites;
    public Image ship_renderer;
    private int ship_index;


    //Star
    public GameObject star_obj;

    //Text
    public Text text_anykey;
    public Text text_story;

    void Start()
    {
        frame_gap = 0;
        ship_index = 0;
        Invoke("ShowShip", 1);
    }

    // Update is called once per frame
    void Update()
    {
        frame_gap += 1;
        if (frame_gap > frame_interval) {

            //背景滚动
            BGRolling();
            ChangeSprites();
         
            frame_gap = 0;
        }
      
    }

    public void ShowAnyKey() {
        AnykeyFlash();
        for (int i = 0; i < bg_objs.Count; i++)
        {
            bg_objs[i].GetComponent<Image>().raycastTarget = true;
        }
    }

    public void ShowStoryText() {
        //故事是这样的，一些机器人脱离人类的统治，在银河中寻找新的家园，现在他们到达了他们的目的地...
        text_story.DOText("故事是这样的\n一些机器人脱离人类的统治，在银河中寻找新的家园，现在他们到达了他们的目的地........点击继续", 8).SetEase(Ease.Linear).OnComplete(()=> {
            for (int i = 0; i < bg_objs.Count; i++)
            {
                bg_objs[i].GetComponent<Button>().onClick.RemoveAllListeners();
                bg_objs[i].GetComponent<Button>().onClick.AddListener(()=> {
                    GameSceneManager.LoadScene("GameScene");
                });
                bg_objs[i].GetComponent<Image>().raycastTarget = true;
            }
        });
    }

    public void AnykeyFlash() {
        text_anykey.DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void ChangeSprites()
    {
        if (ship_index > ship_sprites.Count - 1)
        {
            ship_index = 0;
        }
        ship_renderer.sprite = ship_sprites[ship_index];
        ship_index += 1;
    }

    public void ShowStar() {

        for (int i = 0; i < bg_objs.Count; i++)
        {
            bg_objs[i].GetComponent<Image>().raycastTarget = false;
        }

        text_anykey.gameObject.SetActive(false);
        BgSlowDown();
        star_obj.gameObject.transform.DOLocalMoveY(280, 2).OnComplete(()=>{
            ShipGo();
        });
    }

    public void ShipGo(){
       
        ship_renderer.gameObject.transform.DOLocalMoveY(280, 3).OnComplete(()=> {
            ShowStoryText();
        });
        ship_renderer.gameObject.transform.DOScale(new Vector3(0, 0, 0), 3);
    }

    public void ShowShip()
    {
        ship_renderer.gameObject.transform.DOLocalMoveY(-420, 2).OnComplete(()=> {
            ShowAnyKey();
        });
    }

    public void BgSlowDown() {
        DOTween.To(() => bg_move_speed, x => bg_move_speed = x, 20, 2);
    }

    

    public void BGRolling()
    {
        for (int i = 0; i < bg_objs.Count; i++)
        {
            if (bg_objs[i].transform.localPosition.y < -1080)
            {
                bg_objs[i].transform.localPosition = new Vector3(0, 1080, 0);
            }
            bg_objs[i].transform.Translate(Vector3.down * Time.deltaTime * bg_move_speed);
        }
    }
}
