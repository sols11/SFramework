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
        public StartScene(SceneStateController controller) : base(controller)
        {
            this.SceneName = "Start";
        }

        public override void StateBegin()
        {
            base.StateBegin();
            // 场景初始化
            gameMainProgram.playerMgr.BuildPlayer(Vector3.zero, Quaternion.identity);
            gameMainProgram.playerMgr.CanInput = true;  // 接受输入
                                                        // 其他角色在Player之后创建

            // UI，BGM
            GameMainProgram.Instance.uiManager.ShowUIForms("FadeOut");
            gameMainProgram.audioMgr.PlayMusic(0);
        }

        public override void StateEnd()
		{
            // 先Release其他成员，再调用base
            base.StateEnd();
		}

		public override void FixedUpdate()
		{
            base.FixedUpdate();
		}

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

    }
}
