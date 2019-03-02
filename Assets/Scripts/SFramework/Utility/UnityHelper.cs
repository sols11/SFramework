/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： Unity 帮助脚本
 *    Description: 
 *           功能： 提供程序用户一些常用的功能方法实现，方便程序员快速开发。
 *                  
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
	public class UnityHelper : MonoBehaviour {
        
        /// <summary>
        /// 查找子节点对象
        /// 内部使用“递归算法”
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="chiildName">查找的子对象名称</param>
        /// <returns></returns>
	    public static Transform FindTheChildNode(GameObject goParent,string childName)
        {
            Transform searchTrans = null;                   //查找结果Transform

            searchTrans=goParent.transform.Find(childName);
            if (searchTrans==null)
            {
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans=FindTheChildNode(trans.gameObject, childName);
                    if (searchTrans!=null)
                        return searchTrans;
                }            
            }
            return searchTrans;
        }

        /// <summary>
        /// 获取子节点（对象）脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
	    public static T GetTheChildNodeComponetScripts<T>(GameObject goParent,string childName) where T:Component
        {
            Transform searchTranformNode = null;            //查找特定子节点

            searchTranformNode = FindTheChildNode(goParent, childName);
            if (searchTranformNode != null)
            {
                return searchTranformNode.gameObject.GetComponent<T>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 给子节点添加脚本,添加时会删除已有的重复脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
	    public static T AddChildNodeCompnent<T>(GameObject goParent,string childName) where T:Component
        {
            Transform searchTranform = null;                //查找特定节点结果

            //查找特定子节点
            searchTranform = FindTheChildNode(goParent, childName);
            //如果查找成功，则考虑如果已经有相同的脚本了，则先删除，否则直接添加。
            if (searchTranform!=null)
            {
                //如果已经有相同的脚本了，则先删除
                T[] componentScriptsArray=searchTranform.GetComponents<T>();
                for (int i = 0; i < componentScriptsArray.Length; i++)
                {
                    if (componentScriptsArray[i]!=null)
                    {
                        Destroy(componentScriptsArray[i]);
                    }
                }
                return searchTranform.gameObject.AddComponent<T>();
            }
            else
            {
                return null;
            }
            //如果查找不成功，返回Null.
        }

        /// <summary>
        /// 给子节点添加父对象，且reset
        /// </summary>
        /// <param name="parents">父对象的方位</param>
        /// <param name="child">子对象的方法</param>
	    public static void AddChildNodeToParentNode(Transform parents, Transform child)
	    {
            child.SetParent(parents,false);
	        child.localPosition = Vector3.zero;
	        child.localScale = Vector3.one;
	        child.localEulerAngles = Vector3.zero;
	    }

        /// <summary>
        /// 使用字典查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="_dic"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        public static TValue FindDic<TKey, TValue>(Dictionary<TKey, TValue> _dic, TKey _key)
        {
            if (_dic == null || _dic.Count == 0)
            {
                Debug.LogError(_dic + "未赋值，不可使用");
                return default(TValue);
            }
            if (!_dic.ContainsKey(_key))
            {
                Debug.LogError(_dic + "不存在Key:" + _key);
                return default(TValue);
            }
            else
                return _dic[_key];
        }
    }
}