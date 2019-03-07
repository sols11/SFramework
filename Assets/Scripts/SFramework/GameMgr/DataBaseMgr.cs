/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：游戏数据库（默认基于Json）
    作用：存放游戏中的所有装备、道具等数据，提供给游戏中数据对象使用。
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
    /// 游戏数据库
    /// </summary>
    public class DataBaseMgr : IGameMgr
    {
        // 字典存放所有种类的Item，key也可以用enum
        public Dictionary<string, IEquip> dicEquip;

        public DataBaseMgr(GameMainProgram gameMain) : base(gameMain)
        {
        }

        public override void Awake()
        {
            //dicEquip = gameMain.fileMgr.LoadJsonDataBase<Dictionary<string, IEquip>>("Equip");

            //gameMain.fileMgr.CreateJsonDataBase("Tasks",Tasks);
        }

    }
}