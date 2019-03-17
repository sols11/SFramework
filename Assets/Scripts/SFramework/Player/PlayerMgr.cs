/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：角色控制系统
    作用：负责角色的创建，管理，删除
    使用：调用接口
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 角色控制系统
    /// </summary>
	public class PlayerMgr : IGameMgr
	{
        public IPlayer CurrentPlayer { get; private set; }  // 切换场景时不要消除引用
        public bool CanInput { get; set; }

        public PlayerMgr(GameMainProgram gameMain):base(gameMain)
		{			
		}

        public override void Initialize()
        {
            if (CurrentPlayer != null)
                CurrentPlayer.Initialize();
        }

        public override void Release()
        {
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

        public override void FixedUpdate()
        {
            if (CurrentPlayer != null && CanInput)
                CurrentPlayer.FixedUpdate();
        }

        public override void Update()
        {
            if (CurrentPlayer != null && CanInput)
                CurrentPlayer.Update();
        }

	    public void BuildPlayer(Vector3 position, Quaternion quaternion)
	    {
            if (CurrentPlayer == null)
                SetCurrentPlayer(new ProjectScript.PlayerMain(GameMainProgram.Instance.resourcesMgr.
                    LoadAsset(@"Players\Player", false, position, quaternion)));
            gameMain.gameDataMgr.Load(CurrentPlayer);   // 读档
        }

        public void SetCurrentPlayer(IPlayer player)
		{
             CurrentPlayer = player;
             CurrentPlayer.Initialize();    // 设置Player时进行初始化
        }

        public void SetPlayerPosition(Vector3 pos)
        {
             CurrentPlayer.GameObjectInScene.transform.position = pos;
        }
    }
}
