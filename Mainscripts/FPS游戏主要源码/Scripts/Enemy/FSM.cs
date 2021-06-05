using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using RootMotion.FinalIK;
/// <summary>
/// 敌人AI
/// 有限状态机
/// FSM
/// </summary>
public class FSM : MonoBehaviour
{
    //枚举敌人状态
    public enum FSMstate 
    { 
        None,
        Patrol,
        Chase,
        Attack,
    }
    public FSMstate curState;
    public NavMeshAgent agent;
    public Transform player;
    public Transform patrol_point;
    Animator anim = null;
    public Transform shootPoint;
    public float shootingRange = 100f;
    public AudioSource aduioS;
    public AudioClip gunshotSFX;
    public ParticleSystem muzzleFlash;
    float lastTime;
    public float fireRate = 10.0f;

    public GameObject muzzle;


    public GameObject blood;
    public ParticleSystem bloodFlash;
    public AimIK aimIK;
    public Transform targetPos;

    public Hp _hp;
    /// <summary>
    /// 初始化信息
    /// </summary>
    void Start()
    {
        curState = FSMstate.Patrol;
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        aimIK = GetComponent<AimIK>();//获取AimIk组件
        player = GameObject.Find("Player").GetComponent<Transform>();
        targetPos = GameObject.Find("Enemy_point").GetComponent<Transform>();
        aduioS = GameObject.Find("Player").GetComponent<AudioSource>();
        _hp = GameObject.Find("Player").GetComponent<Hp>();
    }

    void Update()
    {
        //状态之间的转换
        switch (curState)
        {
            case FSMstate.Patrol:
                StatePatrol();
                break;
            case FSMstate.Chase:
                StateChase();
                break;
            case FSMstate.Attack:
                StateAttack();
                break;
        }
    }
    /// <summary>
    /// 巡逻状态
    /// </summary>
    void StatePatrol() {
        Debug.Log("now Patrol");
        curState = FSMstate.Chase;
    }
    /// <summary>
    /// 追击状态
    /// </summary>
    void StateChase()
    {
        Debug.Log("now Chase");
        agent.SetDestination(player.position);
        aimIK.solver.target.position = targetPos.transform.position;
        anim.SetBool("move", true);
        if (Vector3.Distance(this.transform.position, player.position) <= 5) 
        {
            curState = FSMstate.Attack;
        }
    }
    /// <summary>
    /// 攻击状态
    /// </summary>
    void StateAttack()
    {
        Debug.Log("now Attack");
        // anim.SetBool("move", false);
        Vector3 v = targetPos.transform.position;
        aimIK.solver.target.position = v;

        if (Time.time - lastTime > fireRate)
        {
            RaycastHit hit;
            muzzle.SetActive(true);
            aduioS.PlayOneShot(gunshotSFX);
            muzzleFlash.Play();

            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, shootingRange))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);
                if (hit.transform.tag == "Player")
                {
                    blood.transform.position = hit.point;
                    bloodFlash.Play();
                    _hp.hp -= 10;
                    _hp.loseHp = true;
                }
            }
            else
            {

                Debug.DrawLine(transform.position, hit.point, Color.red);
            }
            lastTime = Time.time;
        }
        else {
            muzzle.SetActive(false);
        }
        if (Vector3.Distance(this.transform.position, player.position) > 5) 
        {
            curState = FSMstate.Chase;
        }
    }



}
