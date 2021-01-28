using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancettaOre : Lancetta
{
    public override int GradiPerScattoLancetta
    {
        get { return 360/12; }
    }

    // *** PUBLIC METODS ***
    public override void SeguiAltraLancetta()
    {
        int oraTonda = this.GetValoreInteroCorrente();
        int minuti = orologio.lancettaMinuti.GetValoreInteroCorrente();
        orologio.SetOrario(oraTonda, minuti);
    }
}
