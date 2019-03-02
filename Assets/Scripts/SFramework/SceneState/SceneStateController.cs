using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace SFramework
{
    // 每添加一个场景，都需要修改enum和switch
    public enum SceneState
    {
        StartState,
    }

	/// <summary>
	/// 场景状态机
	/// </summary>
	public class SceneStateController
	{
		public ISceneState CurState { get; private set; }//当前场景
		private bool isSceneBegin = false;//场景是否已经加载

		//构造函数
		public SceneStateController()
		{ }

        /// <summary>
        /// 设置当前场景
        /// </summary>
        public void SetState(SceneState sceneState, bool isNow=true,bool isAsync=false)
        {
            ISceneState State;
		    switch (sceneState)
		    {
                case SceneState.StartState:
                    State = new StartState(this);
                    break;
                default:
                    return;
            }

			Debug.Log("SetState:" + State.ToString());
			isSceneBegin = false;

			// 通知前一個State結束
			if (CurState != null)
				CurState.StateEnd();
            // 載入場景
            if (isNow)
            {
                if (isAsync)
                {
                    UILoading.nextScene = State.StateName;
                    LoadScene("Loading");
                }
                else
                {
                    LoadScene(State.StateName);
                }
            }
			//设置当前场景
			CurState = State;
		}

		//场景的载入
		private void LoadScene(string loadSceneName)
		{
			if (loadSceneName == null || loadSceneName.Length == 0)
				return;
			SceneManager.LoadScene(loadSceneName);
		}

		// 更新
		public void StateUpdate()
		{
			// 是否還在載入
			if (Application.isLoadingLevel)
				return;

            // 通知新的State開始，因为不能保证StateBegin会在什么时候调用，所以放在Update中
            if (CurState != null && isSceneBegin == false)
            {
                CurState.StateBegin();
                isSceneBegin = true;
            }

            //状态的更新，需要StateBegin()执行完后才能执行
            if (CurState != null&& isSceneBegin)
				CurState.StateUpdate();
		}

		public void FixedUpdate()
		{
            if (Application.isLoadingLevel)
                return;
            if (CurState != null && isSceneBegin)
				CurState.FixedUpdate();
		}

        public void ExitGame()
        {
            isSceneBegin = false;

            // 通知前一個State結束
            if (CurState != null)
                CurState.StateEnd();

            Application.Quit();
        }

	}
}