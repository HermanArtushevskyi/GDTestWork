using _Project.CodeBase.Runtime.Services.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Runtime.UI
{
    public class GameView : MonoBehaviour, IView
    {
        public Button AttackBtn;
        public Button SuperAttackBtn;
        public Button RestartBtn;
        public TextMeshProUGUI HealthText;
        public TextMeshProUGUI WaveText;
        public GameObject WinPanel;
        public GameObject LosePanel;
    }
}