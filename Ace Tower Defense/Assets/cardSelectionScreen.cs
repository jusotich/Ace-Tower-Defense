using UnityEngine;

public class cardSelectionScreen : MonoBehaviour
{
    CardDeck cardDeck;
    
    public GameObject cardSelection;

    private int selectedCard;

    public void DiscardCard()
    {
        cardSelection.SetActive(false);
    }

    public void PlaceSelectedCard()
    {
        selectedCard = cardDeck.card;

        cardSelection.SetActive(false);
    }
}
