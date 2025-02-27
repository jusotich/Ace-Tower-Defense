using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int hp;
    public int round;
    public int cash;
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
        cash += moneyAmount;
    }
    public void AdvanceRound()
    {
        round++;
    }
}
