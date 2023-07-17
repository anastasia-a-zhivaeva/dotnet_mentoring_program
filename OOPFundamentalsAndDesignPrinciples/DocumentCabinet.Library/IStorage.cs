namespace DocumentCabinetLibrary
{
    public interface IStorage<Key, Value>
    {
        void Add(Value value);
        Value Get(Key key);
        IEnumerable<Value> GetAll();
        IEnumerable<Value> FindByKeyPart(Key key);
        void Remove(Key key);
        void RemoveAll();
    }
}
