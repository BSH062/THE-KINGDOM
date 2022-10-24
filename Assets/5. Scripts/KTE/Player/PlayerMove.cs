using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Variable

    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float groundCheckDistance = 0.3f;
    [SerializeField]
    private LayerMask groundLayerMask;

    public MainCamera_Action camera;
    private Animator animator;
    private Transform cameraTrans;


    public bool isGrounded = true;
    private Rigidbody rigidbody;

    #endregion Variable

    #region Unity Method
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null) Debug.Log("Rigidbody NULL!");
        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().Play();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        CheckGroundStatus();

        Jump();
    }
    #endregion Unity Method

    #region Method

    // 움직임을 구현하기 위한 메서드 애니메이션처리도 동시에 합니다
    private void Move()
    {
        float walkspeed = speed;
        cameraTrans = camera.cameraArm;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        if (Input.GetKey(KeyCode.LeftShift) && isMove)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isWalk", false);

            walkspeed = speed * 2f;
        }
        else if (isMove == false)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);

        }
        else
        {
            animator.SetBool("isWalk", true);
            animator.SetBool("isRun", false);

            walkspeed = speed;
        }
        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraTrans.forward.x, 0f, cameraTrans.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraTrans.right.x, 0f, cameraTrans.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            transform.forward = moveDir;
            //transform.position += moveDir * Time.deltaTime * walkspeed;
            rigidbody.MovePosition(transform.position + moveDir * Time.deltaTime * walkspeed);
        }
        Debug.DrawRay(cameraTrans.position, new Vector3(cameraTrans.forward.x, 0f, cameraTrans.forward.z), Color.red);
    }

    private void Jump()
    {
        // 땅에 있을 때만 플레이어 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Debug.Log("Jump Click");
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            animator.SetTrigger("isJump");
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawRay(transform.position + (Vector3.up * 0.1f), Vector3.down * groundCheckDistance, Color.red);
#endif

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance, groundLayerMask))
        {
            isGrounded = true;
            animator.SetBool("isGround", true); 
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGround", false);
        }
    }
    #endregion Method
}
