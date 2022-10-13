using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views
{
    public class CardView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image lightImage;
        public TextMeshProUGUI health;
        public TextMeshProUGUI attack;
        public TextMeshProUGUI mana;
        public RectTransform rectTransform;
        public event Action<CardView> OnCardDropped;
        public event Action<CardView> OnCardPicked;
        public event Action<CardView> OnDestroyed;
        public event Action<int> OnNewHealthReceived;
        public event Action OnHealthChanged;
        private Camera camera;

        public void ReceiveNewHealth(int health)
        {
            OnNewHealthReceived?.Invoke(health);
        }

        public void ChangeHealth(int health)
        {
            float time = 0;
            var currentHealth = Convert.ToInt32(this.health.text);
            Observable.EveryUpdate().Select(x =>
            {
                time += Time.deltaTime / 1;
                return (int) Mathf.Lerp(currentHealth, health, time);
            }).TakeWhile(x => x < health).Subscribe(x => { this.health.text = x.ToString(); }, () =>
            {
                this.health.text = health.ToString();
                OnHealthChanged?.Invoke();
            });
        }
        
        public void CreateTexture(byte[] image)
        {
            var texture2D = new Texture2D(2, 2);
            texture2D.LoadImage(image);
            texture2D.Apply();
            backgroundImage.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            lightImage.enabled = true;
            rectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            camera = Camera.main;
            OnCardPicked?.Invoke(this);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var distanceToScreen = camera.WorldToScreenPoint(gameObject.transform.position).z;
            var movePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
            rectTransform.position = new Vector3(movePosition.x, movePosition.y, transform.position.z);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            lightImage.enabled = false;
            OnCardDropped?.Invoke(this);
        }

        public void Destroy()
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}