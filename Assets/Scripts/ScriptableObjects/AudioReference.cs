using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public enum AudioTypes
    {
        PinataBigHitSound, PinataSmallHitSound, PinataHitVoice,
        PinataFinalSmashVoice, PinataFinalSmashSound,
        PinataFallDownSound, PinataIntro, ThemeMusic
    }

    [CreateAssetMenu(fileName = "AudioReference", menuName = "ScriptableObjects/Audio Reference")]
    public class AudioReference : ScriptableObject
    {
        public Dictionary<AudioTypes, List<AudioClip>> MultipleAudioClipTypes { private set; get; }
        public Dictionary<AudioTypes, AudioClip> SingleAudioClipTypes { private set; get; }

        [SerializeField] private List<AudioClip> PinataBigHitSound;
        [SerializeField] private List<AudioClip> PinataSmallHitSound;
        [SerializeField] private List<AudioClip> PinataHitVoice;
        [SerializeField] private List<AudioClip> PinataFinalSmashVoice;
        [SerializeField] private AudioClip PinataFinalSmashSound, PinataFallDownSound, PinataIntro, ThemeMusic;

        public void Init()
        {
            MultipleAudioClipTypes = new Dictionary<AudioTypes, List<AudioClip>>
            {
                { AudioTypes.PinataBigHitSound, PinataBigHitSound },
                { AudioTypes.PinataSmallHitSound, PinataSmallHitSound },
                { AudioTypes.PinataHitVoice, PinataHitVoice },
                { AudioTypes.PinataFinalSmashVoice, PinataFinalSmashVoice }
            };

            SingleAudioClipTypes = new Dictionary<AudioTypes, AudioClip>
            {
                { AudioTypes.PinataFinalSmashSound, PinataFinalSmashSound },
                { AudioTypes.PinataFallDownSound, PinataFallDownSound },
                { AudioTypes.PinataIntro, PinataIntro },
                { AudioTypes.ThemeMusic, ThemeMusic}
            };
        }
    }
}
