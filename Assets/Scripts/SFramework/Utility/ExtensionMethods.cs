using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFramework
{
    /// <summary>
    /// 扩展方法集中管理静态类
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// 为string扩展IsNullOrEmpty方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }

    }
}
