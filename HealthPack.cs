using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            ChangeView changeView = other.GetComponent<ChangeView>();
            if (changeView.thirdPersonCamera.active)
            {
                Destroy(this.gameObject);
                player.heal(10);
            }
        }
    }
}
