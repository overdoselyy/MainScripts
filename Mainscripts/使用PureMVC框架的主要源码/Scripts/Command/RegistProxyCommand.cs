using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistProxyCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        //注册数据层
        AppFacade.instance.RegisterProxy(new BirdDataProxy());
    }
}
