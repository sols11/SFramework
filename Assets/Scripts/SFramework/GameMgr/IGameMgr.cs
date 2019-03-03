/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：子系统抽象类
    作用：子系统作为管理者，需要负责被管理对象的 Awake,Initialize,FixedUpdate,Update,Release 生命周期的调用。
    使用：声明一个Manager类，继承并实现IGameMgr类
    补充：
History:
----------------------------------------------------------------------------*/

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

        // 继承后在这里声明需要被管理的对象

        /// <summary>
        /// 构造函数需指定gameMain
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