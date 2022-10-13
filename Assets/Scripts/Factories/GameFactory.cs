using System.Collections.Generic;
using Models;
using Presenters;
using Services;
using UnityEngine;
using Views;

namespace Factories
{
    public class GameFactory : MonoBehaviour
    {
        [SerializeField] private CardView cardView;
        [SerializeField] private CardHolderView cardHolderView;
        [SerializeField] private CardsDropView cardsDropView;
        [SerializeField] private ChangeValueView changeValueView;
        [SerializeField] private InfoView infoView;

        public List<CardView> CreateCards(int count)
        {
            var views = new List<CardView>();
            for (var i = 0; i < count; i++)
            {
                var view = Instantiate(cardView, cardHolderView.transform);
                var model = new CardModel();
                var presenter = new CardPresenter(view, model);
                views.Add(view);
            }
            cardHolderView.PlaceCards(views);
            return views;
        }

        public void CreateCardsDropService(List<CardView> cardViews)
        {
            var service = new CardsDropService(cardHolderView, cardsDropView, cardViews);
        }

        public void CreateValueChangeService(List<CardView> cardViews)
        {
            var service = new ValueChangeService(changeValueView, cardViews);
        }

        public void CreateInfo(List<CardView> cardViews)
        {
            var presenter = new InfoPresenter(infoView, cardViews);
        }
    }
}