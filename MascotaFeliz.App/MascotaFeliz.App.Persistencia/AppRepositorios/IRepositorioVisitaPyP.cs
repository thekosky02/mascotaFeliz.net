using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;

namespace MascotaFeliz.App.Persistencia
{
    public interface IRepositorioVisitaPyP
    {
        VisitaPyP AddVisita(VisitaPyP visita);
        void DeleteVisita(int idVisita);
        VisitaPyP GetVisita(int idVisita);
        VisitaPyP UpdateVisita(VisitaPyP visita);
    }
}