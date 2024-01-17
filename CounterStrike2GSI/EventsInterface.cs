using CounterStrike2GSI.EventMessages;
using System;
using System.ComponentModel;

namespace CounterStrike2GSI
{
    public class EventsInterface<T> where T : BaseEvent
    {
        /// <summary>
        /// Delegate for handing game events.
        /// </summary>
        /// <param name="game_event">The new game event.</param>
        public delegate void GameEventHandler(T game_event);

        /// <summary>
        /// Event for handing Counter-Strike 2 game events.
        /// </summary>
        public event GameEventHandler GameEvent = delegate { };

        public EventsInterface()
        {
        }

        public virtual void OnNewGameEvent(T e)
        {
            RaiseEvent(GameEvent, e);
        }

        protected void RaiseEvent(MulticastDelegate multi_delegate, object obj)
        {
            foreach (Delegate d in multi_delegate.GetInvocationList())
            {
                if (d.Target is ISynchronizeInvoke)
                {
                    (d.Target as ISynchronizeInvoke).BeginInvoke(d, new object[] { obj });
                }
                else
                {
                    d.DynamicInvoke(obj);
                }
            }
        }
    }
}
