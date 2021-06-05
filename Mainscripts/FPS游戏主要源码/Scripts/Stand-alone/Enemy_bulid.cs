using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人生成
/// </summary>
public class Enemy_bulid : MonoBehaviour
{
    public GameObject EnemyPrefeb;
    public GameObject Portal;
    /*void Start()
    {
        InvokeRepeating("timer", 0f, 1.0f);
    }*/


    //每一产生一个的计时器
    float timer = 0;
    //每一波产生的计时器
    float timer2 = 0;
    //产生敌人的速率
    public float rate;
    //敌人的预制体
    public GameObject enemeyPrefab;
    //每一波的时间
    public float timesofEachWave = 5;
    //每一波的已经产生的数量
    private int count;
    void Update()
    {
        timer2 += Time.deltaTime;
        //每隔一定时间产生一波敌人
        if (timer2 < timesofEachWave && count != 3)
        {

            timer += Time.deltaTime;
            //每隔一定的时间产生一个波敌人
            if (timer > rate)
            {
                Portal.SetActive(false);
                StartCoroutine(changeSkin());
                Quaternion Rotation = Quaternion.Euler(0f, 4.89f, 0f);
                Instantiate(EnemyPrefeb, new Vector3(0.05f, -0.8533221f, -25.48f), Rotation);
                count++;
                timer -= rate;
            }

        }
        //如果每一波产生的时间大于timesofEachWave秒 
        if (timer2 > timesofEachWave)
        {

            timer2 -= timesofEachWave;
            //当前的数量清零
            count = 0;
        }
    }

   /*void timer()
    {
       Debug.Log(string.Format("计时器正在计时！time={0}s", Time.time));
        if (Time.time == 10) 
        {
            Portal.SetActive(false);
            StartCoroutine(changeSkin());
            Quaternion Rotation = Quaternion.Euler(0f, 4.89f, 0f);
            Instantiate(EnemyPrefeb, new Vector3(5.76f, -5.55f, -21.62f),Rotation);
        }
    }*/
    IEnumerator changeSkin()  //协程方法
    {
        yield return new WaitForSeconds(1f);  //暂停协程，0.5秒后执行之后的操作
        Portal.SetActive(true);
    }
}

