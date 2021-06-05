using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 管理多人同步问题
/// 关闭和自己身上
/// 挂有相同脚本的
/// 游戏对象的影响
/// </summary>
public class PlayerSetup : MonoBehaviour
{
	public Camera myCamera;
	public Behaviour[] stuffNeedDisable;

	public Text nameText;
	PhotonView pv;
	void Start()
	{
		pv = GetComponent<PhotonView>();
		if (pv.isMine)
		{
			nameText.text = PhotonNetwork.player.NickName;
		}
		else {
			nameText.text = pv.owner.NickName;
		}
		if (!pv.isMine)
		{
			myCamera.enabled = false;
			for (int i = 0; i < stuffNeedDisable.Length; i++)
			{
				stuffNeedDisable[i].enabled = false;
			}
		}
	}
}
