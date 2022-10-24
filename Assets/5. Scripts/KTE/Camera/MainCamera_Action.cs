using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Action : MonoBehaviour
{
    #region Variable
    [SerializeField]
    public GameObject Target;               // 카메라가 따라다닐 타겟
    [SerializeField]
    public Transform cameraArm;             // 카메라의 좌표값

    public float xSpeed = 0.0f;             // 카메라 x축 스피드
    public float ySpeed = 0.0f;             // 카메라 y축 스피드
    public float yMinLimit = 0.0f;          // 카메라 y축(상하) 높이제한
    public float yMaxLimit = 0.0f;          // 카메라 y축(상하) 높이제한
    public float offsetX = 0.0f;            // 카메라의 x좌표
    public float offsetY = 3.0f;            // 카메라의 y좌표
    public float offsetZ = -3.0f;           // 카메라의 z좌표
    public float Maxoffset = -3.5f;
    public float Minoffset = -0.5f;

    // 카메라 회전축 및 좌표를 위한 변수
    private float x = 0.0f;                
    private float y = 0.0f;
    #endregion Variable

    #region Unity Method
    private void LateUpdate()
    {
        LookCamera();
    }
    #endregion Unity Method

    #region Method
    // 카메라 좌표 설정
    private void LookCamera()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        offsetZ += Input.GetAxis("Mouse ScrollWheel") * 1.5f;
        if (offsetZ <= Maxoffset)
        {
            offsetZ = Maxoffset;
        }
        else if (offsetZ >= Minoffset)
        {
            offsetZ = Minoffset;
        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0, 0.0f, offsetZ) + Target.transform.position + new Vector3(0.0f, offsetY, 0.0f);

        transform.rotation = rotation;
        transform.position = position;

    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    #endregion Method
}
