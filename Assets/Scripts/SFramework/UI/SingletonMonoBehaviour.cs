/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：框架中提供了两个单例模式的模板，此类为继承MonoBehaviour的模板
    作用：易于实现单例模式
    使用：继承该单例类即可
    补充：注意单例类的构造函数必须是private的，这样才能确保类只有一个对象，不让外部类实例化该类
          单例类不能够被继承
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 单例模板，要求T（类）继承MonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        /// <summary>
        /// 外界通过属性调用单例以及创建单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

					if (_instance == null)
					{
						_instance = new GameObject("single_" + typeof(T)).AddComponent<T>();
						print("生成SingletonMonoBehaviour" + typeof(T).ToString());
					}
				}
                return _instance;
            }
        }
        protected virtual void Init()
        {
            DontDestroyOnLoad(gameObject);//切换场景不销毁
            hideFlags = HideFlags.DontSave;
        }
        /// <summary>
        /// awake时进行单例检测和初始化
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
                _instance = GetComponent<T>();
            else if (_instance != GetComponent<T>())
            {
                Debug.LogWarningFormat("场景中超过1个{0}，关闭新增的物体", typeof(T).ToString());
                gameObject.SetActive(false);
                return;
            }
            Init();
        }
    }
}