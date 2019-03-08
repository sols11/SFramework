using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 协程管理器，让非Mono也能使用Mono方法
    /// </summary>
    public class CoroutineMgr : MonoBehaviour
    {
        private static CoroutineMgr _instance;

        public static CoroutineMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CoroutineMgr>();

                    if (_instance == null)
                        _instance = new GameObject("CoroutineMgr").AddComponent<CoroutineMgr>();
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
                _instance = GetComponent<CoroutineMgr>();
            else if (_instance != GetComponent<CoroutineMgr>())
            {
                Debug.LogWarningFormat("There is more than one {0} in the scene，auto inactive the copy one.", typeof(CoroutineMgr).ToString());
                gameObject.SetActive(false);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        //public new void CancelInvoke()
        //{
        //    base.CancelInvoke();
        //}

        //public new void CancelInvoke(string methodName)
        //{
        //    base.CancelInvoke(methodName);
        //}

        //public new void Invoke(string methodName,float time)
        //{
        //    base.Invoke(methodName,time);
        //}

        //public new void InvokeRepeating(string methodName, float time, float repeatRate)
        //{
        //    base.InvokeRepeating(methodName, time, repeatRate);
        //}

        //public new bool IsInvoking()
        //{
        //    return base.IsInvoking();
        //}

        //public new bool IsInvoking(string methodName)
        //{
        //    return base.IsInvoking(methodName);
        //}

        ///其实这里不封装也能调用，因为继承了
        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return base.StartCoroutine(routine);
        }

        public new void StopCoroutine(IEnumerator routine)
        {
            base.StopCoroutine(routine);
        }

        public new void StopAllCoroutines()
        {
            base.StopAllCoroutines();
        }

        public new void StopCoroutine(Coroutine routine)
        {
            base.StopCoroutine(routine);
        }
    }
}