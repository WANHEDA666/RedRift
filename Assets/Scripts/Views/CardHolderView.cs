using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class CardHolderView : MonoBehaviour
    {
        [SerializeField] private RectTransform center;
        private const int xDifference = 80;
        private const int yDifference = 10;
        private const int angle = 10;
        private const int moveDuration = 1;
        private int centerId;
        private int startX;
        private int startY;
        private int startAngle;
        private int i;

        public void PlaceCards(List<CardView> cardViews)
        {
            if (cardViews.Count % 2 == 1)
            {
                centerId = (cardViews.Count - 1) / 2;
                startX = centerId * -xDifference;
                startY = centerId * -yDifference;
                startAngle = centerId * angle;
                i = 0;
            }
            else
            {
                centerId = cardViews.Count / 2;
                startX = centerId * -xDifference + xDifference / 2;
                startY = (centerId - 1) * -yDifference;
                startAngle = centerId * angle - angle / 2;
                i = 1;
            }
            AnimateCards(cardViews);
        }

        private void AnimateCards(List<CardView> cardViews)
        {
            foreach (var cardView in cardViews)
            {
                AnimateCard(cardView, new Vector3(startX, startY));
                cardView.rectTransform.rotation = Quaternion.Euler(0f, 0f, startAngle);
                startX += xDifference;
                startY += i < centerId ? yDifference : -yDifference;
                startAngle -= angle;
                i++;
            }
        }
        
        private void AnimateCard(CardView cardView, Vector3 destination)
        {
            cardView.rectTransform.DOLocalMove(destination, moveDuration);
        }
    }
}