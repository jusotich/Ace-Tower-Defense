using TMPro;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int hp;
    public TextMeshProUGUI hpCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpCounter.text = "hp" + hp.ToString();
    }
}
