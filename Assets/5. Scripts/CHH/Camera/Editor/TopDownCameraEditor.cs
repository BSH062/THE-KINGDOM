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

        // 타겟 받아오기
        public override void OnInspectorGUI()
        {
            targetCamera = (TopDownCamera)target;
            base.OnInspectorGUI();
        }

        // 에디터 기능 확장하기 위해서 받아온 카메라 로직 구현
        private void OnSceneGUI()
        {
            // 타겟 존재여부 확인
            if (!targetCamera || !targetCamera.target)
            {
                return;
            }

            // 타겟 대상과 위치 받아옴
            Transform camTarget = targetCamera.target;
            Vector3 targetPosition = camTarget.position;
            targetPosition.y += targetCamera.lookAtHeight;

            // 거리 확인용 원 그리기
            Handles.color = new Color(1f, 0f, 0f, 0.15f);
            Handles.DrawSolidDisc(targetPosition, Vector3.up, targetCamera.distance);

            Handles.color = new Color(0f, 1f, 0f, 0.75f);
            Handles.DrawWireDisc(targetPosition, Vector3.up, targetCamera.distance);

            // Distance 값을 씬에디터에서 설정을 수정할 수 있도록 슬라이더 추가
            // 0.1단위로 확장
            Handles.color = new Color(1f, 0f, 0f, 0.5f);
            targetCamera.distance = Handles.ScaleSlider(targetCamera.distance, targetPosition, -camTarget.forward, Quaternion.identity, targetCamera.distance, 0.1f);
            targetCamera.distance = Mathf.Clamp(targetCamera.distance, 2f, float.MaxValue);

            Handles.color = new Color(0f, 0f, 1f, 0.5f);
            targetCamera.height = Handles.ScaleSlider(targetCamera.height, targetPosition, Vector3.up, Quaternion.identity, targetCamera.height, 0.1f);
            targetCamera.height = Mathf.Clamp(targetCamera.height, 2f, float.MaxValue);

            // 레이블 달기
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
