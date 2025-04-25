using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private ShellView shellPrefab;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Slider aimSlider;
    [SerializeField] private float minLaunchForce = 15f;
    [SerializeField] private float maxLanuchForce = 30f;
    [SerializeField] private float maxChargeTime = 0.75f;
    
    [SerializeField] private CameraController controller;
    [SerializeField] private AudioSource shootingAuido;
    [SerializeField] private AudioClip chargingClip;
    [SerializeField] private AudioClip fireClip;

    private string fireButton;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool isFired;


    private void OnEnable()
    {
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }

    private void Start()
    {
        fireButton = "Fire";
        chargeSpeed = (maxLanuchForce - minLaunchForce) / maxChargeTime;
        controller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

    }

    private void Update()
    {
        aimSlider.value = minLaunchForce;

        if(currentLaunchForce >= maxLanuchForce && !isFired)
        {
            currentLaunchForce = maxLanuchForce;
            Fire();
        }
        else if (Input.GetButtonDown(fireButton))
        {
            isFired = false;
            currentLaunchForce = minLaunchForce;
            shootingAuido.clip = chargingClip;
            shootingAuido.Play();
        }
        else if (Input.GetButton(fireButton) && !isFired )
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;
            aimSlider.value = currentLaunchForce;
        }
        else if (Input.GetButtonUp(fireButton) && !isFired)
        {
            Fire();
        }

    }

    private void Fire()
    {
        isFired = true;

        ShellModel shellModel = new ShellModel(currentLaunchForce);
        ShellController shellController = new ShellController(shellModel, shellPrefab, fireTransform.position, fireTransform.rotation);

        currentLaunchForce = minLaunchForce;

        shootingAuido.clip = fireClip;
        shootingAuido.Play();
        controller.CameraShake();
    }
}
