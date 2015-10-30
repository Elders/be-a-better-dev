namespace SOLID.DIP
{
    public interface IDocumentPersister
    {
        void Persist(string serializedDoc, string targetFileName);
    }
}