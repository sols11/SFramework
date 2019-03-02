using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 多语言本地化
    /// 从设置获取当前语言，存储不同语言显示的字符，以及更换游戏中的字体
    /// </summary>
    public class LanguageMgr:IGameMgr
    {
        private SettingData SettingSaveData { get; set; }    // 存储
        private Dictionary<string, string> dicLauguageCN;
        private Dictionary<string, string> dicLauguageEN;
        private Font fontCN;
        private Font fontEN;

        public LanguageMgr(GameMainProgram gameMain):base(gameMain){
            dicLauguageCN = new Dictionary<string, string>();
            dicLauguageEN = new Dictionary<string, string>();
        }

        public override void Awake()
        {
            SettingSaveData = gameMain.gameDataMgr.SettingSaveData;
            //LoadLanguageCache
            dicLauguageCN = gameMain.fileMgr.LoadJsonDataBase<Dictionary<string, string>>("Language_CN");
            dicLauguageEN = gameMain.fileMgr.LoadJsonDataBase<Dictionary<string, string>>("Language_EN");
            fontCN = gameMain.resourcesMgr.LoadResource<Font>(@"Fonts\FZYH");
            fontEN = gameMain.resourcesMgr.LoadResource<Font>(@"Fonts\ARJULIAN");
        }

        /// <summary>
        /// 到显示文本信息
        /// </summary>
        /// <param name="stringId">语言的ID</param>
        /// <returns></returns>
        public string ShowText(string stringId)
        {
            if(SettingSaveData.IsChinese)
               return UnityHelper.FindDic(dicLauguageCN, stringId);
            else
               return UnityHelper.FindDic(dicLauguageEN, stringId);
        }

        public Font GetFont(int fontChoose = 0)
        {
            if (fontChoose == 1)
                return fontCN;
            else if (fontChoose == 2)
                return fontEN;
            else if (SettingSaveData.IsChinese)
                return fontCN;
            else
                return fontEN;

        }

    }
}
