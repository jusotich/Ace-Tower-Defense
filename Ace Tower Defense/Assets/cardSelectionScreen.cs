using UnityEngine;

public class cardSelectionScreen : MonoBehaviour
{
    public CardDeck cardDeck;
    public GameObject cardSelection;

    private int selectedCard;

    public void DiscardCard()
    {
        // Skicka tomt torn och disable placering
        BuildManager.main.SelectTower(0, false);
        cardSelection.SetActive(false);
    }


    public void PlaceSelectedCard()
    {
        int selectedCard = cardDeck.card + 1;

        if (selectedCard >= BuildManager.main.towerPrefabs.Length)
        {
            Debug.LogWarning("Selected card index is out of range!");
            return;
        }

        BuildManager.main.SelectTower(selectedCard, true);
        cardSelection.SetActive(false);
    }
}