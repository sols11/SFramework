using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 角色控制系统
    /// 负责角色的创建，管理，删除
    /// </summary>
	public class PlayerMgr : IGameMgr
	{
        public IPlayer CurrentPlayer { get; private set; } //切换场景时不要消除引用
        public bool CanInput { get; set; }

        public PlayerMgr(GameMainProgram gameMain):base(gameMain)
		{			
		}

        public override void Initialize()
        {
            if (CurrentPlayer != null)
                CurrentPlayer.Initialize();
        }

        public override void Release() {
            // Destroy
            if (CurrentPlayer != null)
            {
                CurrentPlayer.Release();
                GameObject.Destroy(CurrentPlayer.GameObjectInScene);
                CurrentPlayer = null;
            }
            else
                Debug.Log("无CurrentPlayer可以销毁！");
        }

		public override void Update() {
            if(CurrentPlayer!=null&& CanInput)
                CurrentPlayer.Update();
		}

		public override void FixedUpdate() {
            if(CurrentPlayer!=null&& CanInput)
                CurrentPlayer.FixedUpdate();
		}

	    public void BuildPlayer(Vector3 position,Quaternion quaternion)
	    {
            //if(CurrentPlayer==null)
	            //SetCurrentPlayer(new DreamKeeper.PlayerYuka(GameMainProgram.Instance.resourcesMgr.
                    //LoadAsset(@"Players\BlockBattle", false,position, quaternion)));
	        //gameMain.gameDataMgr.Load(CurrentPlayer);   // 读档
	    }

        public void SetCurrentPlayer(IPlayer player)
		{
             CurrentPlayer = player;
             CurrentPlayer.Initialize();    // 设置Player时进行初始化
        }

        public void SetPlayerPos(Vector3 pos)
        {
             CurrentPlayer.GameObjectInScene.transform.position = pos;
        }
    }
}
