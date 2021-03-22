using RestApiSelami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiSelami.LieuxData
{
   public  interface ILieuxData
    {
        List<Lieux> GetLieuxs();
        Lieux GetLieux(Guid Id);
        Lieux AddLieux(Lieux lieux);
        void DeleteLieux(Lieux lieux);
        Lieux EditLieux(Lieux lieux);
    }
}
