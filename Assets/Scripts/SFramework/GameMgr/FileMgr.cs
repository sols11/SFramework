/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：负责数据库、配置文件、设置、存档等文件的读取和写入，提供序列化和反序列化功能
    作用：方便开发者序列化和反序列化文件
    使用：调用接口即可。需要配合LitJson插件，框架中已存放
    补充：通过调用JsonMapper.ToJson(_object); 将object序列化为string
           调用JsonMapper.ToObject<T>(_string); 将string反序列化为T
History:
    TODO：将所有已加载的文件缓存下来，选择是否回收
    TODO：增加更多文件类型的支持
----------------------------------------------------------------------------*/

using System.IO;    // StreamWriter, StreamReader FileInfo
using LitJson;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 文件管理者
    /// </summary>
    public class FileMgr : IGameMgr
    {
        private string dataBasePath = Application.streamingAssetsPath + @"\DataBase\";

        public FileMgr(GameMainProgram gameMain):base(gameMain)
		{
        }

        /// <summary>
        /// 用StreamWriter将string写入文件
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_object"></param>
        public void CreateJsonDataBase(string _name, object _object)
        {
            if (File.Exists(dataBasePath + _name + ".json"))   // 检查存在
            {
                File.Delete(dataBasePath + _name + ".json");
                Debug.Log(_name+".json已存在，自动覆盖");
            }
            StreamWriter sw = File.CreateText(dataBasePath + _name + ".json");  //如果重名就会创建失败
            sw.Write(SerializeObject(_object));
            sw.Close();
        }

        /// <summary>
        /// 用StreamReader将读取文件并转为string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_name"></param>
        /// <returns></returns>
        public T LoadJsonDataBase<T>(string _name)
        {
            if (!File.Exists(dataBasePath + _name + ".json")) // 检查存在
            {
                Debug.LogError("Json文件不存在");
                return default(T);
            }
            StreamReader sr = File.OpenText(dataBasePath + _name + ".json");
            string data = sr.ReadToEnd();
            sr.Close();
            if (data.Length > 0)    // 检查非空
                return DeserializeObject<T>(data);
            return default(T);
        }

        public void CreateJsonSaveData(string _name, object _object)
        {
            if (File.Exists(_name + ".json"))   // 检查存在
            {
                File.Delete(_name + ".json");
                //Debug.Log(_name + ".json已存在，自动覆盖");
            }
            StreamWriter sw = File.CreateText(_name + ".json");  //如果重名就会创建失败
            sw.Write(SerializeObject(_object));
            sw.Close();
        }
        
        public T LoadJsonSaveData<T>(string _name)
        {
            if (!File.Exists(_name + ".json"))   // 检查存在
                return default(T);
            StreamReader sr = File.OpenText(_name + ".json");
            string data = sr.ReadToEnd();
            sr.Close();
            if (data.Length > 0)    // 检查非空
                return DeserializeObject<T>(data);
            return default(T);
        }

        /// <summary>
        /// 将对象序列化为string
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        private string SerializeObject(object _object)
        {
            string serializedString = string.Empty;
            serializedString = JsonMapper.ToJson(_object);
            return serializedString;
        }

        /// <summary>
        /// 将string反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_string"></param>
        /// <returns></returns>
        private T DeserializeObject<T>(string _string)
        {
            T deserializedObject = default(T);  //T的默认值
            deserializedObject = JsonMapper.ToObject<T>(_string);
            return deserializedObject;
        }

    }
}