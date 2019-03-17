using System;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// IEnemy,IEnemyMono,IEnemyWeapon3个关联类的中介者
    /// 负责更换装备
    /// </summary>
    public class EnemyMediator
    {
        public IEnemy Enemy { get; set; }
        public IEnemyMono EnemyMono { get; set; }
        public IEnemyWeapon EnemyWeapon { get; set; }
        public IEquip WeaponData { get; set; }  // 和PlayerMediator不同的是，Enemy没有Fit[]，所以需要专门存储武器和防具

        public EnemyMediator(IEnemy enemy)
        {
            Enemy = enemy;
        }

        public void Initialize()
        {
            // 默认装备，可以在后续修改其对象
            EnemyMono = Enemy.GameObjectInScene.GetComponent<IEnemyMono>();
            if (EnemyMono != null)
            {
                EnemyMono.EnemyMedi = this;
                EnemyMono.AnimatorComponent = Enemy.animator;
                EnemyMono.Rgbd = Enemy.Rgbd;
                EnemyWeapon = EnemyMono.iEnemyWeapon;
                EnemyMono.Initialize();
                if (EnemyWeapon != null)
                {
                    EnemyWeapon.EnemyMedi = this;
                    EnemyWeapon.Initialize();
                    EnemyMono.WeaponCollider = EnemyWeapon.WeaponCollider;
                }
                else
                    Debug.LogError("iEnemyWeapon未赋值");
            }

            UpdateEnemyWeapon(EnemyMono.iEnemyWeapon);
        }


        /// <summary>
        /// 设置WeaponData，使用的是装备的武器
        /// 就目前的实现来说，所有Weapon对象均共用同一个IWeaponMono，所以初始化时关联一个默认Weapon，切换武器时切换Weapon对象即可
        /// </summary>
        /// <param name="enemyWeapon"></param>
        public void UpdateEnemyWeapon(IEnemyWeapon enemyWeapon)
        {

        }
    }


}

