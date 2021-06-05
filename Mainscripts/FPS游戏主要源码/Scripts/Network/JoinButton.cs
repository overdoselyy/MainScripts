using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 加入按钮
/// </summary>
public class JoinButton : Photon.PunBehaviour
{
    public Text joinButtonText;
    public void Join()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(joinButtonText.text, options, default);
    }
   
}
