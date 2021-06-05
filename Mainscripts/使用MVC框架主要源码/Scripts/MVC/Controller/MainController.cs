using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// controller要处理的东西 就是业务逻辑
/// 
/// </summary>
public class MainController : MonoBehaviour
{
    //能够在controller中得到界面
    private MainView mainView;

    private static MainController controller = null;

    public static MainController Controller{
        get {
            return controller;
        }
    }

    //1.界面的显影
    public static void ShowMe()
    {
        if (controller == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/MainPanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象为 canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            controller = obj.GetComponent<MainController>();
        }
        controller.gameObject.SetActive(true);
    }
    public static void HideMe()
    {
        if (controller != null)
        {
            //方式一 直接删
            //Destroy(panel.gameObject);
            //panel = null;

            //方式二 设置为隐藏
            controller.gameObject.SetActive(false);
        }
    }

    //3.界面的更新

    private void UpdateInfo(PlayerModel data) {
        if (mainView != null) {
            mainView.UpdateInfo(data);
        }
    }

    void Start()
    {
        //获取同样挂载在同一个对象的 View脚本
        mainView = this.GetComponent<MainView>();
        //第一次的界面更新
        mainView.UpdateInfo(PlayerModel.Data);

        //2.界面的 事件监听 来处理对应的业务逻辑
        mainView.btnRole.onClick.AddListener(() =>
        {
            Debug.Log("点击按钮显示角色面板");
            //通过controller显示角色面板
            RoleController.ShowMe();
        });
        //告知数据模块 当更新时 通知那个函数做处理
        PlayerModel.Data.AddEventListener(UpdateInfo);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //显示主面板
            MainController.ShowMe();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            //隐藏主面板
           MainController.HideMe();
        }
    }
    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }

}
