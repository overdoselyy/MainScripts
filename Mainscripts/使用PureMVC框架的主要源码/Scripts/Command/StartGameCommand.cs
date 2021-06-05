using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns.Command;
using PureMVC.Interfaces;

public class StartGameCommand : SimpleCommand
{

    public override void Execute(INotification notification)
    {
        //游戏开始逻辑
        GameObject.Find("GameManager").AddComponent<GameManager>();

    }
}
