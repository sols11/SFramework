/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：NPC角色控制系统
    作用：存储整个场景中的所有NPC，负责NPC角色的创建，管理，删除
    使用：相较Player和Enemy，Npc创建更为简单，只需要用CreateNpc(string npcName)
    补充：
History:
----------------------------------------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SFramework
{
    /// <summary>
    /// NPC控制系统
    /// </summary>
    public class NpcMgr:IGameMgr
    {
        private List<INPC> npcInSceneList;
        private Dictionary<string, string> npcPathDict;

        public NpcMgr(GameMainProgram gameMain) : base(gameMain)
        {
            npcInSceneList = new List<INPC>();
            npcPathDict = new Dictionary<string, string>();
        }

        public override void Awake()
        {
            // 新的Npc在这里添加
            if (npcPathDict != null)
            {
                // npcPathDict.Add("Merchant", @"Npcs\NpcMerchant");
            }
        }

        public override void Update()
        {
            foreach (INPC n in npcInSceneList)
                n.OnUpdate();
        }

        public override void Release()
        {
            foreach (INPC n in npcInSceneList)
                n.Release();
            npcInSceneList.Clear();
        }

        /// <summary>
        /// 在预制的位置生成NPC的Prefab
        /// </summary>
        /// <param name="npcName"></param>
        public void CreateNpc(string npcName)
        {
            if (string.IsNullOrEmpty(npcName)) return;
            GameObject go = gameMain.resourcesMgr.LoadAsset(UnityHelper.FindDic(npcPathDict, npcName));
            INPC npc = null;
            if (go)
                npc = go.GetComponent<INPC>();
            if (npc != null)
            {
                npcInSceneList.Add(npc);
                npc.Initialize();
            }
            else
            {
                Debug.LogError("无法创建NPC，Npc==null");
            }
        }

        public void RemoveNpc(string npcName)
        {
            if (string.IsNullOrEmpty(npcName)) return;
            // 找到然后移除
            foreach (INPC n in npcInSceneList)
            {
                if (n.gameObject.name == npcName)
                {
                    npcInSceneList.Remove(n);
                    n.Release();
                }
            }
        }
    }
}
