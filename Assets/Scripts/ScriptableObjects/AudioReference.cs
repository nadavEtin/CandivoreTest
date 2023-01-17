using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.ScriptableObjects
{
    public enum AudioTypes
    {
        PinataBigHitSound, PinataSmallHitSound, PinataHitVoice,
        PinataFinalSmashVoice, PinataFinalSmashSound,
        PinataFallDownSound, PinataIntro
    }

    [CreateAssetMenu(fileName = "AudioReference", menuName = "ScriptableObjects/Audio Reference")]
    public class AudioReference : ScriptableObject
    {
        public Dictionary<AudioTypes, AudioContent> AudioClipTypes { private set; get; }

        public List<AudioClip> PinataBigHitSound;
        public List<AudioClip> PinataSmallHitSound;
        public List<AudioClip> PinataHitVoice;
        public List<AudioClip> PinataFinalSmashVoice;
        public AudioClip PinataFinalSmashSound, PinataFallDownSound, PinataIntro;

        public void Init()
        {
            
        }
    }
}
