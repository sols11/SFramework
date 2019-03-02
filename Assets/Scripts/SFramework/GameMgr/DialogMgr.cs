using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace SFramework
{
    /// <summary>
    /// 对话管理
    /// 只是保存所有对话
    /// 对话实质实现由UIDialog控制
    /// </summary>
    public class DialogMgr:IGameMgr
    {
        private Dictionary<string,List<string>> dicTalks;

        public DialogMgr(GameMainProgram gameMain) : base(gameMain)
        {
        }

        public override void Awake()
        {
            //dicTalks= gameMain.fileMgr.LoadJsonDataBase<Dictionary<string, List<string>>>("Dialog");
        }

        public void StartDialog(string key,UnityAction dialogCompleteAction=null)
        {
            List<string> talks = UnityHelper.FindDic(dicTalks, key);

            if (talks == null||talks.Count==0)
            {
                Debug.LogError("对话内容为空");
                return;
            }
            UIDialog.StartDialog(talks,dialogCompleteAction);
        }


    }
}
