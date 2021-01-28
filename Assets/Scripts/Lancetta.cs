using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lancetta : MonoBehaviour
{
    protected float _angoloCorrente;
    protected bool stoVenendoTrascinata = false;
    public abstract int GradiPerScattoLancetta { get; }

    // cache
    public Orologio orologio;

    // *** CALLBACK METODS ***
    protected void Start()
    {
        // cache
        orologio = transform.parent.GetComponent<Orologio>();

        SetAngoloLancetta(0);
    }
    protected void Update()
    {
        ControllaSeTrascinata();
        if (!stoVenendoTrascinata)
        {
            SeguiAltraLancetta();
        }
    }
    protected void OnMouseDrag()
    {
        stoVenendoTrascinata = true;
        SeguiMouse();
    }

    // *** PRIVATE METODS ***
    protected void ControllaSeTrascinata()
    {
        if (stoVenendoTrascinata)
        {
            if (Input.GetMouseButtonUp(0))    // TODO funziona solo con il mouse, trovare alternativa
            {
                stoVenendoTrascinata = false;
            }
        }
    }
    protected void SeguiMouse()
    {
        // determina vettore direzione mouse
        Vector3 posizioneMouseRaw = Input.mousePosition;    // TODO funziona solo con il mouse, trovare alternativa
        Vector3 posizioneMouseScena = Camera.main.ScreenToWorldPoint(posizioneMouseRaw);

        Vector2 direzioneMouse = new Vector2(posizioneMouseScena.x,
                                             posizioneMouseScena.y).normalized;

        // Trova angolo di rotazione sull#asse Z per l'oggetto.
        // Lo fa usando il il fatto che il valore Y è uguale al seno dell'angolo conmpreso tra asse X e vettore posizione.
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

    // *** PUBLIC METODS ***
    /// <summary>
    /// Dato un angolo in gradi superiore a 0, ruota la lancetta in senso orario di quell'angolo.
    /// A un angolo di 0 (o 360, 720...) la lancetta sarà vertocale sulle ore 12 dell'orologio.
    /// </summary>
    /// <param name="nuovoAngolo">float, superiore a 0</param>
    public virtual void SetAngoloLancetta(float nuovoAngolo)
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

    /// <summary>
    /// Ritorna l'angolo in gradi a cui è correntemente posizionata la lancetta.
    /// Se è posizionata in verticale, ritornerà 360. L'angolazione la fa avanazare in senso orario.
    /// </summary>
    /// <returns>float compresa tra 0(escluso) e 360</returns>
    public virtual float GetAngoloLancetta() { return _angoloCorrente; }

    /// <summary>
    /// Ritorna il valore corrente intero della lancetta.
    /// Se una lancetta dei minuti viene posizioneta a 91°, ritornerà "15".
    /// Se una lancetta delle ore viene posizionata a 91°, ritoernerà "3".
    /// </summary>
    /// <returns>Il valore intero di ore/minuti a cui è correntemente posizionata la lancetta.</returns>
    public int GetValoreInteroCorrente()
    {
        int valoreCorrente;
        valoreCorrente = (int)GetAngoloLancetta() / GradiPerScattoLancetta;
        return valoreCorrente;
    }

    public abstract void SeguiAltraLancetta();

}