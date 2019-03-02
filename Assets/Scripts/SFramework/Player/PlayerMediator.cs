using System;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// IPlayer,IPlayerMono,IPlayerWeapon3个关联类的中介者
    /// 负责更换装备
    /// </summary>
    public class PlayerMediator
    {
        public IPlayer Player { get; set; }
        public IPlayerMono PlayerMono { get; set; }
        public IPlayerWeapon PlayerWeapon { get; set; }

        public PlayerMediator(IPlayer _player)
        {
            Player = _player;
        }

        public void Initialize()
        {
            PlayerMono = Player.GameObjectInScene.GetComponent<IPlayerMono>();
            if (PlayerMono)
            {
                PlayerMono.PlayerMedi= this;
                PlayerMono.Rg2d = Player.Rg2d;
                PlayerMono.AnimatorComponent = Player.animator;
                PlayerWeapon = PlayerMono.iPlayerWeapon;
                PlayerMono.Initialize();
                if (PlayerWeapon != null)
                {
                    PlayerWeapon.PlayerMedi = this; // 引用
                    PlayerWeapon.Initialize();
                    PlayerMono.WeaponCollider = PlayerWeapon.WeaponCollider;
                }
                else
                    Debug.LogError("iPlayerWeapon未赋值");
                UpdatePlayerWeapon(PlayerMono.iPlayerWeapon);
            }
        }

        /// <summary>
        /// 用于从装备更新角色和武器属性
        /// </summary>
        /// <param name="equip"></param>
        public void FitEquip()
        {
            UpdatePlayerWeapon(PlayerWeapon);
        }

        

        /// <summary>
        /// 设置WeaponData，使用的是装备的武器
        /// 做强化系统时再加上强化的数值
        /// </summary>
        /// <param name="_weapon">哪一个PlayerWeapon</param>
        private void UpdatePlayerWeapon(IPlayerWeapon _playerWeapon)
        {
        }

    }
}
