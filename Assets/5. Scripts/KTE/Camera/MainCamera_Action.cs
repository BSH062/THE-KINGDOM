using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Action : MonoBehaviour
{
    #region Variable
    [SerializeField]
    public GameObject Target;               // ī�޶� ����ٴ� Ÿ��
    [SerializeField]
    public Transform cameraArm;             // ī�޶��� ��ǥ��

    public float xSpeed = 0.0f;             // ī�޶� x�� ���ǵ�
    public float ySpeed = 0.0f;             // ī�޶� y�� ���ǵ�
    public float yMinLimit = 0.0f;          // ī�޶� y��(����) ��������
    public float yMaxLimit = 0.0f;          // ī�޶� y��(����) ��������
    public float offsetX = 0.0f;            // ī�޶��� x��ǥ
    public float offsetY = 3.0f;            // ī�޶��� y��ǥ
    public float offsetZ = -3.0f;           // ī�޶��� z��ǥ
    public float Maxoffset = -3.5f;
    public float Minoffset = -0.5f;

    // ī�޶� ȸ���� �� ��ǥ�� ���� ����
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
    // ī�޶� ��ǥ ����
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
