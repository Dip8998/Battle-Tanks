using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    private EnemyController controller;
    private NavMeshAgent agent;
    private Transform player; 

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject enemyTankExplosion;

    private ParticleSystem enemyTankExplosionParticles;
    private AudioSource explosionAudio;

    private float nextFireTime = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyTankExplosionParticles = enemyTankExplosion.GetComponent<ParticleSystem>();
        explosionAudio = enemyTankExplosionParticles.GetComponent<AudioSource>();
    }

    public void SetPlayerTransform(Transform playerTransform) 
    {
        player = playerTransform;
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Debug.LogError("Player Transform not set in EnemyView!");
        }
    }

    private void Update()
    {
        if (player == null) return;

        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > 15f)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + controller.GetModel().fireRate;
                controller.Fire();
            }
        }
    }

    public void SetController(EnemyController enemyController)
    {
        controller = enemyController;
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * 20f;
    }

    public void TakeDamage(int damage)
    {
        controller.ReduceHealth(damage);
    }

    public void Die()
    {
        GameObject effect = Instantiate(enemyTankExplosion, transform.position, Quaternion.identity);

        ParticleSystem explosion = effect.GetComponent<ParticleSystem>();
        AudioSource explosionAudioFromEffect = effect.GetComponent<AudioSource>();

        explosion.Play();
        explosionAudioFromEffect.Play(); 

        Destroy(effect, explosion.main.duration + explosion.main.startLifetime.constantMax);
        
        Destroy(gameObject);
    }
}
