using UnityEngine;

public class ShellModel
{
    public float damage;
    public ShellController shellController;

    public ShellModel(float damage)
    {
        this.damage = damage;
    }

    public void SetShellController(ShellController controller)
    {
        this.shellController = controller;
    }
}
