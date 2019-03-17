/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：游戏过程管理器
    作用：对游戏的进程进行分析，控制
    使用：
    补充：功能：暂停游戏，角色死亡事件，敌人死亡事件
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 过程管理
    /// </summary>
    public class CourseMgr : IGameMgr
    {
        private bool enable = false;
        /// <summary>
        /// 是否启用该系统
        /// </summary>
        public bool Enable {
            get
            {
                return enable;
            }
            set
            {
                enable = value;
                if (Enable)
                {
                    // 当启用时，注册死亡事件
                    GameMainProgram.Instance.eventMgr.StartListening(EventName.PlayerDead, OnAfterPlayerDead);
                    // Boss死亡事件
                    GameMainProgram.Instance.eventMgr.StartListening(EventName.BossDead, OnAfterBossDead);
                }
                else
                {
                    GameMainProgram.Instance.eventMgr.StopListening(EventName.PlayerDead, OnAfterPlayerDead);
                    GameMainProgram.Instance.eventMgr.StopListening(EventName.BossDead, OnAfterBossDead);
                }
            }
        }

        private bool pauseGame = false;
        private int defaultTimeScale = 1;

        public CourseMgr(GameMainProgram gameMain):base(gameMain)
		{
        }

        public override void Initialize()
        {
            Enable = false;
            pauseGame = false;
        }

        public override void Release()
        {
            if (!Enable)
                return;
            if (pauseGame) // 解除暂停
                PauseGame();
        }

        public override void Update()
        {
            if (UIDialog.IsTalking)
                return;
            if (Input.GetButtonDown("Cancel"))
            {
                if (Enable)
                    PauseGame();
            }
        }

        public void PauseGame()
        {
            if (!Enable)
            {
                Debug.LogWarning("未开启CourseMgr");
                return;
            }
            if (!pauseGame)
            {
                pauseGame = true;
                GameMainProgram.Instance.uiManager.ShowUIForms("PauseMenu");
                if (Time.timeScale != 0)
                    Time.timeScale = 0;
                else
                    Debug.LogWarning("已经暂停，请检查");
            }
            else
            {
                pauseGame = false;
                GameMainProgram.Instance.uiManager.CloseUIForms("PauseMenu");
                if (Time.timeScale==0)
                    Time.timeScale = defaultTimeScale;
                else
                    Debug.LogWarning("游戏并未暂停，请检查");
            }
        }

        private void OnAfterPlayerDead()
        {
            CoroutineMgr.Instance.StartCoroutine(AfterPlayerDead());
        }

        private void OnAfterBossDead()
        {
            CoroutineMgr.Instance.StartCoroutine(AfterBossDead());
        }

        private IEnumerator AfterPlayerDead()
        {
            GameMainProgram.Instance.courseMgr.Enable = false;
            yield return new WaitForSeconds(2);
            //GameMainProgram.Instance.playerMgr.Release();

        }

        private IEnumerator AfterBossDead()
        {
            yield return new WaitForSeconds(3);
            //gameMain.playerMgr.CurrentPlayer.Gold+=50;
            //UIMessageBox.AddMessage("战斗胜利，30秒后返回");
            //yield return new WaitForSeconds(3);
            //UIMessageBox.AddMessage("任务完成，获得100金币");
            //yield return new WaitForSeconds(1);
            //GameMainProgram.Instance.uiManager.ShowUIForms("FadeIn");
            //yield return new WaitForSeconds(2.5f);


        }
    }
}