using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Teleport : MonoBehaviour {

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            if (this.gameObject.name == "teleporter1")
            {
                string error = "";
                bool flag = false;  
                if (Managers.Inventory.GetItemCount("ore")>=3 && Managers.Inventory.GetItemCount("key") >= 2 && Managers.Inventory.GetItemCount("health") > 5 && Managers.Inventory.GetEquippedKey()=="key")
                {
                    Messenger<int>.Broadcast(GameEvent.OPEN_MCQ_POPUP, 1);
                }
                if (Managers.Inventory.GetItemCount("ore") < 3)
                {
                    error += "ore ";
                    flag = true;
                }
                if (Managers.Inventory.GetItemCount("key") < 2)
                {
                    error += "key ";
                    flag = true;                
                }
                if (Managers.Inventory.GetItemCount("health") <= 5)
                {
                    error += "health ";
                    flag = true;
                }
                if (Managers.Inventory.GetEquippedKey()!="key" || Managers.Inventory.GetEquippedKey()=="")
                {
                    error += "notEquippedKey";
                    flag = true;
                }
                if(flag)
                    Messenger<string>.Broadcast(GameEvent.ERROR_INVENTORY, error);
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        Debug.Log("exitted");
        Messenger.Broadcast(GameEvent.CLOSE_MCQ_POPUP);
        Messenger.Broadcast(GameEvent.ERROR_INVENTORY_CLOSE_POPUP);
    }
}
