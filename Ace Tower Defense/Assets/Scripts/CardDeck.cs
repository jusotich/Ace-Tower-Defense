using System.Linq;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    private GameManager gameManager;
    private int drawDeckCost = 100;
    private int card;
    public Sprite[] cardSprites;
    public GameObject cardUIExplain;
    public UnityEngine.UI.Image cardUIImage;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Assign gameManager dynamically
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        if (cardUIImage == null)
        {
            Debug.LogError("CardUIImage is not assigned in the Inspector!");
        }

        if (cardUIExplain == null)
        {
            Debug.LogError("CardUIExplain is not assigned in the Inspector!");
        }
    }
    public void OnMouseDown()
    {
        if(gameManager.cash > drawDeckCost)
        {
            gameManager.cash -= drawDeckCost;
            
            card = RandomCard();
            
            WhatCard(card);

        }
        else
        {
            Debug.Log("Not Enough Money poor man!");
        }
    }

    private int RandomCard()
    {
        int amountOfCards = cardSprites.Count();
        int card = Random.Range(0, amountOfCards);
        return card;
    }

    private void WhatCard(int card)
    {

        cardUIImage.sprite = cardSprites[card];
        cardUIExplain.SetActive(true);
    }
}
