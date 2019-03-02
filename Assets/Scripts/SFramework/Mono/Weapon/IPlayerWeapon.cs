using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// + 玩家武器所需属性
    /// + PlayerMediator
    /// 武器的控制层
    /// </summary>
    public abstract class IPlayerWeapon:IWeaponMono
    {
        protected string hitEffectPath;   //打击特效
        protected List<GameObject> enemyList;

        public PlayerMediator PlayerMedi { get; set; }
        protected EnemyHurtAttr EnemyHurtAttribute { get; set; }
        /// <summary>
        /// 敌人是否防御住了攻击
        /// </summary>
        protected EnemyAction EnemyReturn { get; set; }

        /// <summary>
        /// 清空队列，提供给AnimEvent
        /// </summary>
        public void ClearList()
        {
            enemyList.Clear();
        }

        public override void Initialize()
        {
            base.Initialize();
            EnemyHurtAttribute = new EnemyHurtAttr();
            EnemyReturn = EnemyAction.Hurt;
            //List必须要初始化
            enemyList = new List<GameObject>();
        }

        public override void Release()
        {
            base.Release();
            ClearList();
        }

    }
}
