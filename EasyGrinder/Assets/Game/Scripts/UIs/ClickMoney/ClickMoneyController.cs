using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClickMoneyController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Image> coins;
    public Text text;

    public void ShowCoins(float count) {
        text.text = "+"+GameUtility.NumConversion(count);
        text.transform.DOLocalMoveY(100,0.8f);
        text.DOFade(0,0.8f);
        for (int i = 0; i < coins.Count; i++) {
            var coin = coins[i];
            var start_x = Random.Range(-50,50);
            var start_y = Random.Range(0, 50);
            Vector3 start_pos = new Vector3(start_x, start_y,0);
            Vector3 pos_1 = new Vector3(coin.transform.localPosition.x, coin.transform.localPosition.y + 40, 0);
            coin.transform.localPosition = start_pos;
            coin.gameObject.SetActive(true);
            // coins[i].transform.DOPath();
            coin.transform.DOLocalMove(new Vector3(coin.transform.localPosition.x, coin.transform.localPosition.y+40,0),0.5f).OnComplete(()=> {
                coin.transform.DOLocalMove(new Vector3(start_pos.x+start_x * 0.25f, coin.transform.localPosition.y - 200),1).SetEase(Ease.InSine);
                coin.DOFade(0,1).OnComplete(()=> { Destroy(gameObject); });
            });
        }
    }
}
