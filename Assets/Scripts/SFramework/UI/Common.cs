/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： 框架核心参数  
 *    Description: 
 *           功能：
 *           1： 系统常量
 *           2： 全局性方法。
 *           3： 系统枚举类型
 *           4： 委托定义
 *                          
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using UnityEngine;

namespace SFramework
{
    #region 系统枚举类型

    /// <summary>
    /// UI窗体（位置）类型
    /// </summary>
    public enum UIFormType
    {
        /// <summary>
        /// 普通窗体
        /// </summary>
        Normal, 
        /// <summary>
        /// 固定窗体，非全屏非弹出窗体均属于这种，位置放在Normal之上，如CharacterInventory
        /// </summary>                            
        Fixed,
        /// <summary>
        /// 弹出窗体，位置最靠前
        /// </summary>
        PopUp
    }

    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        /// <summary>
        /// 可以与其他窗体并列显示的窗体
        /// </summary>
        Normal,
        /// <summary>
        /// “反向切换”:用于弹出窗体，栈存储，即我们一般要求玩家必须先关闭弹出的顶层窗体，再依次关闭下一级窗体。
        /// </summary>
        ReverseChange,
        /// <summary>
        /// 隐藏其他窗体，通常用于全屏窗体
        /// </summary>
        HideOther
    }

    /// <summary>
    /// 遮罩窗体透明度类型,仅对弹出窗体有用
    /// </summary>
    public enum UIFormLucenyType
    {
        /// <summary>
        /// 完全透明，不能穿透
        /// </summary>
        Lucency,
        /// <summary>
        /// 半透明，不能穿透
        /// </summary>
        Translucence,
        /// <summary>
        /// 低透明，不能穿透
        /// </summary>
        ImPenetrable,
        /// <summary>
        /// 可以穿透
        /// </summary>
        Pentrate
    }

    #endregion

    public class Common : MonoBehaviour
    {
        /* 路径常量 */
        public const string SYS_PATH_CANVAS = @"UI\Canvas";
        /* 标签常量 */
        public const string SYS_TAG_CANVAS = "Canvas";
        /* 节点常量 */
        public const string SYS_NORMAL_NODE = "Normal";
        public const string SYS_FIXED_NODE = "Fixed";
        public const string SYS_POPUP_NODE = "PopUp";

        /* 摄像机层深的常量 */

        /* 全局性的方法 */
        //Todo...

        /* 委托的定义 */
        //Todo....
    }
}
