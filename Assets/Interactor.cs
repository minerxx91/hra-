using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayerMask = 8;
    UnityEvent onInteract;
    Hotbar hotbar;
    public GameObject[] items = { };
    //public Dictionary<int, GameObject> items = new Dictionary<int ,GameObject>(3);
    [SerializeField] GameObject hotbarUI;
    [SerializeField] GameObject player;
    [SerializeField] GameObject models3d;
    GameObject lastCollider;
    bool empty;
    void Start()
    {
        hotbar = hotbarUI.GetComponent<Hotbar>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f, interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>())
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;

                if (hit.collider.gameObject.transform.GetChild(0).gameObject.name == "Canvas")
                {
                    hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    lastCollider = hit.collider.gameObject;
                    hit.collider.gameObject.transform.GetChild(0).gameObject.transform.LookAt(player.transform.position);
                    hit.collider.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(0f, hit.collider.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles.y, 0f);
                }
                try
                {
                    items[hotbar.activeSlot].transform.GetChild(0).gameObject.SetActive(false);
                }
                catch { }

                if (Input.GetMouseButtonDown(0))
                {

                    if (hit.collider.gameObject.name == "Pick-Up old" && items[hotbar.activeSlot].tag == "Jerry Can")
                    {
                        hit.collider.gameObject.SetActive(false);
                        onInteract.Invoke();
                        items[hotbar.activeSlot].gameObject.SetActive(false);
                    }
                    if (hit.collider.gameObject.tag == "Jerry Can")
                    {
                        items[hotbar.activeSlot].gameObject.transform.SetParent(models3d.transform, true);
                        items[hotbar.activeSlot].gameObject.transform.position = hit.collider.gameObject.transform.position;
                        items[hotbar.activeSlot].gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        onInteract.Invoke();
                        hit.collider.gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
                        hit.collider.gameObject.transform.localPosition = new Vector3(0f, 1.22f, 0.29f);
                    }
                    else if (hit.collider.gameObject.tag == "Spray")
                    {
                        items[hotbar.activeSlot].gameObject.transform.SetParent(models3d.transform, true);
                        items[hotbar.activeSlot].gameObject.transform.position = hit.collider.gameObject.transform.position;
                        items[hotbar.activeSlot].gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        onInteract.Invoke();
                        hit.collider.gameObject.transform.localPosition = new Vector3(0, 1.12f, 0.039f);
                        hit.collider.gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
                    }
                    else if (hit.collider.gameObject.tag == "Bat")
                    {
                        items[hotbar.activeSlot].gameObject.transform.SetParent(models3d.transform, true);
                        items[hotbar.activeSlot].gameObject.transform.position = hit.collider.gameObject.transform.position;
                        items[hotbar.activeSlot].gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        onInteract.Invoke();
                        hit.transform.parent.localPosition = new Vector3(0.318f, 0.256f, -0.019f);
                        hit.transform.parent.localRotation = Quaternion.Euler(0f, -130f, 0f);
                    }
                    if (hotbar.activeSlot == 0 && hit.collider.gameObject.tag != "Car")
                    {
                        items[0] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 1 && hit.collider.gameObject.tag != "Car")
                    {
                        items[1] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 2 && hit.collider.gameObject.tag != "Car")
                    {
                        items[2] = hit.collider.gameObject;
                    }
                }
            }
        }
        else
        {
            try
            {
                lastCollider.transform.GetChild(0).gameObject.SetActive(false);
            }
            catch{ }
        }
        if (hotbar.activeSlot == 0)
        {
            items[0].gameObject.SetActive(true);
            items[1].gameObject.SetActive(false);
            items[2].gameObject.SetActive(false);
        }
        else if (hotbar.activeSlot == 1)
        {
            items[0].gameObject.SetActive(false);
            items[1].gameObject.SetActive(true);
            items[2].gameObject.SetActive(false);
        }
        else if (hotbar.activeSlot == 2)
        {
            items[0].gameObject.SetActive(false);
            items[1].gameObject.SetActive(false);
            items[2].gameObject.SetActive(true);
        }
    }
}
