using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.Repositories
{
    public class DataRepository : IDataRepository
    {
        public IList<string> FirstNames(string nationality)
        {
            var listFirstNames = new List<string>();

            if (nationality == "Belgian")
            {
                listFirstNames.Add("Arthur");
                listFirstNames.Add("Noah");
                listFirstNames.Add("Jules");
                listFirstNames.Add("Louis");
                listFirstNames.Add("Lucas");
                listFirstNames.Add("Liam");
                listFirstNames.Add("Adam");
                listFirstNames.Add("Victor");
                listFirstNames.Add("Gabriel");
                listFirstNames.Add("Mohamed");
            }
            else if (nationality == "Netherlands")
            {
                listFirstNames.Add("Noah");
                listFirstNames.Add("Sem");
                listFirstNames.Add("Liam");
                listFirstNames.Add("Lucas");
                listFirstNames.Add("Daan");
                listFirstNames.Add("Finn");
                listFirstNames.Add("Levi");
                listFirstNames.Add("Luuk");
                listFirstNames.Add("Mees");
                listFirstNames.Add("James");
            }
            else if (nationality == "French")
            {
                listFirstNames.Add("Liam");
                listFirstNames.Add("Lucas");
                listFirstNames.Add("Raphael");
                listFirstNames.Add("Léo");
                listFirstNames.Add("Noah");
                listFirstNames.Add("Ethan");
                listFirstNames.Add("Louis");
                listFirstNames.Add("Gabriel");
                listFirstNames.Add("Jules");
                listFirstNames.Add("Nathan");
            }
            else if (nationality == "German")
            {
                listFirstNames.Add("Thorsten");
                listFirstNames.Add("Tore");
                listFirstNames.Add("Friedo");
                listFirstNames.Add("Kaleo");
                listFirstNames.Add("Mauritz");
                listFirstNames.Add("Hektor");
                listFirstNames.Add("Norman");
                listFirstNames.Add("Ruven");
                listFirstNames.Add("Elias");
                listFirstNames.Add("Matteo");
            }


            return listFirstNames;
        }

        public IList<string> LastNames(string nationality)
        {
            var listLastNames = new List<string>();

            if (nationality == "Belgian")
            {
                listLastNames.Add("Peeters");
                listLastNames.Add("Janssens");
                listLastNames.Add("Maes");
                listLastNames.Add("Jacobs");
                listLastNames.Add("Mertens");
                listLastNames.Add("Willems");
                listLastNames.Add("Claes");
                listLastNames.Add("Goossens");
                listLastNames.Add("Wouters");
                listLastNames.Add("De Smet");
            }
            else if (nationality == "Netherlands")
            {
                listLastNames.Add("de Jong");
                listLastNames.Add("Jansen");
                listLastNames.Add("de Vries");
                listLastNames.Add("van den Berg");
                listLastNames.Add("Van Dijk");
                listLastNames.Add("Bakker");
                listLastNames.Add("Janssen");
                listLastNames.Add("Visser");
                listLastNames.Add("Smit");
                listLastNames.Add("Meijer");
            }
            else if (nationality == "French")
            {
                listLastNames.Add("Martin");
                listLastNames.Add("Bernard");
                listLastNames.Add("Robert");
                listLastNames.Add("Richard");
                listLastNames.Add("Durand");
                listLastNames.Add("Dubois");
                listLastNames.Add("Moreau");
                listLastNames.Add("Simon");
                listLastNames.Add("Laurent");
                listLastNames.Add("Michel");
            }
            else if (nationality == "German")
            {
                listLastNames.Add("Muller");
                listLastNames.Add("Schmidt");
                listLastNames.Add("Schneider");
                listLastNames.Add("Fischer");
                listLastNames.Add("Weber");
                listLastNames.Add("Meyer");
                listLastNames.Add("Wagner");
                listLastNames.Add("Becker");
                listLastNames.Add("Schulz");
                listLastNames.Add("Hoffmann");
            }


            return listLastNames;

        }

        public IList<string> Nationalities()
        {
            var listNationalities = new List<string>();

            listNationalities.Add("Belgian");
            listNationalities.Add("Netherlands");
            listNationalities.Add("French");
            listNationalities.Add("German");

            return listNationalities;
        }
    }
}
