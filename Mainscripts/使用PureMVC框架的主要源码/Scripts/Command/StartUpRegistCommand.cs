using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns.Command;
using PureMVC.Interfaces;

public class StartUpRegistCommand : MacroCommand//复合指令
{

    protected override void InitializeMacroCommand()
    {
        //初始化复合指令
        //无需绑定消息 直接执行多个指定的 单一指令

        //添加指令
        AddSubCommand(()=>new RegistMediatorCommand() as PureMVC.Interfaces.ICommand);
        AddSubCommand(() => new RegistProxyCommand() as PureMVC.Interfaces.ICommand);
        //添加指令到复合指令后 无需通过消息指令 可直接执行 对应指令类的执行函数
        base.InitializeMacroCommand();
    }

}
