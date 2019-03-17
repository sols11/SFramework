/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：装备基类
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

namespace SFramework
{
    /// <summary>
    /// 装备基类
    /// </summary>
    public class IEquip : IItem
    {
        // 展示属性
        public FitType Type { get; protected set; }     // 装备类型

        public IEquip() : base()
        {
            Type = FitType.Weapon;
            Name = "Equip";
        }
    }
}
