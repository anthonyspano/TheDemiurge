using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using com.ultimate2d.combat;

// change OnHealthChanged to flash red
namespace com.ultimate2d.combat
{
	public class PlayerManager : MonoBehaviour
	{
		public float drawMagnitude; // to help fix cursor bug
		
		// singleton
		private static PlayerManager _instance;
		public static PlayerManager Instance
		{
			get { return _instance; }
		}

		public static GameObject player;



		// health
		[Header("Health")]
		[SerializeField] private int maxHealth;
		public Transform pfHealthBar;
		public HealthSystem pHealth;
		public float positionOffset;

		// ultimate
		[Header("Ultimate")]
		public int maxUlt = 100;
		public int ultCost = 20;
		public UltimateBar ultBar;
		public float invulnAfterHit;
		public int ultAddedOnHit;
		public bool ultReady;

		// animator
		public Animator anim;
		private bool animFinished;

		// for damage
		[Header("Damage")]
		public LayerMask enemyLayerMask;
		public int Attack;
		private SpriteRenderer sr;

		// player properties
		[Header("Special Properties")]
		public float range;
		public float attackCooldownRate; // for attack - 0.21
		public float attackCooldown;
		public float jumpCooldownRate;
		public float jumpCooldown;
		public float JumpDistance;
		public float MaxJumpTime;
		public float JumpSpeed;
		public float MDD;
		public float AttackMoveDistance;
		private RotateAroundPlayer playerAim;
		public float moveSpeed;
		private float inputWindow = 1f;

		// audio
		[Header("Audio")]
		public AudioClip slash1;
		public AudioClip hurt1;
		private AudioSource audioSource;

		// death screen
		[Header("Death Screen")]
		public GameObject ppv;
		PostProcessVolume m_Volume;
		Vignette m_Vignette;
		ColorGrading m_ColorGrading;
		float w;

		// etc
		[Header("Etc")]
		public GameObject wrongWayPanel;
		private bool canMove = true;
		private int wrongWayCount = 0;
		public float pushBackIntensity;
		public bool isBusy;

		public bool CanMove
		{
			get { return canMove;}
			set { canMove = value; }
		}

		[Range(0.01f, 0.99f)]
		public float verticalRunMod;

		private Transform hitbox;
		private BoxCollider2D boxCollider;

		private Vector3 lastMove;
		public Vector3 LastMove
		{
			get { return lastMove; }
			set { lastMove = value; }
		}

		public enum Direction {DownLeft, DownRight, UpLeft, UpRight};
		public Direction pFacingDir;

		public AnimationClip clip;
		[HideInInspector] public float attackAnimLength;

		public Vector2 PitSpawnPoint;

		// Animation control
		[HideInInspector] public bool continueChain;

		// for game manager
		public int killCount = 0;

		private void Start()
		{
			if(player == null)
				player = GameObject.Find("Player");

			// health
			pHealth = new HealthSystem(maxHealth, invulnAfterHit);
			var healthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
			healthBar.Setup(pHealth);

			// ultimate 
			//ultBar.SetUlt(0);
			ultBar.OnUltReady += UltIsReady;

			// death event
			pHealth.OnHealthChanged += OnDamage;
			anim = transform.GetComponent<Animator>();

			// damage
			sr = GetComponent<SpriteRenderer>();

			// wrong way dialogue box
			//wrongWayPanel = GameObject.Find("WrongWayDialogue");

			// aim
			playerAim = GetComponentInChildren<RotateAroundPlayer>();

			// audio
			audioSource = GetComponent<AudioSource>();

			// collision
			boxCollider = GetComponent<BoxCollider2D>();
			hitbox = transform.GetChild(3);

			// animation
			attackAnimLength = clip.length;

		}

		private void Awake()
		{
			// singleton
			if (_instance != null && _instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				_instance = this;
			}

			// remove post effects
			ppv.SetActive(false);

		}

