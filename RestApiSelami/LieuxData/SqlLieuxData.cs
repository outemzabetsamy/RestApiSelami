using RestApiSelami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiSelami.LieuxData
{
    public class SqlLieuxData : ILieuxData
    {
        private LieuxContext _lieuxContext;
        public SqlLieuxData(LieuxContext lieuxContext)
        {
            _lieuxContext = lieuxContext;
        }
        public Lieux AddLieux(Lieux lieux)
        {
            
                lieux.Id = Guid.NewGuid();
            _lieuxContext.Lieuxs.Add(lieux);
           _lieuxContext.SaveChanges();
           return lieux;
        }

        public void DeleteLieux(Lieux lieux)
        {
            _lieuxContext.Lieuxs.Remove(lieux);
           _lieuxContext.SaveChanges();
        }

        public Lieux EditLieux(Lieux lieux)
        {
            
             var existingLieux = _lieuxContext.Lieuxs.Find(lieux.Id);
            if (existingLieux != null)
             {
              existingLieux.Name = lieux.Name;
                existingLieux.Adresse = lieux.Adresse;
              _lieuxContext.Lieuxs.Update(existingLieux);
              _lieuxContext.SaveChanges();
             }
             return lieux;
        }

        public Lieux GetLieux(Guid Id)
        {
            var lieux = _lieuxContext.Lieuxs.Find(Id);
            return lieux;
        }

        public List<Lieux> GetLieuxs()
        {
            return _lieuxContext.Lieuxs.ToList();
        }
    }
}
