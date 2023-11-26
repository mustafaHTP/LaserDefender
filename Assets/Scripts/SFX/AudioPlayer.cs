using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Fire SFX")]
    [Space(1)]
    [SerializeField] private AudioClip fireClip;
    [SerializeField][Range(0f, 1f)] private float fireClipVolume = 1f;
    
    [Header("Damage SFX")]
    [Space(1)]
    [SerializeField] private AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] private float damageClipVolume = 1f;

    public void PlayFireSFX()
    {
        AudioSource.PlayClipAtPoint(
            fireClip, 
            Camera.main.transform.position, 
            fireClipVolume);
    }

    public void PlayDamageSFX()
    {
        AudioSource.PlayClipAtPoint(
            damageClip,
            Camera.main.transform.position,
            damageClipVolume);
    }
}
