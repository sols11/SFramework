using System.Collections;
using UnityEngine;

namespace SFramework
{
	/// <summary>
	/// 场景状态"接口"类
	/// </summary>
	public abstract class ISceneState
	{
		public string StateName { get; set; }

		// 控制者
		protected SceneStateController Controller = null;

		// 建構者
		public ISceneState(SceneStateController controller)
		{
		    Controller = controller;
		}

		public virtual void StateBegin(){ }
		public virtual void StateEnd(){ }
		public virtual void StateUpdate(){ }
		public virtual void FixedUpdate(){ }

		public override string ToString()
		{
		    return StateName;
		}


	}
}
