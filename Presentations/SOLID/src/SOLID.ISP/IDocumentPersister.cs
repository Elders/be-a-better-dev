namespace SOLID.ISP
{
    public interface IDocumentPersister
    {
        void Persist(string serializedDoc, string targetFileName);
    }
}
