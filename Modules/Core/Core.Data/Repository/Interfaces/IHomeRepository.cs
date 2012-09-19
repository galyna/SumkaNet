using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Entities;

namespace Core.Data.Repository.Interfaces
{
   public interface IHomeRepository
    {
       IList<Store> GetStores();
    }
}
