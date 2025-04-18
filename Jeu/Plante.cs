public abstract class Plante 
{
    public string Nom { get; protected set; }
    public string Nature { get; protected set; }
    public List<string> Saisons { get; protected set; } 
    public string TerrainPrefere { get; protected set; } 
    public List<double> ZonesTempPreferee { get; protected set; }
    public double Espace { get; protected set; }
    public double VitesseCroissance { get; protected set; }
    public List<Maladie> MaladiesPossibles { get; protected set; }
    public int Productivite { get; protected set; }
    public double BesoinEau { get; protected set; }  // entre 0.0 et 1.0
    public double BesoinLumineux { get; protected set; } // entre 0.0 et 1.0
    public int Age { get; protected set; }
    public int Sante { get; protected set; }
    public bool EstMort { get; protected set; }
    public double Taille { get; protected set; }
    public Maladie? MaladieActuelle { get; protected set; }
    public int DureeMaladieRestante { get; protected set; }
    public Random rng { get; set; }



    public Plante(string nom, string nature, List<string> saisons, string terrainPrefere, List<double> zonesTempPreferee, double espace, double vitesse, List<Maladie> maladies, int productivite, double besoinEau, double besoinLumineux)
    {
        Nom = nom;
        Nature = nature;
        Saisons = saisons;
        TerrainPrefere = terrainPrefere;
        ZonesTempPreferee = zonesTempPreferee;
        Espace = espace;
        VitesseCroissance = vitesse;
        MaladiesPossibles = maladies;
        Productivite = productivite;
        BesoinEau = besoinEau;
        BesoinLumineux = besoinLumineux;
        Age = 0;
        Sante = 100;
        EstMort = false;
        Taille = 0;
        rng = new Random();
    }

    public bool ConditionsPalnte(Meteo meteo, Terrain terrain)
    {
        int conditionOk = 0;
        if (meteo.Temperature >= ZonesTempPreferee.Min() && meteo.Temperature <= ZonesTempPreferee.Max())
        {
            conditionOk++;
        }
        else Sante = Math.Max(Sante - 25, 0);

        if (terrain.Type == TerrainPrefere)
        {
            conditionOk++;
        }
        else Sante = Math.Max(Sante - 25, 0);

        if (meteo.Pluie >= BesoinEau)
        {
            conditionOk++;
        }
        else Sante = Math.Max(Sante - 25, 0);

        if (meteo.Soleil >= BesoinLumineux)
        {
            conditionOk++;
        }
        else Sante = Math.Max(Sante - 25, 0);

        conditionOk = conditionOk*25;
        if (conditionOk < 50)
        {
            EstMort = true;
            Sante = 0;
            return false;
        } 
        return true;
    }

    public void Arrosser(double quantite)
    {
        if (quantite < BesoinEau)
        {
            Sante = Math.Max(Sante - 10, 0);
            Console.WriteLine($"La plante {Nom} n'a pas assez d'eau. La santé a diminué.");
        }
        else 
        {
            Sante = Math.Min(Sante + 10, 100);
            Console.WriteLine($"La plante {Nom} a été correctement arrosée.");
        }
    }

    public void AppliquerLumiere(double quantite)
    {
        if (quantite < BesoinLumineux)
        {
            Sante = Math.Max(Sante - 10, 0);
            Console.WriteLine($"La plante {Nom} n'a pas assez de lumière. La santé a diminué.");
        }
        else
        {
            Sante = Math.Min(Sante + 10, 100);
            Console.WriteLine($"La plante {Nom} a eu correctement la lumière.");
        }
    }

    public void Pousser()
    {
        if (EstMort)
        {
            Console.WriteLine($"La plante {Nom} est déjà mort donc tu peux pas la pousser");
        }
        else if (MaladieActuelle != null)
        {
            Console.WriteLine($"La plante {Nom} est malade, donc sa santé ainsi que son taille diminues, faut la traiter");
            Taille -= 1;
            Sante = Math.Max(Sante - 10, 0);
        }
        else 
        {
            Taille += 1;
            Console.WriteLine($"La plante {Nom} est poussée ");
        }
    }

    public bool PeutRecolter()
    {
        if (Sante == 100)
        {
            return true;
        }
        return false;
    }

    
     
    public void AppliquerMaladie() 
    { 
        if (MaladieActuelle != null)
        {
            return;
        }
        foreach (var maladie in MaladiesPossibles)
        {
            if (rng.NextDouble() < maladie.ProbabiliteApparition) 
            { 
                MaladieActuelle = maladie; 
                DureeMaladieRestante = maladie.Duree;
                Console.WriteLine($"La plante {Nom} est tombée malade : {maladie.Nom}");
                break; // sorti , car la plante peux attraper une seule maladie
            } 
        }
        
    }

    public void AppliquerTraitement() // Soigner la plante si elle est malade
    {
        if (MaladieActuelle != null)
        {
            MaladieActuelle = null;
            Sante = Math.Min(Sante + 10, 100);
        }
        else 
        {
            Console.WriteLine($"La plante {Nom} est déjà en bonne santé");
        }
    } 

    public void AfficherEtat()
    {
        if (EstMort)
        {
            Console.WriteLine($"La plante {Nom} est déjà mort");
        }
        else if (MaladieActuelle == null)
        {
            Console.WriteLine($"{Nom} | Santé : {Sante}% | Taille : {Taille} | Croissance : {VitesseCroissance}");
        }
        else 
        {
            Console.WriteLine($"{Nom} | Santé : {Sante}% | Taille : {Taille} | Croissance : {VitesseCroissance} | Maladie : {MaladieActuelle.Nom}");
        }
        
    }
    

  

}