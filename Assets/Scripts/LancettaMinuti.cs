using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancettaMinuti : Lancetta
{
    public override int NumeroScattiSuOrologio { get { return 60; } }
    public override int GradiPerScattoLancetta { get { return 360 / NumeroScattiSuOrologio; } }


    // *** PRIVATE METHODS ***
    protected override void SeguiMouse()
    {
        base.SeguiMouse();
        ArrotondaAScattoPiuVicino();

        if (! orologio.orologioContinuo) { return; }

        bool haAumentatoOra = (_angoloPrecedente > 335 && _angoloCorrente < 25);
        if (haAumentatoOra)
        {
            int oreCorrenti = orologio.lancettaOre.GetValoreInteroCorrente();
            orologio.lancettaOre.SetValoreIntero(oreCorrenti + 1);
        }

        bool haCalatoOra = (_angoloPrecedente < 25 && _angoloCorrente > 335);
        if (haCalatoOra)
        {
            int oreCorrenti = orologio.lancettaOre.GetValoreInteroCorrente();
            orologio.lancettaOre.SetValoreIntero(oreCorrenti - 1);
        }
    }
    protected override void SistemaLancetteAlRilascio()
    {
        int ampiezzaScatti = (int)orologio.precisioneMinuti;
        int valoreCorrente = GetValoreInteroCorrente();
        int scarto = valoreCorrente % ampiezzaScatti;
        
        int valoreArrotondato;
        if (scarto <= ampiezzaScatti / 2)
        {
            valoreArrotondato = valoreCorrente - scarto;
        }
        else
        {
            valoreArrotondato = valoreCorrente
                                + (ampiezzaScatti - scarto);
        }

        orologio.SetOrario(orologio.lancettaOre.GetValoreInteroCorrente(),
                           valoreArrotondato);
    }

}