using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public interface IService<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);    
    }

    public interface IVisitService : IService<Visit>
    {

    }

    public interface IPatientService : IService<Patient>
    {
        Patient Get(string pesel);
    }


    public class DbPatientService : IPatientService
    {
        private MyContext context;

        public void Add(Patient patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();

        }

        public IEnumerable<Patient> Get()
        {
            throw new NotImplementedException();
        }

        public Patient Get(int id)
        {
            throw new NotImplementedException();
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

        public IList<Patient> Search(string firstname)
        {
            throw new NotImplementedException();
        }

    }

    public class FakePatientService : IPatientService
    {
        private readonly IList<Patient> patients;


        public FakePatientService()
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
