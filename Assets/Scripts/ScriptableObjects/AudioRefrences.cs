using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioReference", menuName = "ScriptableObjects/Audio Refrence")]
    public class AudioRefrences : ScriptableObject
    {
        public List<AudioClip> PinataBigHitSfx;
        public List<AudioClip> PinataSmallHitSfx;
        public List<AudioClip> PinataHitVoice;
        public List<AudioClip> PinataFinalSmashVoice;
        public AudioClip PinataFinalSmashSound, PinataFallDownSound, PinataIntro;
    }
}
