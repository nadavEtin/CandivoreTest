using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPinataPrizeManager
    {
        Dictionary<PrizeTypes, ObjectTypes> _prizeObjectRefs { get; }
    }
}
