using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Managers
{
    public interface IAudioManager
    {
        void PlaySound(AudioTypes type);

        void PinataClick(bool smallHit);
    }
}
