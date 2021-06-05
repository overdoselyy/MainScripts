using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float returnX;//返回限制X （当小于x 返回原位置）
    public Vector3 curPos;//原位置
    // Start is called before the first frame update
    void Start()
    {
        curPos = transform.position;
    }


    void Update()
    {
        //游戏结束 不运动
        if (!GameManager.instance.IsGameOver) {

            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= returnX)
            {

                transform.position = curPos;

            }

        }

    }
}
