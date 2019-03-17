using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SFramework
{
    /// <summary>
    /// + Enemy的所有属性
    /// </summary>
    public class IEnemy : ICharacter
	{
        protected EnemyMediator EnemyMedi { get; set; }

        public IEnemy(GameObject gameObject):base(gameObject)
		{
			if (GameObjectInScene != null)
			{
				animator = GameObjectInScene.GetComponent<Animator>();
				Rgbd = GameObjectInScene.GetComponent<Rigidbody>();
                // 关联中介者
                EnemyMedi = new EnemyMediator(this);
                EnemyMedi.Initialize();
            }
		}

        public override void Release()
        {
            EnemyMedi.EnemyMono.Release();    // 先释放再销毁
            if (GameObjectInScene != null)
                GameObject.Destroy(GameObjectInScene);
        }
        public virtual EnemyAction Hurt(EnemyHurtAttr enemyHurtAttr)
		{
            return EnemyAction.Hurt;
		}

        public virtual void WhenPlayerDead()
        {
        }
	}
}
