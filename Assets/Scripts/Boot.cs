using Factories;
using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private GameFactory gameFactory;

    private void Awake()
    {
        var cardViews = gameFactory.CreateCards(Random.Range(4, 7));
        gameFactory.CreateCardsDropService(cardViews);
        gameFactory.CreateValueChangeService(cardViews);
        gameFactory.CreateInfo(cardViews);
    }
}