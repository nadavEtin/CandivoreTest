using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;

namespace Assets.Scripts.GameplayObjects.GameplayObjSubclasses
{
    public interface IPinataPrizeGenerator
    {
        List<ObjectTypes> GetPinataPrizes();
    }
}
