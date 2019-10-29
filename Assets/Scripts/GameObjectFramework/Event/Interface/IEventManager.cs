using GameEnum;
using System;
namespace GameEvent
{
    public interface IEventManager
    {
        bool Check(GameEventEnum type, EventHandler<GameEventArgs> handler);

        void Fire(GameEventEnum type, UnityEngine.GameObject sender, GameEventArgs e);

        void Subscribe(GameEventEnum type, EventHandler<GameEventArgs> handler);
        
        void UnSubscribe(GameEventEnum type, EventHandler<GameEventArgs> handler);

    }
}
