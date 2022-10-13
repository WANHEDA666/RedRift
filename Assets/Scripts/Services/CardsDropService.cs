using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Views;

namespace Services
{
    public class CardsDropService
    {
        private readonly CardHolderView cardHolderView;
        private readonly CardsDropView cardsDropView;
        private readonly List<CardView> cardViews;
        private const int moveDuration = 1;

        public CardsDropService(CardHolderView cardHolderView, CardsDropView cardsDropView, List<CardView> cardViews)
        {
            this.cardHolderView = cardHolderView;
            this.cardsDropView = cardsDropView;
            this.cardViews = cardViews;
            AddListeners();
        }

        private void AddListeners()
        {
            foreach (var cardView in cardViews)
            {
                cardView.OnCardPicked += RemoveCard;
                cardView.OnCardDropped += CompareCardDrop;
                cardView.OnDestroyed += RemoveCard;
            }
        }

        private void CompareCardDrop(CardView cardView)
        {
            var result = WorldRect(cardsDropView.rectTransform).Overlaps(WorldRect(cardView.rectTransform));
            if (result)
            {
                AnimateCard(cardView, cardsDropView.transform.position);
            }
            else
            {
                cardViews.Add(cardView);
                cardView.transform.SetAsLastSibling();
                cardHolderView.PlaceCards(cardViews);
            }
        }

        private void RemoveCard(CardView cardView)
        {
            cardViews.Remove(cardView);
            cardHolderView.PlaceCards(cardViews);
        }

        private Rect WorldRect(RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
        }
        
        private void AnimateCard(CardView cardView, Vector3 destination)
        {
            cardView.transform.DOMove(destination, moveDuration);
        }
    }
}