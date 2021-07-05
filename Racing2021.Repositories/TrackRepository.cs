using Racing2021.Models;
using Racing2021.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Racing2021.Repositories
{
    public class TrackRepository : IRepository<Track>
    {
        private readonly string path = @".\Data";
        private readonly string file = $"Tracks.xml";

        public TrackRepository()
        {
            Create();
        }

        public IList<Track> Create(IList<Track> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<Track>));
                foreach (var track in itemList)
                {
                    writer.WriteStartElement(nameof(Track));
                    writer.WriteAttributeString(nameof(Track.Name), track.Name.ToString());
                    foreach (var tracktile in track.TrackTiles)
                    {
                        writer.WriteStartElement(nameof(TrackTile));
                        writer.WriteElementString("type", tracktile.ToString());
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

        public IList<Track> Get()
        {
            return (Get(file));
        }

        public IList<Track> Get(string filename)
        {
            var TrackList = new List<Track>();
            var fileString = File.ReadAllText(Path.Combine(path, filename));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return TrackList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<Track>));
                    do
                    {
                        var readTrack = new Track();

                        readTrack.Name = xmlReader.GetAttribute(nameof(Track.Name));

                        xmlReader.ReadStartElement(nameof(Track));

                        readTrack.TrackTiles = new List<TrackTile>();

                        do
                        {
                            xmlReader.ReadStartElement(nameof(TrackTile));
                            xmlReader.ReadStartElement("type");
                            readTrack.TrackTiles.Add((TrackTile)Enum.Parse(typeof(TrackTile), xmlReader.ReadContentAsString()));
                            xmlReader.ReadEndElement();

                        } while (xmlReader.ReadToNextSibling(nameof(TrackTile)));


                        TrackList.Add(readTrack);
                    } while (xmlReader.ReadToNextSibling(nameof(Track)));
                }
            }

            return TrackList;
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
