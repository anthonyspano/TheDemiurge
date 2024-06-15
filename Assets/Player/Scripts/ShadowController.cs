using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
	public class ShadowController : MonoBehaviour
	{
		private UltimateBar ultBar;

		private void Start()
		{
			ultBar = PlayerManager.Instance.ultBar;
		}
		
		// private void OnTriggerEnter2D(Collider2D other)
		// {
		// 	if(other.gameObject.CompareTag("HitBox"))
		// 	{
		// 		Debug.Log("shadow hit");
		// 		ultBar.AddUlt(20);
		// 		Destroy(gameObject);
		// 	}
		// }

		private void OnCollisionEnter2D(Collision2D other)
		{

			if(other.gameObject.CompareTag("HitBox"))
			{
				Debug.Log("shadow hit");
				ultBar.AddUlt(20);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("BigCultist"))
			{
				Debug.Log("shadow hit");
				ultBar.AddUlt(20);
				Destroy(gameObject);
			}
		}


	}
}