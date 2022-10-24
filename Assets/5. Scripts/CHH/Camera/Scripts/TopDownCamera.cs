using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Cameras
{
    public class TopDownCamera : MonoBehaviour
    {
        # region Varibles

        public float height = 4f;
        public float distance = 7f;
        public float angle = 45f;
        public float lookAtHeight = 2f;
        public float smoothSpeed = 0.5f;

        private Vector3 refVelocity;

        public Transform target;

        #endregion Varibles

        #region UnityMethods

        // ī�޶� �ڿ������� �̵��ϵ��� ���
        private void LateUpdate()
        {
            HandleCamera();
        }

        #endregion UnityMethods

        public void HandleCamera()
        {
            // Ÿ���� ������ ����
            if (!target)
            {
                return;
            }

            // ī�޶� ���� ��ǥ
            Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
            //Debug.DrawLine(target.position, worldPosition, Color.red);

            // ī�޶� ȸ����
            Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;
            //Debug.DrawLine(target.position, rotatedVector, Color.green);

            // ī�޶� ��ġ �缳��
            Vector3 finalTargetPosition = target.position;
            finalTargetPosition.y += lookAtHeight;

            Vector3 finalPosition = finalTargetPosition + rotatedVector;
            //Debug.DrawLine(target.position, finalPosition, Color.blue);

            // ī�޶� �̵�
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);

            // ī�޶� Ÿ���� �ٶ󺸵��� ����
            transform.LookAt(target.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            if (target)
            {
                Vector3 lookAtPosition = target.position;
                lookAtPosition.y += lookAtHeight;
                Gizmos.DrawLine(transform.position, lookAtPosition);
                Gizmos.DrawSphere(lookAtPosition, 0.25f);
            }
            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }
}
