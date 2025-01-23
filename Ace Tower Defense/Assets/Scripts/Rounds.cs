using UnityEngine;
using TMPro;

public class Rounds : MonoBehaviour
{
    public int startRound;
    public int currentRound;
    public int endRound;
    public int diesToDestroy;
    public TextMeshProUGUI roundCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        roundCounter.text = "round: " + currentRound.ToString();
    }
}
