using UnityEngine;

public class EnemyBasklass : MonoBehaviour
{
    public float startHealth = 200f;
    private float health;
    public int damage = 10;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
