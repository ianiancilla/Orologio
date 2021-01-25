using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancetta : MonoBehaviour
{
    bool VengoTrascinata { get; set; }

    void Start()
    {
        VengoTrascinata = false;
    }

    void Update()
    {
        // TODO check, se ci clicchi sopra lo attivi
        VengoTrascinata = Input.GetMouseButton(0);

        if (VengoTrascinata)
        {
            SeguiMouse();
        }

    }

    private void SeguiMouse()
    {
        // determina vettore direzione mouse
        Vector3 posizioneMouseRaw = Input.mousePosition;
        Vector3 posizioneMouseScena = Camera.main.ScreenToWorldPoint(posizioneMouseRaw);

        Vector2 direzioneMouse = new Vector2(posizioneMouseScena.x,
                                             posizioneMouseScena.y).normalized;

        // allinea oggetto alla direzione del mouse
        float targetRotazioneZ = 1;
        if (direzioneMouse.x > 0)
        {
            targetRotazioneZ = Mathf.Asin(direzioneMouse.y) * Mathf.Rad2Deg;
        }
        else
        {
            targetRotazioneZ = 180 - Mathf.Asin(direzioneMouse.y) * Mathf.Rad2Deg;
        }

        transform.localRotation = Quaternion.Euler(0, 0, targetRotazioneZ);
    }
}