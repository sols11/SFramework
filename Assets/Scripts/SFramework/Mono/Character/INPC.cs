using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// NPC基类，NPC都会说话，无声音，不移动，动态添加到场景，以Prefab形式保存
    /// </summary>
    public abstract class INPC:MonoBehaviour
    {
        public Animator AnimatorComponent { get; protected set; }

        protected string dialogKey = string.Empty;
        protected float maxDistance = 2;
        protected IPlayer player;
        protected Transform playerTransform;
        protected GameMainProgram gameMainProgram;

        /// <summary>
        /// 初始化和释放由NpcMgr调用
        /// </summary>
        public virtual void Initialize()
        {
            gameMainProgram=GameMainProgram.Instance;
            AnimatorComponent = gameObject.GetComponent<Animator>();
            // Npc的生成应该在Player之后
            player = GameMainProgram.Instance.playerMgr.CurrentPlayer;
            if (player != null)
                playerTransform = player.GameObjectInScene.transform;
            else
                Debug.LogError("未能获取到Player");
        }

        public virtual void Release()
        {
            Destroy(gameObject);
        }

        public virtual void OnUpdate()
        {
            if (player == null)
            {
                player = gameMainProgram.playerMgr.CurrentPlayer;
                if (player != null)
                    playerTransform = player.GameObjectInScene.transform;
            }
            if (!UIDialog.IsTalking)
            {
                if (playerTransform == null)
                    return;
                if (Vector3.Distance(playerTransform.position, transform.position) > maxDistance)
                    return;
                //if(gameMainProgram.courseMgr.normalMenuOpen)   // 如果暂停菜单打开着，那么不对话
                    return;
                // 在半径为maxDistance的圆范围内时检测输入
                if (Input.GetButtonDown("Attack1"))
                {
                    // 对话并禁止移动
                    UIDialog.IsTalking = true;
                    gameMainProgram.playerMgr.CurrentPlayer.CanMove = false;
                    //转向，懒得做，因为禁止移动做的都不好 gameMainProgram.playerMgr.CurrentPlayer.GameObjectInScene.transform.rotation
                    gameMainProgram.dialogMgr.StartDialog(dialogKey, OnDialogComplete);
                }
            }
        }

        /// <summary>
        /// 对话结束时的事件
        /// </summary>
        protected virtual void OnDialogComplete()
        {
            UIDialog.IsTalking = false;
        }

    }
}
