using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{

    public GameObject gameOverScreen;
    public GameObject endScreen;
    public bool endTriggerActive = false;
    public AudioSource deathSound;
    private AudioSource[] allAudioSources;

    //Handle Monster interaction. Shows respective screen on death
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            StopAllAudio();
            deathSound.Play();  
            Time.timeScale = 0;
            
            if (endTriggerActive)
            {
                endScreen.SetActive(true);
            } 
            else
            {
                gameOverScreen.SetActive(true);
            }
            
        }

        
    }
 
    //Stop all audio for when player dies
    void StopAllAudio() {
     allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
     foreach( AudioSource audioS in allAudioSources) {
         audioS.Stop();
    }
 }
}
