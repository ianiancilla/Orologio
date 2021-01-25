using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancetta : MonoBehaviour
{
    private bool VengoTrascinata { get; set; }
    private float _angoloCorrente;

    void Start()
    {
        SetAngoloLancetta(0);
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
        Debug.Log("angolo corrente " + _angoloCorrente);
    }

    private void SeguiMouse()
    {
        // determina vettore direzione mouse
        Vector3 posizioneMouseRaw = Input.mousePosition;
        Vector3 posizioneMouseScena = Camera.main.ScreenToWorldPoint(posizioneMouseRaw);

        Vector2 direzioneMouse = new Vector2(posizioneMouseScena.x,
                                             posizioneMouseScena.y).normalized;

        // allinea oggetto alla direzione del mouse
        float targetRotazioneZ;
        if (direzioneMouse.x > 0)
        {
            targetRotazioneZ = Mathf.Asin(direzioneMouse.y) * Mathf.Rad2Deg;
        }
        else
        {
            targetRotazioneZ = 180 - Mathf.Asin(direzioneMouse.y) * Mathf.Rad2Deg;
        }
        if (targetRotazioneZ <= 0)
        {
            targetRotazioneZ += 360;    // perché nel quadrante x/-y avevamo una rotazione da 0 a -90
        }

        // trova il valore dell'angolo in gradi
        float nuovoAngolo = (360 - targetRotazioneZ) + 90;
        if (nuovoAngolo > 360) { nuovoAngolo -= 360; }

        SetAngoloLancetta(nuovoAngolo);
    }

    public void SetAngoloLancetta(float nuovoAngolo)
    {
        // check che sia inferiore a 360°
        nuovoAngolo = nuovoAngolo % 360;
        if (nuovoAngolo == 0) { nuovoAngolo = 360; }

        _angoloCorrente = nuovoAngolo;

        // calcola l'angolo di rotazione da applicare
        float targetRotazioneZ;
        targetRotazioneZ = 360 - nuovoAngolo;
        targetRotazioneZ += 90;
        targetRotazioneZ = targetRotazioneZ % 360;

        // sposta la lancetta nella nuova posizione
        transform.rotation = Quaternion.Euler(0, 0, targetRotazioneZ);
    }
}