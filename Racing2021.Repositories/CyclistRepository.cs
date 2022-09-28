using Racing2021.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Racing2021.Repositories
{
    class CyclistRepository : IRepository<Cyclist>
    {
        private readonly string path = @".\Data";
        private readonly string file = $"Cyclists.xml";

        public CyclistRepository()
        {
            Create();
        }

        public IList<Cyclist> Create(IList<Cyclist> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Cyclist>));
                foreach (var cyclist in itemList)
                {
                    writer.WriteStartElement(nameof(Cyclist));
                    writer.WriteAttributeString(nameof(Cyclist.Id), cyclist.Id.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.TeamId), cyclist.TeamId.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.Name), cyclist.Name.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedHorizontalPotential), cyclist.CyclistSpeedHorizontalPotential.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedCobblestonesPotential), cyclist.CyclistSpeedCobblestonesPotential.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedDownPotential), cyclist.CyclistSpeedDownPotential.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedUpPotential), cyclist.CyclistSpeedUpPotential.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedHorizontal), cyclist.CyclistSpeedHorizontal.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedCobblestones), cyclist.CyclistSpeedCobblestones.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedDown), cyclist.CyclistSpeedDown.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.CyclistSpeedUp), cyclist.CyclistSpeedUp.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.Age), cyclist.Age.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.SelectedForRace), cyclist.SelectedForRace.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.Nationality), cyclist.Nationality.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.Contract.YearsLeft), cyclist.Contract.YearsLeft.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.Contract.SalaryPerYear), cyclist.Contract.SalaryPerYear.ToString());
                    writer.WriteAttributeString(nameof(Cyclist.TeamLeader), cyclist.TeamLeader.ToString());
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

        public IList<Cyclist> Get()
        {
            return (Get(file));
        }

        public IList<Cyclist> Get(string filename)
        {
            var CyclistList = new List<Cyclist>();
            var fileString = File.ReadAllText(Path.Combine(path, file));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return CyclistList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Cyclist>));
                    do
                    {
                        var readCyclist = new Cyclist();
                        readCyclist.Contract = new Contract();

                        readCyclist.Id = int.Parse(xmlReader.GetAttribute(nameof(Cyclist.Id)));
                        readCyclist.TeamId = int.Parse(xmlReader.GetAttribute(nameof(Cyclist.TeamId)));
                        readCyclist.Name = xmlReader.GetAttribute(nameof(Cyclist.Name));
                        readCyclist.CyclistSpeedHorizontalPotential = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedHorizontalPotential)));
                        readCyclist.CyclistSpeedCobblestonesPotential = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedCobblestonesPotential)));
                        readCyclist.CyclistSpeedDownPotential = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedDownPotential)));
                        readCyclist.CyclistSpeedUpPotential = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedUpPotential)));
                        readCyclist.CyclistSpeedHorizontal = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedHorizontal)));
                        readCyclist.CyclistSpeedCobblestones = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedCobblestones)));
                        readCyclist.CyclistSpeedDown = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedDown)));
                        readCyclist.CyclistSpeedUp = float.Parse(xmlReader.GetAttribute(nameof(Cyclist.CyclistSpeedUp)));
                        readCyclist.Age = int.Parse(xmlReader.GetAttribute(nameof(Cyclist.Age)));
                        readCyclist.SelectedForRace = bool.Parse(xmlReader.GetAttribute(nameof(Cyclist.SelectedForRace)));
                        readCyclist.Nationality = xmlReader.GetAttribute(nameof(Cyclist.Nationality));
                        readCyclist.Contract.YearsLeft = int.Parse(xmlReader.GetAttribute(nameof(Cyclist.Contract.YearsLeft)));
                        readCyclist.Contract.SalaryPerYear = int.Parse(xmlReader.GetAttribute(nameof(Cyclist.Contract.SalaryPerYear)));
                        readCyclist.TeamLeader = bool.Parse(xmlReader.GetAttribute(nameof(Cyclist.TeamLeader)));

                        CyclistList.Add(readCyclist);
                    } while (xmlReader.ReadToNextSibling(nameof(Cyclist)));
                }
            }

            return CyclistList;
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
