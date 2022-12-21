using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AddressBook_dotnet6.Model
{
    public class BasePerson
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Address { get; set; }

        public virtual string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
