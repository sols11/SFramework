using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 音乐管理
    /// 控制游戏中的音乐音效大小，以及播放BGM
    /// 要求BGM都放在Music目录下
    /// </summary>
    public class AudioMgr:IGameMgr
    {
        private SettingData SettingSaveData { get; set; }    // 只用于获取数据，不进行写入
        private List<string> _musicPathList;
        private GameObject gameObject;
        private AudioSource musicAudioSource;
        private List<AudioSource> soundAudioSources;    // 管理所有音效

        public AudioMgr(GameMainProgram gameMain) : base(gameMain)
        {
            _musicPathList = new List<string>();
            soundAudioSources = new List<AudioSource>();
        }

        public override void Awake()
        {
            SettingSaveData = gameMain.gameDataMgr.SettingSaveData;

            if (gameObject == null)
            {
                gameObject = new GameObject("AudioMgr");
                musicAudioSource = gameObject.AddComponent<AudioSource>();
                musicAudioSource.loop = true;
                musicAudioSource.playOnAwake = false;
                musicAudioSource.volume = SettingSaveData.MusicVolume / 100.0f;    // /100
                Object.DontDestroyOnLoad(gameObject);
            }

            _musicPathList.Add("FinalBattle");
        }

        public override void Release()
        {
            soundAudioSources.Clear();
        }

        public void AddSound(AudioSource sound)
        {
            if (sound == null)
                return;
            soundAudioSources.Add(sound);
            sound.volume = SettingSaveData.SoundVolume / 100.0f;    // /100
        }

        public void RemoveSound(AudioSource sound)
        {
            if (sound == null)
                return;
            if(soundAudioSources.Contains(sound))
                soundAudioSources.Remove(sound);
        }

        public void PlayMusic(int index)
        {
            musicAudioSource.clip = gameMain.resourcesMgr.LoadResource<AudioClip>(@"Music\" + _musicPathList[index], false);
            musicAudioSource.Play();
        }

        public void PlayMusic(string name)
        {
            int index = _musicPathList.FindIndex(path => path == name);
            musicAudioSource.clip = gameMain.resourcesMgr.LoadResource<AudioClip>(@"Music\" + _musicPathList[index], false);
            musicAudioSource.Play();
        }

        public void StopMusic()
        {
            musicAudioSource.Stop();
        }

        public void ChangeMusicVolume(int volume)
        {
            SettingSaveData.MusicVolume = volume;
            musicAudioSource.volume = volume/100.0f;    // /100
        }

        public void ChangeSoundVolume(int volume)
        {
            SettingSaveData.SoundVolume = volume;
            foreach (var sound in soundAudioSources)
            {
                if (sound != null)
                    sound.volume = volume / 100.0f;    // /100
            }
        }
    }
}
