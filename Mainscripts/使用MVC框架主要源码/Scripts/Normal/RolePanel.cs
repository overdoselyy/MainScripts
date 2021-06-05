using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RolePanel : MonoBehaviour
{
    //1.获得控件
    public Text txtHp;
    public Text txtLev;
    public Text txtAtk;
    public Text txtDef;
    public Text txtCrit;
    public Text txtMiss;
    public Text txtLuck;

    public Button btnClose;
    public Button btnLevUp;
    private static RolePanel panel;
    //4.动态显隐
    public static void ShowMe()
    {
        if (panel == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象为 canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            panel = obj.GetComponent<RolePanel>();
        }
        //如果是隐藏的形式 hide 在这要显示
        panel.gameObject.SetActive(true);
        //显示完面板 更新该面板的信息
        panel.UpdateInfo();
    }
    public static void HideMe()
    {
        if (panel != null)
        {
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
        btnClose.onClick.AddListener(() =>
        {
            //关闭
            Debug.Log("关闭");
            HideMe();
        });
        btnClose.onClick.AddListener(() =>
        {
            //升级
            Debug.Log("升级");
            //升级就是 数据的更新
            //数据存在本地
            //获取本地数据
            int lev = PlayerPrefs.GetInt("PlayerLev", 1);
            int hp = PlayerPrefs.GetInt("PlayerHp", 100);
            int atk = PlayerPrefs.GetInt("PlayerAtk", 555);
            int def = PlayerPrefs.GetInt("PlayerDef", 666);
            int miss = PlayerPrefs.GetInt("PlayerMiss", 666);
            int luck = PlayerPrefs.GetInt("PlayerLuck", 555);
            int crit = PlayerPrefs.GetInt("PlayerCrit", 888);
            //然后通过一定的升级规则改变它
            lev += 1;
            hp += lev;
            atk += lev;
            def += lev;
            crit += lev;
            miss += lev;
            luck += lev;
            //存起来
            PlayerPrefs.SetInt("PlayerLev", lev);
            PlayerPrefs.SetInt("PlayerHp", hp);
            PlayerPrefs.SetInt("PlayerDef", def);
            PlayerPrefs.SetInt("PlayerAtk", atk);
            PlayerPrefs.SetInt("PlayerMiss", miss);
            PlayerPrefs.SetInt("PlayerLuck", luck);
            PlayerPrefs.SetInt("PlayerCrit", crit);
            //同步更新面板上的数据
            UpdateInfo();

            //更新主面板的内容
            MainPanel.Panel.UpdateInfo(); 
        });

    }



    //3.更新信息
    public void UpdateInfo()
    {
        //获取玩家数据 更新玩家信息
        //获取玩家数据的方式 1.网络请求 2.json 3.xml 4.2进制 5.PlayerPrefs公共类
        //通过PlayerPrefs来获取本地存储的玩家信息 更新到界面上
        txtLev.text = "Lv." + PlayerPrefs.GetInt("PlayerLev", 1);
        txtHp.text = PlayerPrefs.GetInt("PlayerHp", 100).ToString();
        txtDef.text = PlayerPrefs.GetInt("PlayerMoney", 666).ToString();
        txtAtk.text = PlayerPrefs.GetInt("PlayerGem", 555).ToString();
        txtCrit.text = PlayerPrefs.GetInt("PlayerPower", 888).ToString();
        txtMiss.text = PlayerPrefs.GetInt("PlayerMoney", 666).ToString();
        txtLuck.text = PlayerPrefs.GetInt("PlayerGem", 555).ToString();
    }

}
