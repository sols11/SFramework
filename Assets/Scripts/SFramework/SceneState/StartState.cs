using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
	public class StartState : ISceneState
	{
		private GameMainProgram gameMainProgram;//主程序

		public StartState(SceneStateController controller) : base(controller)
		{
			this.StateName = "Start";
		}

		public override void StateBegin()
		{
			gameMainProgram=GameMainProgram.Instance;
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
