using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TankSelection : MonoBehaviour
    {
        [SerializeField] private TankSpawner tankSpawner;

        public void GreenTankSelection()
        {
            tankSpawner.CreateTank(TankTypes.GreenTank);
            this.gameObject.SetActive(false);
        }

        public void BlueTankSelection()
        {
            tankSpawner.CreateTank(TankTypes.BlueTank);
            this.gameObject.SetActive(false);
        }

        public void RedTankSelection()
        {
            tankSpawner.CreateTank(TankTypes.RedTank);
            this.gameObject.SetActive(false);
        }
    }
}