using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class AudioManager
    {
        private static List<AudioSource> _audioSources;
        private static AudioReference _audioRefs;

        public static void Init()
        {
            _audioRefs = Resources.Load<AudioReference>("AudioReference");
            _audioSources = new List<AudioSource>();
            var asContainer = new GameObject("AudioSources");
            _audioSources.Add(asContainer.AddComponent<AudioSource>());
            _audioSources.Add(asContainer.AddComponent<AudioSource>());
        }

        public static void PlayRandomSound(AudioTypes type)
        {
            var clipList = _audioRefs.
        }

        public static void PlayRandomPinataHitVoice()
        {
            var clip = _audioRefs.PinataHitVoice[Randomizer.GetNumberInRange(0, _audioRefs.PinataHitVoice.Count)];
            
        }
        
        public static void PlayRandomPinataBigHitSfx()
        {
            var clip = _audioRefs.PinataBigHitSound[Randomizer.GetNumberInRange(0, _audioRefs.PinataBigHitSound.Count)];
            
        }
        
        public static void PlayRandomPinataSmallHitSfx()
        {
            var clip = _audioRefs.PinataSmallHitSound[Randomizer.GetNumberInRange(0, _audioRefs.PinataSmallHitSound.Count)];
            
        }
    }
}