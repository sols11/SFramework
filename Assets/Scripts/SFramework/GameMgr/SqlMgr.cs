/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：Mysql数据库系统
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using UnityEngine;
//
namespace SFramework
{
    /// <summary>
    /// 数据库管理工具，暂时不用
    /// </summary>
    public class SqlMgr : IGameMgr
    {
        public static bool Enable { get; set; }

        //public static MySqlConnection mySqlConnection;//连接类对象
        private static string database = "mygame";
        private static string host = "127.0.0.1";
        private static string id = "root";
        private static string pwd = "86696686";

        public SqlMgr(GameMainProgram gameMain) : base(gameMain)
        {
        }

        /*public override void Initialize()
        {
            Enable = false;
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        public static void OpenSql()
        {
            try
            {
                //string.Format是将指定的 String类型的数据中的每个格式项替换为相应对象的值的文本等效项。
                string sqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};", database, host, id, pwd, "3306");
                mySqlConnection = new MySqlConnection(sqlString);
                mySqlConnection.Open();
                Enable = true;
            }
            catch (Exception)
            {
                throw new Exception("服务器连接失败.....");
            }
        }

        public override void Release()
        {
            if (!Enable)
                return;
            Close();
        }

        /// <summary>
        /// 执行Sql语句（可以自己写语句，也可以用方法生成），将结果保存在DataSet
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        /// <returns></returns>
        public static DataSet QuerySet(string sqlString)
        {
            if (mySqlConnection.State == ConnectionState.Open)
            {
                // 输出指令
                Debug.Log("执行："+sqlString);
                DataSet ds = new DataSet();
                try
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlString, mySqlConnection);
                    mySqlDataAdapter.Fill(ds);
                }
                catch (Exception e)
                {
                    throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
                }
                return ds;
            }
            return null;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="colName">属性列</param>
        /// <param name="colType">属性类型</param>
        /// <returns></returns>
        public DataSet CreateTable(string name, string[] colName, string[] colType)
        {
            if (colName.Length != colType.Length)
            {
                throw new Exception("输入不正确：" + "columns.Length != colType.Length");
            }
            string query = "CREATE TABLE  " + name + "(" + colName[0] + " " + colType[0];
            for (int i = 1; i < colName.Length; i++)
            {
                query += "," + colName[i] + " " + colType[i];
            }
            query += ")";
            return QuerySet(query);
        }

        /// <summary>
        /// 创建具有id自增的表
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="col">属性列</param>
        /// <param name="colType">属性列类型</param>
        /// <returns></returns>
        public DataSet CreateTableAutoId(string name, string[] col, string[] colType)
        {
            if (col.Length != colType.Length)
            {

                throw new Exception("columns.Length != colType.Length");

            }

            string query = "CREATE TABLE  " + name + " (" + col[0] + " " + colType[0] + " NOT NULL AUTO_INCREMENT";

            for (int i = 1; i < col.Length; ++i)
            {

                query += ", " + col[i] + " " + colType[i];

            }

            query += ", PRIMARY KEY (" + col[0] + ")" + ")";

            //    Debug.Log(query);

            return QuerySet(query);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="items">需要查询的列</param>
        /// <param name="whereColName">查询的条件列</param>
        /// <param name="operation">条件操作符</param>
        /// <param name="value">条件的值</param>
        /// <returns></returns>
        public DataSet Select(string tableName, string[] items, string[] whereColName, string[] operation, string[] value)
        {
            if (whereColName.Length != operation.Length || operation.Length != value.Length)
            {
                throw new Exception("输入不正确：" + "col.Length != operation.Length != values.Length");
            }
            string query = "SELECT " + items[0];
            for (int i = 1; i < items.Length; i++)
            {
                query += "," + items[i];
            }
            query += "  FROM  " + tableName + "  WHERE " + " " + whereColName[0] + operation[0] + " '" + value[0] + "'";
            for (int i = 1; i < whereColName.Length; i++)
            {
                query += " AND " + whereColName[i] + operation[i] + "' " + value[i] + "'";
            }
            return QuerySet(query);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">条件：删除列</param>
        /// <param name="colsvalues">删除该列属性值所在得行</param>
        /// <returns></returns>
        public DataSet Delete(string tableName, string[] cols, string[] colsvalues)
        {
            string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

            for (int i = 1; i < colsvalues.Length; ++i)
            {

                query += " or " + cols[i] + " = " + colsvalues[i];
            }
            //  Debug.Log(query);
            return QuerySet(query);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="cols">更新列</param>
        /// <param name="colsvalues">更新的值</param>
        /// <param name="selectkey">条件：列</param>
        /// <param name="selectvalue">条件：值</param>
        /// <returns></returns>
        public DataSet UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
        {

            string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

            for (int i = 1; i < colsvalues.Length; ++i)
            {

                query += ", " + cols[i] + " =" + colsvalues[i];
            }

            query += " WHERE " + selectkey + " = " + selectvalue + " ";

            return QuerySet(query);
        }

        /// <summary>
        /// 插入一条数据，包括所有，不适用自动累加ID。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="values">插入值</param>
        /// <returns></returns>
        public DataSet InsertInto(string tableName, string[] values)
        {

            string query = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";

            for (int i = 1; i < values.Length; ++i)
            {

                query += ", " + "'" + values[i] + "'";

            }

            query += ")";

            // Debug.Log(query);
            return QuerySet(query);
        }

        /// <summary>
        /// 插入部分
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="col">属性列</param>
        /// <param name="values">属性值</param>
        /// <returns></returns>
        public DataSet InsertInto(string tableName, string[] col, string[] values)
        {

            if (col.Length != values.Length)
            {

                throw new Exception("columns.Length != colType.Length");

            }

            string query = "INSERT INTO " + tableName + " (" + col[0];
            for (int i = 1; i < col.Length; ++i)
            {

                query += ", " + col[i];

            }

            query += ") VALUES (" + "'" + values[0] + "'";
            for (int i = 1; i < values.Length; ++i)
            {

                query += ", " + "'" + values[i] + "'";

            }

            query += ")";

            //   Debug.Log(query);
            return QuerySet(query);

        }

        public void ReadDs(DataSet ds)
        {
            if (ds != null)
            {
                DataTable table = ds.Tables[0];
                foreach (DataRow dataRow in table.Rows)
                {
                    foreach (DataColumn dataColumn in table.Columns)
                    {
                        Debug.Log(dataRow[dataColumn]);
                    }
                }
            }
        }

        public void Close()
        {

            if (mySqlConnection != null)
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
                mySqlConnection = null;
            }

        }*/

    }
}