using UnityEngine;
/// <summary>
/// 武器基本信息
/// </summary>
public class WeaponState : MonoBehaviour, IPunObservable
{ 
    public int bulletTotal = 100;
    public int bulletPerMag = 30;
    public int bulletTotal2;
    public int bulletPerMag2;

    public int bulletCurrent;
    public int damage = 50;
    public float bulletSpeed = 12;

    float lastTime;
    public float fireRate = 0.1f;
    public bool IsFiring = false;



    public Transform shootingPiont;
    public float shootingRange = 100f;

    public GameObject leftPosition;

    public AudioSource aduioS;
    public AudioClip gunshotSFX;
    public AudioClip reloadSFX;
    public AudioClip nobulletSFX;

    public GameObject a;
    public ParticleSystem muzzleFlash;

    public PickWeapon _pickWeapon;
    public PlayerInfo _playerInfo;
    public croos_hair _croos_Hair;
    public PlayerReset _playerReset;

    public bool isReload=false;
    public bool isFire = true;
    public bool hitPlayer = false;


    public GameObject plane;
    public ParticleSystem M4PlaneFlash;
    public ParticleSystem CopPlaneFlash;
    void Start()
    {
        bulletTotal2 = bulletTotal;
        bulletPerMag2 = bulletPerMag;
        bulletCurrent = bulletPerMag;

    }
    void Update()
    {
        if (_playerReset.dead == false)
        {

            if (Time.time - lastTime > fireRate)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (isFire)
                    {
                        IsFiring = true;
                        if (IsFiring)
                        {
                            PhotonView pv = transform.GetComponent<PhotonView>();
                            pv.RPC("Fire", PhotonTargets.All);

                            lastTime = Time.time;
                        }

                    }
                }
                else
                {
                    //命中敌人准星变化
                    _croos_Hair.lineStyle.normal.background = _croos_Hair.crosshairTexture;
                    IsFiring = false;
                    a.SetActive(false);
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                PhotonView pv = transform.GetComponent<PhotonView>();
                pv.RPC("Reload", PhotonTargets.All);
                lastTime = Time.time;
            }

        }
    }
    /// <summary>
    /// 远程调用 换弹
    /// </summary>
    [PunRPC]
    void Reload() {
        if (bulletTotal > 0)
        {
            int bulletNeed = bulletPerMag - bulletCurrent;
            if (bulletNeed > 0)
            {
                isReload = true;
                isFire = false;
                _pickWeapon.isPickWeapon = false;
                aduioS.PlayOneShot(reloadSFX);
                if (bulletNeed > bulletTotal)
                {
                    bulletCurrent += bulletTotal;
                    bulletTotal = 0;
                }
                else
                {
                    bulletCurrent += bulletNeed;
                    bulletTotal -= bulletNeed;
                }
            }

        }
    }
    /// <summary>
    /// 远程调用 开火
    /// </summary>
    [PunRPC]
    void Fire(){
        if (bulletCurrent > 0)
        {
            --bulletCurrent;
            a.SetActive(true);
            aduioS.PlayOneShot(gunshotSFX);
            muzzleFlash.Play();
            RaycastHit hit;//射线检测 如果命中玩家 远程调用 GetDamage方法
            if (Physics.Raycast(shootingPiont.position, shootingPiont.forward, out hit,shootingRange)) 
            {
                Debug.DrawLine(transform.position, hit.point, Color.yellow);
                if (hit.transform.tag == "Player")
                {
                    PhotonView pv = hit.transform.GetComponent<PhotonView>();
                    Debug.Log(hit.transform.name + " found");
                    pv.RPC("GetDamage", PhotonTargets.All, damage, _playerInfo.photonView.viewID,hit.point,PhotonNetwork.player.NickName);
                    _croos_Hair.lineStyle.normal.background = _croos_Hair.crosshairTexture2;
                }
                else {
                    _croos_Hair.lineStyle.normal.background = _croos_Hair.crosshairTexture;
                }
            }
        }
        else {
            aduioS.PlayOneShot(nobulletSFX);
        }
      

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(IsFiring);
        }
        else
        {
            // Network player, receive data
            this.IsFiring = (bool)stream.ReceiveNext();   
        }
    }
}
