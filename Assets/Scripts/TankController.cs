using UnityEngine;

public class TankController
{
    private TankModel tankModel;
    private TankView tankView;

    private Rigidbody rb;
    private CameraController virtualCam;

    public TankController(TankModel _tankModel, TankView _tankView, CameraController virtualCam)
    {
        tankModel = _tankModel;
        tankView = GameObject.Instantiate<TankView>(_tankView);
        rb = tankView.GetRigidbody();
        tankModel.SetTankController(this);
        tankView.SetTankController(this);
        tankView.ChangeColor(tankModel.color);

        tankModel.ResetData();
        this.virtualCam = virtualCam;
        virtualCam.SetPlayer(tankView.transform);

    }


    public void Move(float movement)
    {
        rb.velocity = tankView.transform.forward * movement * tankModel.moveSpeed;
    }
    public void Rotate(float rotate)
    {
        Vector3 vector = new Vector3(0f, rotate * tankModel.rotateSpeed, 0f);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public TankModel GetTankModel()
    {
        return tankModel;
    }

    public TankView GetTankView()
    {
        return tankView;
    }

    public void SetHealthUI()
    {
        tankView.SetHealthUI();
    }

    public void TakeDamage(int damage)
    {
        tankModel.currentHealth -= damage;
        SetHealthUI();
        if(tankModel.currentHealth <= 0 && !tankModel.isDead)
        {
            tankView.OnDeath();
        }
    }
}
