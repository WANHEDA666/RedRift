using Models;
using Views;

namespace Presenters
{
    public class CardPresenter
    {
        private readonly CardView cardView;
        private readonly CardModel cardModel;

        public CardPresenter(CardView cardView, CardModel cardModel)
        {
            this.cardView = cardView;
            this.cardModel = cardModel;
            AddListeners();
            cardModel.DownloadBytes();
        }

        private void AddListeners()
        {
            cardModel.ImageAnswer += cardView.CreateTexture;
            cardView.OnNewHealthReceived += SetNewNewHealth;
        }

        private void SetNewNewHealth(int health)
        {
            if (health > 0) cardView.ChangeHealth(health);
            else RemoveListeners();
        }

        private void RemoveListeners()
        {
            cardModel.ImageAnswer -= cardView.CreateTexture;
            cardView.Destroy();;
        }
    }
}