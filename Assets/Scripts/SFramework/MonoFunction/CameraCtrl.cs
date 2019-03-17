/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：相机控制系统
    作用：
    使用：
    补充：TODO：如果需要一些特殊效果，可以添加DoTween之类的插件
History:
----------------------------------------------------------------------------*/

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
        private ShakeObject shakeComponent;
        private AutoCam autoCam;

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
                shakeComponent = mainCamera.GetComponent<ShakeObject>();
                autoCam = mainCamera.GetComponentInParent<AutoCam>();
            }
        }

        public void ShakeMainCamera(Vector3 _directionStregth, float _startTime = 0, float _Speed = 1)
        {
            if (shakeComponent != null)
            {
                shakeComponent.directionStrength = _directionStregth;
                shakeComponent.startTime = _startTime;
                shakeComponent.speed = _Speed;
                shakeComponent.enabled = true;
            }
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
            autoCam.SetAreaLimit(x, x_, z, z_);
        }

        /// <summary>
        /// 设置是否启用AutoCam
        /// </summary>
        /// <param name="enable"></param>
        public void EnableAutoCam(bool enable)
        {
            autoCam.enabled = enable;
        }
    }
}