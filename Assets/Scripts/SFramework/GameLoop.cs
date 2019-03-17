/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：游戏的入口类，继承了MonoBehaviour
    作用：控制场景状态机SceneStateController的初始状态、更新及设置起始场景
    使用：在每个场景，我们只需要创建一个挂载了GameLoop脚本的物体，并选择需要动态加载的场景状态，就可以测试目标场景，而无需修改代码。这使得场景测试更加快捷
    补充：项目主逻辑只用这一个Monobehaviour脚本驱动，并且设置了DontDestroyOnLoad，使之在每个场景下都保持为单例
History:
----------------------------------------------------------------------------*/

using UnityEngine;

namespace SFramework
{
	/// <summary>
	/// 游戏主循环
	/// </summary>
	public class GameLoop : MonoBehaviour
	{
        private static GameLoop _instance;

        public static GameLoop Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameLoop>();

                    if (_instance == null)
                        _instance = new GameObject("GameLoop").AddComponent<GameLoop>();
                }
                return _instance;
            }
        }

        public SceneStateController sceneStateController = new SceneStateController();

        [SerializeField]
        private SceneState sceneState;

        void Awake()
        {
            if (_instance == null)
                _instance = GetComponent<GameLoop>();
            else if (_instance != GetComponent<GameLoop>())
            {
                Debug.LogWarningFormat("There is more than one {0} in the scene，auto inactive the copy one.", typeof(GameLoop).ToString());
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

		void Start()
		{
            // 要测试的场景，只需要在Inspector中设置就行了
            sceneStateController.SetState(sceneState, false);
		}

        void FixedUpdate()
        {
            //物理相关的处理
            sceneStateController.FixedUpdate();
        }

        void Update()
		{
			sceneStateController.StateUpdate();
		}
	}
}
