using Racing2021.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Racing2021.Repositories
{
    class ManagerRepository : IRepository<Manager>
    {
        private readonly string path = @".\Data";
        private readonly string file = $"Managers.xml";

        public ManagerRepository()
        {
            Create();
        }

        public IList<Manager> Create(IList<Manager> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Manager>));
                foreach (var manager in itemList)
                {
                    writer.WriteStartElement(nameof(Manager));
                    writer.WriteAttributeString(nameof(Manager.Id), manager.Id.ToString());
                    writer.WriteAttributeString(nameof(Manager.TeamId), manager.TeamId.ToString());
                    writer.WriteAttributeString(nameof(Manager.Name), manager.Name.ToString());
                    writer.WriteAttributeString(nameof(Manager.Age), manager.Age.ToString());
                    writer.WriteAttributeString(nameof(Manager.Nationality), manager.Nationality.ToString());
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

        public IList<Manager> Get()
        {
            return (Get(file));
        }

        public IList<Manager> Get(string filename)
        {
            var ManagerList = new List<Manager>();
            var fileString = File.ReadAllText(Path.Combine(path, file));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return ManagerList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Manager>));
                    do
                    {
                        var readManager = new Manager();

                        readManager.Id = int.Parse(xmlReader.GetAttribute(nameof(Manager.Id)));
                        readManager.TeamId = int.Parse(xmlReader.GetAttribute(nameof(Manager.TeamId)));
                        readManager.Name = xmlReader.GetAttribute(nameof(Manager.Name));
                        readManager.Age = int.Parse(xmlReader.GetAttribute(nameof(Manager.Age)));
                        readManager.Nationality = xmlReader.GetAttribute(nameof(Manager.Nationality));

                        ManagerList.Add(readManager);
                    } while (xmlReader.ReadToNextSibling(nameof(Manager)));
                }
            }

            return ManagerList;
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
