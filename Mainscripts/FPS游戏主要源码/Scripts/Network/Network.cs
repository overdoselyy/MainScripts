using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏版本
/// </summary>
public class Network : Photon.PunBehaviour
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.5.1");
    }
}
