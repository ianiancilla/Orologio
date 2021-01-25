using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancetta : MonoBehaviour
{
    public enum TipiDiLancetta { Ore = 0, Minuti = 1 }
    [SerializeField] TipiDiLancetta tipo = TipiDiLancetta.Ore;

    private float _angoloCorrente;
    private bool stoVenendoTrascinata = false;
    
    // cache
    private Orologio orologio;

    void Start()
    {
        // cache
        orologio = transform.parent.GetComponent<Orologio>();

        SetAngoloLancetta(0);
    }
    private void Update()
    {
        ControllaSeTrascinata();
        if (!stoVenendoTrascinata)
        {
            SeguiAltraLancetta();
        }
    }
    private void OnMouseDrag()
    {
        stoVenendoTrascinata = true;
        SeguiMouse();
    }
    private void ControllaSeTrascinata()
    {
        if (stoVenendoTrascinata)
        {
            if (Input.GetMouseButtonUp(0))
            {
                stoVenendoTrascinata = false;
            }
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
    private void SeguiAltraLancetta()
    {
        switch (tipo)
        {
            case TipiDiLancetta.Ore:
                OreSeguonoMinuti();
                break;
            case TipiDiLancetta.Minuti:
                MinutiSeguonoOre();
                break;
        }
    }
    private void OreSeguonoMinuti()
    {
        int oraTonda = this.GetValoreInteroCorrente();
        int minuti = orologio.lancettaMinuti.GetValoreInteroCorrente();
        orologio.SetOrario(oraTonda, minuti);
    }
    private void MinutiSeguonoOre()
    {
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
        targetRotazioneZ %= 360;

        // sposta la lancetta nella nuova posizione
        transform.rotation = Quaternion.Euler(0, 0, targetRotazioneZ);
    }
    
    public float GetAngoloLancetta() { return _angoloCorrente; }
    public int GetValoreInteroCorrente()
    {
        int valoreCorrente;
        switch (tipo)
        {
            case TipiDiLancetta.Ore:
                valoreCorrente = (int)GetAngoloLancetta() / 30;
                break;
            case TipiDiLancetta.Minuti:
                valoreCorrente = (int)GetAngoloLancetta() / 6;
                break;
            default:
                valoreCorrente = 0;
                Debug.Log("Non hai assegnato un tipo corretto a una delle lancette dell'orologio.");
                break;
        }
        return valoreCorrente;
    }
}