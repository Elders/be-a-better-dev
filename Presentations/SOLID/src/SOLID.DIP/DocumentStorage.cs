namespace SOLID.DIP
{
    public abstract class DocumentStorage : IInputRetriever, IDocumentPersister
    {
        public abstract string GetData(string sourceFileName);
        public abstract void Persist(string serializedDoc, string targetFileName);
    }
}