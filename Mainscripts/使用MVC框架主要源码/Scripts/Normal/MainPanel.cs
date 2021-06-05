using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    //1.获得控件
    public Text txtName;
    public Text txtLev;
    public Text txtMoney;
    public Text txtGem;
    public Text txtPower;

    public Button btnRole;

    private static MainPanel panel;

    public static MainPanel Panel {
        get {
            return panel;
        }
    }
    //4.动态显隐
    public static void ShowMe() {
        if (panel == null) {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/MainPanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象为 canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            panel = obj.GetComponent<MainPanel>();
        }
        //如果是隐藏的形式 hide 在这要显示
        panel.gameObject.SetActive(true);
        //显示完面板 更新该面板的信息
        panel.UpdateInfo();
    }
    public static void HideMe()
    {
        if (panel != null) {
            //方式一 直接删
            //Destroy(panel.gameObject);
            //panel = null;

            //方式二 设置为隐藏
            panel.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        //2.添加事件
        btnRole.onClick.AddListener(() =>
        {
            //打开角色面板的逻辑
            Debug.Log("按钮点击");
            //显示角色面板
            RolePanel.ShowMe();
        });
        //btnRole.onClick.AddListener(ClickBtnRole);

    }

    private void ClickBtnRole() { 
        //打开角色面板的逻辑
    }



    //3.更新信息
    public void UpdateInfo() {
        //获取玩家数据 更新玩家信息
        //获取玩家数据的方式 1.网络请求 2.json 3.xml 4.2进制 5.PlayerPrefs公共类
        //通过PlayerPrefs来获取本地存储的玩家信息 更新到界面上
        txtName.text = PlayerPrefs.GetString("PlayerName","旺旺");
        txtName.text = "Lv." + PlayerPrefs.GetInt("PlayerLev", 1).ToString();
        txtMoney.text = PlayerPrefs.GetInt("PlayerMoney", 666).ToString();
        txtGem.text = PlayerPrefs.GetInt("PlayerGem", 555).ToString();
        txtPower.text = PlayerPrefs.GetInt("PlayerPower", 888).ToString();
    }
}
