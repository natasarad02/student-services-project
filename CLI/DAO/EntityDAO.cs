using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.DAO;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
public class EntityDAO<T> where T : ISerializable, new()
{
    private readonly List<T> entities;
    private readonly Storage<T> storage;

    public EntityDAO(string filename)
    {
        storage = new Storage<T>(filename + ".txt");
        entities = storage.Load();
    }



}