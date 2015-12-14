using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public interface IMovable
    {
        int Position { get; set; }

        void MoveUp();

        void MoveDown();
    }
}
