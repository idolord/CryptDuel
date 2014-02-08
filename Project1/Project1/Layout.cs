using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Layout
    {
        int attaque;
        int deplacement;
        int vision;
        int x = 13;
        int y = 13;

        public Layout(int attaque, int deplacement, int vision)
        {
            this.attaque = attaque;
            this.deplacement = deplacement;
            this.vision = vision;
        }

        public void genererLayout()
        {
            int calculAttaque1 = (x / 2) - attaque;
            int calculAttaque2 = (x / 2) + attaque;
            int calculDeplacement1 = (x / 2) - deplacement;
            int calculDeplacement2 = (x / 2) + deplacement;
            int calculVision1 = (x / 2) - vision;
            int calculVision2 = (x / 2) + vision;
            
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    bool conditionJoueur = (j == ((x / 2))) && (i == ((y / 2)));
                    bool conditionAttaque = ((j >= calculAttaque1 && j <= calculAttaque2) && (i >= calculAttaque1 && i <= calculAttaque2));//((j >= calculAttaque1 && j <= calculAttaque2) || (i >= calculAttaque1 && i <= calculAttaque2))
                    bool conditionDeplacement = ((j >= calculDeplacement1 && j <= calculDeplacement2) && (i >= calculDeplacement1 && i <= calculDeplacement2));
                    bool conditionVision = ((j >= calculVision1 && j <= calculVision2) && (i >= calculVision1 && i <= calculVision2));

                    if (conditionJoueur)
                        Console.Write(" J ");
                    else if (conditionAttaque)
                        Console.Write(" A ");
                    else if (conditionDeplacement)
                        Console.Write(" D ");
                    else if (conditionVision)
                        Console.Write(" V ");
                    else
                        Console.Write(" L ");
                }
                Console.WriteLine();
            }
        }
    }
}
