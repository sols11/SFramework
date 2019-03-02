/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： UI遮罩管理器  
 *    Description: 
 *           功能： 负责“弹出窗体”模态显示实现
 *                  
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace SFramework
{
	public class UIMaskMgr : IGameMgr {
        /*  字段 */
        private UIManager uiManager;
        //UI根节点对象
	    private GameObject canvasGO = null;
        //UI脚本节点对象
	    //private Transform _TraUIScriptsNode = null;
        //顶层面板
	    private GameObject _GoTopPanel;
        //遮罩面板的引用
	    private GameObject _GoMaskPanel;
        //UI摄像机
	    private Camera _UICamera;
        //UI摄像机原始的“层深”
	    private float _OriginalUICameralDepth;

        public UIMaskMgr(GameMainProgram gameMain):base(gameMain)
		{

        }

        public override void Initialize()
        {
            //字段初始化
            uiManager = gameMain.uiManager;
            //得到UI根节点对象、脚本节点对象
            canvasGO = uiManager.CanvasGO;
            //把本脚本实例，作为“脚本节点对象”的子节点。
            //UnityHelper.AddChildNodeToParentNode(_TraUIScriptsNode, this.gameObject.transform);
            //得到“顶层面板”、“遮罩面板”
            _GoTopPanel = canvasGO;
            _GoMaskPanel = UnityHelper.FindTheChildNode(canvasGO, "UIMaskPanel").gameObject;
            //得到UI摄像机原始的“层深”
            _UICamera = uiManager.UICamera;
            if (_UICamera != null)
            {
                //得到UI摄像机原始“层深”
                _OriginalUICameralDepth = _UICamera.depth;
            }
            else
            {
                Debug.Log(GetType() + "/Start()/UI_Camera is Null!,Please Check! ");
            }
        }


        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="goDisplayUIForms">需要显示的UI窗体</param>
        /// <param name="lucenyType">显示透明度属性</param>
	    public void SetMaskWindow(GameObject goDisplayUIForms,UIFormLucenyType lucenyType=UIFormLucenyType.Lucency)
        {
	        //顶层窗体下移
            _GoTopPanel.transform.SetAsLastSibling();
            //启用遮罩窗体以及设置透明度
            switch (lucenyType)
            {
                    //完全透明，不能穿透
                case UIFormLucenyType.Lucency:
                    _GoMaskPanel.SetActive(true);
                    Color newColor1=new Color(255/255F,255/255F,255/255F,0F/255F);
                    _GoMaskPanel.GetComponent<Image>().color = newColor1; 
                    break;
                    //半透明，不能穿透
                case UIFormLucenyType.Translucence:
                    _GoMaskPanel.SetActive(true);
                    Color newColor2 = new Color(220/255F, 220/255F, 220/255F, 50/255F);
                    _GoMaskPanel.GetComponent<Image>().color = newColor2; 
                    break;
                    //低透明，不能穿透
                case UIFormLucenyType.ImPenetrable:
                    _GoMaskPanel.SetActive(true);
                    Color newColor3=new Color(50/255F,50/255F,50/255F,200F/255F);
                    _GoMaskPanel.GetComponent<Image>().color = newColor3; 
                    break;
                    //可以穿透
                case UIFormLucenyType.Pentrate:
                    if (_GoMaskPanel.activeInHierarchy)
                    {
                        _GoMaskPanel.SetActive(false);
                    }
                    break;

                default:
                    break;
            }

            //遮罩窗体下移
            _GoMaskPanel.transform.SetAsLastSibling();
            //显示窗体的下移
            goDisplayUIForms.transform.SetAsLastSibling();
            //增加当前UI摄像机的层深（保证当前摄像机为最前显示）
            if (_UICamera!=null)
            {
                _UICamera.depth = _UICamera.depth + 100;    //增加层深
            }

	    }

        /// <summary>
        /// 取消遮罩状态
        /// </summary>
	    public void CancelMaskWindow()
	    {
            //顶层窗体上移
            _GoTopPanel.transform.SetAsFirstSibling();
            //禁用遮罩窗体
	        if (_GoMaskPanel.activeInHierarchy)
	        {
                //隐藏
	            _GoMaskPanel.SetActive(false);
            }

	        //恢复当前UI摄像机的层深 
            if (_UICamera != null)
            {
                _UICamera.depth = _OriginalUICameralDepth;  //恢复层深
            }
        }


	}
}