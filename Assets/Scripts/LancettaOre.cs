using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancettaOre : Lancetta
{
    protected override int ValoreAngoloGiro { get { return 360; } }
    public override int NumeroScattiSuOrologio { get { return 12; } }
    public override int GradiPerScattoLancetta { get { return 360/ NumeroScattiSuOrologio; } }
}
