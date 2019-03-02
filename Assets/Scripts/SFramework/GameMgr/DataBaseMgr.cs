using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 游戏数据库
    /// 存放游戏中的所有装备、道具数据
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