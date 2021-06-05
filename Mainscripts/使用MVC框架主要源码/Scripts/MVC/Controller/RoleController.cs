using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : MonoBehaviour
{
    //能够在controller中得到界面
    private RoleView roleView;

    private static RoleController controller = null;

    public static RoleController Controller
    {
        get
        {
            return controller;
        }
    }

    //1.界面的显影
    public static void ShowMe()
    {
        if (controller == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象为 canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            controller = obj.GetComponent<RoleController>();
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

    void Start()
    {
        //获取同样挂载在同一个对象的 View脚本
        roleView = this.GetComponent<RoleView>();
        //第一次的界面更新
        roleView.UpdateInfo(PlayerModel.Data);

        //2.界面的 事件监听 来处理对应的业务逻辑
        roleView.btnClose.onClick.AddListener(() =>
        {
            Debug.Log("点击按钮关闭");
            HideMe();
        });
        roleView.btnLevUp.onClick.AddListener(() =>
        {
            Debug.Log("点击升级按钮");
            //通过数据模块进行升级 达到数据改变
            PlayerModel.Data.LevUp();
        });
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
