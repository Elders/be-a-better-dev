using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLID.DIP
{
    public static class InputRetriever
    {
        private static readonly Dictionary<Func<string, bool>, IInputRetriever> inputRetrievers = new Dictionary<Func<string, bool>, IInputRetriever>();

        public static void RegisterInputRetriever(Func<string, bool> evaluator, IInputRetriever retriever)
        {
            inputRetrievers.Add(evaluator, retriever);
        }

        public static IInputRetriever ForFileName(string fileName)
        {
            return inputRetrievers.First(x => x.Key(fileName)).Value;
        }
    }
}
