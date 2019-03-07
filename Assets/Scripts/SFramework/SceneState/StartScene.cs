/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：StartState是ISceneState接口的一个实例，作为初始场景状态类展示场景状态的使用方法
    作用：控制主程序的Initialize、FixedUpdate、Update、Release
    使用：建议的命名为：SceneName要和场景名称相同，SceneState类名为"场景名称"+"Scene"。场景、场景名、场景类一一对应，不要重名
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
	public class StartScene : ISceneState
	{
		private GameMainProgram gameMainProgram;    // 主程序

		public StartScene()
		{
			this.SceneName = "Start";
		}

        public override void StateBegin()
        {
            gameMainProgram = GameMainProgram.Instance;
            gameMainProgram.Initialize();
        }

        public override void StateEnd()
		{
			gameMainProgram.Release();
		}

		public override void StateUpdate()
		{
			gameMainProgram.Update();
		}

		public override void FixedUpdate()
		{
			gameMainProgram.FixedUpdate();
		}
	}
}
