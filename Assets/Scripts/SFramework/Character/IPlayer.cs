/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：玩家角色的基类，玩家所拥有的各种存储信息
    作用：
    使用：
    补充：
History:
    2019/03/12 添加了PlayerMediator等属性
    TODO：专门建立一个数据类PlayerData来存放数据
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// Player的所有属性
    /// </summary>
	public class IPlayer : ICharacter
	{
		protected bool canMove = false;   // CanMovePosition
		protected float h;  // 水平移动
		protected float v;  // 垂直移动
        protected GameData gameData;

		public float Speed { get; set; }
        public int Gold { get; set; }
        public bool CanAttack { get; set; }
        // 装备和道具
        public IEquip[] Fit { get; set; }
        public int[] PropNum { get; set; }
        // 任务数据
        public List<TaskData> TasksData { get; set; }

        // Player的HP,SP需要更新UI，即set时调用UpdateHP_SP()
        public override int CurrentHP
        {
            get { return base.CurrentHP; }
            set
            {
                base.CurrentHP = value;
                UpdateHP_SP();
            }
        }
        public override int CurrentSP
        {
            get { return base.CurrentSP; }
            set
            {
                base.CurrentSP = value;
                UpdateHP_SP();
            }
        }

        /// <summary>
        /// 在设置时将相关值赋为0，主要是设置false时需要
        /// </summary>
        public virtual bool CanMove
		{
			get { return canMove; }
			set { h = 0; v = 0; canMove = value; }
		}
		public bool CanRotate { get; set; }
        public PlayerMediator PlayerMedi { get; set; }

        /// <summary>
        /// Initialize只在第一次创建时执行初始化代码，之后切换Scene时都不用再次初始化，所以Data也没有改变
        /// </summary>
        /// <param name="gameObject"></param>
        public IPlayer(GameObject gameObject):base(gameObject)
        {
            Speed = 5;
            if (GameObjectInScene != null)
            {
                animator = GameObjectInScene.GetComponent<Animator>();
                Rgbd = GameObjectInScene.GetComponent<Rigidbody>();
                // 关联中介者
                PlayerMedi = new PlayerMediator(this);
                PlayerMedi.Initialize();
            }
        }

        public override void Release()
        {
            PlayerMedi.PlayerMono.Release();
        }

        public virtual void Hurt(PlayerHurtAttr playerHurtAttr) { }

        public override void Dead()
        {
            base.Dead();
            GameMainProgram.Instance.eventMgr.InvokeEvent(EventName.PlayerDead);    // 触发死亡事件
        }

        /// <summary>
        /// 触发更新Player的HP和SP的事件。这常用于更新UI
        /// </summary>
        private void UpdateHP_SP()
        {
            GameMainProgram.Instance.eventMgr.InvokeEvent(EventName.PlayerHP_SP);
        }

    }
}