using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;
/// <summary>
/// 网络异步加载场景
/// </summary>
public class NetworkManager : Photon.PunBehaviour
{
	public GameObject nameUI;
	public InputField playername;
	private AsyncOperation asy;
	public Slider progressB;
	public GameObject playerUI;
	public GameObject sence;
	public GameObject lodingUI;
	public Text txt;
	void Start()
	{
		//StartCoroutine("LoadScene");
		PhotonNetwork.ConnectUsingSettings("0.0.1");
	}
	void Update()
	{
			if (asy.progress < 0.9f)
			{
				progressB.value = asy.progress;
				txt.text = "正在加载，请稍候 ";
			}
			else {
				progressB.value = 1;
				txt.text = "请输入任意按钮以继续 ";
				if (Input.anyKeyDown)
				{
					asy.allowSceneActivation = true;
				}
			}
		//txt.text = asy.progress * 100 + "%";
	}
	public override void OnConnectedToMaster()
    {
		nameUI.SetActive(true);
		PhotonNetwork.JoinLobby();
    }

	IEnumerator LoadScene()
	{
		asy = SceneManager.LoadSceneAsync("NetworkTest");
		asy.allowSceneActivation = false;
		Debug.Log(asy.progress);
		yield return asy;
	}
	IEnumerator changeSkin()  //协程方法
	{
		yield return new WaitForSeconds(1f);
		asy.allowSceneActivation = true;
	}
	public void PlayButton() {
		PhotonNetwork.player.NickName = playername.text;
		nameUI.SetActive(false);

		sence.SetActive(false);
		playerUI.SetActive(false);
		lodingUI.SetActive(true);

		StartCoroutine("LoadScene");
	}
}

