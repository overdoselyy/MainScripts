using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 开火控制
/// </summary>
public class Fire : MonoBehaviour,IPunObservable
{
    float lastTime;
    public float fireRate = 0.3f;
    public bool IsFiring = false;
    public WeaponState _weaponState;
    void Update()
    {
        if (Time.time - lastTime > fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                if (_weaponState.isFire)
                {
                    IsFiring = true;
                    if (IsFiring)
                    {
                        onFire();
                        lastTime = Time.time;
                    }
                }
            }
            else
            {
                IsFiring = false;
                _weaponState.a.SetActive(false);
            }
        }
    }
    void onFire()
    {
        if (_weaponState.bulletCurrent > 0)
        {
            --_weaponState.bulletCurrent;
            _weaponState.a.SetActive(true);
            _weaponState.aduioS.PlayOneShot(_weaponState.gunshotSFX);
            _weaponState.muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(_weaponState.shootingPiont.position, _weaponState.shootingPiont.forward, out hit, _weaponState.shootingRange))
            {

                if (hit.transform.tag == "Player")
                {
                    PhotonView pv = hit.transform.GetComponent<PhotonView>();
                    Debug.Log(hit.transform.name + " found");
                    pv.RPC("GetDamage", PhotonTargets.All, _weaponState.damage, _weaponState._playerInfo.photonView.viewID);
                }
            }
        }
        else
        {
            _weaponState.aduioS.PlayOneShot(_weaponState.nobulletSFX);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(IsFiring);
        }
        else
        {
            this.IsFiring = (bool)stream.ReceiveNext();
        }
    }
}
