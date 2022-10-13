using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class ChangeValueView : MonoBehaviour
    {
        [SerializeField] private Button changeValueButton;
        public event Action OnChangeValueButtonClicked;

        private void Awake()
        {
            changeValueButton.onClick.AddListener(() => OnChangeValueButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            changeValueButton.onClick.RemoveAllListeners();
        }
    }
}