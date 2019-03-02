using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SFramework
{
    /// <summary>
    /// NPC控制系统
    /// 负责NPC的创建，管理，删除
    /// 相较Player和Enemy，Npc创建更为简单，只需要用CreateNpc(string npcName)
    /// </summary>
    public class NpcMgr:IGameMgr
    {
        private List<INPC> npcInScene;
        private Dictionary<string, string> dicNpcPaths;

        public NpcMgr(GameMainProgram gameMain) : base(gameMain)
        {
            npcInScene=new List<INPC>();
            dicNpcPaths=new Dictionary<string, string>();
        }

        public override void Awake()
        {
            // 新的Npc在这里添加
            if (dicNpcPaths != null)
            {
                dicNpcPaths.Add("Merchant", @"Npcs\NpcMerchant");
                dicNpcPaths.Add("Recycler", @"Npcs\NpcRecycler");
                dicNpcPaths.Add("Battler", @"Npcs\NpcBattler");
            }
        }

        public override void Update()
        {
            foreach (INPC n in npcInScene)
                n.OnUpdate();
        }

        public override void Release()
        {
            foreach (INPC n in npcInScene)
                n.Release();
            npcInScene.Clear();
        }

        /// <summary>
        /// 在预制的位置生成NPC的Prefab
        /// </summary>
        /// <param name="npcName"></param>
        public void CreateNpc(string npcName)
        {
            if (string.IsNullOrEmpty(npcName)) return;
            GameObject go = gameMain.resourcesMgr.LoadAsset(UnityHelper.FindDic(dicNpcPaths, npcName));
            INPC npc = null;
            if (go)
                npc = go.GetComponent<INPC>();
            if (npc != null)
            {
                npcInScene.Add(npc);
                npc.Initialize();
            }
            else
            {
                Debug.LogError("Npc==null");
            }
        }

        public void RemoveNpc(string npcName)
        {
            if (string.IsNullOrEmpty(npcName)) return;
            // 找到然后移除
            foreach (INPC n in npcInScene)
            {
                if (n.gameObject.name == npcName)
                {
                    npcInScene.Remove(n);
                    n.Release();
                }
            }
        }
    }
}
