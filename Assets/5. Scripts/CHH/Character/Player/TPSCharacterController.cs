using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class TPSCharacterController : MonoBehaviour
{
    #region Variable

    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform cameraArm;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayerMask;
    private bool isGrounded = true;

    public int level = 1;
    public int hp = 100;
    public bool isFaster;
    public bool isShild;
    public bool isDie;
    public bool isVictory;

    private float shildTime;
    private float fasterTime;

    public Animator animator;
    private Rigidbody rigid;
    private CharacterController characterController;
    private Vector3 originalCameraArmPos;
    Vector3 jumpVelocity;

    #endregion Variable

    #region Unity Method

    private void Start()
    {
        Time.timeScale = 1f;
        animator = characterBody.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalCameraArmPos = cameraArm.position - characterBody.position;

        hp = 100;
        isFaster = false;
        isShild = false;
        isDie = false;
        isVictory = false;
    }

    private void Update()
    {
        CheckGroundStatus();
        Movement();
        Jump();
        LookAround();

        //if (isShild) OnShild();
        //else OffShild();

        // if (isFaster) OnFaster();
        //else OffFaster();
    }

    private void LateUpdate()
    {
        cameraArm.position = characterBody.transform.position + originalCameraArmPos;
    }

    #endregion Unity Method

    #region Method

    private void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        
        animator.SetBool("isRun", isMove);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            //characterBody.forward = moveDir;
            //transform.position += moveDir * speed * Time.deltaTime;
            var velocity = moveDir * speed;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));   // 마우스 상하좌우 수치값
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float rotX = camAngle.x - mouseDelta.y;
        float rotY = camAngle.y - mouseDelta.x;

        if(rotX < 180)
        {
            rotX = Mathf.Clamp(rotX, -1f, 70f);
        }
        else
        {
            rotX = Mathf.Clamp(rotX, 335f, 361f);
        }

        if(rotY < 180)
        {
            rotY = Mathf.Clamp(rotY, -1f, 70f);
            cameraArm.rotation = Quaternion.Euler(rotY, camAngle.x + mouseDelta.y, camAngle.z);
        }

        cameraArm.rotation = Quaternion.Euler(rotX, camAngle.y + mouseDelta.x, camAngle.z);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            //Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            characterController.Move(jumpVelocity);
            animator.SetTrigger("isJump");
        }
    }

    // 땅에 있는지 체크
    public void CheckGroundStatus()
    {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance, groundLayerMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            //Debug.Log("Grounded False");
        }
    }
    #endregion Method
}
