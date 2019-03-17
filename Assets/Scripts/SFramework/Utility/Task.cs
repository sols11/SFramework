/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：简易的任务系统，包括存储任务显示信息的Task结构，和存储玩家任务完成情况的TaskData结构
    作用：
    使用：可以根据项目需要改写代码。添加任务-调用IPlayer.AddTask（目前暂时只在UITasksMenu中用到）
    补充：TODO：暂未完善
History:
----------------------------------------------------------------------------*/

using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 存储任务信息，存在数据库文件中
    /// </summary>
    public class Task
    {
        public string TaskName { get; set; }
        public string TaskPlace { get; set; }
        public int TaskAward { get; set; }

        public Task()
        {
            TaskName = "讨伐xxx";
            TaskPlace = "xx";
            TaskAward = 0;
        }
    }

    /// <summary>
    /// 存储任务完成相关信息，存在存档里
    /// </summary>
    public class TaskData
    {
        public int Id { get; set; } // 对应在任务数据库的下标
        public bool IsActive { get; set; }

        public bool HasCompleted
        {
            get { return HasCompletedRank1 || HasCompletedRank2 || HasCompletedRank3; }
        }
        public bool HasCompletedRank1 { get; set; }
        public bool HasCompletedRank2 { get; set; }
        public bool HasCompletedRank3 { get; set; }

        /// <summary>
        /// 添加新任务时，默认的构造
        /// </summary>
        public TaskData()
        {
            Id = 0;
            IsActive = true;
        }
    }
}
