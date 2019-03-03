/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：实现异步加载的UI进度条
    作用：Loading场景作为异步的中转站
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SFramework
{
    public class UILoading : MonoBehaviour
    {
        public static string nextScene = "Main";    // 静态传值
        public Image fillImg;
        public Text percentageText;

        void Start()
        {
            StartCoroutine(LoadingSceneAsync(nextScene));
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private IEnumerator LoadingSceneAsync(string sceneName)
        {
            float displayProgress = 0;    // 显示进度
            float toProgress = 0;     // 当前加载的进度
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;    // 未加载完时不激活这个Scene
            while (op.progress < 0.9f)
            {
                toProgress = (int)op.progress;
                while (displayProgress < toProgress)
                {
                    displayProgress += 0.01f;
                    percentageText.text = (int)(displayProgress * 100)+"%";
                    fillImg.fillAmount = displayProgress;
                    yield return new WaitForEndOfFrame();
                }
            }
            toProgress = 1;
            while (displayProgress < toProgress)
            {
                displayProgress += 0.01f;
                percentageText.text = (int)(displayProgress * 100) + "%";
                fillImg.fillAmount = displayProgress;
                yield return new WaitForEndOfFrame();
            }
            op.allowSceneActivation = true; // 其实同步如果更快的话早就加载完了，但是再快我们也要限制在100帧后才能激活新场景
        }
    }
}