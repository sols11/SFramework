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
        //密钥,用于防止拷贝存档// 
        public string key;

        //下面是添加需要储存的内容，目前都是Player的内容// 
        // Player的属性由装备和初始值决定，无需记录
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Gold { get; set; }
        public int MedicineID { get; set; }
        // 技能开关
        public bool CanAvoid { get; set; }
        public bool CanAttack2 { get; set; }
        public bool CanAttack3 { get; set; }
        public bool CanDush { get; set; }
        // 装备和道具
        public IEquip[] Fit { get; set; }
        public int[] PropNum { get; set; }  // 和Player使用同一地址
        // 商店数据
        public bool[] HasProp { get; set; }    // 存储商店售卖哪些道具

        public GameData()
        {
            //构造函数，设置默认存档
            key = SystemInfo.deviceUniqueIdentifier;    //设定密钥，根据具体平台设定// 
            // Player
            Name = "我";
            Rank = 1;
            Gold = 100;

            // 【以后需要删掉】
            CanAvoid = true;
            CanAttack2 = true;
            CanAttack3 = true;
            CanDush = true;
            // 【以后需要删掉】

            // 初始装备
            Fit = new IEquip[6];
            Fit[(int)FitType.Weapon]= UnityHelper.FindDic(GameMainProgram.Instance.dataBaseMgr.dicEquip, "太刀");
            // 初始背包，1~31值为-1
            PropNum = new int[32];
            for (int i = 1; i < 32; i++)
                PropNum[i] = -1;
            PropNum[0] = 3;
            MedicineID = 0;
            // 其他
            HasProp=new bool[32];
            HasProp[0] = true;
            HasProp[1] = true;
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