		void Update()
		{

			var x = Input.GetAxis("Horizontal");
			var y = Input.GetAxis("Vertical");
			if(Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f)
			{
				LastMove = new Vector3(x,y,0).normalized;
			}
			anim.SetFloat("MoveX", LastMove.x);
			anim.SetFloat("MoveY", LastMove.y);

			float facingDir = Mathf.Atan2(LastMove.y, LastMove.x) * Mathf.Rad2Deg;
			if(facingDir < 90 && facingDir >= 0) 
				pFacingDir = Direction.UpRight; // player face
			else if(facingDir >= 90 && facingDir <= 180) 
				pFacingDir = Direction.UpLeft;
			else if(facingDir >= -90 && facingDir < 0) 
				pFacingDir = Direction.DownRight;
			else if(facingDir >= -180 || facingDir < -90) 
				pFacingDir = Direction.DownLeft;
			
			
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			
			//Debug.Log("hit");

			if(col.transform.CompareTag("BigCultist"))
			{
				if(col.GetContact(0).collider.transform.CompareTag("PlayerHitBox"))
				{
					Instance.pHealth.Damage(70);
					audioSource.PlayOneShot(hurt1, 0.7f);
				}

			}

			if(col.transform.CompareTag("Projectile"))
			{
				//Debug.Log(col.GetContact(0).otherCollider.transform.name);
				if(col.GetContact(0).otherCollider.transform.CompareTag("PlayerAttack"))
				{
					Instance.pHealth.Damage(40);
					audioSource.PlayOneShot(hurt1, 0.7f);
				}
			}

		}

		private IEnumerator ToggleSpawnCollider(Collider2D col)
		{


			// wait until player is x distance from spawn point
			yield return new WaitUntil(() => Vector2.Distance(transform.position, PitSpawnPoint) > 4);
			col.enabled = true;

		}

		private void OnDamage(object sender, System.EventArgs e)
		{
			if(pHealth.GetHealth() <= 0)
			{
				// Death sequence
				anim.SetBool("isDead", true);
				Death();
			}

			else
			{
				StartCoroutine(FlashRed());
			}



		}

		private IEnumerator FlashRed()
		{
			var timer = 0.28f;
			sr.color = Color.red;
			yield return new WaitForSeconds(timer);
			sr.color = Color.white;
			yield return new WaitForSeconds(timer);
			sr.color = Color.red;
			yield return new WaitForSeconds(timer);
			sr.color = Color.white;

		}

		private void Death()
		{
			// triggered after death animation
			anim.enabled = false;
			canMove = false;
			isBusy = true;

			var reticle = transform.Find("Reticle");
			reticle.gameObject.SetActive(false);

			ppv.SetActive(true);
			// reset scene after timer
			StartCoroutine("LoadScene");
		}

		IEnumerator LoadScene()
		{
			yield return new WaitForSeconds(2.5f);
			SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
		}

		public bool AnimFinished()
		{
			return animFinished;
		}

		public void FinishAttackAnimation()
		{
			PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
			anim.Play("Player Idle");
		}


		public void FinishJumpAnimation()
		{
			anim.SetBool("isJumping", false);
		}

		private bool jumping;

		public void StartJumpCD()
		{
			if(!jumping)
				StartCoroutine("JumpCooldown");
		}

		private IEnumerator JumpCooldown()
		{
			jumping = true;
			yield return new WaitForSeconds(jumpCooldownRate);
			jumpCooldown = 0;
			jumping = false;
		}

		public void StartAttackCD()
		{
			attackCooldown = attackCooldownRate;
			StartCoroutine("AttackCooldown");
			
		}

		private IEnumerator AttackCooldown()
		{
			yield return new WaitForSeconds(attackCooldownRate);
			attackCooldown = 0;
		}

		public void PlayAttackSound()
		{
			audioSource.Play();
		}

		public void PushBack()
		{
			// get position of beam 
			var cursorPos = GetComponentInChildren<PowerManager>().transform.position;
			// translate in opposite direction in relation to player (take the difference)
			transform.Translate((transform.position - cursorPos) * Time.deltaTime * pushBackIntensity);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, transform.position + LastMove * drawMagnitude);
		}

		private void UltIsReady(object sender, EventArgs e)
		{
			ultReady = true;
		}

		public void FireUltimate()
		{
			GetComponentInChildren<PowerManager>().FireUltimate();
		}
		
		public void LogAnimationID()
		{
			Debug.Log(anim.GetCurrentAnimatorStateInfo(0).shortNameHash);
		}


	}
}