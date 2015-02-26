#region

using System.Text;
using Novacode;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;
using Tabster.Data.Xml;

#endregion

namespace WordDoc
{
    public class WordDocImporter : ITablatureFileImporter
    {
        public WordDocImporter()
        {
            FileType = new FileType("Microsoft Office Open XML Format Document", ".docx");
        }

        #region Implementation of ITablatureDocumentImporter

        public FileType FileType { get; private set; }

        public TablatureDocument Import(string fileName)
        {
            using (var document = DocX.Load(fileName))
            {
                var sb = new StringBuilder();

                foreach (var paragraph in document.Paragraphs)
                {
                    sb.AppendLine(paragraph.Text);
                    sb.AppendLine();
                }

                var doc = new TablatureDocument {Contents = sb.ToString()};
                return doc;
            }
        }

        public TablatureDocument Import(string fileName, string artist, string title, TablatureType type)
        {
            var doc = Import(fileName);
            doc.Artist = artist;
            doc.Title = title;
            doc.Type = type;
            return doc;
        }

        #endregion
    }
}