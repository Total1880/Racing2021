using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.Repositories
{
    public class DataRepository : IDataRepository
    {
        public IList<string> FirstNames()
        {
            var listFirstNames = new List<string>();

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

            return listFirstNames;
        }

        public IList<string> LastNames()
        {
            var listLastNames = new List<string>();

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

            return listLastNames;

        }
    }
}
