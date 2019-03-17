/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 线程管理
    /// </summary>
    public class ThreadMgr : IGameMgr
    {
        public List<Thread> ThreadList { get; set; }

        public ThreadMgr(GameMainProgram gameMain) : base(gameMain)
        {
            ThreadList = new List<Thread>();
        }

        public override void Initialize()
        {
        }

        public override void Release()
        {
        }

        /// <summary>
        /// 创建无参线程对象
        /// </summary>
        /// <param name="fun"></param>
        public Thread CreateThread(ThreadStart fun)
        {
            Thread thread = new Thread(fun);
            ThreadList.Add(thread);
            //启动线程
            thread.Start();
            return thread;
        }

        /// <summary>
        /// 创建有参线程对象
        /// </summary>
        /// <param name="fun"></param>
        public Thread CreateThread(ParameterizedThreadStart fun)
        {
            Thread thread = new Thread(fun);
            ThreadList.Add(thread);
            //启动线程
            thread.Start();
            return thread;
        }

        /// <summary>
        /// 在线程池中创建一个后台线程
        /// </summary>
        /// <param name="callback"></param>
        public void CreateInThreadPool(WaitCallback callback)
        {
            ThreadPool.QueueUserWorkItem(callback);
        }

    }
}