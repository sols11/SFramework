/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：游戏角色的基类，角色包括Player, Enemy, NPC等
    作用：提供一个角色模板，具备基本的属性和逻辑
    使用：继承
    补充：TODO:包含默认的基本属性，可根据需要修改。如果是2D游戏，Rigidbody需要改为Rigidbody2D
History:
    2019/03/12 补充了注释说明，修改了属性set值时的逻辑，增强了需求的扩展性
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 游戏中任何活的角色
    /// </summary>
    public abstract class ICharacter
	{
        // 字段
        public Animator animator;               // 动画状态机
        protected AnimatorStateInfo stateInfo;
        protected int maxHP = 10;               // 保持一个底线值是为了避免一开始HP就为0
        protected int currentHP = 10;
        protected int maxSP = 100;
        protected int currentSP = 100;
        protected bool isDead = false;          // 是否死亡
        // 属性
        public GameObject GameObjectInScene { get; set; }   // 设置对应物体
        public bool IsDead { get { return isDead; } }
        public Rigidbody Rgbd { get; set; }     // 2D游戏可修改为Rigidbody2D
        public string Name { get; set; }
		public float MoveSpeed { get; set; }
		public float RotSpeed { get; set; }
        public int Rank { get; set; }           // 等级
        public float HPpercent { get; protected set; }
        public float SPpercent { get; protected set; }

        /// <summary>
        /// 注意，如果需要设置初始的Max HP，请设置CurrentHP = MaxHP，以将其回复满
        /// </summary>
        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value < 0 ? 0 : value; }
        }
        /// <summary>
        ///  当CurrentHP《=0时，会自动调用Dead方法
        /// </summary>
		public virtual int CurrentHP
        {
            get { return currentHP; }
            set
            {
                currentHP = value >= MaxHP ? MaxHP : value;
                if (currentHP <= 0)
                {
                    currentHP = 0;
                    Dead();
                }
                HPpercent = currentHP * 1.0f / MaxHP;
            }
        }
        public int MaxSP
        {
            get { return maxSP; }
            set { maxSP = value < 0 ? 0 : value; }
        }
        public virtual int CurrentSP
        {
            get { return currentSP; }
            set
            {
                currentSP = value >= MaxSP ? MaxSP : value;
                currentSP = value < 0 ? 0 : value;
                SPpercent = currentSP * 1.0f / MaxSP;
            }
        }
        
        //构造函数
        public ICharacter(GameObject gameObject)
        {
            GameObjectInScene = gameObject;
        }

        public virtual void Initialize() { }
		public virtual void Release() { }
		public virtual void Update() { }
		public virtual void FixedUpdate() { }

        // 方法
        public virtual void Dead()
		{
			if (isDead)
				return;
			isDead = true;
		}
	}
}
