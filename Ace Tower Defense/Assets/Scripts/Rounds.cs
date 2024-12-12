using UnityEngine;
using TMPro;

public class Rounds : MonoBehaviour
{
    public int startRound;
    public int baloonsToPop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(baloonsToPop <= 0)
        {
            AdvanceRound();
        }
    }
    
    void AdvanceRound()
    {
        startRound++;
    }
}
