using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace My.Cameras
{
    [CustomEditor(typeof(TopDownCamera))]
    public class TopDownCameraEditor : Editor
    {
        #region Variables
        private TopDownCamera targetCamera;
        #endregion Variables

        #region Unity Methods

        // Ÿ�� �޾ƿ���
        public override void OnInspectorGUI()
        {
            targetCamera = (TopDownCamera)target;
            base.OnInspectorGUI();
        }

        // ������ ��� Ȯ���ϱ� ���ؼ� �޾ƿ� ī�޶� ���� ����
        private void OnSceneGUI()
        {
            // Ÿ�� ���翩�� Ȯ��
            if (!targetCamera || !targetCamera.target)
            {
                return;
            }

            // Ÿ�� ���� ��ġ �޾ƿ�
            Transform camTarget = targetCamera.target;
            Vector3 targetPosition = camTarget.position;
            targetPosition.y += targetCamera.lookAtHeight;

            // �Ÿ� Ȯ�ο� �� �׸���
            Handles.color = new Color(1f, 0f, 0f, 0.15f);
            Handles.DrawSolidDisc(targetPosition, Vector3.up, targetCamera.distance);

            Handles.color = new Color(0f, 1f, 0f, 0.75f);
            Handles.DrawWireDisc(targetPosition, Vector3.up, targetCamera.distance);

            // Distance ���� �������Ϳ��� ������ ������ �� �ֵ��� �����̴� �߰�
            // 0.1������ Ȯ��
            Handles.color = new Color(1f, 0f, 0f, 0.5f);
            targetCamera.distance = Handles.ScaleSlider(targetCamera.distance, targetPosition, -camTarget.forward, Quaternion.identity, targetCamera.distance, 0.1f);
            targetCamera.distance = Mathf.Clamp(targetCamera.distance, 2f, float.MaxValue);

            Handles.color = new Color(0f, 0f, 1f, 0.5f);
            targetCamera.height = Handles.ScaleSlider(targetCamera.height, targetPosition, Vector3.up, Quaternion.identity, targetCamera.height, 0.1f);
            targetCamera.height = Mathf.Clamp(targetCamera.height, 2f, float.MaxValue);

            // ���̺� �ޱ�
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 15;
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperCenter;

            Handles.Label(targetPosition + (-camTarget.forward * targetCamera.distance), "Distance", labelStyle);

            labelStyle.alignment = TextAnchor.MiddleRight;
            Handles.Label(targetPosition + (Vector3.up * targetCamera.height), "Height", labelStyle);

            targetCamera.HandleCamera();
        }

        #endregion Unity Methods
    }
}
