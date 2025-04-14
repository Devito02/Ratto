using NUnit.Framework.Constraints;
using UnityEngine;

public class Aim_Target : MonoBehaviour
{
    private Vector3 _basePlayerPos;
    public float MaxDistance;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        Vector2 ScreenCenter = new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2);
        _basePlayerPos = Player.transform.position;
        Vector2 _mousePos = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, Screen.currentResolution.width),
            Mathf.Clamp(Input.mousePosition.y, 0, Screen.currentResolution.height));
        transform.position = _basePlayerPos + new Vector3((_mousePos - ScreenCenter).x, _basePlayerPos.y, (_mousePos - ScreenCenter).y) * MaxDistance;
    }



}
