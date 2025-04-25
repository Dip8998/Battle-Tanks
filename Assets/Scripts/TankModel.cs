using UnityEngine;

public class TankModel 
{
    private TankController tankController;

    public float moveSpeed;
    public float rotateSpeed;
    public TankTypes tankType;
    public Material color;
    public int startingHealth = 100;
    public int currentHealth;
    public bool isDead;

    public TankModel(float _moveSpeed, float _rotateSpeed, TankTypes tank, Material _color, int health)
    {
        moveSpeed = _moveSpeed;
        rotateSpeed = _rotateSpeed;
        tankType = tank;
        color = _color;
        startingHealth = health;
    }
    public void ResetData()
    {
        currentHealth = startingHealth;
        isDead = false;
        tankController.SetHealthUI();
    }
    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
}
