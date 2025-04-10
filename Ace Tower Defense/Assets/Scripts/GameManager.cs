using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        DIE();
    }
    public void DIE()
    {
        if (hp <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void GetCash(int moneyAmount)
    {

        if (round > 5)
        {
            cash += moneyAmount/2;
            return;
        }
        else
        {
            cash += moneyAmount;
        }
        
    }
    public void AdvanceRound()
    {
        GetCash(200);
        round++;
    }
}
