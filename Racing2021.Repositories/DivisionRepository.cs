using Racing2021.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Racing2021.Repositories
{
    class DivisionRepository : IRepository<Division>
    {
        private readonly string path = @".\Data";
        private readonly string file = $"Divisions.xml";

        public DivisionRepository()
        {
            Create();
        }

        public IList<Division> Create(IList<Division> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Division>));

                foreach (var division in itemList)
                {
                    writer.WriteStartElement(nameof(Division));
                    writer.WriteAttributeString(nameof(Division.Id), division.Id.ToString());
                    writer.WriteAttributeString(nameof(Division.Tier), division.Tier.ToString());
                    writer.WriteAttributeString(nameof(Division.Reputation), division.Reputation.ToString());
                    writer.WriteAttributeString(nameof(Division.Name), division.Name.ToString());
                    foreach (var teamId in division.TeamsId)
                    {
                        writer.WriteStartElement(nameof(Division.TeamsId));
                        writer.WriteElementString("teamId", teamId.ToString());
                        writer.WriteEndElement();
                    }
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

        public IList<Division> Get()
        {
            return (Get(file));
        }

        public IList<Division> Get(string filename)
        {
            var divisionList = new List<Division>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return divisionList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Division>));

                    do
                    {
                        var readDivision = new Division();

                        readDivision.Id = int.Parse(xmlReader.GetAttribute(nameof(Division.Id)));
                        readDivision.Tier = int.Parse(xmlReader.GetAttribute(nameof(Division.Tier)));
                        readDivision.Reputation = int.Parse(xmlReader.GetAttribute(nameof(Division.Reputation)));
                        readDivision.Name = xmlReader.GetAttribute(nameof(Division.Name));

                        xmlReader.ReadStartElement(nameof(Division));

                        readDivision.TeamsId = new List<int>();

                        do
                        {
                            xmlReader.ReadStartElement(nameof(Division.TeamsId));
                            xmlReader.ReadStartElement("teamId");
                            readDivision.TeamsId.Add(xmlReader.ReadContentAsInt());
                            xmlReader.ReadEndElement();

                        } while (xmlReader.ReadToNextSibling(nameof(Division.TeamsId)));

                        divisionList.Add(readDivision);
                    } while (xmlReader.ReadToNextSibling(nameof(Division)));
                }
            }

            return divisionList;
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
