using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    ///+ 可在检视面板设置的属性
    ///+ 武器战斗系统所需属性
    ///会细分为Player和Enemy两种，两者的武器可以互相转换
    /// </summary>
    public class IWeaponMono : MonoBehaviour
    {
        //可在检视面板设置的属性
        public float m_AttackFactor = 1;  //攻击力系数，不同的攻击方式改变的是攻击力系数而不是基础攻击力
        public float m_VelocityForward = 6;
        public float m_VelocityVertical;
        public bool canDefeatedFly = false; // 攻击后是否击飞
        protected ResourcesMgr resourcesMgr;

        public int BasicAttack { get; set; }
        public int Crit { get; set; }
        public SpecialAbility Special { get; set; }
        //武器战斗系统所需属性
        public float AttackFactor { get { return m_AttackFactor; } set { m_AttackFactor = value; } }
        public float VelocityForward { get { return m_VelocityForward; } set { m_VelocityForward = value; } }
        public float VelocityVertical { get { return m_VelocityVertical; } protected set { m_VelocityVertical = value; } }
        public Vector3 TransformForward { get; protected set; }
        public Collider WeaponCollider { get; protected set; }
        protected float RealAttack { get; set; }

        public virtual void Initialize()
        {
            resourcesMgr = GameMainProgram.Instance.resourcesMgr;
            WeaponCollider = GetComponent<Collider>();
            AttackFactor = 1;
            VelocityForward = 7;
        }

        public virtual void Release() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        protected virtual void OnTriggerEnter(Collider col) { }

    }
}