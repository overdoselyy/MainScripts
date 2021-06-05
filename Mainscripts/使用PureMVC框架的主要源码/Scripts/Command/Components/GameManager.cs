using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    BirdDataProxy dataProxy;
    bool isGameOver;

    Bird player;
    //移动物体
    GameObject[] gameObjects;
    public bool IsGameOver {
        get {
            return isGameOver;
        }
    }
    public BirdDataProxy DataProxy {
        get {
            return dataProxy;
        }
    }
    private void Awake()
    {
        //游戏加载
        GameInit();
        instance = this;
    }

    public void GameOver() {

        StopAllCoroutines();//关闭所有定时器
        isGameOver = true;
        //显示游戏结束界面
        MIDUI.UIPanelManager.instance.ShowUIPanel("GameOverUIPanel");

    }

    public void ReStartGame() {
        isGameOver = false;
        foreach (var obj in gameObjects)
        {
            var moveObj = obj.GetComponent<MoveObject>();
            moveObj.transform.position = moveObj.curPos;//恢复初始位置
        }
        //dataProxy 数据层的时间 和分数 重置 -> 视图更新
        dataProxy.ReStart();
        //player开启控制 取消动力学
        player.ReStart();
        StartCoroutine(GameUpdate());

    }
    public void GameInit() {
        //开启 小鸟 控制
        player = GameObject.FindGameObjectWithTag("Player").AddComponent<Bird>();
        //添加物体移动组件
        gameObjects = GameObject.FindGameObjectsWithTag("MovedObj");
        foreach (var obj in gameObjects)
        {
            var moveObj =  obj.AddComponent<MoveObject>();
            if (moveObj.name == "Ground") {
                
            
            }
            switch (obj.name)
            {
                case "Ground":
                    moveObj.returnX = -3.5f;
                    break;
                case "PipeRoot":
                    moveObj.returnX = -54f;
                    break;
            }

        }
        //获取数据层
        dataProxy = AppFacade.instance.RetrieveProxy(BirdDataProxy.NAME) as BirdDataProxy;


        //开启游戏协程
        StartCoroutine(GameUpdate());


    }
    IEnumerator GameUpdate() {

        while (true) {
            yield return new WaitForSeconds(1f);
            dataProxy.TimeUpdate();
        }

    }

    public static GameManager instance = new GameManager();

}
