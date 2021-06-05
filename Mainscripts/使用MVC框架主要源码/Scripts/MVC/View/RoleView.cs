using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleView : MonoBehaviour
{
    //1.获得控件
    public Text txtHp;
    public Text txtLev;
    public Text txtAtk;
    public Text txtDef;
    public Text txtMiss;
    public Text txtLuck;
    public Text txtCrit;

    public Button btnClose;
    public Button btnLevUp;

    //2.提供面板更新相关的方法给外部
    public void UpdateInfo(PlayerModel data)
    {
        txtHp.text = data.Hp.ToString();
        txtLev.text = "Lv." + data.Lev;
        txtAtk.text = data.Atk.ToString();
        txtDef.text = data.Def.ToString();
        txtMiss.text = data.Miss.ToString();
        txtLuck.text = data.Luck.ToString();
        txtCrit.text = data.Crit.ToString();
    }

}
