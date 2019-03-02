using System.IO;    //StreamWriter,StreamReader FileInfo
using LitJson;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 负责Json数据库、配置文件、设置存档的读取和写入，提供Json序列化和反序列化
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