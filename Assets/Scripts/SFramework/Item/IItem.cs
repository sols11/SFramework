/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：游戏中所有物品的基类
    作用：这里提供一些常用的属性
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 物品的基类
    /// </summary>
    public abstract class IItem
    {
        // 购买价格和出售价格
        private int price;
        private int salePrice;
        // 基本属性
        private Sprite icon;    // 不要让icon序列化，让icon以方法形式获取
        public string IconPath { get; protected set; }  // 图标的对应路径
        public string Name { get; protected set; }      // 物品名
        public string Detail { get; protected set; }    // 详细信息
        public bool CanSale { get; protected set; }     // 是否可出售
        // 价格限制在0~int.maxvalue
        public int Price
        {
            get { return price; }
            protected set
            {
                if (price < 0)
                    price = 0;
                else if (price > int.MaxValue)
                    price = int.MaxValue;
                else
                    price = value;
            }
        }                             
        public int SalePrice
        {
            get { return salePrice; }
            protected set
            {
                if (salePrice < 0)
                    salePrice = 0;
                else if (salePrice > int.MaxValue)
                    salePrice = int.MaxValue;
                else
                    salePrice = value;
            }
        }
        // 显示属性（也是常见属性）
        public int HP { get; protected set; }           // 增加HP
        public int SP { get; protected set; }           // 增加SP
        public int Attack { get; protected set; }       // 增加伤害
        public int Defend { get; protected set; }       // 增加防御力
        public int Crit { get; protected set; }         // 增加暴击
        public int Speed { get; protected set; }        // 增加速度

        public IItem()
        {
            icon = null;
            IconPath = @"Icons\";
            Name = string.Empty;
            Detail = string.Empty;
            CanSale = false;
            Price = 0;
            SalePrice = 0;
            HP = 0;
            SP = 0;
            Attack = 0;
            Defend = 0;
            Crit = 0;
            Speed = 0;
        }

        public Sprite GetIcon()
        {
            if(icon==null)
                icon = GameMainProgram.Instance.resourcesMgr.LoadResource<Sprite>(IconPath, false);
            return icon;
        }
    }
}