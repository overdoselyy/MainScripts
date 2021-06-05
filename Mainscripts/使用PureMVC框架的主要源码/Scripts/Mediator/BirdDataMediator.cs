using MIDUI;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 视图组件
/// </summary>
public class BirdDataMediator : Mediator
{
    GameingUIPanel view;//视图组件 包含三个文本
    public static new string NAME = "BirdDataMediator";
    public BirdDataMediator() :base(NAME)
    {
        //获取视图组件
        view = UIPanelManager.instance.GetUIPanel("GameingUIPanel") as GameingUIPanel;
    
    }
    //添加监听消息
    public override string[] ListNotificationInterests()
    {
        List<string> msgs = new List<string>();
        msgs.Add(FlappyBird.AppConst.updadeTimeUI);
        msgs.Add(FlappyBird.AppConst.updadeScoreUI);
        return msgs.ToArray();
    }
    //处理监听消息
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case FlappyBird.AppConst.updadeScoreUI:
                //显示分数
                BirdData data = (BirdData)notification.Body;
                view.scoreTxt.text = "分数：" + data.score;
                view.highScoreTxt.text = "最高分：" + data.hightScore;
                break;
            case FlappyBird.AppConst.updadeTimeUI:
                //显示时间
                int time = (int)notification.Body;
                view.timeTxt.text = "时间：" + time;
                break;
            default:
                break;
        }
    }


}
