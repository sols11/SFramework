/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：场景状态类的基类（接口）
    作用：控制接口方法的初始化、更新、释放
    使用：场景状态类决定了这个场景中存在哪些对象，使用哪些功能
    补充：
History:
    2019/03/15 之前保留了一个awake方法，现在用不上了，用构造函数实现
----------------------------------------------------------------------------*/

using System.Collections;
using UnityEngine;

namespace SFramework
{
	/// <summary>
	/// 场景状态"接口"类
	/// </summary>
	public abstract class ISceneState
	{
		public string SceneName { get; set; }                       // UnityScene文件对应的名称，即需要加载的场景名称
        protected SceneStateController Controller { get; set; }     // 控制者

		public ISceneState(SceneStateController controller)
        {
            Controller = controller;
        }

        public virtual void StateBegin(){ }
		public virtual void StateEnd(){ }
		public virtual void FixedUpdate(){ }
		public virtual void StateUpdate(){ }

		public override string ToString()
		{
		    return SceneName;
		}


	}
}
