using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace com.ultimate2d.combat
{
public class PowerManager : MonoBehaviour
{
    public float range;
    public int SpecialDamage;
    private Animator anim;
    private UltimateBar ultimateCharge;
    public int ultCost;
    public Image powerIcon;


    // circling player
    private Vector3 positionOffset;
    [Range(0, 360)]
    private float angle = 0;
    public float CircleRadius;
    public float ElevationOffset = 0;
    public float RotationSpeed = 1;

    // sound
    private AudioSource audioSource;
    public AudioClip soundEffect;
    public AudioClip powerReadySound;

    public Material outlineShader;



    private void Start()
    {
        anim = GetComponent<Animator>();
        ultimateCharge = PlayerManager.Instance.GetComponent<UltimateBar>();
        powerIcon.color = new Color(0,0,0, .80f);

        audioSource = GetComponent<AudioSource>();
        
        ultimateCharge.OnUltReady += UltReady;
        ultimateCharge.OnUltUsed += UltUsed;
    }

    private void UltReady(object sender, EventArgs e)
    {
        powerIcon.color = new Color(1,1,1, 1f);
        // set aura around player
        outlineShader.SetFloat("_OutlineThickness", 1);
        audioSource.PlayOneShot(powerReadySound, 1f);

    }

    private void UltUsed(object sender, EventArgs e)
    {
        // use this to turn off bool
        powerIcon.color = new Color(0,0,0, .80f);
        outlineShader.SetFloat("_OutlineThickness", 0);
    }

    public void FireUltimate() // performs all immediate actions on screen
    {
        //Debug.Log("firing!");
        ultimateCharge.AddUlt(-ultCost);
        BeamSetup();
        anim.Play("BeamAttack");
        PlayerManager.Instance.anim.SetBool("isBeaming", true);
        PlayerManager.Instance.CanMove = false;
        StartCoroutine("PushBack"); // being pushed back during the ultimate
    }

    private void LateUpdate()
    {

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
        
            // Get stick input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Check if there's any stick input
            if (horizontal != 0 || vertical != 0)
            {
                // Calculate the target angle from stick input
                float targetAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

                // Get the current angle of the object relative to the player
                Vector2 directionToObject = transform.position - PlayerManager.Instance.transform.position;
                float currentAngle = Mathf.Atan2(directionToObject.y, directionToObject.x) * Mathf.Rad2Deg;

                // Calculate the angle difference
                float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

                // Rotate the object around the player
                float rotationStep = RotationSpeed * Time.deltaTime * Mathf.Sign(angleDifference);
                if (Mathf.Abs(rotationStep) > Mathf.Abs(angleDifference))
                {
                    rotationStep = angleDifference; // Snap to target angle if close enough
                }

                transform.RotateAround(PlayerManager.Instance.transform.position, Vector3.forward, rotationStep);
            }
        
        
        
        
        
        }



    }

    private float NegToPosRad(float a)
    {
        return Mathf.PI - Mathf.Abs(a) + Mathf.PI;
    }
    
    private float PosToNegRad(float a)
    {
        return a - Mathf.PI * 2;
    }

    public void BeamSetup()
    {
        // set position and rotation same as cursor
        var reticle = GameObject.Find("Reticle");
        var r_vector = reticle.transform.position - PlayerManager.Instance.transform.position;
        r_vector.Normalize();
        angle = Mathf.Atan2(r_vector.y, r_vector.x);
        r_vector *= 4.4f; // distance from player
        transform.position = reticle.transform.position + r_vector;
        transform.rotation = reticle.transform.rotation;

        // play beam sound
        audioSource.PlayOneShot(soundEffect);
        
        
    }


    private IEnumerator PushBack()
    {
        yield return null;
        while(anim.GetCurrentAnimatorStateInfo(0).IsName("BeamAttack"))
        {
            // push player back
            PlayerManager.Instance.PushBack();
            yield return null;
        }

        transform.parent.GetComponent<Animator>().SetBool("isBeaming", false);
    }
    public void StopAnimation()
    {
        PlayerManager.Instance.CanMove = true;
        PlayerManager.Instance.isBusy = false;
        anim.SetBool("IsBeaming", false);
        transform.parent.GetComponent<Animator>().SetBool("isBeaming", false);
        anim.Play("Empty");
        PlayerManager.Instance.ultReady = false;

    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(transform.position, range);
    // }
}

}