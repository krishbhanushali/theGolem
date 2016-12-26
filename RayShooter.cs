using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    private Texture2D crossHairImage;
    bool showingCursor;
    private GunSwap gs;
    AudioClip gunAudio;
    AudioSource audioSource;
    Animation _animation;
    void Start()
    {
        _camera = GetComponent<Camera>();
        
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), crossHairImage);
        gs = GetComponent<GunSwap>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GunSwap gs = player.GetComponentInChildren<GunSwap>();
               
            if (gs.getCurrentWeaponIndex() == 0)
            {
                Managers.Audio.PlaySound(Resources.Load("Music/pistol_shot") as AudioClip);
            }
            if(gs.getCurrentWeaponIndex() == 1)
            {
                Managers.Audio.PlaySound(Resources.Load("Music/pistol2") as AudioClip);
            }
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
                Ray ray = _camera.ScreenPointToRay(point);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                    if (target != null)
                    {
                        Debug.Log(target.tag);
                        if (target.tag == "Enemy")
                        {
                            target.ReactToHit();
                        }
                        if (target.tag == "Boss")
                        {
                            target.BossReactToHit();
                        }
                    }
                    
                    else
                    {
                        StartCoroutine(SphereIndicator(hit.point));
                    }
                }
            }
        }
        
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GunSwap gs = player.GetComponentInChildren<GunSwap>();
        if(gs.getCurrentWeaponIndex()==0)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = pos;
            yield return new WaitForSeconds(1);
            Destroy(sphere);
        }
        else if(gs.getCurrentWeaponIndex()==1)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = pos;
            yield return new WaitForSeconds(1);
            Destroy(cube);
        }
        
        
    }
}