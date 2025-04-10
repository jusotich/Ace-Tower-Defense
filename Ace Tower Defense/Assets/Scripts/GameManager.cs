using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int hp;
    public int round;
    public float cash;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI cashText;

    private void Update()
    {
        hpText.text = "HP: " + hp.ToString();
        roundText.text = "Round: " + round.ToString();
        cashText.text = "Cash: " + cash.ToString();
    }
    public void TakeDamage(int takenDamage)
    {
        hp -= takenDamage;
    }
    public void GetCash(int moneyAmount)
    {
        if(round <= 20)
        {
            cash += moneyAmount;
        }
        else if (round <= 50)
        {
            cash += moneyAmount;
            return;
        }
        else if(round > 50)
        {
            return;
            cash += moneyAmount/2f;
        }
        
    }
    public void AdvanceRound()
    {
        GetCash(1000);
        round++;
    }
}
