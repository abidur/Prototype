using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YelpTest.Models
{
    public class UserAgenda
    {
        public UserAgenda()
        {
            id = Guid.NewGuid();
        }
        private readonly Guid id;

        public Guid Id
        {
            get
            {
                return id;
            }
        }

        public IList<Agenda> Agendas { get; set; }
        
    }
}