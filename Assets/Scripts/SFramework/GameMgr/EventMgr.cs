/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：事件管理器
    作用：集中管理游戏中的UnityEvent事件和EventHandler事件，注册、移除、调用事件均通过这个系统
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;   // EventArgs需要

namespace SFramework
{
    /// <summary>
    /// 事件系统
    /// </summary>
    public class EventMgr : IGameMgr
    {
        public delegate void EventHandler(object sender, EventArgs e);

        private Dictionary<EventName, UnityEvent> eventDictionary;
        private Dictionary<EventHandlerName, EventHandler> eventHandlerDictionary;

        public EventMgr(GameMainProgram gameMain) : base(gameMain)
        {
            eventDictionary = new Dictionary<EventName, UnityEvent>();
        }

        /// <summary>
        /// 注册监听事件，如果字典中不存在那么创建
        /// </summary>
        /// <param name="_eventName"></param>
        /// <param name="_listener"></param>
        public void StartListening(EventName _eventName, UnityAction _listener)
        {
            UnityEvent _thisEvent = null;
            if (eventDictionary.TryGetValue(_eventName, out _thisEvent))
            {
                _thisEvent.AddListener(_listener);
            }
            else
            {
                _thisEvent = new UnityEvent();
                _thisEvent.AddListener(_listener);
                eventDictionary.Add(_eventName, _thisEvent);
            }
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="_eventName"></param>
        /// <param name="_listener"></param>
        public void StopListening(EventName _eventName, UnityAction _listener)
        {
            UnityEvent _thisEvent = null;
            if (eventDictionary.TryGetValue(_eventName, out _thisEvent))
            {
                _thisEvent.RemoveListener(_listener);
            }
        }

        /// <summary>
        /// 触发指定事件
        /// </summary>
        /// <param name="_eventName"></param>
        public void InvokeEvent(EventName _eventName)
        {
            UnityEvent _thisEvent = null;
            if (eventDictionary.TryGetValue(_eventName, out _thisEvent))
            {
                _thisEvent.Invoke();
            }
        }

        /// <summary>
        /// 注册监听事件，如果字典中不存在那么创建
        /// </summary>
        /// <param name="_eventName"></param>
        /// <param name="_listener"></param>
        public void StartListening(EventHandlerName _eventName, EventHandler _listener)
        {
            EventHandler _thisEvent = null;
            if (eventHandlerDictionary.TryGetValue(_eventName, out _thisEvent))
            {
                _thisEvent+=_listener;
            }
            else
            {
                _thisEvent=new EventHandler(_listener);
                eventHandlerDictionary.Add(_eventName, _thisEvent);
            }
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="_eventName"></param>
        /// <param name="_listener"></param>
        public void StopListening(EventHandlerName _eventName, EventHandler _listener)
        {
            EventHandler _thisEvent = null;
            if (eventHandlerDictionary.TryGetValue(_eventName, out _thisEvent))
            {
                _thisEvent-=_listener;
            }
        }

        /// <summary>
        /// 触发指定事件
        /// </summary>
        /// <param name="_eventName"></param>
        public void InvokeEvent(EventHandlerName _eventName,object sender,EventArgs e)
        {
            EventHandler _thisEvent = null;
            if (eventHandlerDictionary.TryGetValue(_eventName, out _thisEvent))
            {
                _thisEvent(sender,e);
            }
        }

    }
}