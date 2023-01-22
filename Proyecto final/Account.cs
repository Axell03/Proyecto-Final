using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final
{
    class Account
    {
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

}
