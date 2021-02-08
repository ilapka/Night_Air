using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    public static FloatingTextController Instance;

    private FloatingText popupText;
    private GameObject canvas;

    public void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;


        canvas = GameObject.Find("Canvas");
        popupText = Resources.Load<FloatingText>("UI/Combat/PopupTextParent");
    }

    public void CreateFloatingText(string text, Transform location, bool isKill)
    {
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector3(location.position.x + Random.Range(-0.5f, 0.5f), location.position.y + Random.Range(-0.5f, 0.5f), location.position.z));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
