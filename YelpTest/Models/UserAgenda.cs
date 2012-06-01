using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YelpTest.Models
{
    public class UserAgenda
    {
        private readonly Guid id = Guid.NewGuid();

        public Guid Id
        {
            get
            {
                return id;
            }
        }

        public IList<Agenda> Id { get; set; }
    }
}