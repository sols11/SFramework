/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：系统全局变量定义文件
    作用：定义系统的全局变量，在本文件保存除UI外所有全局公共枚举。UI相关的全局变量放在Common.cs文件中
    使用：可以直接为契合项目而修改本文件，设置你需要的全局变量（包括枚举类型）
    补充：
History:
----------------------------------------------------------------------------*/

namespace SFramework
{
    /// <summary>
    /// 系统全局变量设置
    /// </summary>
    public static class SystemDefine
    {
        // 填写全局变量

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
        Wall = 15,
        PostProcessing = 16,
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
        Hurt = 0,       // 受伤
        Defend = 1,     // 轻攻击防御，重攻击被破防
        Parry = 2,      // 轻攻击防御，重攻击弹开
        Shield = 3,     // 轻重攻击都弹开
        Miss = 4,       // 未命中
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
