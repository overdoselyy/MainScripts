using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 人物移动
/// </summary>
public class Move : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public bool grounded = true;
    Animator anim = null;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded == true)
                {
                    anim.SetBool("jump", true);
                    moveDirection.y = jumpSpeed;
                    grounded = false;
                }
            }
            else {
                anim.SetBool("jump", false);
                grounded = true;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
