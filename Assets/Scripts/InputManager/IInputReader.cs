using UnityEngine;

namespace SuikaClone.Core
{
    public interface IInputReader
    {
        public bool MouseLMB { get; }
        public Vector2 MousePosition { get;  }
    }
}
