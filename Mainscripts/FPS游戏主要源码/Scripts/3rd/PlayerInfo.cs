using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 角色主要信息
/// </summary>
public class PlayerInfo : MonoBehaviour, IPunObservable
{
    public int health = 100;
    public int kills = 0;
    public GameObject cop;
    RagdollUtility ragdollUtility;
    public GameObject blood;
    public ParticleSystem bloodFlash;
    public ShootControl _shootControl;
    public Move _move;
    public croos_hair _croos_Hair;
    public PlayerReset _playerReset;
    public PhotonView photonView;
    public Text kill;
    public bool dealth;

    public GameObject winUI;
    public Text winName;

    public GameObject blood_img;
    void Start()
    {
        kill = GameObject.Find("kill").GetComponent<Text>();
        cop.SetActive(false);
        ragdollUtility = GetComponent<RagdollUtility>();
        photonView = transform.GetComponent<PhotonView>();
    }
    /// <summary>
    /// 远程调用RPC方法（受到伤害） 
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_killerViewID"></param>
    /// <param name="_hitPoint"></param>
    /// <param name="killerName"></param>
    [PunRPC]
    public void GetDamage(int _damage, int _killerViewID, Vector3 _hitPoint, string killerName) {

        blood.transform.position = _hitPoint;

        blood_img.SetActive(true);
        StartCoroutine(changeSkin3());
        bloodFlash.Play();
        if (!dealth) {
            health -= _damage;
            if (health <= 0)
            {
                dealth = true;
                kill.text += killerName + " 击杀了 " + photonView.owner.NickName + "\n";
                StartCoroutine("changeSkin");
                ragdollUtility.EnableRagdoll();
                CharacterController controller = GetComponent<CharacterController>();
                _move.enabled = false;
                controller.enabled = false;
                _shootControl.enabled = false;
                _croos_Hair.enabled = false;
                _playerReset.dead = true;
                PhotonView killserPhotonView = PhotonView.Find(_killerViewID);
                if (killserPhotonView.isMine == false)
                {
                    killserPhotonView.RPC("Addkill", PhotonTargets.All,killerName); 
                }
            }
        }
    }
    /// <summary>
    /// 远程调用 杀敌数增加
    /// </summary>
    /// <param name="killerName"></param>
    [PunRPC]
    public void Addkill(string killerName) 
    {
        kills += 1;
        if (kills == 30) {
            winUI.SetActive(true);
            winName.text = killerName;
            StartCoroutine("changeSkin2");
        }
    }
    IEnumerator changeSkin2()  //协程方法
    {
        yield return new WaitForSeconds(10f);
        PhotonView pv = transform.GetComponent<PhotonView>();
        pv.RPC("Exit", PhotonTargets.AllBuffered);
    }
    /// <summary>
    /// 退出房间
    /// </summary>
    [PunRPC]
    public void Exit()
    {
        PhotonNetwork.LeaveRoom();
    }
    IEnumerator changeSkin()  //协程方法
    {
        yield return new WaitForSeconds(10f);
        kill.text = "";

    }
    IEnumerator changeSkin3()  //协程方法
    {
        yield return new WaitForSeconds(0.3f);

        blood_img.SetActive(false);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            stream.SendNext(this.health);
        }
        else {
            this.health = (int)stream.ReceiveNext();
        }
    }
}
