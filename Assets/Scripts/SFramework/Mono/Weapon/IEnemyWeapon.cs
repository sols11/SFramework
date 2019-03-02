using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    ///+ 敌人武器所需属性
    ///+ EnemyMediator
    ///武器的控制层
    /// </summary>
    public abstract class IEnemyWeapon : IWeaponMono
	{
        public EnemyMediator EnemyMedi { get; set; }
        public bool IsOnlyPlayer { get; set; }
        protected PlayerHurtAttr PlayerHurtAttr { get; set; }
        protected IPlayerMono OnlyPlayerMono { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            //一定要初始化
            TransformForward = Vector3.zero;
            PlayerHurtAttr = new PlayerHurtAttr();
        }

    }
}