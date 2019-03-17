/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 传参给Player受伤的属性类
    /// </summary>
    public class PlayerHurtAttr
    {
        public int Attack { get; set; }
        public float VelocityForward { get; set; }
        public float VelocityVertical { get; set; }
        public Vector3 TransformForward { get; set; }
        public bool CanDefeatedFly { get; set; }

        public PlayerHurtAttr()
        { }

        public void ModifyAttr(int attack, float velocityForward, float velocityVertical, Vector3 transformForward,
            bool canDefeatedFly=false)
        {
            Attack = attack;
            VelocityForward = velocityForward;
            VelocityVertical = velocityVertical;
            TransformForward = transformForward;
            CanDefeatedFly = canDefeatedFly;
        }
    }
}