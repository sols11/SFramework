using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
	/// <summary>
	/// 传参给Enemy受伤的属性类
	/// </summary>
	public class EnemyHurtAttr
	{
		public int Attack { get; set; }
		public float VelocityForward { get; set; }
		public float VelocityVertical { get; set; }
        public Vector3 TransformForward { get; set; }

        public EnemyHurtAttr()
		{	}

		public void ModifyAttr(int attack, float velocityForward, float velocityVertical,Vector3 transformForward)
		{
			Attack = attack;
            VelocityForward = velocityForward;
            VelocityVertical = velocityVertical;
            TransformForward = transformForward;
		}
	}
}