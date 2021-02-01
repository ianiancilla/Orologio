using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orologio : MonoBehaviour
{
    // costants
    const string NOME_LANCETTA_ORE = "LancettaOre";
    const string NOME_LANCETTA_MINUTI = "LancettaMinuti";
    const string NOME_CENTRO_OROLOGIO = "CentroOrologio";

    [Header("Veste grafica")]
    [SerializeField] Sprite spriteQuadrante;
    [SerializeField] Sprite spriteLancettaOre;
    [SerializeField] Sprite spriteLancettaMinuti;
    [SerializeField] Sprite spriteCentroOrologio;
    [SerializeField] Color coloreLancettaOre = Color.red;
    [SerializeField] Color coloreLancettaMinuti = Color.green;

    [Header("Funzionamento")]
    [SerializeField] bool trascinamentoLancettaOre = false;
    [SerializeField] bool trascinamentoLancettaMinuti = false;

    public enum PrecisioneMinuti { uno=1, cinque=5, quindici=15, trenta=30}
    [SerializeField] public PrecisioneMinuti precisioneMinuti = PrecisioneMinuti.uno;

    // member variables
    [HideInInspector]
    public Lancetta lancettaOre;
    [HideInInspector]
    public Lancetta lancettaMinuti;

    // Start is called before the first frame update
    void Start()
    {
        // cache
        lancettaOre = transform.Find(NOME_LANCETTA_ORE).GetComponent<LancettaOre>();
        lancettaMinuti = transform.Find(NOME_LANCETTA_MINUTI).GetComponent<LancettaMinuti>();

        SetupAspettoOrologio();

        SetTrascinamentoOre(trascinamentoLancettaOre);
        SetTrascinamentoMinuti(trascinamentoLancettaMinuti);
    }

    private void SetupAspettoOrologio()
    {
        if (spriteLancettaOre)
        {
            SpriteRenderer renderer = transform.Find(NOME_LANCETTA_ORE).Find("Lancetta").GetComponent<SpriteRenderer>();
            renderer.sprite = spriteLancettaOre;
            renderer.color = coloreLancettaOre;
        }

        if (spriteLancettaOre)
        {
            SpriteRenderer renderer = transform.Find(NOME_LANCETTA_MINUTI).Find("Lancetta").GetComponent<SpriteRenderer>();
            renderer.sprite = spriteLancettaMinuti;
            renderer.color = coloreLancettaMinuti;
        }

        if (spriteCentroOrologio)
        {
            SpriteRenderer renderer = transform.Find(NOME_CENTRO_OROLOGIO).GetComponent<SpriteRenderer>();
            renderer.sprite = spriteCentroOrologio;
        }
    }

    // *** PUBLIC METHODS ***
    /// <summary>
    /// Dandogli numero intero di ore e minuti, setta l'orologio sulla giusta posizione.
    /// </summary>
    /// <param name="ore">ore, espresse come int maggiore di 0</param>
    /// <param name="minuti">minuti, espressi come int maggiore di 0</param>
    public void SetOrario(int ore, int minuti)
    {
        float angoloLancettaOre = (ore * lancettaOre.GradiPerScattoLancetta) 
                                  + (minuti / 2);
        lancettaOre.SetAngoloLancetta(angoloLancettaOre);

        float angoloLancettaMinuti = minuti * lancettaMinuti.GradiPerScattoLancetta;
        lancettaMinuti.SetAngoloLancetta(angoloLancettaMinuti);
    }

    /// <summary>
    /// Ritorna l'orario corrente dell'orologio. Le ore sono in formato 12-11, non 0-24.
    /// </summary>
    /// <returns>Un array di due int: il primo sono le ore, il secondo i minuti</returns>
    public int[] GetOrario()
    {
        int[] orario = new int[2];
        orario[0] = lancettaOre.GetValoreInteroCorrente();
        orario[1] = lancettaMinuti.GetValoreInteroCorrente();
        return orario;
    }

    /// <summary>
    /// Setta la modalità della lancetta delle ore (trascinabile o fissa)
    /// </summary>
    /// <param name="trascinabili">true se la lancetta deve essere trascinabile</param>
    public void SetTrascinamentoOre(bool trascinabili)
    {
        trascinamentoLancettaOre = trascinabili;
        lancettaOre.trascinabile = trascinabili;
    }

    /// <summary>
    /// Setta la modalità della lancetta dei minuti (trascinabile o fissa)
    /// </summary>
    /// <param name="trascinabili">true se la lancetta deve essere trascinabile</param>
    public void SetTrascinamentoMinuti(bool trascinabili)
    {
        trascinamentoLancettaMinuti = trascinabili;
        lancettaMinuti.trascinabile = trascinabili;
    }
}
