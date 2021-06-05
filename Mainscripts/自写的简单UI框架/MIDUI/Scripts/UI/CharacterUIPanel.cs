using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MIDUI
{
    public class CharacterUIPanel : BaseUIPanel
    {
        public override void Show()
        {
            base.Show();
            transform.DOScale(Vector3.zero, 0.5f).From();
        }

        public override void Hide()
        {
            StartCoroutine(DoHide());
        }

        IEnumerator DoHide() {

            transform.DOScale(Vector3.zero, 0.5f);
            yield return new WaitForSeconds(0.8f);
            transform.localScale = Vector3.one;//恢复比例
            base.Hide();

        
        }
    }


}
