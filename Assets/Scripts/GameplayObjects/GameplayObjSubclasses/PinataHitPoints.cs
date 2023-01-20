using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.GameplayObjects.GameplayObjSubclasses
{
    public class PinataHitPoints : IPinataHitPoints
    {
        //each "hit point" = 1 short click on the pinata
        private int _hitPoints;
        private GameParameters _gameParams;

        public PinataHitPoints(GameParameters gameParameters)
        {
            _gameParams = gameParameters;
            _hitPoints = _gameParams.PinataClicksToDestroy;
        }

        public bool PinataClick(float clickDuration)
        {
            _hitPoints -= ClickPower(clickDuration);
            if(_hitPoints <= 0)
            {
                //TODO: send game over event
                return false;
            }
            else
            {
                return true;
            }
        }

        private int ClickPower(float clickDuration)
        {
            return clickDuration >= _gameParams.LongClickThreshold ? _gameParams.LongClickPower : _gameParams.ShortClickPower;
        }
    }
}
