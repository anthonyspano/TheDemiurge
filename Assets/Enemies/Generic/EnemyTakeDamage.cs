﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
public class EnemyTakeDamage : MonoBehaviour
{
    // health
    public HealthSystem healthSystem;
    //public HealthBar healthBar; // referenced with scene healthbar

    public int maxHealth;

    public int ultAddedOnHit;

    private Animator anim;
    private EnemyManager em;
    private void Awake() 
    {
        anim = transform.parent.GetComponent<Animator>();
        
        // health
        healthSystem = new HealthSystem(maxHealth, 0f);
        //healthBar.Setup(healthSystem);
        // health - death event
        healthSystem.OnHealthChanged += OnDamage;
        em = transform.parent.GetComponent<EnemyManager>();
    }

	private void OnDamage(object sender, System.EventArgs e) 
	{
    	if(healthSystem.GetHealth() <= 0)
		{
			// Death sequence
			//anim.SetBool("isDead", true);
            //StartCoroutine("Death");
            GameManager.Instance.EnemyDeathCount();
            Destroy(transform.parent.gameObject);
		}
        else 
        {
            StartCoroutine(FlashRed());
            transform.parent.GetComponent<AudioSource>().PlayOneShot(em.hurtSound, 0.8f);
            if(healthSystem.GetHealth() < 50)
            {
                em.timeToReact = true;
            }
        }

	}

    IEnumerator Death()
    {
        // disable further movements
        // enemy manager
        // transform.parent.GetComponent<BlockBattleSystem>().CanMove = false;
        // transform.parent.GetComponent<BlockBattleSystem>().Dead = true;
        

        // play death anim
        anim.Play("enemy_death", 0);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length); 

        GameManager.Instance.EnemyDeathCount();


        Destroy(transform.parent.gameObject);
    }

    public IEnumerator FlashRed()
    {
        var repeatTimes = 3;
        var timer = 0.1f; // just seems like a good number
        var sr = transform.parent.GetComponent<SpriteRenderer>();
		for(int i = 0; i < repeatTimes; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(timer);
            sr.color = Color.white;
            yield return new WaitForSeconds(timer);
        }
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     if(col.GetContact(0).collider.transform.CompareTag("PlayerHitBox"))
    //     {
    //         healthSystem.Damage(PlayerManager.Instance.Attack);
    //         PlayerManager.Instance.ultBar.AddUlt(PlayerManager.Instance.ultAddedOnHit); // consider source
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.transform.name);
        if(col.transform.CompareTag("PlayerHitBox"))
        {
            healthSystem.Damage(PlayerManager.Instance.Attack);
            PlayerManager.Instance.ultBar.AddUlt(ultAddedOnHit); // consider source
        }
    }
}

}