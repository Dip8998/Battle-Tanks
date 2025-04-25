using UnityEngine;

public class ShellView : MonoBehaviour
{
    [SerializeField] private GameObject bulletParticleEffect;
    [SerializeField] private CapsuleCollider bulletCollider;
    [SerializeField] private int onPlayerHealth;
    [SerializeField] private int onEnemyHealth;
    [SerializeField] private AudioClip shellExplosionAudio;

    private ParticleSystem bulletExplosion;
    [SerializeField] private AudioSource bulletExplosionSource;
    private ShellController shellController;

    private void Start()
    {
        bulletParticleEffect.SetActive(true);
        bulletExplosion = bulletParticleEffect.GetComponent<ParticleSystem>();
        bulletExplosionSource = bulletExplosion.GetComponent<AudioSource>();
    }

    public void SetShellController(ShellController controller)
    {
        shellController = controller;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyView enemy = collision.gameObject.GetComponent<EnemyView>();
            if (enemy != null)
            {
                enemy.TakeDamage(onEnemyHealth); 
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            TankView playerTank = collision.gameObject.GetComponent<TankView>();
            if (playerTank != null)
            {
                playerTank.GetTankController().TakeDamage(onPlayerHealth);
            }
        }

        PlayExplosionEffect();
    }


    public void PlayExplosionEffect()
    {
        bulletCollider.enabled = false;

        GameObject effect = Instantiate(bulletParticleEffect, transform.position, Quaternion.identity);
        ParticleSystem explosion = effect.GetComponent<ParticleSystem>();
        AudioSource explosionAudio = explosion.GetComponent<AudioSource>();

        explosion.Play();
        explosionAudio.clip = shellExplosionAudio;
        explosionAudio.Play();
        Destroy(effect, explosion.main.duration + explosion.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
