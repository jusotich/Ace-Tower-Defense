using System.Linq;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    private GameManager gameManager;
    private int drawDeckCost = 100;
    public int card;
    public Sprite[] cardSprites;
    public GameObject cardUIExplain;
    public UnityEngine.UI.Image cardUIImage;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        int amountOfCards = cardSprites.Length;

     
        float randomValue = Mathf.Pow(Random.value, 2.5f); 

 
        int card = Mathf.FloorToInt(randomValue * (amountOfCards - 1));

        return card;
    }


    private void WhatCard(int card)
    {

        cardUIImage.sprite = cardSprites[card];
        cardUIExplain.SetActive(true);
    }
}
