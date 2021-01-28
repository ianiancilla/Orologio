using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancettaMinuti : Lancetta
{
    public override int GradiPerScattoLancetta
    {
        get { return 360/60; }
    }

    // *** PUBLIC METODS ***
    public override void SeguiAltraLancetta()
    {
        //int oraTonda = orologio.lancettaOre.GetValoreInteroCorrente();
        //int minuti = this.GetValoreInteroCorrente();
        //orologio.SetOrario(oraTonda, minuti);
    }
}