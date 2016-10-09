using UnityEngine;
using System.Collections;

public class SoundEffectController : MonoBehaviour {

    public static SoundEffectController Instance;

    public AudioClip[] jumpSound;
    public AudioClip[] wallJumpSound;
    public AudioClip[] landingSound;
    public AudioClip[] stepSound;
    public AudioClip[] attackSound;
    public AudioClip[] hurtSound;
    public AudioClip[] punishedSound;
    public AudioClip[] pickFlowerSound;



    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakeJumpSound()
    {
        int sound = Random.Range(0, jumpSound.Length);
        MakeSound(jumpSound[sound]);
    }

    public void MakeWallJumpSound()
    {
        int sound = Random.Range(0, wallJumpSound.Length);
        MakeSound(wallJumpSound[sound]);
    }

    public void MakeLandingSound()
    {
        int sound = Random.Range(0, landingSound.Length);
        MakeSound(landingSound[sound]);
    }

    public void MakeStepSound()
    {
        int sound = Random.Range(0, stepSound.Length);
        MakeSound(stepSound[sound]);
    }

    public void MakeAttackSound()
    {
        int sound = Random.Range(0, attackSound.Length);
        MakeSound(attackSound[sound]);
    }

    public void MakeHurtSound()
    {
        int sound = Random.Range(0, hurtSound.Length);
        MakeSound(hurtSound[sound]);
    }

    public void MakePunishedSound()
    {
        int sound = Random.Range(0, punishedSound.Length);
        MakeSound(punishedSound[sound]);
    }

    public void MakePickFlowerSound()
    {
        int sound = Random.Range(0, pickFlowerSound.Length);
        MakeSound(pickFlowerSound[sound]);
    }

    /// <summary>
    /// Play a given sound
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
