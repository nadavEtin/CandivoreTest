using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioReference", menuName = "ScriptableObjects/AudioRefrence")]
    public class AudioRefrences : ScriptableObject
    {
        [SerializeField] private List<AudioClip> _pinataBigHitSfx;
        [SerializeField] private List<AudioClip> _pinataSmallHitSfx;
        [SerializeField] private List<AudioClip> _pinataHitVoice;
        [SerializeField] private List<AudioClip> _pinataFinalSmashVoice;
        [SerializeField] private AudioClip _pinataFinalSmashSound, _pinataFallDownSound, _pinataIntro;
    }
}
