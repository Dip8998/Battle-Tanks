using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankView : MonoBehaviour
{
    private TankController tankController;

    private float movement;
    private float rotation;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer[] childs;
    [SerializeField] public Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color zeroHealthColor = Color.white;
    [SerializeField] private GameObject explosionPrefab;
    private GameOverController gameOverScreen;

    private AudioSource explosionAudio;
    private ParticleSystem explosionParticle;

    private void Awake()
    {
        explosionParticle = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionAudio = explosionParticle.GetComponent<AudioSource>();
        explosionParticle.gameObject.SetActive(false);
    }

    void Update()
    {
        Movement();

        if (movement != 0)
        {
            tankController.Move(movement);
        }

        if (rotation != 0)
        {
            tankController.Rotate(rotation);
        }

    }

    public void SetHealthUI()
    {
        healthSlider.value = tankController.GetTankModel().currentHealth;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, tankController.GetTankModel().currentHealth / tankController.GetTankModel().startingHealth);
    }

    private void Movement()
    {
        movement = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public void ChangeColor(Material color)
    {
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].material = color;
        }
    }

    public TankController GetTankController()
    {
        return tankController;
    }
    public void SetGameOverScreen(GameOverController gameOver)
    {
        gameOverScreen = gameOver;
    }

    public void OnDeath()
    {
        tankController.GetTankModel().isDead = true;

        explosionParticle.transform.position = transform.position;
        explosionParticle.gameObject.SetActive(true);
        explosionParticle.Play();
        explosionAudio.Play();
        GameObject destroyedTank = this.gameObject; 

        if (gameOverScreen != null)
        {
            gameOverScreen.gameObject.SetActive(true);
            gameOverScreen.SetCurrentTank(destroyedTank); 
        }
        else
        {
            Debug.LogError("GameOverController reference is null in TankView!");
        }
        Destroy(destroyedTank);
    }
}
