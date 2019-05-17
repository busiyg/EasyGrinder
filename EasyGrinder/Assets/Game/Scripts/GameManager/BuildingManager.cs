using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager Instance;

    public static BuildingManager GetInstance() {
        return Instance;
    }
    public List<BuildingObj> buildings_obj;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHouse(int code,string name)
    {
        var obj = GetHouseObjById(code);
        obj.obj.transform.DOLocalMoveY(0.14f,0.5f);
        obj.text.gameObject.SetActive(true);
        obj.text.text = name;
    }

    public void Test() {
        ShowHouse(1,"核电站");
        ShowHouse(2, "核电站");
        ShowHouse(3, "核电站");
        ShowHouse(4, "核电站");
        ShowHouse(5, "核电站");
        ShowHouse(6, "核电站");
    }

    public void HideHouse(int code) {
        var obj = GetHouseObjById(code);
        obj.obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.text.gameObject.SetActive(false);
        obj.text.text = "";
    }

    public static BuildingObj GetHouseObjById(int id) {
        foreach (var obj in Instance.buildings_obj) {
            if (id ==obj.code) {
                return obj;
            }
        }

        return null;
    }

}

[System.Serializable]
public class BuildingObj {
    public GameObject obj;
    public TextMesh text;
    public int code;
}
