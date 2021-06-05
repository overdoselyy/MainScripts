using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 数据助理  操作游戏数据
/// </summary>
public class BirdDataProxy : Proxy
{
    BirdData data;//数据引用
    public static new string NAME = "BirdDataProxy";

    public BirdDataProxy() :base(NAME){
        data = new BirdData();
        data.time = 0;
        data.score = 0;
    }
    public void TimeUpdate() {
        data.time++;
        //通知视图更新显示
        SendNotification(FlappyBird.AppConst.updadeTimeUI, data.time);
    
    }
    public void ScoreUpdate(int _addScore = 20)
    {
        data.score += _addScore;
        //通知视图更新显示

        if (data.score > data.hightScore) {
            data.hightScore = data.score;//更新最高分
        }
        //更新视图消息
        SendNotification(FlappyBird.AppConst.updadeScoreUI, data);

    }

    public void ReStart() {

        data.time = 0;
        data.score = 0;
        SendNotification(FlappyBird.AppConst.updadeTimeUI, data.time);
        SendNotification(FlappyBird.AppConst.updadeScoreUI, data);
    }
}
