namespace StudentskaSluzba.Serialization;

public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);

    void GetEntityById(string id);
    void UpdateEntity();
}
