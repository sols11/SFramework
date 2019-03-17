/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：敌方角色控制系统
    作用：存储整个场景中的所有Enemy，负责敌方角色的创建，管理，删除
    使用：调用接口
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 敌人控制系统
    /// </summary>
	public class EnemyMgr : IGameMgr
    {
        private List<IEnemy> enemysInScene;

        public EnemyMgr(GameMainProgram gameMain) : base(gameMain)
        {
            enemysInScene = new List<IEnemy>();
        }

        public override void Initialize()
        {
            gameMain.eventMgr.StartListening(EventName.PlayerDead, NotifyPlayerDead);
        }
        public override void Release()
        {
            foreach (IEnemy e in enemysInScene)
            {
                if (e != null)
                    e.Release();
            }
            enemysInScene.Clear();
            gameMain.eventMgr.StopListening(EventName.PlayerDead, NotifyPlayerDead);
        }
        public override void FixedUpdate()
        {
            foreach (IEnemy e in enemysInScene)
                e.FixedUpdate();
        }
        public override void Update()
        {
            foreach (IEnemy e in enemysInScene)
                e.Update();
        }
        public void NotifyPlayerDead()
        {
            foreach (IEnemy e in enemysInScene)
                e.WhenPlayerDead();
        }

        /// <summary>
        /// 加入Enemy并初始化
        /// </summary>
        /// <param name="_newEnemy"></param>
		public void AddEnemy(IEnemy _enemy)
        {
            if (_enemy != null)
            {
                enemysInScene.Add(_enemy);
                _enemy.Initialize();
            }
        }

        /// <summary>
        /// 删除Enemy并释放
        /// </summary>
        /// <param name="_enemy"></param>
        private void RemoveEnemy(IEnemy _enemy)
        {
            if (_enemy != null)
            {
                enemysInScene.Remove(_enemy);
                _enemy.Release();
            }
        }

    }
}