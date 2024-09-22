using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.Runtime.Services.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickData : MonoBehaviour
    {
        public bool ClickedThisFrame { get; private set; }
        
        private Button _button;
        private ILateUpdate _update;

        [Inject]
        private void GetDependencies(ILateUpdate update)
        {
            _update = update;
        }

        private void Start()
        {
            _button = GetComponent<Button>();
            _update.OnLateUpdate += OnUpdate;
            
            _button.onClick.AddListener(() => ClickedThisFrame = true);
        }

        private void OnUpdate()
        {
            ClickedThisFrame = false;
        }

        private void OnDestroy()
        {
            _update.OnLateUpdate -= OnUpdate;
        }
    }
}