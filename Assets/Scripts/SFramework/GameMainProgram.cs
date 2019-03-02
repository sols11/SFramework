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

        public ResourcesMgr resourcesMgr;
        public FileMgr fileMgr;
        public DataBaseMgr dataBaseMgr;
        public GameDataMgr gameDataMgr;
        public LanguageMgr languageMgr;
        //public CoroutineMgr coroutineMgr; // 继承Mono的Mgr
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

        /// 注意单例类的构造函数必须是private的，这样才能确保类只有一个对象，不让外部类实例化该类
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
        /// 初次构造后的初始化(通常需要用到其他Mgr)
        /// </summary>
        public void Awake()
        {
            // 注意Awake不能放在构造函数内执行，因为这将导致主程序未构造完毕就开始使用，破坏了单例的存在，造成栈溢出错误
            //dataBaseMgr.Awake();
            //gameDataMgr.Awake();
            //languageMgr.Awake();
            uiManager.Awake();
            //audioMgr.Awake();
            //dialogMgr.Awake();
            //npcMgr.Awake();
        }

        // 有要执行的方法再添加到这里
        public void Initialize()
		{
            // 场景重新加载
            playerMgr.Initialize();
			enemyMgr.Initialize();
            uiManager.Initialize();
            uiMaskMgr.Initialize();
            courseMgr.Initialize();
		}

		public void Release()
		{
            // 场景切换
            resourcesMgr.Release();
			playerMgr.Release();
			enemyMgr.Release();
            //npcMgr.Release();
            uiManager.Release();
            uiMaskMgr.Release();
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
        public void FixedUpdate()
		{
            resourcesMgr.FixedUpdate();
            playerMgr.FixedUpdate();
			enemyMgr.FixedUpdate();
            uiManager.FixedUpdate();
		}
	}
}
