using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.name} Triggered by {other.gameObject.name}");
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        crashVFX.GetComponent<AudioSource>().enabled = true;
        var playerControls = GetComponent<PlayerControls>();
        playerControls.enabled = false;
        playerControls.StopFiring();
        GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<Rigidbody>().useGravity = true;
        transform.Find("Collider")?.gameObject.SetActive(false);
        transform.parent.GetComponent<Animator>().enabled = false;

        Invoke("ReloadLevel", loadDelay);
    }
    
    void ReloadLevel()
    {
        gameManager.ReloadLevel();
    }
}
