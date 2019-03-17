/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：道具基类
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 道具基类
    /// </summary>
    public class IProp : IItem
    {
        // 展示属性
        public int Id { get; protected set; }
        public PropType Type { get; protected set; }
        public int MaxNum { get; protected set; }  //上限数目

        public IProp():base()
        {
            Id = 0;
            Type = PropType.Medicine;
            Name = "Prop";
            MaxNum = 100;
        }

    }
}
