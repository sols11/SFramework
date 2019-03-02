using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

namespace SFramework
{
    /// <summary>
    /// + 装备等级与强化
    /// 装备基类
    /// </summary>
    public class IEquip : IItem
    {
        //不需考虑计算时超出范围可能性的属性就不写具体的set了
        //展示属性
        public FitType Type { get; protected set; }
        public int Level { get; protected set; }  //强化等级
        //强化属性加成，先不写
        private bool IsIntensified { get; set; }  //是否强化过
        public bool CanLevelUP { get; set; }    //是否可强化

        public IEquip() : base()
        {
            Type = FitType.Weapon;
            Level = 0;
            Name = "Equip";
            IsIntensified = false;
            CanLevelUP = false;
        }
    }
}
