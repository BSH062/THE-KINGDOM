using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    #region Variables

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float dashDistance = 5f;
    [SerializeField]
    private float groundCheckDistance = 0.2f;
    [SerializeField]
    private LayerMask groundLayerMask;
    private bool isGrounded = true;

    public Animator anim;
    private Rigidbody rigidbody;
    private Vector3 inputDirection = Vector3.zero;
    private Vector3 startPos;

    
    public GameObject settingUI;        // 해상도 UI

    #endregion Variables

    #region Unity Method

    void Start()
    {
        startPos = transform.position;
        rigidbody = GetComponent<Rigidbody>();

        if (rigidbody == null) Debug.Log("Rigidbody NULL!");

        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        CheckGroundStatus();

        Move();

        Jump();

        Dash();

        SettingToggle();

    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + inputDirection * speed * Time.fixedDeltaTime);
    }

    #endregion Unity Method


    #region Method

    private void SettingToggle()
    {
        if (Input.GetButtonDown("Tab"))
        {
            if (settingUI.activeSelf)
            {
                settingUI.SetActive(false);
            }
            else
            {
                settingUI.SetActive(true);
            }
        }
    }

    private void Move()
    {
        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        // 이동중이면 입력된 방향으로 설정
        if (inputDirection != Vector3.zero)
        {
            transform.forward = inputDirection;
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    private void Jump()
    {
        // 땅에 있을 때만 플레이어 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump Click");
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }
    }

    private void Dash()
    {
        // 플레이어 데쉬
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash Click");
            // 지금 현제 rigidbody에 설정된 저항값을 Log함수화 시켜서 점차적으로 느려지게 만듬
            Vector3 dashVelocity = Vector3.Scale(transform.forward,
                dashDistance * new Vector3(
                    (Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime),
                    0,
                    (Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime)
                    )
                );
            rigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }

    // 땅에 있는지 체크
    void CheckGroundStatus()
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
