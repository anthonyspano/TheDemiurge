using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace com.ultimate2d.combat
{
public class HyperBeam : MonoBehaviour
{
    public float range;
    public int SpecialDamage;
    private Animator anim;
    private UltimateBar ultimateCharge;
    public int ultCost;
    public Image powerIcon;

    public float telegraphTime;


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
        PlayerManager.Instance._renderer.material = Resources.Load<Material>("GlowingOutline");
        PlayerManager.Instance.outlineThickness = 1;
        PlayerManager.Instance._renderer.material.SetFloat("_OutlineThickness", PlayerManager.Instance.outlineThickness); 
        audioSource.PlayOneShot(powerReadySound, 1f);

    }

    private void UltUsed(object sender, EventArgs e)
    {
        // use this to turn off bool
        powerIcon.color = new Color(0,0,0, .80f);
        PlayerManager.Instance.outlineThickness = 0;
        PlayerManager.Instance._renderer.material = Resources.Load<Material>("GlowingOutline");
        PlayerManager.Instance._renderer.material.SetFloat("_OutlineThickness", PlayerManager.Instance.outlineThickness); 
    }

    public IEnumerator FireUltimate() // performs all immediate actions on screen
    {
        // allow player to move while charging
        PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Move;
        //Debug.Log("firing!");
        ultimateCharge.AddUlt(-ultCost);
        // enable spriterenderer for reticle to telegraph
        GameObject.Find("Reticle").GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log(PlayerController.Instance.playerStatus);
        // pause for beam telegraph
        yield return new WaitForSeconds(telegraphTime);
        // lock player in for ultimate firing
        PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Ultimate;
        // set position of beam object to cursor
        // ensure beam object is in correct position
        BeamSetup();
        // play beam sound
        audioSource.PlayOneShot(soundEffect);
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

    // TBI: enable reticle sprite renderer to show that the beam is priming for a second or so 
    // then disable right before ult fires
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

     
        //transform.parent.GetComponent<Animator>().SetBool("isBeaming", false);
    }

    public void StopAnimation()
    {
        // end of beam animation
        PlayerManager.Instance.CanMove = true;
        PlayerManager.Instance.isBusy = false;
        anim.SetBool("IsBeaming", false);
        transform.parent.GetComponent<Animator>().SetBool("isBeaming", false);
        anim.Play("Empty");
        PlayerManager.Instance.ultReady = false;
        Debug.Log(GameObject.Find("Reticle"));
        GameObject.Find("Reticle").GetComponent<SpriteRenderer>().enabled = false;

    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(transform.position, range);
    // }
}

}