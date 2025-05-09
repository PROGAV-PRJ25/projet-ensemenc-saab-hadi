﻿
Terrain terrain = new Terre(5);

Maladie mildiou = new Maladie("Mildiou", 7, 3, 0.4);
Maladie rouille = new Maladie("Rouille", 5, 2, 0.5);
Maladie cochenille = new Maladie("Cochenille", 5, 4, 0.6);
Maladie fongique = new Maladie("fongique", 9, 4, 3);
Maladie botrytis = new Maladie("Botrytis", 2, 3, 4);

List<Plante> catalogue = new List<Plante>
{
    new Comestible("🍅 Tomate", "Légume", new List<string>{"Été"}, "Terre", (15, 30), 1, 1.2, new List<Maladie>{mildiou}, 3, 0.6, 0.8, 5),
    new Comestible("🥕 Carotte", "Légume", new List<string>{"Printemps"}, "Terre", (10, 25), 0.8, 1.0, new List<Maladie>{rouille}, 2, 0.5, 0.7, 4),
    new Comestible("🌵 Cactus", "Cactaceae", new List<string> {"Été"}, "Sable", (10, 40), 0.3, 0.2,  new List<Maladie> {cochenille}, 2, 0.2, 0.9, 1.5),
    new Comestible("🍉 Pastèque","Fruit",new List<string> { "Été", "Fin du printemps" },"Sable",(20, 35),1.5,0.3,new List<Maladie>{fongique},4,5.0,8.0,3.0),
    new Comestible("🍓 Fraise","Fruit",new List<string> { "Printemps", "Été" }, "Sol riche, frais et bien drainé",(15, 25), 0.3,0.4, new List<Maladie>{botrytis},20,2.0,6.0,0.25),
};

Simulateur simulateur = new Simulateur(terrain, catalogue);
terrain.AfficherJardin();
simulateur.AfficherCatalogueEtSemer();

int nbActions = 3;
Menu menu = new Menu();
int tour = 0;

bool enCours = true;
while (enCours)
{
    int choix;
    Meteo m = Meteo.Generer();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Météo d'aujoud'hui");
    Console.ResetColor();
    Console.WriteLine(m);
    terrain.AfficherJardin();
    Console.WriteLine("Appuyez sur une touche pour continuer...");
    Console.ReadKey();
    if (m.Condition == "Grele" || m.Condition == "Tempete" || m.Temperature < 5 || m.Temperature > 30 || m.Pluie > 0.8)
    {
        int urgen = 0;
        foreach (var plante in terrain.Plantes)
        {
            if (!plante.ProtectionPhysique || !plante.EstProtegee)
            {
                urgen++;
            }
        }
        if (urgen > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("⚠️  Urgence ! La météo impose un traitement d'urgence !");
            Console.ResetColor();
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            choix = menu.AfficherMenuUrgence();
            if (choix == 1)
            {
                simulateur.ProtegerContrePluie();
                
            }
            else if (choix == 2)
            {
                simulateur.ProtegerContreGel();
            }
            else if (choix == 3)
            {
                simulateur.ProtegerContreCanicule();
            }
            else if (choix == 4)
            {
                simulateur.ProtegerContreVent();
            }
        }
        else 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ La météo est mauvaise mais ne vous inquiétez pas, toutes les plantes sont protégées.");
            Console.ResetColor();
        }
        
    }
    else 
    {
        choix = menu.AfficherMenu();
        if (choix == 1)
        {
            if (nbActions > 0)
            {
                simulateur.AfficherCatalogueEtSemer();
                nbActions--;
                
            }
            else 
            {
                Console.WriteLine($"Le nombre des actions possibles par tour est atteint ! un tour va etre passer");
                tour++;
                simulateur.PasserTour(tour);
                nbActions = 3;
            }
        }
        else if (choix == 2)
        {
            if (nbActions > 0)
            {
                simulateur.ArroserUnePlante();
                nbActions--;
                
            }
            else 
            {
                Console.WriteLine($"Le nombre des actions possibles par tour est atteint ! un tour va etre passer");
                tour++;
                simulateur.PasserTour(tour);
                nbActions = 3;
            }
        }
        else if (choix == 3)
        {
            if (nbActions > 0)
            {
                simulateur.TraiterUnePlante();
                nbActions--;
                
            }
            else 
            {
                Console.WriteLine($"Le nombre des actions possibles par tour est atteint ! un tour va etre passer");
                tour++;
                simulateur.PasserTour(tour);
                nbActions = 3;
            }
        }
        else if (choix == 4)
        {
            if (nbActions > 0)
            {
                simulateur.AfficherEtatPlantes();
                nbActions--;
                
            }
            else 
            {
                Console.WriteLine($"Le nombre des actions possibles par tour est atteint ! un tour va etre passer");
                tour++;
                simulateur.PasserTour(tour);
                nbActions = 3;
            }
        }
        else if (choix == 5)
        {
            tour++;
            simulateur.PasserTour(tour);
            nbActions = 3;
        }
        else if (choix == 6)
        {
            Console.WriteLine("Au revoir !");
            return;
        }
        
    }
   
           
    

    
}
