public class EnemyModel
{
    public float moveSpeed;
    public float fireRate;
    public int health;
    public EnemyController controller;

    public EnemyModel(float moveSpeed, float fireRate, int health)
    {
        this.moveSpeed = moveSpeed;
        this.fireRate = fireRate;
        this.health = health;
    }

    public void SetController(EnemyController enemyController)
    {
        controller = enemyController;
    }
}
