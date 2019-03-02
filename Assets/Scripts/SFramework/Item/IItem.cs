using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    ///+ 基本属性
    ///+ 显示属性
    ///+ 构造函数中初始化默认值
    ///物品的基类
    /// </summary>
    public abstract class IItem
    {
        private int price;
        private int salePrice;
        // 基本属性
        private Sprite icon;    // 不要让icon序列化，让icon以方法形式获取
        public string IconPath { get; protected set; }
        public string Name { get; protected set; }
        public string Detail { get; protected set; }
        public bool CanSale { get; protected set; }
        public int Price { get { return price; } protected set {
                if (price < 0)
                    price = 0;
                else if (price > 9999)
                    price = 9999;
                else
                    price = value;

            } }
        public int SalePrice
        {
            get { return salePrice; }
            protected set
            {
                if (salePrice < 0)
                    salePrice = 0;
                else if (salePrice > 9999)
                    salePrice = 9999;
                else
                    salePrice = value;

            }
        }
        // 显示属性
        public SpecialAbility Special { get; protected set; }
        public int HP { get; protected set; }
        public int SP { get; protected set; }
        public int Attack { get; protected set; }
        public int Defend { get; protected set; }
        public int Crit { get; protected set; }
        public int Speed { get; protected set; }

        public IItem()
        {
            icon = null;
            IconPath = @"Icons\";
            Name = string.Empty;
            Detail = string.Empty;
            CanSale = false;
            Price = 0;
            SalePrice = 0;
            Special = SpecialAbility.无;
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