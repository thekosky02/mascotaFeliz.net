using System;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MascotaFeliz.App.Persistencia
{
    public class RepositorioVisitaPyP : IRepositorioVisitaPyP
    {
        /// <summary>
        /// Referencia al contexto de VisitaPyP
        /// </summary>
        private readonly AppContext _appContext;
        /// <summary>
        /// Metodo Constructor Utiiza 
        /// Inyeccion de dependencias para indicar el contexto a utilizar
        /// </summary>
        /// <param name="appContext"></param>//

        public RepositorioVisitaPyP(AppContext appContext)
        {
            _appContext = appContext;
        }

        public VisitaPyP AddVisita(VisitaPyP visita)
        {
            var visitaAdicionada = _appContext.VisitasPyP.Add(visita);
            _appContext.SaveChanges();
            return visitaAdicionada.Entity;
        }

        public void DeleteVisita(int idVisita)
        {
            var visitaEncontrada = _appContext.VisitasPyP.FirstOrDefault(v => v.Id == idVisita);

            if (visitaEncontrada == null)
                return;
                _appContext.VisitasPyP.Remove(visitaEncontrada);
                _appContext.SaveChanges();
        }

        public VisitaPyP GetVisita(int idVisita)
        {
            return _appContext.VisitasPyP.FirstOrDefault(v => v.Id == idVisita);
        }

        public VisitaPyP UpdateVisita(VisitaPyP visita)
        {
            var visitaEncontrada = _appContext.VisitasPyP.FirstOrDefault(v => v.Id == visita.Id);

            if (visitaEncontrada != null)
            {
                visitaEncontrada.FechaVisita = visita.FechaVisita;
                visitaEncontrada.Temperatura = visita.Temperatura;
                visitaEncontrada.Peso = visita.Peso;
                visitaEncontrada.FrecuenciaRespiratoria = visita.FrecuenciaRespiratoria;
                visitaEncontrada.FrecuenciaCardiaca = visita.FrecuenciaCardiaca;
                visitaEncontrada.EstadoAnimo = visita.EstadoAnimo;
                visitaEncontrada.IdVeterinario = visita.IdVeterinario;
                visitaEncontrada.Recomendaciones = visita.Recomendaciones;
                _appContext.SaveChanges();
            }
            return visitaEncontrada;
        }
    }
}