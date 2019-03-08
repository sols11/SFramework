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
    2019/03/07 简化了场景的加载和存放方式
----------------------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

namespace SFramework
{
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
        /// 设置当前场景（加载场景）
        /// </summary>
        /// <typeparam name="T">需要加载的场景类</typeparam>
        /// <param name="isNow"></param>
        /// <param name="isAsync"></param>
        public void SetState<T>(bool isNow = true, bool isAsync = false) where T: ISceneState, new()
        {
            // 用泛型方法<T>创建新场景状态state，并将state添加到dict，通过state.SceneName加载对应场景
            ISceneState state = new T();
            if (stateDict.ContainsKey(state.SceneName))
            {
                Debug.Log(state.SceneName + "is contained.");
                state = stateDict[state.SceneName];  // 替换掉原来的state
            }
            else
            {
                state.Awake(this);
                stateDict.Add(state.SceneName, state);
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
			// 是否还在载入
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

        public void ExitApplication()
        {
            isSceneBegin = false;

            // 通知前一個State结束
            if (CurrentState != null)
                CurrentState.StateEnd();

            Application.Quit();
        }

	}
}