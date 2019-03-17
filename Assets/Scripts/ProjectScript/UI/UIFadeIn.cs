/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：简单的淡入效果
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/

using UnityEngine;
using SFramework;
using UnityEngine.UI;

namespace ProjectScript
{
    public class UIFadeIn : ViewBase
    {
        private float fadeSpeed = 0.5f;
        private Image image;
        private Color color;
        private bool isFading = true;

        private void Awake()
        {
            base.UIForm_Type = UIFormType.PopUp;
            base.UIForm_ShowMode = UIFormShowMode.ReverseChange;
            base.UIForm_LucencyType = UIFormLucenyType.Lucency;
            image = GetComponent<Image>();
            color = image.color;
        }

        private void Update()
        {
            if (isFading)
            {
                if (color.a < 1)
                {
                    color.a += Time.deltaTime * fadeSpeed;
                    image.color = color;
                }
                else
                {
                    isFading = false;
                }
            }
        }
    }
}