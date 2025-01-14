using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening; // Import DOTween namespace

public class DissolveEffect : MonoBehaviour
{
    public Material dissolveMaterial; // Assign the material with the dissolve shader
    public Material dissolveMaterialInstance; // Assign the material with the dissolve shader
    public float dissolveDuration; // Duration of the dissolve effect
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // instantiate dissolve material so each one is independent
        dissolveMaterialInstance = new Material(dissolveMaterial);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material = dissolveMaterialInstance;

        //StartDissolve();
    }

    public void StartDissolve()
    {
        // Tween the _DissolveThreshold property from 0 to 1
        DOTween.To(() => dissolveMaterialInstance.GetFloat("_DissolveThreshold"), 
                   x => dissolveMaterialInstance.SetFloat("_DissolveThreshold", x), 
                   1f, dissolveDuration)
               .SetEase(Ease.InOutQuad);
    }
}

