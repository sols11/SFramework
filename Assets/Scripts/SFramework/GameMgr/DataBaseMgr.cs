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
        public Dictionary<string, IEquip> equipDict;
        public Dictionary<string, IEquip> enemyEquipDict;
        public List<Task> Tasks { get; set; }
        public IProp[] Props { get; private set; }

        public DataBaseMgr(GameMainProgram gameMain) : base(gameMain)
        {
        }

        /// <summary>
        /// 常用于调用FileMgr加载数据库文件
        /// </summary>
        public override void Awake()
        {
            equipDict = gameMain.fileMgr.LoadJsonDataBase<Dictionary<string, IEquip>>("Equip");
            Debug.Log("Equip数据加载完毕");
            Props = gameMain.fileMgr.LoadJsonDataBase<IProp[]>("Prop");
            Debug.Log("Prop数据库加载完毕");
            enemyEquipDict = gameMain.fileMgr.LoadJsonDataBase<Dictionary<string, IEquip>>("EnemyEquip");
            Debug.Log("EnemyEquip数据加载完毕");
            Tasks = gameMain.fileMgr.LoadJsonDataBase<List<Task>>("Tasks");
            Debug.Log("Tasks数据加载完毕");
            Debug.Log("数据库文件全部加载完毕");
        }

    }
}