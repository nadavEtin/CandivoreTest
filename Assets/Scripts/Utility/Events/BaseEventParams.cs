using Assets.Scripts.Utility.Events;

namespace Assets.Scripts.Utility
{
    public abstract class BaseEventParams
    {
        private static EmptyParams _empty = new EmptyParams();

        public static EmptyParams Empty => _empty;
    }
}
