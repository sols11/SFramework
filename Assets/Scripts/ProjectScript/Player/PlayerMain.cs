/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2019/03/12
Description:
    简介：主玩家控制器，即默认的PlayerController
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework;

namespace ProjectScript
{
    public class PlayerMain : IPlayer
    {
        /// <summary>
        /// 创建时的初始化
        /// </summary>
        /// <param name="gameObject"></param>
        public PlayerMain(GameObject gameObject) : base(gameObject)
        {
            // 以下是其他属性
            CanMove = true;
            CanRotate = true;
        }

        /// <summary>
        /// 每次切换场景时执行
        /// </summary>
        public override void Initialize()
        {
        }

        public override void Release()
        {
            base.Release();
        }

        public override void FixedUpdate()
        {
            // 当Avoid转向其他状态时也可以移动
            //if (CanMove)
            //    if (stateInfo.IsName("Idle") || stateInfo.IsName("Run") || stateInfo.IsName("AtoR"))
            //        GroundMove(h, v);
        }

        public override void Update()
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
    }
}
