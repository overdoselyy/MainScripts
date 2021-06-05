using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 联网对战
/// 初始化玩家
/// </summary>
public class MainLvNetworkManager : Photon.PunBehaviour
{
	public GameObject playerPrefeb;
	public GameObject timerPrefeb;
	private void OnLevelWasLoaded(int level)
	{
		StartCoroutine(ShowPlayer());
	}
	IEnumerator ShowPlayer()
	{
		yield return new WaitForSeconds(0.1f);
		PhotonNetwork.Instantiate(playerPrefeb.name, new Vector3(Random.Range(0f, 10f), 1, 1), Quaternion.identity, 0);
		//PhotonNetwork.InstantiateSceneObject(timerPrefeb.name,new Vector3(0,0,0),Quaternion.identity,0,null);
		//PhotonNetwork.Instantiate(playerPrefeb.name, new Vector3(0, 5, 0), Quaternion.identity, 0);
	}
}