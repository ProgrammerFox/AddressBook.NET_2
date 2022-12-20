using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook_dotnet6.Model
{
    public class AddressBook<T> : List<T> 
        where T : BasePerson
    {
        public void Swap(int id1, int id2)
        {
            (this[id1], this[id2]) = (this[id2], this[id1]);
        }
    }
}
