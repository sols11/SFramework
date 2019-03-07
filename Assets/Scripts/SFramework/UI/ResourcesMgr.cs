/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：封装了Unity的Resources资源加载功能并进行管理，可以动态加载所有的资源。使用了缓冲池，智能清理等技术，
          提供哈希表和字典两个不同的缓冲容器，提供加载和克隆两个不同的资源操作。
    作用：负责场景中游戏对象的动态加载，加载的对象来源于Resources文件夹，被加载的对象可使用缓冲池技术。
    使用：调用接口即可
    补充：智能清理：若缓冲池的GameObject还没有active==false时创建新GameObject，并在一定时间后销毁。加载GameObject时也可以选择不使用这里提供的缓冲池
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 资源管理者
    /// </summary>
    public class ResourcesMgr : IGameMgr
    {
        private Hashtable resourcesHashTable = null;                 // 容器键值对集合,存放已加载的资源
        private Dictionary<string,GameObject> gameObjectDict;        // 克隆物体存储字典
        private float autoDestroyTime = 10;

        public ResourcesMgr(GameMainProgram gameMain) : base(gameMain)
        {
            // 字段初始化
            resourcesHashTable = new Hashtable();
            gameObjectDict = new Dictionary<string, GameObject>();
        }

        public override void Release()
        {
            resourcesHashTable.Clear();
            gameObjectDict.Clear();
        }

        /// <summary>
        /// 读取资源（是否要缓冲到hash表中）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isCatch">是否加入缓冲池</param>
        /// <returns></returns>
        public T LoadResource<T>(string path, bool isCatch = false) where T : Object
        {
            if (resourcesHashTable.Contains(path))
            {
                Debug.Log("Contain");
                return resourcesHashTable[path] as T;
            }

            T TResource = Resources.Load<T>(path);
            if (TResource == null)
            {
                Debug.LogError(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查。 path=" + path);
            }
            else if (isCatch)
            {
                resourcesHashTable.Add(path, TResource);
            }

            return TResource;
        }

        /// <summary>
        /// 按预制位置克隆，如UI
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public GameObject LoadAsset(string path, bool isCatch = false)
        {
            if (gameObjectDict.ContainsKey(path))
            {
                Debug.Log("ContainGameObject");
                gameObjectDict[path].SetActive(true);
                return gameObjectDict[path];
            }
            GameObject gameObjectLoaded = Resources.Load<GameObject>(path);
            if (gameObjectLoaded != null)
            {
                gameObjectLoaded = GameObject.Instantiate(gameObjectLoaded);
                if (isCatch)
                    gameObjectDict.Add(path, gameObjectLoaded);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
            }
            return gameObjectLoaded;
        }

        /// <summary>
        /// 读取资源并克隆（带对象缓冲技术）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch">是否加入缓冲池</param>
        /// <returns></returns>
        public GameObject LoadAsset(string path, bool isCatch,Vector3 position,Quaternion rotation)
        {
            if (gameObjectDict.ContainsKey(path))
            {
                gameObjectDict[path].transform.position = position;
                gameObjectDict[path].transform.rotation = rotation;
                // 缓冲池中已存在可用对象
                if (gameObjectDict[path].activeSelf == false)
                {
                    gameObjectDict[path].SetActive(true);
                    return gameObjectDict[path];
                }
                else
                {
                    GameObject newGameObject = GameObject.Instantiate(gameObjectDict[path], position, rotation);
                    GameObject.Destroy(newGameObject, autoDestroyTime); // 10s后清理掉多出来的GO
                    return newGameObject;
                }
            }
            GameObject gameObjectLoaded = Resources.Load<GameObject>(path);
            if (gameObjectLoaded != null)
            {
                gameObjectLoaded = GameObject.Instantiate(gameObjectLoaded, position, rotation);
                if(isCatch)
                    gameObjectDict.Add(path, gameObjectLoaded);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
            }
            return gameObjectLoaded;
        }
    } // Class_end
}

