using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLID.DIP
{
    public static class DocumentPersister
    {
        private static readonly Dictionary<Func<string, bool>, IDocumentPersister> documentPersisters = new Dictionary<Func<string, bool>, IDocumentPersister>();

        public static void RegisterDocumentPersister(Func<string, bool> evaluator, IDocumentPersister persister)
        {
            documentPersisters.Add(evaluator, persister);
        }

        public static IDocumentPersister ForFileName(string fileName)
        {
            return documentPersisters.First(x => x.Key(fileName)).Value;
        }
    }
}
