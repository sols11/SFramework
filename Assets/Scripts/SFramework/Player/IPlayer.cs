using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SFramework
{
    /// <summary>
    /// + Player的所有属性
    /// </summary>
	public class IPlayer : ICharacter
	{
		protected bool canMove = false;   //CanMovePosition
		protected float h;
		protected float v;
        protected GameData gameData;

		public float Speed { get; set; }    // AvoidSpeed用MoveSpeed*2代替

        //Player的HP,SP需要更新UI
        public override int CurrentHP
        {
            get { return base.CurrentHP; }
            set { base.CurrentHP = value;
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
        
        /// <summary>
        /// Initialize只在第一次创建时执行初始化代码，之后切换Scene时都不用再次初始化，所以Data也没有改变
        /// </summary>
        /// <param name="gameObject"></param>
        public IPlayer(GameObject gameObject):base(gameObject)
        {
            if (GameObjectInScene != null)
            {
                animator = GameObjectInScene.GetComponent<Animator>();
                Rg2d = GameObjectInScene.GetComponent<Rigidbody2D>();
            }
        } 

        public virtual void Hurt(PlayerHurtAttr playerHurtAttr) { }

        public override void Dead()
        {
            base.Dead();
            GameMainProgram.Instance.eventMgr.InvokeEvent(EventName.PlayerDead);    // 触发死亡事件
        }

        private void UpdateHP_SP()
        {
            GameMainProgram.Instance.eventMgr.InvokeEvent(EventName.PlayerHP_SP);
        }

    }
}