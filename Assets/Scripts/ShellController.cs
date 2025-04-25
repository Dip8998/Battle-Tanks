using UnityEngine;

public class ShellController
{
    private ShellModel shellModel;
    private ShellView shellView;

    public ShellController(ShellModel model, ShellView viewPrefab, Vector3 spawnPos, Quaternion spawnRot)
    {
        shellModel = model;

        shellView = GameObject.Instantiate(viewPrefab, spawnPos, spawnRot);
        shellModel.SetShellController(this);
        shellView.SetShellController(this);

        Rigidbody rb = shellView.GetComponent<Rigidbody>();
        rb.velocity = shellView.transform.forward * shellModel.damage; 
    }
}
