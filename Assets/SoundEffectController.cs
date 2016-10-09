using UnityEngine;
using System.Collections;

public class SoundEffectController : MonoBehaviour {

    public static SoundEffectController Instance;

    public AudioClip[] jumpSound;
    public AudioClip[] wallJumpSound;
    public AudioClip[] landingSound;
    public AudioClip[] stepSound;


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
