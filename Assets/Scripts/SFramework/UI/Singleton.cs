using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 单例模板，不继承Mono，new() 约束可以让编译器知道：提供的任何类型参数都必须具有可访问的无参数（或默认）构造函数。
	/// 注意单例类的构造函数必须是private的，这样才能确保类只有一个对象，不让外部类实例化该类
	/// 单例类不能够被继承
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
					//不继承mono的单例不一定要挂载到go上
					Debug.Log("生成Singleton" + typeof(T).ToString());
                }
                return _instance;
            }
        }

    }
}