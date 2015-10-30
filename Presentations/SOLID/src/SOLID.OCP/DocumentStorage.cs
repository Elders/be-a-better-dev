namespace SOLID.OCP
{
    public abstract class DocumentStorage
    {
        public abstract string GetData(string sourceFileName);
        public abstract void Persist(string serializedDoc, string targetFileName);
    }
}
