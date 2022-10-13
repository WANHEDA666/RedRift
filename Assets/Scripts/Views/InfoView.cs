using TMPro;
using UnityEngine;

namespace Views
{
    public class InfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI attack;
        [SerializeField] private TextMeshProUGUI mana;

        public void SetValues(int health, int attack, int mana)
        {
            this.health.text = health.ToString();
            this.attack.text = attack.ToString();
            this.mana.text = mana.ToString();
        }
    }
}