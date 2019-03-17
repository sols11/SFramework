/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：本身是一个单例，控制全局唯一的背景音乐播放器
    作用：管理游戏中各个音乐音效的播放停止，音量大小等
    使用：要求BGM都放在Musics目录下
    补充：
History:
----------------------------------------------------------------------------*/

using System.Collections.Generic;
using UnityEngine;

namespace SFramework
{
    /// <summary>
    /// 音乐管理
    /// </summary>
    public class AudioMgr:IGameMgr
    {
        private SettingData SettingSaveData { get; set; }    // 只用于获取数据，不进行写入
        private List<string> musicPathList;
        private GameObject gameObject;
        private AudioSource musicAudioSource;
        private List<AudioSource> soundAudioSources;         // 管理所有音效
        private string musicResouceDir = @"Musics\";

        public AudioMgr(GameMainProgram gameMain) : base(gameMain)
        {
            musicPathList = new List<string>();
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
            // 添加音乐
            musicPathList.Add("WhiteLie");
            Debug.Log("音乐列表确认完毕");
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
            musicAudioSource.clip = gameMain.resourcesMgr.LoadResource<AudioClip>(musicResouceDir + musicPathList[index], false);
            musicAudioSource.Play();
        }

        public void PlayMusic(string name)
        {
            int index = musicPathList.FindIndex(path => path == name);
            musicAudioSource.clip = gameMain.resourcesMgr.LoadResource<AudioClip>(musicResouceDir + musicPathList[index], false);
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
