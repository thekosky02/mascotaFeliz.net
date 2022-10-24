using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MascotaFeliz.App.Dominio;
using MascotaFeliz.App.Persistencia;

namespace MascotaFeliz.App.Frontedn.Pages
{
    public class CrearVisitaModel : PageModel
    {
        private readonly IRepositorioVisitaPyP _repoVisita;
        private readonly IRepositorioHistoria _repoHistoria;
        private readonly IRepositorioVeterinario _repoVeterinario;
        private readonly IRepositorioMascota _repoMascota;

        [BindProperty]
        public VisitaPyP visita {get; set;}
        public Mascota mascota {get; set;}
        public Historia historia {get; set;}
        public DateTime FechaActual {get; set;}

        public IEnumerable<Veterinario> listaVeterinarios {get; set;}

        public CrearVisitaModel()
        {
            this._repoVisita = new RepositorioVisitaPyP(new Persistencia.AppContext());
            this._repoHistoria = new RepositorioHistoria(new Persistencia.AppContext());
            this._repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        }

        public IActionResult OnGet(int mascotaId, int historiaId, int? visitaId)
        {
            listaVeterinarios = _repoVeterinario.GetAllVeterinarios();

            mascota = _repoMascota.GetMascota(mascotaId);
            historia = _repoHistoria.GetHistoria(historiaId);

            visita = new VisitaPyP(); 
            FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            visita.FechaVisita = FechaActual;
            
            return Page();
        }

        public IActionResult OnPost(VisitaPyP visita, int mascotaId, int historiaId, int veterinarioId)
        {
            if (ModelState.IsValid)
            {
                visita.IdVeterinario = veterinarioId;
                _repoVisita.AddVisita(visita);
                _repoHistoria.AsignarVisita(historiaId, visita.Id);
                
                return RedirectToPage("../Historias/HistoriaClinica", new {mascotaId = mascotaId, historiaId = historiaId});
            }
            else
            {
                return Page();
            }
        }
    }
}