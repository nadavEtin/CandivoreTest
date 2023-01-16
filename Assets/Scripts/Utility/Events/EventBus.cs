using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility
{
    public enum GameplayEventType
    {

    }

    public static class EventBus
    {
        private static Dictionary<GameplayEventType, List<Action<BaseEventParams>>> _subscription = 
            new Dictionary<GameplayEventType, List<Action<BaseEventParams>>>();

        public static void Subscribe(GameplayEventType eventType, Action<BaseEventParams> handler)
        {
            if (!_subscription.ContainsKey(eventType))     
                _subscription.Add(eventType, new List<Action<BaseEventParams>>());
            
            if (!_subscription[eventType].Contains(handler))
                _subscription[eventType].Add(handler);
        }

        public static void Unsubscribe(GameplayEventType eventType, Action<BaseEventParams> handler)
        {
            if (!_subscription.ContainsKey(eventType))            
                return;           

            var handlersList = _subscription[eventType];
            handlersList.Remove(handler);
        }

        public static void Publish(GameplayEventType eventType, BaseEventParams eventParams)
        {
            if (!_subscription.ContainsKey(eventType))           
                return;            

            var handlers = _subscription[eventType];
            for (int i = 0; i < handlers.Count; i++)
            {
                var handler = handlers[i];
                handler?.Invoke(eventParams);
            }
        }
    }
}
