using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 联机部分
/// 主菜单管理
/// </summary>
public class MainMenuManager : Photon.PunBehaviour
{
	public int maxplayer;
	public GameObject roomUI;
	public GameObject mainUI;
	public InputField roomname;
	public GameObject roomListUI;

	private AsyncOperation asy;
	public Slider progressB;

	public GameObject playerUI;
	public GameObject sence;
	public GameObject lodingUI;
	public GameObject HelpUI;
	public Text txt;
	void Start()
    {
		//PhotonNetwork.ConnectUsingSettings("0.0.1");
	    mainUI.SetActive(true);
	}
	//public override void OnConnectedToMaster()
	//{
	//	Debug.Log("sssssssssssssssssssssssss");
	//	PhotonNetwork.JoinLobby();
	//}

	void Update()
	{

		if (asy.progress < 0.9f)
		{
			progressB.value = asy.progress;
			txt.text = "正在加载，请稍候 ";
		}
		else
		{
			progressB.value = 1;
			txt.text = "请输入任意按钮以继续 ";
			if (Input.anyKeyDown)
			{
				asy.allowSceneActivation = true;
			}
		}
	}

	IEnumerator LoadScene()
	{
		//u3d 5.3之后使用using UnityEngine.SceneManagement加载场景
		asy = PhotonNetwork.LoadLevelAsync("Main");
		//不允许加载完毕自动切换场景，因为有时候加载太快了就看不到加载进度条UI效果了
		asy.allowSceneActivation = false;
		yield return asy;
	}
	IEnumerator LoadScene2()
	{
		//u3d 5.3之后使用using UnityEngine.SceneManagement加载场景
		asy = SceneManager.LoadSceneAsync("Stand-alone");
		//不允许加载完毕自动切换场景，因为有时候加载太快了就看不到加载进度条UI效果了
		asy.allowSceneActivation = false;
		yield return asy;
	}


	public void RoomBackButton()
	{
		roomUI.SetActive(false);
		roomListUI.SetActive(false);
		mainUI.SetActive(true);
	}
	public void MainPlayButton()
	{
		mainUI.SetActive(false);
		roomUI.SetActive(true);
		roomListUI.SetActive(true);
		if (PhotonNetwork.insideLobby)
		{
			roomListUI.SetActive(true);
		}
	}
	public void HelpButton()
	{
		mainUI.SetActive(false);
		HelpUI.SetActive(true);
	}
	public void HelpExitButton()
	{
		HelpUI.SetActive(false);
		mainUI.SetActive(true);

	}
	public void PlayStandAloneButton()
	{
		roomListUI.SetActive(false);
		roomUI.SetActive(false);
		sence.SetActive(false);
		playerUI.SetActive(false);
		lodingUI.SetActive(true);
		StartCoroutine("LoadScene2");
	}
	public void JoinOrCreatButton()
	{
		if (roomname.text.Length < 2)
		{
			return;
		}
		roomUI.SetActive(false);
		RoomOptions options = new RoomOptions { MaxPlayers = 4 };
		PhotonNetwork.JoinOrCreateRoom(roomname.text, options, default);
	}

	public void StartMatching()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		PhotonNetwork.CreateRoom("1");
	}
	public override void OnCreatedRoom()
	{
		Debug.Log("created room");
	}
	public override void OnJoinedRoom()
	{
		Debug.Log("join room");
		roomUI.SetActive(false);
		sence.SetActive(false);
		playerUI.SetActive(false);
		lodingUI.SetActive(true);
		//PhotonNetwork.LoadLevel("Main");
		StartCoroutine("LoadScene");
		PhotonNetwork.automaticallySyncScene = true;
	}
	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		int number = PhotonNetwork.playerList.Length;
		Debug.Log(number + " player now in the room");
		if (number == maxplayer)
		{
			if (PhotonNetwork.isMasterClient)
			{
				roomListUI.SetActive(false);
				roomUI.SetActive(false);
				sence.SetActive(false);
				playerUI.SetActive(false);
				lodingUI.SetActive(true);
				StartCoroutine("LoadScene");
				//PhotonNetwork.LoadLevel("Main");
			}
		}

	}
		public void QuitGame()
		{
	#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
	#else
			Application.Quit();
	#endif
		}

}
