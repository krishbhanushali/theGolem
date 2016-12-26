using UnityEngine;
using System.Collections;

public class CollectibleItem : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private string itemName;
    void OnTriggerEnter(Collider other)
    {
        
        //Other functions to do upon collection
        if (other.CompareTag("Player"))
        {
            ChangeView currentCamera = other.GetComponent<ChangeView>();
            if (currentCamera.thirdPersonCamera.activeInHierarchy)
            {
                if (itemName.Equals("ore"))
                {
                    Managers.Audio.PlaySound(Resources.Load("Music/ore") as AudioClip);
                }
                if (itemName.Equals("health"))
                {
                    Managers.Audio.PlaySound(Resources.Load("Music/health") as AudioClip);
                }
                if (itemName.Equals("key"))
                {
                    Managers.Audio.PlaySound(Resources.Load("Music/key") as AudioClip);
                }
                Debug.Log("Item collected in Collectible Item: " + itemName);
                Managers.Inventory.AddItem(itemName);
                Destroy(this.gameObject);
            }
        }
    }
}
