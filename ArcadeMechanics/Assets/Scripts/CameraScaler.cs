using UnityEngine;

public class CameraScaler : MonoBehaviour
{

    private int current_w = 0;
    private int current_h = 0;

    public float w_amount = 33.5f; //Minimum units horizontally
    public float h_amount = 20f; //Minimum units vertically

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        SetRes();
    }

    private void SetRes()
    {
        current_w = Screen.width;
        current_h = Screen.height;
        float width_size = (float)(w_amount * Screen.height / Screen.width * 0.5);
        float height_size = (float)(h_amount * Screen.width / Screen.height * 0.5) * ((float)Screen.height / Screen.width);
        cam.orthographicSize = Mathf.Max(height_size, width_size);
    }

    void Update()
    {
        if (Screen.width != current_w || Screen.height != current_h)
        {
            SetRes();
        }
    }
}