using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;

namespace MascotaFeliz.App.Persistencia
{
    public interface IRepositorioHistoria
    {
        Historia AddHistoria(Historia historia);
        Historia GetHistoria(int idHistoria);
        void DeleteHistoria(int idHistoria);
        Historia UpdateHistoria(Historia historia);
        VisitaPyP AsignarVisita(int idHistoria, int idVisita);
    }
}