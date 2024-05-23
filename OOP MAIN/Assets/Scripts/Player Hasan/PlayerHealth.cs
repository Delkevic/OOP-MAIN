using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image healthBar;
    Animator anim;
    bool isImmune;
    public bool isDying=false;

    public float immunityTime;
    public float defense = 10;

    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
        maxHealth = PlayerPrefs.GetFloat("MaxHealth", maxHealth);
        currentHealth = PlayerPrefs.GetFloat("CurrentHealth", currentHealth);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&& !isImmune)
        {
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            if (player.isBlocking)
                currentHealth -= collision.GetComponent<EnemyStats>().damage * ((defense + 40) / 100);
            else
                currentHealth -= collision.GetComponent<EnemyStats>().damage * (defense / 100);
            StartCoroutine(Immunity());
            anim.SetTrigger("Hit");
            if (currentHealth <= 0)
            {
                StartCoroutine(death());
            }
        }

    }

    IEnumerator death()
    {
        isDying = true;
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        currentHealth = 0;
        Destroy(gameObject);
        healthBar.fillAmount = 0;
    }

    IEnumerator Immunity()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityTime);
        isImmune = false;
    }
}
