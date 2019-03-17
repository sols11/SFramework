using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SFramework
{
    /// <summary>
    ///+ iEnemyWeapon 预制属性，进行初始化
    ///+ EnemyMediator
    ///+ 动画事件
    ///+ Target 导航目标
    ///由EnemyMediator初始化，不使用Mono生命周期
    ///Enemy的MonoBehaviour脚本，用于武器碰撞检测、动画事件和行为树控制
    /// </summary>
    public class IEnemyMono : ICharacterMono
	{
		public string targetTag = "Player";
        public IEnemyWeapon iEnemyWeapon;      //在预制时赋好的变量，与武器引用

        public EnemyMediator EnemyMedi { get; set; }
        public Transform Target { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            FindTarget();

        }
        public override void Release()
        {
            iEnemyWeapon.Release();
        }

        private void Update()
        {
            if (Target == null)
                FindTarget();
        }

        /// <summary>
        /// 封装iEnemy成员，不要直接使用iEnemy
        /// </summary>
        /// <param name="enemyHurtAttr"></param>
        public virtual EnemyAction Hurt(EnemyHurtAttr enemyHurtAttr)
		{
			return EnemyMedi.Enemy.Hurt(enemyHurtAttr);
		}

        /// <summary>
        /// findTagTarget
        /// </summary>
        public void FindTarget()
        {
            var targetObj = GameObject.FindGameObjectWithTag(targetTag);
            if (targetObj)
                Target = targetObj.transform;
        }

    }
}