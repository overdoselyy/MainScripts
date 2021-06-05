using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistMediatorCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        //注册视图层
        AppFacade.instance.RegisterMediator(new BirdDataMediator());
    }

}
