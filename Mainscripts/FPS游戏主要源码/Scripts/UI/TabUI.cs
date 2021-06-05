using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 显示玩家列表
/// </summary>
public class TabUI : MonoBehaviour
{
    public GameObject table;
    public Text tableText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            table.SetActive(true);
            TableUI();
        }
        if(Input.GetKeyUp(KeyCode.Tab))
       {
            tableText.text = "";
            table.SetActive(false);

        }
    }
    public void TableUI() {
        int players = PhotonNetwork.playerList.Length; ;
        for (int i = 0; i < players; i++)
        {
            tableText.text +=" 玩家 ： " + PhotonNetwork.playerList[i].NickName + "\n";
        }
       
    }
}
