using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
namespace MIDUI {

    public class MainUIPanel : BaseUIPanel
    {
        Button[] panelButtons;

        private void Awake()
        {
            panelButtons = transform.Find("Content").GetComponentsInChildren<Button>();
            //绑定监听
            foreach (Button btn in panelButtons)
            {
                btn.onClick.AddListener(() =>
                {
                    switch (btn.name) {
                        case "CharacterBtn":
                            UIPanelManager.instance.ShowUIPanel("CharacterPanel");
                            break;
                        case "BagBtn":
                            UIPanelManager.instance.ShowUIPanel("BagPanel");
                            break;
                        case "SkillBtn":
                            UIPanelManager.instance.ShowUIPanel("SkillPanel");
                            break;
                        case "TaskBtn":
                            UIPanelManager.instance.ShowUIPanel("TaskPanel");
                            break;
                        default:
                            break;
                    }


                });


            }

        }
        public override void Show()
        {
            base.Show(); 
            transform.localPosition = Vector3.zero;
        }
    }

}
