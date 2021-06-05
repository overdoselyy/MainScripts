using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
/// <summary>
/// 大厅房间管理
/// </summary>
public class RoonmListManager : Photon.PunBehaviour
{
    public GameObject roomNamePreferb;
    public Transform gridLayout;
    public override void OnReceivedRoomListUpdate()
    {
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        for (int i = 0; i < gridLayout.childCount; i++) {
            if (gridLayout.GetChild(i).gameObject.GetComponentInChildren<Text>().text == roomList[i].Name)
            {
                Destroy(gridLayout.GetChild(i).gameObject);
                if (roomList[i].PlayerCount == 0) {
                    Destroy(gridLayout.GetChild(i).gameObject);
                }
            }

        }
        foreach (RoomInfo room in roomList) 
        {
            GameObject newRoom = Instantiate(roomNamePreferb, gridLayout.position, Quaternion.identity);

            newRoom.GetComponentInChildren<Text>().text = room.Name ;

            newRoom.transform.SetParent(gridLayout);
        }
    }
 
}
