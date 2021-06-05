using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MIDUI {
    public class GameOverUIPanel : BaseUIPanel
{
        private void Awake()
        {
            transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                //隐藏所有页面
                UIPanelManager.instance.HideAllUIPanel();
                //显示开启页面
                UIPanelManager.instance.ShowUIPanel("StartUIPanel");
            });
        }
        public override void Show()
        {
            base.Show();
            transform.DOMoveY(2000, 0.5f).From();
        }
    }

}
