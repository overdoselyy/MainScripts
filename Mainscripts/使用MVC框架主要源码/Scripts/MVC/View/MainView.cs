using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    //1.获得控件
    public Text txtName;
    public Text txtLev;
    public Text txtMoney;
    public Text txtGem;
    public Text txtPower;

    public Button btnRole;
    public Button btnSill;

    //2.提供面板更新相关的方法给外部
    public void UpdateInfo(PlayerModel data) 
    {
        txtName.text = data.PlayerName;
        txtLev.text = "Lv." + data.Lev;
        txtMoney.text = data.Money.ToString();
        txtPower.text = data.Power.ToString();
        txtGem.text = data.Gem.ToString();
    }
}
