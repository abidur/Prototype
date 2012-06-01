using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YelpTest.Models
{
    public interface IUserAgendaRepository
    {
        //IList<Agenda> GetUserAgenda(Guid id);

        bool AddAgenda(UserAgenda userAgenda, Agenda agenda);

        bool RemoveAgenda(UserAgenda userAgenda, Agenda agenda);
    }
}