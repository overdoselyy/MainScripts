using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
/// <summary>
/// 武器切换
/// </summary>
public class PickWeapon : MonoBehaviour, IPunObservable
{
    public List<WeaponState> Weapons;
    public WeaponState Curweapon;
    public int CurweaponIdx;

    public FullBodyBipedIK biedIK;
    Animator anim = null;

    public GameObject cop;
    public GameObject m4;

    AnimatorStateInfo info;

    public bool isPickWeapon = true;


    public bool pickWeapon = false;

    public PlayerReset _playerReset;
    public float f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (_playerReset.dead == false)
        {
            if (isPickWeapon)
            {
                info = anim.GetCurrentAnimatorStateInfo(3);

                if (info.tagHash != Animator.StringToHash("pickWeapon")) {
                    f = Input.GetAxis("Mouse ScrollWheel");
                    if (f > 0)
                    {
                        pickWeapon = true;
                        if (pickWeapon)
                        {
                            PhotonView pv = transform.GetComponent<PhotonView>();
                            pv.RPC("NextWeapon", PhotonTargets.All, 1);
                        }

                    }
                    else if (f < 0)
                    {
                        pickWeapon = true;
                        if (pickWeapon)
                        {
                            PhotonView pv = transform.GetComponent<PhotonView>();
                            pv.RPC("NextWeapon", PhotonTargets.All, -1);
                        }

                    }
                    else
                    {
                        anim.SetBool("pick_cop", false);
                        anim.SetBool("pick_m4", false);

                    }
                }
            }
        }
        else
        {
            Weapons[0].bulletTotal = Weapons[0].bulletTotal2;
            Weapons[0].bulletPerMag = Weapons[0].bulletPerMag2;
            Weapons[0].bulletCurrent = Weapons[0].bulletPerMag;
            Weapons[1].bulletTotal = Weapons[1].bulletTotal2;
            Weapons[1].bulletPerMag = Weapons[1].bulletPerMag2;
            Weapons[1].bulletCurrent = Weapons[1].bulletPerMag;
        }
    }
    /// <summary>
    /// 切换到下一把枪
    /// </summary>
    /// <param name="step"></param>
    [PunRPC]
    public void NextWeapon(int step)
    {
        var idx = (CurweaponIdx + step + Weapons.Count) % Weapons.Count;
        if (CurweaponIdx == 0)
        {
            anim.SetBool("pick_cop", true);
            cop.SetActive(false);
            m4.SetActive(true);
        }
        if (CurweaponIdx == 1)
        {
            anim.SetBool("pick_m4", true);
            m4.SetActive(false);
            cop.SetActive(true);
        }
        Curweapon.gameObject.SetActive(false);
        Curweapon = Weapons[idx];
        StartCoroutine("changeSkin");
        biedIK.solver.leftHandEffector.target = Curweapon.leftPosition.transform;
        CurweaponIdx = idx;
    }
    IEnumerator changeSkin()  //协程方法
    {
        yield return new WaitForSeconds(0.6f);  //暂停协程，0.5秒后执行之后的操作
        Curweapon.gameObject.SetActive(true);
        pickWeapon = false;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(pickWeapon);
        }
        else
        {
            // Network player, receive data
            this.pickWeapon = (bool)stream.ReceiveNext();
        }
    }
}
