using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);    
    }

    public interface IVisitRepository : IRepository<Visit>
    {

    }

    public interface IPatientRepository : IRepository<Patient>
    {
        Patient Get(string pesel);
    }


    public abstract class DbEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : Base
    {
        private readonly IList<TEntity> entities;

        public virtual void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

    public class DbVisitRepository : 
        DbEntityRepository<Visit>, IVisitRepository
    {
        
    }

    public class DbPatientRepository 
        : DbEntityRepository<Patient>, IPatientRepository
    {
        public Patient Get(string pesel)
        {
            throw new NotImplementedException();
        }


        public override void Remove(int id)
        {
            base.Remove(id);
        }
    }

    public class FakePatientRepository : IPatientRepository
    {
        private readonly IList<Patient> patients;


        public FakePatientRepository()
        {
            patients = new List<Patient>();
        }

        public void Add(Patient patient)
        {
            patients.Add(patient);
        }

        public IEnumerable<Patient> Get()
        {
            return patients;
        }

        public Patient Get(int id)
        {
            return patients.SingleOrDefault(p => p.Id == id);
        }

        public Patient Get(string pesel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
