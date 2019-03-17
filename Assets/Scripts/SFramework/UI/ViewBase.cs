/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：UI继承的基类
    作用：设置3个枚举的值
          提供4种生命周期状态供外界调用
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SFramework
{
    public class ViewBase : MonoBehaviour
    {
        // 字段
        private UIFormType _UIForm_Type = UIFormType.Normal;
        private UIFormShowMode _UIForm_ShowMode = UIFormShowMode.Normal;
        private UIFormLucenyType _UIForm_LucencyType = UIFormLucenyType.Lucency;

        // 属性
        // 对UIMgr的引用将在生成该UI时赋值
        public UIManager UI_Manager { get; set; }
        public UIMaskMgr UI_MaskMgr { get; set; }
        public LanguageMgr LanguageMgr { get; set; }

        // UI窗体（位置）类型
        public UIFormType UIForm_Type { get { return _UIForm_Type; }set { _UIForm_Type = value; } }
        // UI窗体显示类型
        public UIFormShowMode UIForm_ShowMode { get { return _UIForm_ShowMode; } set { _UIForm_ShowMode = value; } }
        // UI窗体透明度类型
        public UIFormLucenyType UIForm_LucencyType { get { return _UIForm_LucencyType; } set { _UIForm_LucencyType = value; } }

        public virtual void UpdateUI() { }

        #region  窗体的四种(生命周期)状态

        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Display()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (UIForm_Type == UIFormType.PopUp)
            {
                if(UI_MaskMgr!=null)
                    UI_MaskMgr.SetMaskWindow(this.gameObject, UIForm_LucencyType);
                else
                {
                    UI_MaskMgr = GameMainProgram.Instance.uiMaskMgr;
                    UI_MaskMgr.SetMaskWindow(this.gameObject, UIForm_LucencyType);
                    Debug.Log("UI未获取UI_MaskMgr，自动从主程序获取");
                }
            }
        }

        /// <summary>
        /// 隐藏状态
        /// </summary>
	    public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
            //取消模态窗体调用
            if (UIForm_Type == UIFormType.PopUp)
            {
                if (UI_MaskMgr != null)
                    UI_MaskMgr.CancelMaskWindow();
                else
                {
                    UI_MaskMgr = GameMainProgram.Instance.uiMaskMgr;
                    UI_MaskMgr.CancelMaskWindow();
                    Debug.Log("UI未获取UI_MaskMgr，自动从主程序获取");
                }
            }
        }

        /// <summary>
        /// 重新显示状态
        /// </summary>
	    public virtual void Redisplay()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (UI_MaskMgr != null)
                UI_MaskMgr.SetMaskWindow(this.gameObject, UIForm_LucencyType);
            else
            {
                UI_MaskMgr = GameMainProgram.Instance.uiMaskMgr;
                UI_MaskMgr.SetMaskWindow(this.gameObject, UIForm_LucencyType);
                Debug.Log("UI未获取UI_MaskMgr，自动从主程序获取");
            }
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
	    public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }


        #endregion
        #region 封装子类常用的方法

        /*protected void RigisterButtonObjectEvent(string buttonName, EventTriggerListener.VoidDelegate delHandle)
        {
        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托：需要注册的方法</param>
            GameObject goButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName).gameObject;
            //给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }
        }*/

        /// <summary>
        /// 多语言本地化显示Text
        /// </summary>
        /// <param name="uiText">要修改的Text</param>
        /// <param name="stringId">字符串的Key</param>
        /// <param name="fontChoose">默认0，1强制CN字体，2强制EN字体。其他数字：简中CN字体，英语EN字体</param>
        public void ShowText(Text uiText,string stringId,int fontChoose=0)
        {
            if (LanguageMgr == null)
                LanguageMgr = GameMainProgram.Instance.languageMgr;
            if (LanguageMgr != null)
            {
                uiText.text = LanguageMgr.ShowText(stringId);
                uiText.font = LanguageMgr.GetFont(fontChoose);
            }
            else
                uiText.text = string.Empty;
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
	    public void OpenUIForm(string uiFormName)
        {
            //如果没有引用到的话使用主程序调用
            if (UI_Manager != null)
                UI_Manager.ShowUIForms(uiFormName);
            else
                GameMainProgram.Instance.uiManager.ShowUIForms(uiFormName);
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
	    public void CloseUIForm()
        {
            string strUIFromName = string.Empty;            //处理后的UIFrom 名称
            int intPosition = -1;

            strUIFromName = GetType().ToString();             //命名空间+类名
            intPosition = strUIFromName.IndexOf('.');
            if (intPosition != -1)
            {
                //剪切字符串中“.”之间的部分，也就是命名空间后面的部分
                strUIFromName = strUIFromName.Substring(intPosition + 1);
            }
            //最后得到的就是当前UI窗体名称
            if (UI_Manager != null)
                UI_Manager.CloseUIForms(strUIFromName);
            else
                GameMainProgram.Instance.uiManager.CloseUIForms(strUIFromName);
        }

        #endregion
    }
}
