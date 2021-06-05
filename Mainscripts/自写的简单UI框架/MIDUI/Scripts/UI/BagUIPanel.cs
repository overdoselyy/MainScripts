using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MIDUI
{
    public class BagUIPanel : BaseUIPanel
    {
        public override void Show()
        {
            base.Show();
            transform.DOMoveY(2000, 0.5f).From();
        }
    }


}
