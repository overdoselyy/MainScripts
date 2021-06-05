using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 游戏内主菜单UI
/// </summary>
public class MainMenuUI : Photon.PunBehaviour
{
    public GameObject mainMenuUI;
    public GameObject setMenuUI;
    public Text vsyText;
    public InputField Fps;
    private bool vsy = true;//垂直同步是否开启
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && setMenuUI.activeSelf.Equals(false)) 
        {
            mainMenuUI.SetActive(true);
        }
    }
    public void BackGame() {
        mainMenuUI.SetActive(false);
    }
    public override void OnLeftRoom()
    {
        // 加载大厅场景
        SceneManager.LoadScene("NetworkTest");
    }
    public void Skode_LeaveRoom()
    { 
            PhotonNetwork.LeaveRoom();
    }
    public void QuitGame()
   {
      #if UNITY_EDITOR
       UnityEditor.EditorApplication.isPlaying = false;
      #else
        Application.Quit();
      #endif
    }
    public void SetMenu() {
        mainMenuUI.SetActive(false);
        setMenuUI.SetActive(true);
    }
    public void VsyBtn() {
        if (vsy == true)
        {
            VsyncClose();
            vsyText.text = "关闭";
            vsy = false;
        }
        else {
            VsyncOpen();
            vsyText.text = "开启";
            vsy = true;
        }
    }
    public void SetFps() {
        float FPS =  float.Parse(Fps.text);
        Application.targetFrameRate = (int)FPS;
    }
    public void BackMainMenu() {
        mainMenuUI.SetActive(true);
        setMenuUI.SetActive(false);
    }
    public void VsyncOpen()
    {
        QualitySettings.vSyncCount = 1;
    }
    public void VsyncClose()
    {
        QualitySettings.vSyncCount = 0;
    }
}
