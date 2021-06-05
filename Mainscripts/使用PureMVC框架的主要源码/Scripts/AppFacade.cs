using PureMVC.Patterns.Facade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 注册MVC三层
/// </summary>
public class AppFacade : Facade
{
    void Init() {
        //注册控制指令
        RegisterCommand(FlappyBird.AppConst.startGame,()=>new StartGameCommand());
        RegisterCommand(FlappyBird.AppConst.startUpRegist, () => new StartUpRegistCommand());
        //注册视图

        //注册数据
    }
    public void StartUp() { 
    

    
    }
    AppFacade() :base("bird")
    { 
        Init();

    }
    public static AppFacade instance = new AppFacade();
}
