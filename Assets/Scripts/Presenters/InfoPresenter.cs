using System;
using System.Collections.Generic;
using Views;

namespace Presenters
{
    public class InfoPresenter
    {
        private readonly InfoView infoView;
        private readonly List<CardView> cardViews;

        public InfoPresenter(InfoView infoView, List<CardView> cardViews)
        {
            this.infoView = infoView;
            this.cardViews = cardViews;
            CalculateValues();
            AddListeners();
        }

        private void CalculateValues()
        {
            var health = 0;
            var attack = 0;
            var mana = 0;
            foreach (var cardView in cardViews)
            {
                health += Convert.ToInt32(cardView.health.text);
                attack += Convert.ToInt32(cardView.attack.text);
                mana += Convert.ToInt32(cardView.mana.text);
            }
            infoView.SetValues(health, attack, mana);
        }

        private void AddListeners()
        {
            foreach (var cardView in cardViews)
            {
                cardView.OnHealthChanged += CalculateValues;
                cardView.OnDestroyed += RemoveValue;
            }
        }

        private void RemoveValue(CardView cardView)
        {
            cardView.OnHealthChanged -= CalculateValues;
            cardView.OnDestroyed -= RemoveValue;
            cardViews.Remove(cardView);
            CalculateValues();
        }
    }
}