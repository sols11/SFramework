using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SFramework
{
    public class UIDialog:ViewBase
    {
        // public AudioClip mAudioClip;             // 打字的声音，不是没打一个字播放一下，开始的时候播放结束就停止播放  
        public Text DialogText;
        public static bool IsTalking { get; set; }

        private static List<string> Talks { get; set; }
        private static UnityAction DialogCompleteAction { get; set; }    // 函数指针

        private float letterPause = 0.1f;
        private string showString;   // 要显示的完整字符串
        private int index = 0;       //index表示当前显示的是第几句话，nextIndex在当前对话已结束时=index+1
        private int nextIndex = 0;
        private StringBuilder sb=new StringBuilder();   

        private void Awake()
        {
            //定义本窗体的性质(弹出窗体)
            base.UIForm_Type = UIFormType.PopUp;
            base.UIForm_ShowMode = UIFormShowMode.ReverseChange;
            base.UIForm_LucencyType = UIFormLucenyType.Lucency;
        }

        private void OnEnable()
        {
            // 打开即对话
            index = 0;
            nextIndex = 0;
            letterPause = 0.1f;
            NextTalk();
            //GameMainProgram.Instance.playerMgr.CanInput = false;
            // CoroutineMgr.Instance.StartCoroutine(Talking());
        }

        /// <summary>
        /// 调用这个静态方法就开始对话，不执行action就赋null
        /// </summary>
        /// <param name="talks"></param>
        /// <param name="dialogCompleteAction"></param>
        public static void StartDialog(List<string> talks, UnityAction dialogCompleteAction)
        {
            Talks = talks;
            DialogCompleteAction = dialogCompleteAction;
            GameMainProgram.Instance.uiManager.ShowUIForms("Dialog");
        }

        private void Update()
        {
            if (Input.GetButtonDown("Attack1"))
            {
                // 对话没有显示完
                if (index == nextIndex)
                {
                    letterPause = 0; //加快显示速度，让对话速度显示完
                }
                // 对话未结束（是否是最后一句对话）
                else if (index < Talks.Count - 1)
                {
                    letterPause = 0.1f;
                    index++;
                    NextTalk();
                }
                // 对话结束
                else
                {
                    GameMainProgram.Instance.uiManager.CloseUIForms("Dialog");
                    if (DialogCompleteAction != null)
                        DialogCompleteAction();
                    // GameMainProgram.Instance.playerMgr.CanInput = true;
                    // GameMainProgram.Instance.eventMgr.InvokeEvent(EventName.DialogComplete);
                }
            }
        }

        /**切换语句功能*/
        void NextTalk()
        {
            showString = Talks[index];  // 把你输出的字先赋值给word
            sb.Remove(0, sb.Length);    // 清空
            DialogText.text = string.Empty;//把你要显示的字先抹除，以便你可以在最初显示的时候显示为空，当然你也可以加上其他字，让他先显示，打字机效果打的字会显示在这个后面
            StartCoroutine(TypeText());
        }

        /**输出文本功能*/
        IEnumerator TypeText()
        {
            foreach (char letter in showString.ToCharArray())
            {
                sb.Append(letter);
                DialogText.text = sb.ToString();    //把这些字赋值给Text
                yield return new WaitForSeconds(letterPause);
            }
            sb.Append(" ▼");    // 标记可按键的提示
            DialogText.text = sb.ToString(); 
            nextIndex++;                //避免出现下一句不显示的情况将对话记录+1
        }

    }
}
