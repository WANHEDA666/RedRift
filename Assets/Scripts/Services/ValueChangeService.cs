using System.Collections.Generic;
using UnityEngine;
using Views;

namespace Services
{
    public class ValueChangeService
    {
        private readonly ChangeValueView changeValueView;
        private readonly List<CardView> cardViews;
        private int i;

        public ValueChangeService(ChangeValueView changeValueView, List<CardView> cardViews)
        {
            this.changeValueView = changeValueView;
            this.cardViews = cardViews;
            AddListeners();
        }

        private void AddListeners()
        {
            changeValueView.OnChangeValueButtonClicked += ChangeCardValue;
            foreach (var cardView in cardViews)
            {
                cardView.OnDestroyed += RemoveValue;
            }
        }

        private void ChangeCardValue()
        {
            if (cardViews.Count > 0)
            {
                var randomValue = Random.Range(-2, 10);
                cardViews[i].ReceiveNewHealth(randomValue);
                i = i + 1 == cardViews.Count ? 0 : i + 1;
            }
        }

        private void RemoveValue(CardView cardPresenter)
        {
            cardViews.Remove(cardPresenter);
            i -= 1;
        }
    }
}