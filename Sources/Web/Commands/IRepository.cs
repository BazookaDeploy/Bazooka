using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Commands
{
    public interface IRepository
    {
        T Get<T>(int id);

        void Save<T>(T aggregate);
    }
}
