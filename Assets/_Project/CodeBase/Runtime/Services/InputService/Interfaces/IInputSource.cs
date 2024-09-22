using _Project.CodeBase.Runtime.Services.InputService.Common;

namespace _Project.CodeBase.Runtime.Services.InputService.Interfaces
{
    public interface IInputSource
    {
        public RawInput GetInput();
    }
}