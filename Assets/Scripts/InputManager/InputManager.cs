using System;
using UnityEngine;

namespace SuikaClone.Core
{
    public class InputManager : MonoBehaviour, IInputReader
    {
        public bool MouseLMB { get; private set; }
        public Vector2 MousePosition { get; private set; }

        private void Update()
        {
            HandleMouseInput();
            HandleMousePosition();
        }

        private void HandleMouseInput()
        {
            // 0 -> Left Mouse Button
            if (Input.GetMouseButtonDown(0)) MouseLMB = true;
            else MouseLMB = false;
        }

        private void HandleMousePosition()
        {
            MousePosition = Input.mousePosition;
        }
    }
}
