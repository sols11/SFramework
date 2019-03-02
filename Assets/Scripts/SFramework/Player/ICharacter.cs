using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 游戏中任何活的角色
    /// TODO:包含默认的基本属性，可根据需要修改
    /// </summary>
    public abstract class ICharacter
	{
		protected int maxHP=10;   // 保持一个底线值是为了避免一开始HP就为0
		protected int currentHP=10;
        protected int maxSP = 100;
        protected int currentSP = 100;
        protected bool isDead = false;			// 是否已挂
		//动画状态机
		public Animator animator;
		protected AnimatorStateInfo stateInfo;

		public string Name { get; set; }
		public float MoveSpeed { get; set; }
        /// <summary>
        /// set时将CurrentHP回复满
        /// </summary>
        public int MaxHP { get { return maxHP; } set { maxHP = value < 0 ? 0 : value;
                CurrentHP = MaxHP;
            } }
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
        public float HPpercent { get; protected set; }
        public int MaxSP { get { return maxSP; } set { maxSP = value < 0 ? 0 : value;
                CurrentSP = MaxSP;
            } }
        public virtual int CurrentSP
        {
            get { return currentSP; }
            set
            {
                currentSP = value >= MaxSP ? MaxSP : value;
                currentSP = value < 0 ? 0 : currentSP;
                SPpercent = currentSP * 1.0f / MaxSP;
            }
        }
        public float SPpercent { get; protected set; }
        //设置对应物体
        public GameObject GameObjectInScene { get; set; }
		public bool IsDead { get { return isDead; } }
        public Rigidbody2D Rg2d { get; set; }

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
