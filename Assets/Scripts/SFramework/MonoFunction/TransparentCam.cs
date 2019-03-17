using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 遮挡半透查询控制器，挂于摄像机
    /// </summary>
    public class TransparentCam : MonoBehaviour
    {
        public Transform targetObject { get; set; }  //目标对象

        public class TransparentParam
        {
            // 将material替换shader，而sharedmaterial为原来的shader
            public Material[] materials = null;
            public Material[] sharedMats = null;
            public float currentFadeTime = 0;
            public bool isTransparent = true;
        }

        private float height = 1f;             //目标对象Y方向偏移
        private float destTransparent = 0.2f;    //遮挡半透的最终半透强度
        private float fadeInTime = 0.3f;         //开始遮挡半透时渐变时间

        private int transparentLayer; //需要遮挡半透的层级
        private Dictionary<Renderer, TransparentParam> transparentDic = new Dictionary<Renderer, TransparentParam>();
        private List<Renderer> clearList = new List<Renderer>();

        private void Start()
        {
            transparentLayer = 1 << LayerMask.NameToLayer("Default");
        }

        /// <summary>
        /// 调用以下3个方法
        /// </summary>
        void Update()
        {
            if (targetObject == null)
                return;
            UpdateTransparentObject();
            UpdateRayCastHit();
            RemoveUnuseTransparent();
        }

        /// <summary>
        /// 每帧对字典中每个材质做渐变处理（控制Shader属性）
        /// </summary>
        public void UpdateTransparentObject()
        {
            var var = transparentDic.GetEnumerator();
            while (var.MoveNext())
            {
                TransparentParam param = var.Current.Value;
                param.isTransparent = false;
                foreach (var mat in param.materials)
                {
                    Color col = mat.GetColor("_Color");
                    param.currentFadeTime += Time.deltaTime;
                    float t = param.currentFadeTime / fadeInTime;
                    col.a = Mathf.Lerp(1, destTransparent, t);
                    mat.SetColor("_Color", col);
                }
            }
        }

        /// <summary>
        /// 相机发出射线，对碰撞到的物体的子物体所有renderer做材质替换
        /// </summary>
        public void UpdateRayCastHit()
        {
            RaycastHit[] rayHits = null;
            //视线方向为从自身（相机）指向目标位置
            Vector3 targetPos = targetObject.position + new Vector3(0, height, 0);
            Vector3 viewDir = (targetPos - transform.position).normalized;
            Vector3 oriPos = transform.position;
            float distance = Vector3.Distance(oriPos, targetPos);
            rayHits = Physics.RaycastAll(oriPos, viewDir, distance, transparentLayer);
            //直接在Scene画一条线，方便观察射线
            Debug.DrawLine(oriPos, targetPos, Color.red);
            // 对子物体所有renderer做材质替换
            foreach (var hit in rayHits)
            {
                Renderer[] renderers = hit.collider.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    AddTransparent(r);
                }
            }
        }

        /// <summary>
        /// 对renderer做shader替换
        /// </summary>
        /// <param name="renderer"></param>
        void AddTransparent(Renderer renderer)
        {
            TransparentParam param = null;
            // 看是不是已经替换过shader的
            transparentDic.TryGetValue(renderer, out param);
            if (param == null)
            {
                param = new TransparentParam();
                // 将新的加入字典
                transparentDic.Add(renderer, param);
                // 此处顺序不能反，调用material会产生材质实例。
                param.sharedMats = renderer.sharedMaterials;
                param.materials = renderer.materials;
                foreach (var v in param.materials)
                {
                    // 这里选择Transparent/Bumped Diffuse作为Shader，注意要把这个Shader打包进来
                    v.shader = Shader.Find("Legacy Shaders/Transparent/Bumped Diffuse");
                }
            }
            param.isTransparent = true;
        }

        /// <summary>
        /// 将不再是遮挡半透的材质恢复
        /// </summary>
        public void RemoveUnuseTransparent()
        {
            clearList.Clear();
            var var = transparentDic.GetEnumerator();
            while (var.MoveNext())
            {
                // 非透明的,换回时用sharedMats还原之前的materials
                if (var.Current.Value.isTransparent == false)
                {
                    //用完后材质实例不会销毁，可以被unloadunuseasset销毁或切场景销毁。
                    var.Current.Key.materials = var.Current.Value.sharedMats;
                    clearList.Add(var.Current.Key);
                }
            }
            foreach (var v in clearList)
                transparentDic.Remove(v);
        }

    }
}