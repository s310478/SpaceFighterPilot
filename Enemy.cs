using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard; // Get the script ScoreBoard.cs, use it's public classes
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if(hitPoints < 1)
        {
            KillEnemy();
        }
        
    }

    void ProcessHit()
    {
        GameObject fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform; 

        hitPoints--; // subtract 1
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); // transorm.position = current position, Quternion.identity = no rotation
        fx.transform.parent = parentGameObject.transform; // the explosion vfx, once instantiated, becomes a child of 'parentGameObject' (= Spawn At Runtime gameObject)

        //Debug.Log($"{this.name} hit by {other.gameObject.name}");
        Destroy(gameObject);
    }
}
