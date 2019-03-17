using System;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 现在的简单实现：camera拍摄两物体位置的中点，并随距离offsetZ
    /// 距离最近时（2），PosZ为mainCamera当前的数值（-8.3)
    /// 然后随距离增加，PosZ+=0.5*distance
    /// 因为如果offsetY，必然需要offsetRotX，但这样offsetZ的作用就不大了，而且旋转容易让人头晕
    /// 如果哪个场景效果不够，可以考虑加上distanceMinMax限制，和offsetXY
    /// 【构建场景时，让Player与Enemy出生点对称，LevelCamera设置为此时的偏移点,画面中心为原点且能照到两个物体】
    /// 
    /// 我们的屏幕是16:9的比例
    /// 实际绝对能够拍到的范围是以屏幕中心为原点，distance为直径的圆
    /// 当距离过大时，相机切换到另一个角度，以保证能拍到两物体
    /// 最好就是用圆形场景，不要用方的，尤其长方形
    /// 
    /// 在不同的场景可以设置不同的系数
    /// 我们先在场景中摆放好boss和player使其最近，计算出他们的最小distance
    /// 然后修改相机的位置，为他们在这个distance时的最佳观测位置（需保证两者连线中点为原点）
    /// 然后让他们distance最大，再次设置最佳观测位置
    /// 两次相机位置的差值/distance之差 就得到了需要的系数
    /// 当然也可以设置distance小于一定值时，相机offset不变
    /// </summary>
    //[ExecuteInEditMode]
    public class AutoCam : MonoBehaviour
    {
        public bool EnableAreaLimit { get; set; }
        [SerializeField]
        private float m_MoveSpeed = 10;
        //[SerializeField]
        //private float m_RotSpeed = 3;
        [SerializeField]
        private bool m_AutoTarget = true;        // Whether the rig should automatically target the player.
        [SerializeField]
        private bool m_TargetEnemy = true;
        [SerializeField]
        private UpdateType m_UpdateType = UpdateType.FixedUpdate;         // stores the selected update type
        // 追加
        private float xLimit;
        private float x_Limit;
        private float zLimit;
        private float z_Limit;
        private Vector3 clampPos = Vector3.zero;
        public TransparentCam transparentCam;

        private string playerTag = "Player";
        private string enemyTag = "Enemy";
        public Transform playerTransform;
        public Transform enemyTransform;
        public float distance = 0;
        //public float distanceMin = 12;

        // 以下分别是PosY,PosZ，RotX的默认系数
        //public float offsetY = 0.4f;
        public float offsetZ = -0.5f;
        //public float offsetX = 0.9f;

        public enum UpdateType // The available methods of updating are:
        {
            FixedUpdate, // Update in FixedUpdate (for tracking rigidbodies).
            LateUpdate, // Update in LateUpdate. (for tracking objects that are moved in Update)
            ManualUpdate, // user must call to update camera
        }

        private void Start()
        {
            if (m_AutoTarget)
            {
                FindAndTargetPlayer();
            }
            if (m_TargetEnemy)
            {
                FindAndTargetEnemy();
            }
            if (playerTransform == null) return;
        }

        private void FindAndTargetPlayer()
        {
            GameObject targetObj = GameObject.FindGameObjectWithTag(playerTag);
            if (targetObj)
            {
                playerTransform = targetObj.transform;
                if (transparentCam != null)
                    transparentCam.targetObject = playerTransform;
            }
        }

        private void FindAndTargetEnemy()
        {
            GameObject targetObj = GameObject.FindGameObjectWithTag(enemyTag);
            if (targetObj)
            {
                enemyTransform = targetObj.transform;
            }
        }

        /// <summary>
        /// 限制Camera移动范围
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x_"></param>
        /// <param name="z"></param>
        /// <param name="z_"></param>
        public void SetAreaLimit(float x, float x_, float z, float z_)
        {
            EnableAreaLimit = true;
            xLimit = x;
            x_Limit = x_;
            zLimit = z;
            z_Limit = z_;
        }

        private void FollowTarget(float deltaTime)
        {
            // 退出判断
            if (deltaTime <= 0 || playerTransform == null)
                return;

            if(m_TargetEnemy&& enemyTransform!=null && playerTransform.gameObject.activeSelf)
            {
                distance = Vector3.Distance(playerTransform.position, enemyTransform.position);
                // 通过系数计算offset
                Vector3 aimPos = (playerTransform.position + enemyTransform.position) / 2 + new Vector3(0, 0, offsetZ * distance);
                //Quaternion q = Quaternion.identity;
                //if (distance > distanceMin)
                //{
                //    aimPos += new Vector3(0, 1.6f, -4);
                //    q= Quaternion.Euler(2.5f, 0, 0);
                //}
                transform.position = Vector3.Lerp(transform.position, aimPos , deltaTime * m_MoveSpeed);
                // 通过系数计算RotX
                //transform.rotation = Quaternion.Slerp(transform.rotation, q, deltaTime * m_RotSpeed);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, deltaTime * m_MoveSpeed);
            }
            if (!EnableAreaLimit)
                return;
            else
            {
                // 限制相机移动范围
                clampPos = new Vector3(Mathf.Clamp(playerTransform.position.x, x_Limit, xLimit), playerTransform.position.y,
                    Mathf.Clamp(playerTransform.position.z, z_Limit, zLimit));
                transform.position = Vector3.Lerp(transform.position, clampPos, deltaTime * m_MoveSpeed);
            }
        }

        private void LaterUpdate()
        {
            if (m_AutoTarget && (playerTransform == null || !playerTransform.gameObject.activeSelf))
                FindAndTargetPlayer();
            if (m_TargetEnemy)
                FindAndTargetEnemy();
            if (m_UpdateType == UpdateType.LateUpdate)
                FollowTarget(Time.deltaTime);
        }

        private void ManualUpdate()
        {
            if (m_AutoTarget && (playerTransform == null || !playerTransform.gameObject.activeSelf))
            {
                FindAndTargetPlayer();
            }
            if (m_TargetEnemy)
                FindAndTargetEnemy();
            if (m_UpdateType == UpdateType.ManualUpdate)
            {
                FollowTarget(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (m_AutoTarget && (playerTransform == null || !playerTransform.gameObject.activeSelf))
            {
                FindAndTargetPlayer();
            }
            if (m_TargetEnemy && (enemyTransform == null || !enemyTransform.gameObject.activeSelf))
            {
                FindAndTargetEnemy();
            }
            if (m_UpdateType == UpdateType.FixedUpdate)
            {
                FollowTarget(Time.deltaTime);
            }
        }
    }
}
