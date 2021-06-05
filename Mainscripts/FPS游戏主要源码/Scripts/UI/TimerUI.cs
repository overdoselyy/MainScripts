using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 计时器的网络同步
/// </summary>
public class TimerUI : Photon.PunBehaviour
{
    public float time_All = 300;//计时的总时间（单位秒）  
    public float time_Left;//剩余时间  
    public Text time;
    public bool changeTime = false;

    void Start()
    {
        time = GameObject.Find("Time").GetComponent<Text>();
        if (PhotonNetwork.isMasterClient)
        {
            
            PhotonView pv = transform.GetComponent<PhotonView>();
            pv.RPC("Timer", PhotonTargets.AllBuffered);
        }
       

    
      
    }
    private void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonView pv = transform.GetComponent<PhotonView>();
            pv.RPC("UpdateTimer", PhotonTargets.AllBuffered);
        }
    }
    [PunRPC]
    public void Timer()
    {
        if (time.text == "")
        {
            time_Left = time_All;
            changeTime = true;
        }
    }
    [PunRPC]
    public void UpdateTimer()
    {
        if (changeTime == true)
        {
            StartTimer();
        }
    }

    /// <summary>  
    /// 开始计时   
    /// </summary>  
    void StartTimer()
    {
        time_Left -= Time.deltaTime;
        time.text = GetTime(time_Left);
    }


    /// <summary>  
    /// 获取总的时间字符串  
    /// </summary>  
    string GetTime(float time)
    {
        return GetMinute(time) + GetSecond(time);
    }


    /// <summary>  
    /// 获取小时  
    /// </summary>  
    string GetHour(float time)
    {
        int timer = (int)(time / 3600);
        string timerStr;
        if (timer < 10)
            timerStr = "0" + timer.ToString() + ":";
        else
            timerStr = timer.ToString() + ":";
        return timerStr;

    }
    /// <summary>  
    ///获取分钟   
    /// </summary>  
    string GetMinute(float time)
    {
        int timer = (int)((time % 3600) / 60);
        string timerStr;
        if (timer < 10)
            timerStr = "0" + timer.ToString() + ":";
        else
            timerStr = timer.ToString() + ":";
        return timerStr;
    }
    /// <summary>  
    /// 获取秒  
    /// </summary>  
    string GetSecond(float time)
    {
        int timer = (int)((time % 3600) % 60);
        string timerStr;
        if (timer < 10)
            timerStr = "0" + timer.ToString();
        else
            timerStr = timer.ToString();
        return timerStr;
    }
}
