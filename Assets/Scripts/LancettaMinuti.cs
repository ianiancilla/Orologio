using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancettaMinuti : Lancetta
{
    public override int NumeroScattiSuOrologio { get { return 60; } }
    public override int GradiPerScattoLancetta { get { return 360/ NumeroScattiSuOrologio; } }

    // *** PRIVATE METHODS ***
    protected override void SeguiMouse()
    {
        base.SeguiMouse();
        ArrotondaAScattoPiuVicino();
    }
}