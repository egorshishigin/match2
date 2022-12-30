using System.Collections.Generic;

namespace Pause
{
    public class PauseManager : IPuaseHandler
    {
        private readonly List<IPuaseHandler> _handlers = new List<IPuaseHandler>();

        public bool Paused { get; private set; }

        public void Register(IPuaseHandler handler)
        {
            _handlers.Add(handler);
        }

        public void UnRegister(IPuaseHandler handler)
        {
            _handlers.Remove(handler);
        }

        public void SetPause(bool paused)
        {
            Paused = paused;

            foreach (var handler in _handlers)
            {
                handler.SetPause(paused);
            }
        }
    }
}