using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final
{
    public class Account
    {
        internal decimal Transactionst;

        public string ID { get; set; }
        public string Email { get; set; }

        public Account(string name, string id, string email)
        {
            Name = name;
            ID = id;
            Email = email;
        }

        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

}
