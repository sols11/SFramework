/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：UI管理器
    作用：整个UI框架的核心，用户程序通过本脚本，来实现框架绝大多数的功能实现。
    使用：我们提供了一个默认的Canvas，放在Resources\UI文件夹下，Canvas下的UI会分别保存在Normal, Fixed, Popup层级下。
           1.将你所需要显示的UI保存为Prefab，将其路径加入UIManager字典集中管理
           2.创建UI脚本，继承ViewBase，使用SFramework命名空间，放在项目命名空间下，设置
           3.写好button等的事件，调用UIManager的方法ShowUIForms和CloseUIForms来开关UI
           4.这样每次只需要在当前UI脚本中写好要调用的方法和UI名称，而不用管UI对象是什么类型，就可以实现UI切换了（底层由UIManager自动管理）
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// UI框架
    /// </summary>
    public class UIManager : IGameMgr {
        // 保存canvas的引用
        public GameObject CanvasGO { get; private set; }
        public Camera UICamera { get; private set; }
        public UIMaskMgr UiMaskMgr { get; private set; }

        // 字段 
        // UI窗体预设路径 <窗体预设名称, 窗体预设路径>
	    private Dictionary<string, string> formToPathDict; 
        // 缓存所有UI窗体
	    private Dictionary<string, ViewBase> allUIFormsDict;
        // 当前显示的UI窗体
	    public Dictionary<string, ViewBase> currentShowUIFormsDict;
        // 定义“栈”集合,存储显示当前所有[反向切换]的窗体类型
        private Stack<ViewBase> currentUIFormsStack;
        // UI根节点
	    private Transform canvasTransform = null;
        // 全屏幕显示的节点
	    private Transform normalTransform = null;
        // 固定显示的节点
	    private Transform fixedTransform = null;
        // 弹出节点
	    private Transform popUpTransform = null;
        // UI管理脚本的节点
	    private Transform uiScriptsTransform = null;

        public UIManager(GameMainProgram gameMain) : base(gameMain)
        {
            //字段初始化
            allUIFormsDict = new Dictionary<string, ViewBase>();
            currentShowUIFormsDict = new Dictionary<string, ViewBase>();
            formToPathDict = new Dictionary<string, string>();
            currentUIFormsStack = new Stack<ViewBase>();
        }

        public override void Awake()
        {
            // 智能自动读取UI文件路径【虽然方便，但是由于Canvas等Prefab不能添加进来，所以不用这个功能了】
            if (formToPathDict != null)
            {
                /*DirectoryInfo dir = new DirectoryInfo(Application.dataPath + @"\Resources\UI\");
                FileInfo[] files = dir.GetFiles("*.prefab");    // 扫描文件 GetFiles("*.txt");可以实现扫描扫描txt文件 
                foreach (FileInfo fi in files)
                {
                    // 移除文件名后缀
                    string str = fi.Name.Remove(fi.Name.LastIndexOf('.'));
                    // 加入路径字典
                    _DicFormsPaths.Add(str, @"UI\" + str);
                    Debug.Log(@"UI\" + str);
                }*/

                formToPathDict.Add("BeginBackground", @"UI\BeginBackground");
                formToPathDict.Add("CharacterSelect", @"UI\CharacterSelect");
                formToPathDict.Add("PlayerHUD", @"UI\PlayerHUD");
                formToPathDict.Add("MedicineHUD", @"UI\MedicineHUD");
                formToPathDict.Add("PauseMenu", @"UI\PauseMenu");
                formToPathDict.Add("MainMenu", @"UI\MainMenu");
                formToPathDict.Add("StageMenu", @"UI\StageMenu");
                formToPathDict.Add("TestMenu", @"UI\TestMenu");
                formToPathDict.Add("NormalMenu", @"UI\NormalMenu");
                formToPathDict.Add("TasksMenu", @"UI\TasksMenu");
                formToPathDict.Add("GlobalMap", @"UI\GlobalMap");
                formToPathDict.Add("CharacterInfo", @"UI\CharacterInfo");
                formToPathDict.Add("Inventory", @"UI\Inventory");
                formToPathDict.Add("Store", @"UI\Store");
                formToPathDict.Add("StoreSale", @"UI\StoreSale");
                formToPathDict.Add("Setting", @"UI\Setting");
                formToPathDict.Add("HelpMenu", @"UI\HelpMenu");
                formToPathDict.Add("PromptBox", @"UI\PromptBox");
                formToPathDict.Add("MessageBox", @"UI\MessageBox");
                formToPathDict.Add("Dialog", @"UI\Dialog");
                formToPathDict.Add("FadeIn", @"UI\FadeIn");
                formToPathDict.Add("FadeOut", @"UI\FadeOut");
                formToPathDict.Add("FadeInWhite", @"UI\FadeInWhite");
                formToPathDict.Add("FadeOutWhite", @"UI\FadeOutWhite");
            }
        }

        //初始化核心数据，加载“UI窗体路径”到集合中。
        public override void Initialize()
	    {
            UiMaskMgr = gameMain.uiMaskMgr;
            //初始化加载（根UI窗体）Canvas预设
            InitRootCanvasLoading();
            //得到UI根节点、全屏节点、固定节点、弹出节点
            if (CanvasGO != null)
                canvasTransform = CanvasGO.transform;
            else
            {
                canvasTransform = GameObject.FindGameObjectWithTag(Common.SYS_TAG_CANVAS).transform;
                Debug.LogError("加载Canvas失败");
            }
            normalTransform = UnityHelper.FindTheChildNode(canvasTransform.gameObject, Common.SYS_NORMAL_NODE);
            fixedTransform = UnityHelper.FindTheChildNode(canvasTransform.gameObject, Common.SYS_FIXED_NODE);
            popUpTransform = UnityHelper.FindTheChildNode(canvasTransform.gameObject, Common.SYS_POPUP_NODE);
            UICamera = UnityHelper.FindTheChildNode(canvasTransform.gameObject, "UICamera").GetComponent<Camera>();
        }

        public override void Release()
        {
            allUIFormsDict.Clear();
            currentShowUIFormsDict.Clear();
            currentUIFormsStack.Clear();
            GameObject.Destroy(CanvasGO);
        }

        /// <summary>
        /// 显示（打开）UI窗体
        /// 请使用这个方法，不要用LoadUIForm等方法
        /// 功能：
        /// 1: 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 2: 根据不同的UI窗体的“显示模式”，分别作不同的加载处理
        /// </summary>
        /// <param name="uiFormName">UI窗体预设的名称</param>
	    public void ShowUIForms(string uiFormName)
        {
            ViewBase baseUIForms=null;                    //UI窗体基类

            //参数的检查
            if (string.IsNullOrEmpty(uiFormName)) return;

            //根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
            baseUIForms = LoadFormsToAllUIFormsCatch(uiFormName);
            if (baseUIForms == null) return;


            //根据不同的UI窗体的显示模式，分别作不同的加载处理
            switch (baseUIForms.UIForm_ShowMode)
            {                    
                case UIFormShowMode.Normal:                 //“普通显示”窗口模式
                    //把当前窗体加载到“当前窗体”集合中。
                    LoadUIToCurrentCache(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:          //需要“反向切换”窗口模式
                    PushUIFormToStack(uiFormName);
                    break;
                case UIFormShowMode.HideOther:              //“隐藏其他”窗口模式
                    EnterUIFormsAndHideOther(uiFormName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 关闭（返回上一个）窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        public void CloseUIForms(string uiFormName)
        { 
            ViewBase baseUiForm;                          //窗体基类

            //参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;
            //“所有UI窗体”集合中，如果没有记录，则直接返回
            allUIFormsDict.TryGetValue(uiFormName,out baseUiForm);
            if(baseUiForm==null ) return;
            //根据窗体不同的显示类型，分别作不同的关闭处理
            switch (baseUiForm.UIForm_ShowMode)
	        {
                case UIFormShowMode.Normal:
                    //普通窗体的关闭
                    ExitUIForms(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:
                    //反向切换窗体的关闭
                    PopUIFroms();
                    break;
                case UIFormShowMode.HideOther:
                    //隐藏其他窗体关闭
                    ExitUIFormsAndDisplayOther(uiFormName);
                    break;

		        default:
                    break;
	        }
        }

        #region  显示“UI管理器”内部核心数据，测试使用
        
        /// <summary>
        /// 显示"所有UI窗体"集合的数量
        /// </summary>
        /// <returns></returns>
        public int ShowALLUIFormCount()
        {
            if (allUIFormsDict != null)
            {
                return allUIFormsDict.Count;
            }
            else {
                return 0;
            }   
        }

        /// <summary>
        /// 显示"当前窗体"集合中数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentUIFormsCount()
        {
            if (currentShowUIFormsDict != null)
            {
                return currentShowUIFormsDict.Count;
            }
            else
            {
                return 0;
            }           
        }

        /// <summary>
        /// 显示“当前栈”集合中窗体数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentStackUIFormsCount()
        {
            if (currentUIFormsStack != null)
            {
                return currentUIFormsStack.Count;
            }
            else
            {
                return 0;
            }           
        }

        #endregion

        #region 私有方法
        //初始化加载（根UI窗体）Canvas预设
	    private void InitRootCanvasLoading()
	    {
	        CanvasGO                               = gameMain.resourcesMgr.LoadAsset(Common.SYS_PATH_CANVAS, false);
	    }

        /// <summary>
        /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 功能： 检查“所有UI窗体”集合中，是否已经加载过，否则才加载。
        /// </summary>
        /// <param name="uiFormsName">UI窗体（预设）的名称</param>
        /// <returns></returns>
	    private ViewBase LoadFormsToAllUIFormsCatch(string uiFormsName)
	    {
	        ViewBase baseUIResult = null;                 //加载的返回UI窗体基类

	        allUIFormsDict.TryGetValue(uiFormsName, out baseUIResult);
            if (baseUIResult==null)
	        {
                //加载指定名称的“UI窗体”，调用LoadUIForm
                baseUIResult = LoadUIForm(uiFormsName);
                // 赋值引用
                baseUIResult.UI_Manager = this;
                baseUIResult.UI_MaskMgr = UiMaskMgr;
	        }

	        return baseUIResult;
	    }

        /// <summary>
        /// 加载指定名称的“UI窗体”
        /// 功能：
        ///    1：根据“UI窗体名称”，加载预设克隆体。
        ///    2：根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到“根窗体”下不同的节点。
        ///    3：隐藏刚创建的UI克隆体。
        ///    4：把克隆体，加入到“所有UI窗体”（缓存）集合中。
        /// 
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
	    private ViewBase LoadUIForm(string uiFormName)
        {
            string strUIFormPaths = null;                   //UI窗体路径
            GameObject goCloneUIPrefabs = null;             //创建的UI克隆体预设
            ViewBase baseUiForm=null;                     //窗体基类

            //根据UI窗体名称，得到对应的加载路径
            formToPathDict.TryGetValue(uiFormName, out strUIFormPaths);
            //根据“UI窗体名称”，加载“预设克隆体”
            if (!string.IsNullOrEmpty(strUIFormPaths))
            {
                goCloneUIPrefabs = gameMain.resourcesMgr.LoadAsset(strUIFormPaths, false);
            }
            //设置“UI克隆体”的父节点（根据克隆体中带的脚本中不同的“位置信息”）
            if (canvasTransform != null && goCloneUIPrefabs != null)
            {
                baseUiForm = goCloneUIPrefabs.GetComponent<ViewBase>();
                if (baseUiForm == null)
                {
                    Debug.Log("ViewBase==null! ,请先确认窗体预设对象上是否加载了ViewBase的子类脚本！ 参数 uiFormName=" + uiFormName);
                    return null;
                }
                switch (baseUiForm.UIForm_Type)
                {
                    case UIFormType.Normal:                 //普通窗体节点
                        goCloneUIPrefabs.transform.SetParent(normalTransform, false);
                        break;
                    case UIFormType.Fixed:                  //固定窗体节点
                        goCloneUIPrefabs.transform.SetParent(fixedTransform, false);
                        break;
                    case UIFormType.PopUp:                  //弹出窗体节点
                        goCloneUIPrefabs.transform.SetParent(popUpTransform, false);
                        break;
                    default:
                        break;
                }

                //设置隐藏
                goCloneUIPrefabs.SetActive(false);
                //把克隆体，加入到“所有UI窗体”（缓存）集合中。
                allUIFormsDict.Add(uiFormName, baseUiForm);
                return baseUiForm;
            }
            else
            {
                Debug.Log("_TraCanvasTransfrom==null Or goCloneUIPrefabs==null!! ,Plese Check!, 参数uiFormName="+uiFormName); 
            }

            Debug.Log("出现不可以预估的错误，请检查，参数 uiFormName="+uiFormName);
            return null;
        }//Mehtod_end

        /// <summary>
        /// 把当前窗体加载到“当前窗体”集合中
        /// </summary>
        /// <param name="uiFormName">窗体预设的名称</param>
	    private void LoadUIToCurrentCache(string uiFormName)
	    {
	        ViewBase baseUiForm;                          //UI窗体基类
	        ViewBase baseUIFormFromAllCache;              //从“所有窗体集合”中得到的窗体

	        //如果“正在显示”的集合中，存在整个UI窗体，则直接返回
	        currentShowUIFormsDict.TryGetValue(uiFormName, out baseUiForm);
	        if (baseUiForm != null) return;
	        //把当前窗体，加载到“正在显示”集合中
	        allUIFormsDict.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache!=null)
	        {
                currentShowUIFormsDict.Add(uiFormName, baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();           //显示当前窗体
            }
	    }
 
        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName">窗体的名称</param>
        private void PushUIFormToStack(string uiFormName)
        { 
            ViewBase baseUIForm;                          //UI窗体

            //判断“栈”集合中，是否有其他的窗体，有则“冻结”处理。
            if(currentUIFormsStack.Count>0)
            {
                ViewBase topUIForm=currentUIFormsStack.Peek();
                //栈顶元素作冻结处理
                topUIForm.Freeze();
            }
            //判断“UI所有窗体”集合是否有指定的UI窗体，有则处理。
            allUIFormsDict.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm!=null)
            {
                //当前窗口显示状态
                baseUIForm.Display();
                //把指定的UI窗体，入栈操作。
                currentUIFormsStack.Push(baseUIForm);
            }else{
                Debug.Log("baseUIForm==null,Please Check, 参数 uiFormName=" + uiFormName);
            }
        }

        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="strUIFormName"></param>
        private void ExitUIForms(string strUIFormName)
        { 
            ViewBase baseUIForm;                          //窗体基类

            //"正在显示集合"中如果没有记录，则直接返回。
            currentShowUIFormsDict.TryGetValue(strUIFormName, out baseUIForm);
            if(baseUIForm==null) return ;
            //指定窗体，标记为“隐藏状态”，且从"正在显示集合"中移除。
            baseUIForm.Hiding();
            currentShowUIFormsDict.Remove(strUIFormName);
        }

        //（“反向切换”属性）窗体的出栈逻辑
        private void PopUIFroms()
        { 
            if(currentUIFormsStack.Count>=2)
            {
                //出栈处理
                ViewBase topUIForms = currentUIFormsStack.Pop();
                //做隐藏处理
                topUIForms.Hiding();
                //出栈后，下一个窗体做“重新显示”处理。
                ViewBase nextUIForms = currentUIFormsStack.Peek();   // 获取栈顶
                nextUIForms.Redisplay();
                return;
            }
            else if (currentUIFormsStack.Count ==1)
            {
                //出栈处理
                ViewBase topUIForms = currentUIFormsStack.Pop();
                //做隐藏处理
                topUIForms.Hiding();
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)打开窗体，且隐藏其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void EnterUIFormsAndHideOther(string strUIName)
        { 
            ViewBase baseUIForm;                          //UI窗体基类
            ViewBase baseUIFormFromALL;                   //从集合中得到的UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            currentShowUIFormsDict.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm != null) return;

            //把“正在显示集合”与“栈集合”中所有窗体都隐藏。
            foreach (ViewBase baseUI in currentShowUIFormsDict.Values)
            {
                baseUI.Hiding();
            }
            foreach (ViewBase staUI in currentUIFormsStack)
            {
                staUI.Hiding();
            }

            //把当前窗体加入到“正在显示窗体”集合中，且做显示处理。
            allUIFormsDict.TryGetValue(strUIName, out baseUIFormFromALL);
            if (baseUIFormFromALL!=null)
            {
                currentShowUIFormsDict.Add(strUIName, baseUIFormFromALL);
                //窗体显示
                baseUIFormFromALL.Display();
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)关闭窗体，且显示其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void ExitUIFormsAndDisplayOther(string strUIName)
        {
            ViewBase baseUIForm;                          //UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            currentShowUIFormsDict.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm == null) return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            baseUIForm.Hiding();
            currentShowUIFormsDict.Remove(strUIName);

            //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
            foreach (ViewBase baseUI in currentShowUIFormsDict.Values)
            {
                baseUI.Redisplay();
            }
            foreach (ViewBase staUI in currentUIFormsStack)
            {
                staUI.Redisplay();
            }
        }

        /// <summary>
        /// 是否清空“栈集合”中得数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (currentUIFormsStack != null && currentUIFormsStack.Count>=1)
            {
                //清空栈集合
                currentUIFormsStack.Clear();
                return true;
            }

            return false;
        }
        
        #endregion

    }//class_end
}