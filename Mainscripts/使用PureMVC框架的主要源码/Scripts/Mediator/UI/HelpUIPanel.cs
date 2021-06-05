using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MIDUI
{
    public class HelpUIPanel : BaseUIPanel
    {
        private void Awake()
        {
            transform.Find("GameBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                //隐藏其他界面
                UIPanelManager.instance.HideAllUIPanel();
                //显示游戏进行时UI
                UIPanelManager.instance.ShowUIPanel("GameingUIPanel");
            });
        }
    }
}