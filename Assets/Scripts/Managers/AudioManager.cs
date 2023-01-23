using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AudioManager : IAudioManager
    {
        private readonly List<AudioSource> _audioSources;
        private readonly AudioReference _audioRefs;
        private readonly GameParameters _gameParams;

        public AudioManager()
        {
            _audioRefs = Resources.Load<AudioReference>("AudioReference");
            _gameParams = Resources.Load<GameParameters>("GameParams");
            _audioRefs.Init();
            _audioSources = new List<AudioSource>();
            var asContainer = new GameObject("AudioSources");
            var musicSource = asContainer.AddComponent<AudioSource>();
            musicSource.clip = _audioRefs.SingleAudioClipTypes[AudioTypes.ThemeMusic];
            musicSource.loop = true;
            musicSource.volume = _gameParams.MusicVolume;
            musicSource.Play();
            _audioSources.Add(musicSource);
            _audioSources.Add(asContainer.AddComponent<AudioSource>());
        }

        public void PlaySound(AudioTypes type)
        {
            //Validation that at least one clip was assigned to the audio type
            if (!_audioRefs.SingleAudioClipTypes.ContainsKey(type) && !_audioRefs.MultipleAudioClipTypes.ContainsKey(type))
            {
                Debug.LogError("missing audio type clips");
                return;
            }

            //Get a random clip if multiple options are available
            AudioClip desiredClip;
            if (_audioRefs.SingleAudioClipTypes.ContainsKey(type))
                desiredClip = _audioRefs.SingleAudioClipTypes[type];
            else
                desiredClip = _audioRefs.MultipleAudioClipTypes[type][Randomizer.GetNumberInRange(0, _audioRefs.MultipleAudioClipTypes[type].Count)];
            FindAvailableSource(desiredClip);
        }

        public void PinataClick(bool smallHit)
        {
            PlaySound(AudioTypes.PinataHitVoice);
            PlaySound(smallHit ? AudioTypes.PinataSmallHitSound : AudioTypes.PinataBigHitSound);
        }

        private void FindAvailableSource(AudioClip clip)
        {
            //Locate a free audio source if one is available
            var availableSource = _audioSources.FirstOrDefault(s => !s.isPlaying);
            if (availableSource != null)
            {
                availableSource.clip = clip;
                availableSource.Play();
            }
            else
            {
                //Create a new audio source if all existing ones are in use
                var newAs = _audioSources[0].gameObject.AddComponent<AudioSource>();
                _audioSources.Add(newAs);
                newAs.clip = clip;
                newAs.Play();
            }
        }
    }
}