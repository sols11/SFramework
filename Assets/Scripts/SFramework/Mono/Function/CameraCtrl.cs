using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework {
    /// <summary>
    /// Camera控制器，建议直接挂在MainCamera上
    /// </summary>
    public class CameraCtrl : MonoBehaviour {
        private static CameraCtrl _instance;
        private Camera mainCamera;
        private Y_shake shakeComponent;

        public static CameraCtrl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraCtrl>();

                    if (_instance == null)
                        _instance = new GameObject("CameraCtrl").AddComponent<CameraCtrl>();
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
                _instance = GetComponent<CameraCtrl>();
            else if (_instance != GetComponent<CameraCtrl>())
            {
                Debug.LogWarningFormat("There is more than one {0} in the scene，auto inactive the copy one.", typeof(CameraCtrl).ToString());
                gameObject.SetActive(false);
                return;
            }
            mainCamera = Camera.main;
            if (mainCamera)
            {
                shakeComponent = mainCamera.GetComponent<Y_shake>();
            }
        }

        public void ShakeMainCamera(Vector3 _directionStregth,float _startTime=0,float _Speed=1)
        {
            shakeComponent.directionStrength = _directionStregth;
            shakeComponent.startTime = _startTime;
            shakeComponent.Speed = _Speed;
            shakeComponent.enabled = true;
        }

        /// <summary>
        /// 边界控制，限制AutoCam的移动范围
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x_"></param>
        /// <param name="z"></param>
        /// <param name="z_"></param>
        public void SetAreaLimit(float x, float x_, float z, float z_)
        {
        }

    }
}