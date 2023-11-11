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

   
    /*private int GenerateId()
    {
        if (entities.Count == 0)
            return 0;
        return entities[^1].Id + 1;

    }*/
    public T AddEntity(T t)
    {
        entities.Add(t);
        storage.Save(entities);
        return t;
    }

    /*public Vehicle? UpdateVehicle(Vehicle vehicle)
    {
        Vehicle? oldVehicle = GetVehicleById(vehicle.Id);
        if (oldVehicle is null) return null;

        oldVehicle.NumberOfWheels = vehicle.NumberOfWheels;
        oldVehicle.Name = vehicle.Name;

        _storage.Save(_vehicles);
        return oldVehicle;
    }*/

    public T UpdateEntity (T t)
    {
        return t;
    }

    public T ? DeleteEntity(T t)
    {
        return t;
    }

    /*public T? GetEntityById(int id)
    {
        DA LI INTERFEJS MOZE IMATI POLJA PA DA SE IM
        return entities.Find(t => t.Id == id);
    }*/


}
//