using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPinataPrizeManager
    {
        List<PrizeData> GetPinataPrizes();
        Dictionary<PrizeTypes, ObjectTypes> _prizeObjectRefs { get; }
    }
}
