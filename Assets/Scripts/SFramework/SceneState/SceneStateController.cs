/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：状态机类
    作用：控制场景的切换，包括异步加载和同步加载两种方式，还包括退出游戏功能。
    使用：对当前场景状态执行场景初始化、更新、释放。枚举SceneState并在SetState时用switch语句加载相应的场景
    补充：
History:
----------------------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

namespace SFramework
{
    // 每添加一个场景状态类，都需要修改enum和switch
    public enum SceneState
    {
        StartScene,
    }

	/// <summary>
	/// 场景状态机
	/// </summary>
	public class SceneStateController
	{
		public ISceneState CurrentState { get; private set; }   // 当前场景
		private bool isSceneBegin = false;                      // 场景是否已经加载
        private Dictionary<string, ISceneState> stateDict;      // <StateName, SceneState>对应字典

        public SceneStateController()
		{ }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetState<T>() where T: ISceneState, new()
        {
            // 用泛型方法<T>将state添加到list，之后set state时传入字符串参数，直接检索该字符串对应的state
            ISceneState state = new T();
            state.Awake(this);
            // stateDict.Add(state.SceneName, state);
        }

        /// <summary>
        /// 设置当前场景
        /// </summary>
        public void SetState(SceneState sceneState, bool isNow = true, bool isAsync = false)
        {
            ISceneState state;
            switch (sceneState)
            {
                case SceneState.StartScene:
                    state = new StartScene();
                    break;
                default:
                    return;
            }

            Debug.Log("SetState:" + state.ToString());
            isSceneBegin = false;

            // 通知前一个State结束
            if (CurrentState != null)
                CurrentState.StateEnd();
            // 载入场景
            if (isNow)
            {
                if (isAsync)
                {
                    UILoading.nextScene = state.SceneName;
                    LoadScene("Loading");
                }
                else
                {
                    LoadScene(state.SceneName);
                }
            }
            // 设置当前场景
            CurrentState = state;
        }

		// 场景的载入
		private void LoadScene(string loadSceneName)
		{
			if (loadSceneName == null || loadSceneName.Length == 0)
				return;
			SceneManager.LoadScene(loadSceneName);
		}

		// 更新
		public void StateUpdate()
		{
			// 是否還在载入
			if (Application.isLoadingLevel)
				return;

            // 通知新的State开始，因为不能保证StateBegin会在什么时候调用，所以放在Update中
            if (CurrentState != null && isSceneBegin == false)
            {
                CurrentState.StateBegin();
                isSceneBegin = true;
            }

            //状态的更新，需要StateBegin()执行完后才能执行
            if (CurrentState != null && isSceneBegin)
				CurrentState.StateUpdate();
		}

		public void FixedUpdate()
		{
            if (Application.isLoadingLevel)
                return;
            if (CurrentState != null && isSceneBegin)
				CurrentState.FixedUpdate();
		}

        public void ExitGame()
        {
            isSceneBegin = false;

            // 通知前一個State结束
            if (CurrentState != null)
                CurrentState.StateEnd();

            Application.Quit();
        }

	}
}