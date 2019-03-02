using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 敌人控制系统
    /// 负责敌人的创建，管理，删除
    /// 存储整个场景中的所有Enemy
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
        public override void Update()
        {
            foreach (IEnemy e in enemysInScene)
                e.Update();
        }
        public override void FixedUpdate()
        {
            foreach (IEnemy e in enemysInScene)
                e.FixedUpdate();
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