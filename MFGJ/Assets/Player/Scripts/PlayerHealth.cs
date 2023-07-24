using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider hydrationMeter;
    public Slider healthBar;
    public int hydration = 50;
    public bool damageable = true;

    [SerializeField]
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = 100;
        hydrationMeter.maxValue = 50;
        InvokeRepeating("Dehydrate", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        hydrationMeter.value = hydration;
        healthBar.value = health;
    }

    void Dehydrate()
    {
        hydration--;
    }

    public void TakeDamage(int damage)
    {
        if (damageable) health -= damage;
    }
}
