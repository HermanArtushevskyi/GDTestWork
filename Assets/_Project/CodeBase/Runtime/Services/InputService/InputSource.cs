using _Project.CodeBase.Runtime.Services.InputService.Common;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.InputService
{
    public class InputSource : IInputSource
    {
        private readonly IInputSource _keyboardSource;

        public InputSource(IInputSource keyboardSource)
        {
            _keyboardSource = keyboardSource;
        }

        public RawInput GetInput()
        {
            RawInput input = _keyboardSource.GetInput();
            return input;
        }
    }
    
    public class KeyboardSource : IInputSource
    {
        public RawInput GetInput()
        {
            RawInput input = new RawInput();
            input.Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return input;
        }
    }
}