/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：游戏主程序，单例模式，控制子系统
    作用：整合所有子系统的接口及功能，并控制所有子系统的生命周期，每个子系统的生命周期为：
    使用：可以直接调用 GameMainProgram.Instance.你需要使用的子系统
          要添加新的子系统时，需要修改该类的源码
    补充：只有主程序是Singleton单例，这样访问所有子系统都不需要使用单例。该类执行顺序：
            1. 构造函数
            2. Awake        初次构造后的初始化(通常需要用到其他Mgr)
            3. Initialize   加载新场景后的初始化，场景重新加载后也会调用
            4. FixedUpdate  固定时间的更新
            5. Update       每帧循环更新
            6. Release      场景结束时的释放
            所有子系统在切换场景时不会被销毁，只会释放需要释放的空间
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
	/// <summary>
	/// 游戏主程序，Singleton，控制子系统
	/// </summary>
	public class GameMainProgram
	{
		// Singleton单例，只要主程序是单例，那么子系统都不需要单例
		private static GameMainProgram _instance;
		public static GameMainProgram Instance
		{
			get
			{
                if (_instance == null)
                {
                    _instance = new GameMainProgram();
                    _instance.Awake();
                }
				return _instance;
			}
		}

        // 继承Mono的Mgr，不需要通过GameMainProgram调用，如CoroutineMgr
        public ResourcesMgr resourcesMgr;
        public FileMgr fileMgr;
        public DataBaseMgr dataBaseMgr;
        public GameDataMgr gameDataMgr;
        public LanguageMgr languageMgr;
        public PlayerMgr playerMgr;
        public EnemyMgr enemyMgr;
	    public NpcMgr npcMgr;
        public UIManager uiManager;
        public UIMaskMgr uiMaskMgr;
        public EventMgr eventMgr;
        public CourseMgr courseMgr;
	    public AudioMgr audioMgr;
	    public DialogMgr dialogMgr;
	    public SqlMgr sqlMgr;
	    public ThreadMgr threadMgr;

        /// <summary>
        /// 注意单例类的构造函数必须是private的，这样才能确保类只有一个对象，不让外部类实例化该类
        /// </summary>
        private GameMainProgram() {
            // 构造
            resourcesMgr = new ResourcesMgr(this);
            fileMgr = new FileMgr(this);
            dataBaseMgr = new DataBaseMgr(this);
            gameDataMgr = new GameDataMgr(this);
            languageMgr = new LanguageMgr(this);
            playerMgr = new PlayerMgr(this);
            enemyMgr = new EnemyMgr(this);
            npcMgr = new NpcMgr(this);
            uiManager = new UIManager(this);
            uiMaskMgr = new UIMaskMgr(this);
            eventMgr = new EventMgr(this);
            courseMgr = new CourseMgr(this);
            audioMgr = new AudioMgr(this);
            dialogMgr = new DialogMgr(this);
            sqlMgr = new SqlMgr(this);
            threadMgr = new ThreadMgr(this);
        }

        /// <summary>
        /// 初次构造后的初始化(Awake方法通常需要用到其他Mgr，因此需要在构造函数之后执行)
        /// </summary>
        public void Awake()
        {
            // 注意Awake不能放在构造函数内执行，因为这将导致主程序未构造完毕就开始使用，破坏了单例的存在，造成栈溢出错误
            dataBaseMgr.Awake();
            languageMgr.Awake();
            uiManager.Awake();
            audioMgr.Awake();
            dialogMgr.Awake();
            npcMgr.Awake();
            Debug.Log("框架初始化完成");
        }

        /// <summary>
        /// 每次场景加载后调用
        /// </summary>
        public void Initialize()
		{
            playerMgr.Initialize();
			enemyMgr.Initialize();
            uiManager.Initialize();
            uiMaskMgr.Initialize();
            courseMgr.Initialize();
		}

        /// <summary>
        /// 场景切换时调用
        /// </summary>
		public void Release()
		{
            resourcesMgr.Release();
			playerMgr.Release();
			enemyMgr.Release();
            //npcMgr.Release();
            uiManager.Release();
            uiMaskMgr.Release();
		}

        public void FixedUpdate()
        {
            resourcesMgr.FixedUpdate();
            playerMgr.FixedUpdate();
            enemyMgr.FixedUpdate();
            uiManager.FixedUpdate();
        }

        public void Update()
		{
            resourcesMgr.Update();
            playerMgr.Update();
			enemyMgr.Update();
            //npcMgr.Update();
            uiManager.Update();
            courseMgr.Update();
        }
	}
}
