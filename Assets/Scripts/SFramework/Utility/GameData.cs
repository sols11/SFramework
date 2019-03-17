/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：存档包含的数据（包括游戏存档和设置存档）
    作用：持久化数据
    使用：根据自己需要保存的数据来编写
    补充：
History:
----------------------------------------------------------------------------*/

using UnityEngine;
using System.Collections.Generic;

namespace SFramework
{
    /// <summary>
    /// Xml存档的数据对象
    /// 读取引用类型的数据从GameData获取比较保险，读取值类型数据则从CurrentPlayer获取
    /// </summary>
    public class GameData
    {
        // 密钥,用于防止拷贝存档
        public string key;

        // 下面是添加需要储存的内容，目前都是Player的内容
        // Player的属性由装备和初始值决定，无需记录
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Gold { get; set; }
        // 技能开关
        public bool CanAttack { get; set; }
        // 装备和道具
        public IEquip[] Fit { get; set; }
        public int[] PropNum { get; set; }     // 和Player使用同一地址
        // 任务数据
        public List<TaskData> TasksData { get; set; }

        public GameData()
        {
            //构造函数，设置默认存档
            key = SystemInfo.deviceUniqueIdentifier;    //设定密钥，根据具体平台设定// 
            // Player
            Name = "我";
            Rank = 1;
            Gold = 100;

            CanAttack = true;
            // 初始装备
            Fit = new IEquip[6];
            Fit[(int)FitType.Weapon] = UnityHelper.FindDic(GameMainProgram.Instance.dataBaseMgr.equipDict, "太刀");
            // 初始背包，1~31值为-1
            PropNum = new int[32];
            for (int i = 1; i < 32; i++)
                PropNum[i] = -1;
            PropNum[0] = 3;
            // 其他
            TasksData = new List<TaskData>();
        }
    }

    /// <summary>
    /// 存储设置数据。
    /// 结束场景时存档，加载场景时读档（BuildPlayer），但是只有第一次读档是从文件中读档，之后直接读取运行时存档即可
    /// </summary>
    public class SettingData
    {
        // 因为不能存float，所以int放大100倍
        public int MusicVolume { get; set; }
        public int SoundVolume { get; set; }
        public bool IsChinese { get; set; } // 语言变更后需要重新打开UI才能生效，但是一般打开Setting时，其他UI都没打开

        public SettingData()
        {
            MusicVolume = 100;
            SoundVolume = 100;
            IsChinese = true;
        }
    }
}
