using UnityEngine;
using System.Collections;

namespace SFramework
{
	/// <summary>
	/// 游戏管理者抽象类
	/// </summary>
	public abstract class IGameMgr
	{
		protected GameMainProgram gameMain = null;

        //继承后在这里声明Mgr对象

        /// <summary>
        /// 构造函数指定m_GameMain
        /// </summary>
        /// <param name="gameMain"></param>
        public IGameMgr(GameMainProgram gameMain)
		{
			this.gameMain = gameMain;
		}

		public virtual void Awake() { }
        public virtual void Initialize() { }
		public virtual void Release() { }
		public virtual void Update() { }
		public virtual void FixedUpdate() { }

	}
}