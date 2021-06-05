using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]//编辑扩展 当本组件的游戏物体没有对应组件时自动添加
public class Bird : MonoBehaviour
{
    public float upPower = 5;//向上力
    Rigidbody2D rigidbody2D;
    Vector2 reStartPos;
    private void Awake()
    {
        //gameObject.AddComponent<Rigidbody2D>();//添加刚体
        rigidbody2D = GetComponent<Rigidbody2D>();
        //代码锁定刚体的旋转
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        reStartPos = transform.position;
    }
    void Update()
    {
        if (!GameManager.instance.IsGameOver)
        {
            if (Input.GetButtonDown("Fire1")) //鼠标左键 移动端 触摸屏幕
            {
                rigidbody2D.velocity = Vector2.up * upPower;
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "AddScore") {

            GameManager.instance.DataProxy.ScoreUpdate(10);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Edge") {
            return;
        
        }
        rigidbody2D.isKinematic = true;//设置为动力学刚体  不受外力影响
        rigidbody2D.velocity = Vector3.zero;
        GameManager.instance.GameOver();//停止定时器 游戏结束
    }


    //重置游戏

    public void ReStart() {

        rigidbody2D.isKinematic = false;
        //重置位置
        transform.position = reStartPos;
    }
}
