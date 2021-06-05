using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MIDUI {
    public class GameingUIPanel : BaseUIPanel
    {
        public Text timeTxt;
        public Text scoreTxt;
        public Text highScoreTxt;

        private void Awake()
        {

            timeTxt = transform.Find("Contents").Find("Time").GetComponent<Text>();
            scoreTxt = transform.Find("Contents").Find("Score").GetComponent<Text>();
            highScoreTxt = transform.Find("Contents").Find("HightScore").GetComponent<Text>();
        }
        private void Start()
        {
            //启动注册
            AppFacade.instance.SendNotification(FlappyBird.AppConst.startUpRegist);
            //游戏开始指令消息
            AppFacade.instance.SendNotification(FlappyBird.AppConst.startGame);

        }
        private void OnEnable()
        {
            if (GameManager.instance != null && GameManager.instance.IsGameOver) {

                GameManager.instance.ReStartGame();
            }
        }
    } 
    

}

