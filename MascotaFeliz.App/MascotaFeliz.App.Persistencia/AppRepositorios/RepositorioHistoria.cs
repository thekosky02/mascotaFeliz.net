using System;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MascotaFeliz.App.Persistencia
{
    public class RepositorioHistoria : IRepositorioHistoria
    {
        /// <summary>
        /// Referencia al contexto de Historia
        /// </summary>
        private readonly AppContext _appContext;
        /// <summary>
        /// Metodo Constructor Utiiza 
        /// Inyeccion de dependencias para indicar el contexto a utilizar
        /// </summary>
        /// <param name="appContext"></param>//

        public RepositorioHistoria(AppContext appContext)
        {
            _appContext = appContext;
        }

        public Historia AddHistoria(Historia historia)
        {
            var historiaAdicionada = _appContext.Historias.Add(historia);
            _appContext.SaveChanges();
            return historiaAdicionada.Entity;
        }

        public Historia GetHistoria(int idHistoria)
        {
            return _appContext.Historias.Include("VisitasPyP").FirstOrDefault(h => h.Id == idHistoria);
        }

        public void DeleteHistoria(int idHistoria)
        {
            var historiaEncontrada = _appContext.Historias.FirstOrDefault(h => h.Id == idHistoria);
            if (historiaEncontrada == null)
                return;
            _appContext.Historias.Remove(historiaEncontrada);
            _appContext.SaveChanges();
        }

        public Historia UpdateHistoria(Historia historia)
        {
            var historiaEncontrada = _appContext.Historias.Include("VisitasPyP").FirstOrDefault(h => h.Id == historia.Id);

            if (historiaEncontrada != null)
            {
                historiaEncontrada.FechaInicial = historia.FechaInicial;
                historiaEncontrada.VisitasPyP = historia.VisitasPyP;

                _appContext.SaveChanges();
            }
            return historiaEncontrada;
        }

        public VisitaPyP AsignarVisita(int idHistoria, int idVisita)
        {
            var historiaEncontrada = _appContext.Historias.Include("VisitasPyP").FirstOrDefault(h => h.Id == idHistoria);
            var visitaEncontrada = _appContext.VisitasPyP.FirstOrDefault(v => v.Id == idVisita);

            if (historiaEncontrada != null && visitaEncontrada != null)
            {
                historiaEncontrada.VisitasPyP.Add(visitaEncontrada);
                _appContext.SaveChanges();

                return visitaEncontrada;
            }
            else
            {
                return null;
            }
        }
    }
}