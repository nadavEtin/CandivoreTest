using Assets.Scripts.GameplayObjects.GameplayObjUtility;
using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class PrizeShelfContainer : MonoBehaviour, IPrizeShelfContainer
    {
        [SerializeField] private List<PrizeShelf> _prizeShelves;
        private Dictionary<ObjectTypes, IPrizeShelf> _prizePositions;
        private IAnimationManager _animationManager;
        private IObjectPool _objectPool;

        public void Init(AssetReference assetReference, IAnimationManager animationManager, IObjectPool objectPool)
        {
            _animationManager = animationManager;
            _objectPool = objectPool;
            for (int i = 0; i < _prizeShelves.Count; i++)
            {
                _prizeShelves[i].Init(assetReference, _animationManager);
            }
        }

        public ShelfPrizeData ReceivePrize(ObjectTypes prizeType, int amount, GameObject particleFx)
        {
            //If prize type already exists on a shelf send it there, otherwise assign it to an empty position
            if (_prizePositions.ContainsKey(prizeType))
            {
                return _prizePositions[prizeType].AddPrize(prizeType, amount, particleFx);
            }
            else
            {
                var freeShelf = _prizeShelves.FirstOrDefault(x => x._prizes.Any(y => y.type == ObjectTypes.None));
                if (freeShelf != null)
                {
                    var prizePos = freeShelf.AddPrize(prizeType, amount, particleFx);
                    _prizePositions.Add(prizeType, freeShelf);
                    return prizePos;
                }
                else
                {
                    Debug.LogError("all shelves are full");
                }
            }

            return null;
        }

        public void PlayGameEndAnimations()
        {
            StartCoroutine(GameEndAnimations());
        }

        private void Start()
        {
            _prizePositions = new Dictionary<ObjectTypes, IPrizeShelf>();
        }

        private GameObject PlayGameEndAnimations(ObjectTypes type, ShelfPrizeData prizeData)
        {
            var particleObj = _objectPool.GetObjectFromPool(_animationManager.GetPrizeParticleType(type));
            var particleScript = particleObj.GetComponent<IParticleScript>();
            var psMain = particleObj.GetComponent<IParticleScript>().ParticleSystem.main;
            var psShape = particleObj.GetComponent<IParticleScript>().ParticleSystem.shape;
            var psRenderer = particleObj.GetComponent<ParticleSystemRenderer>();
            particleScript.Init(_objectPool.AddObjectToPool, _animationManager.GetPrizeParticleType(type));
            particleObj.transform.position = prizeData.shelfPos.position;
            psMain.startSpeed = 2;
            psMain.startSize = 0.75f;
            psShape.shapeType = ParticleSystemShapeType.Sphere;
            psShape.angle = 360;
            psRenderer.sortingOrder = 4;
            particleObj.GetComponent<ParticleSystem>().Play();
            var flashFx = _objectPool.GetObjectFromPool(ObjectTypes.BlueFlashFx);
            flashFx.GetComponent<IParticleScript>().Init(_objectPool.AddObjectToPool, ObjectTypes.BlueFlashFx);
            flashFx.transform.position = prizeData.shelfPos.position;
            flashFx.GetComponent<ParticleSystem>().Play();
            return particleObj;
        }

        IEnumerator GameEndAnimations()
        {
            foreach (var key in _prizePositions.Keys)
            {
                var prizeData = _prizePositions[key].GetShelfPrize(key);
                var particleSys = PlayGameEndAnimations(prizeData.type, prizeData);
                var corner = Randomizer.GetPositiveOrNegative() == 1 ? GeneralData.TopLeftCorner : GeneralData.TopRightCorner;
                _animationManager.MoveTransform(particleSys.transform, corner * 1.7f, 0.5f, 0.7f);
                yield return new WaitForSeconds(1.3f);
            }
        }
    }
}
