using Racing2021.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Racing2021.Repositories
{
    class TeamRepository : IRepository<Team>
    {
        private readonly string path = @".\Data";
        private readonly string file = $"Teams.xml";

        public TeamRepository()
        {
            Create();
        }

        public IList<Team> Create(IList<Team> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Team>));
                foreach (var team in itemList)
                {
                    writer.WriteStartElement(nameof(Team));
                    writer.WriteAttributeString(nameof(Team.Id), team.Id.ToString());
                    writer.WriteAttributeString(nameof(Team.Name), team.Name.ToString());
                    writer.WriteAttributeString(nameof(Team.JerseyName), team.JerseyName.ToString());
                    writer.WriteAttributeString(nameof(Team.Money), team.Money.ToString());
                    writer.WriteAttributeString(nameof(Team.ManagerId), team.ManagerId.ToString());
                    writer.WriteAttributeString(nameof(Team.Reputation), team.Reputation.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.Flush();
            }

            using (StreamWriter streamWriter = File.CreateText(Path.Combine(path, file)))
            {
                streamWriter.Write(stream);
            }

            return itemList;
        }

        public IList<Team> Get()
        {
            return (Get(file));
        }

        public IList<Team> Get(string filename)
        {
            var TeamList = new List<Team>();
            var fileString = File.ReadAllText(Path.Combine(path, file));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return TeamList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Cyclist>));
                    do
                    {
                        var readTeam = new Team();

                        readTeam.Id = int.Parse(xmlReader.GetAttribute(nameof(Team.Id)));
                        readTeam.Name = xmlReader.GetAttribute(nameof(Team.Name));
                        readTeam.JerseyName = xmlReader.GetAttribute(nameof(Team.JerseyName));
                        readTeam.Money = int.Parse(xmlReader.GetAttribute(nameof(Team.Money)));
                        readTeam.ManagerId = int.Parse(xmlReader.GetAttribute(nameof(Team.ManagerId)));
                        readTeam.Reputation = int.Parse(xmlReader.GetAttribute(nameof(Team.Reputation)));

                        TeamList.Add(readTeam);
                    } while (xmlReader.ReadToNextSibling(nameof(Team)));
                }
            }

            return TeamList;
        }

        private void Create()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(Path.Combine(path, file)))
            {
                var createdFile = File.Create(Path.Combine(path, file));
                createdFile.Close();
            }
        }
    }
}
