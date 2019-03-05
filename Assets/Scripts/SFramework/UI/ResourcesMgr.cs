/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：负责场景中游戏对象的加载，对象全部来源于Resources文件夹，可使用缓冲池技术
    作用：封装了Unity的Resources资源加载功能并进行管理，可以动态加载所有的资源。使用了缓冲池，智能清理等技术，提供哈希表和字典两个不同的缓冲容器，提供加载和克隆两个不同的资源操作。
    使用：继承该单例类即可
    补充：智能清理：若缓冲池的GO还没有active=false时创建新GameObject，并在一定时间后销毁。GO也可以选择不使用这里提供的缓冲池
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SFramework
{
    /// <summary>
    /// 资源管理者
    /// </summary>
    public class ResourcesMgr : IGameMgr
    {
        private Hashtable ht = null;                        //容器键值对集合
        private Dictionary<string,GameObject> dicGO;        //克隆物体存储字典
        private float autoDestroyTime = 10;

        public ResourcesMgr(GameMainProgram gameMain):base(gameMain)
		{
            //字段初始化
            ht = new Hashtable();
            dicGO = new Dictionary<string, GameObject>();
        }

        public override void Release()
        {
            ht.Clear();
            dicGO.Clear();
        }

        /// <summary>
        /// 读取资源（是否要缓冲到hash表中）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isCatch">是否加入缓冲池</param>
        /// <returns></returns>
        public T LoadResource<T>(string path, bool isCatch=false) where T : Object
        {
            if (ht.Contains(path))
            {
                Debug.Log("Contain");
                return ht[path] as T;
            }

            T TResource = Resources.Load<T>(path);
            if (TResource == null)
            {
                Debug.LogError(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查。 path=" + path);
            }
            else if (isCatch)
            {
                ht.Add(path, TResource);
            }

            return TResource;
        }
        /// <summary>
        /// 按预制位置克隆，如UI
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public GameObject LoadAsset(string path, bool isCatch=false)
        {
            if (dicGO.ContainsKey(path))
            {
                Debug.Log("ContainGO");
                dicGO[path].SetActive(true);
                return dicGO[path];
            }
            GameObject goObj = Resources.Load<GameObject>(path);
            if (goObj != null)
            {
                goObj = GameObject.Instantiate(goObj);
                if (isCatch)
                    dicGO.Add(path, goObj);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
            }
            return goObj;
        }

        /// <summary>
        /// 读取资源并克隆（带对象缓冲技术）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch">是否加入缓冲池</param>
        /// <returns></returns>
        public GameObject LoadAsset(string path, bool isCatch,Vector3 _position,Quaternion _rotation)
        {
            if (dicGO.ContainsKey(path))
            {
                dicGO[path].transform.position = _position;
                dicGO[path].transform.rotation = _rotation;
                if (dicGO[path].activeSelf == false)
                {
                    dicGO[path].SetActive(true);
                    return dicGO[path];
                }
                else
                {
                    GameObject _goObj = GameObject.Instantiate(dicGO[path], _position, _rotation);
                    GameObject.Destroy(_goObj, autoDestroyTime); //10s后清理掉多出来的GO
                    return _goObj;
                }
            }
            GameObject goObj = Resources.Load<GameObject>(path);
            if (goObj != null)
            {
                goObj = GameObject.Instantiate(goObj, _position, _rotation);
                if(isCatch)
                    dicGO.Add(path, goObj);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
            }
            return goObj;
        }
    }//Class_end
}

