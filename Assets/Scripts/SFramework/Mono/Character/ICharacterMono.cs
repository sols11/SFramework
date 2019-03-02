using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// + 音效
    /// + Component
    /// 会细分为Player和Enemy两种，所以只作为通用接口，具备一些组件访问和通用接口
    /// </summary>
    public class ICharacterMono : MonoBehaviour
    {
        //语音，音效
        public AudioClip[] voice;
        public AudioClip[] sound;
        //两个音源
        private AudioSource[] allAudioSource = new AudioSource[2];

        public Animator AnimatorComponent { get; set; }
        public Rigidbody2D Rg2d { get; set; }
        
        /// <summary>
        /// 主武器的碰撞体
        /// </summary>
        public Collider WeaponCollider { get; set; }

        /// <summary>
        /// 初始化和释放两个方法都交给Mediator调用
        /// </summary>
        public virtual void Initialize()
        {
            if (!allAudioSource[0] || !allAudioSource[1])
            {
                allAudioSource = GetComponents<AudioSource>();
                GameMainProgram.Instance.audioMgr.AddSound(allAudioSource[0]);
                GameMainProgram.Instance.audioMgr.AddSound(allAudioSource[1]);
            }
        }

        public virtual void Release()
        {
            GameMainProgram.Instance.audioMgr.RemoveSound(allAudioSource[0]);
            GameMainProgram.Instance.audioMgr.RemoveSound(allAudioSource[1]);
        }

        /// <summary>
        /// 播放语音
        /// </summary>
        /// <param name="index"></param>
        public void PlayVoice(int index)
        {
            if (index >= voice.Length || index < 0)
                return;
            allAudioSource[0].clip = voice[index];
            allAudioSource[0].Play();
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="index"></param>
        public void PlaySound(int index)
        {
            if (index >= sound.Length || index < 0)
                return;
            allAudioSource[1].clip = sound[index];
            allAudioSource[1].Play();
        }
    }
}
