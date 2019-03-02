using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 定义系统的全局变量，在本文件保存所有全局公共枚举，UI相关的放在Common
    /// </summary>
    public static class SystemDefine
    {


    }

    public enum ObjectLayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        BuiltinLayer3,
        Water = 4,
        UI = 5,
        BuiltinLayer6,
        BuiltinLayer7,
        Ground = 8,
        Player = 9,
        Enemy = 10,
        Effect = 11,
        PlayerWeapon = 12,
        EnemyWeapon = 13,
        Without = 14,
    }

    /// <summary>
    /// 装备的特殊能力，敌我共用
    /// </summary>
    public enum SpecialAbility
    {
        无=0,
    }

    /// <summary>
    /// 装备的类型，及对应的Slot位置
    /// </summary>
    public enum FitType
    {
        Prop = -1, // 非药物的道具，不可装备
        Cloth = 0,
        Cestus = 1,
        Weapon = 2,
        Decoration = 3,
        Pants = 4,
        Shoe = 5,
    }

    public enum PropType
    {
        Medicine=0,
        Material=1,
        Event=2
    }

    /// <summary>
    /// 定义Hurt的返回值
    /// </summary>
    public enum EnemyAction
    {
        Hurt = 0, // 受伤
        Defend = 1, // 轻攻击防御，重攻击被破防
        Parry = 2, // 轻攻击防御，重攻击弹开
        Shield = 3, // 轻重攻击都弹开
        Miss = 4, // 未命中
    }

    public enum EnemyType
    {
        Monster = 0,
        Warrior = 1,
        Magian = 2,
    }

    /// <summary>
    /// 各个事件
    /// </summary>
    public enum EventName
    {
        PlayerHP_SP = 0,
        PlayerDead = 1,
        MedicineNum = 2,
        BossDead = 3,
        DialogComplete=4,
    }

    /// <summary>
    /// 使用标准委托的事件
    /// </summary>
    public enum EventHandlerName
    { }



}
