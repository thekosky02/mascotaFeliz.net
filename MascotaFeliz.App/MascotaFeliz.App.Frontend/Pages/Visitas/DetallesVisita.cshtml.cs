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
    public class DetallesVisitaModel : PageModel
    {
        private readonly IRepositorioVisitaPyP _repoVisita;
        private readonly IRepositorioMascota _repoMascota;
        private readonly IRepositorioHistoria _repoHistoria;
        private readonly IRepositorioVeterinario _repoVeterinario;

        public VisitaPyP visita {get; set;}
        public Mascota mascota {get; set;}
        public Historia historia {get; set;}
        public Veterinario veterinario {get; set;}

        public DetallesVisitaModel()
        {
            this._repoVisita = new RepositorioVisitaPyP(new  Persistencia.AppContext());
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
            this._repoHistoria = new RepositorioHistoria(new Persistencia.AppContext());
            this._repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
        }

        public IActionResult OnGet(int visitaId, int mascotaId, int historiaId)
        {
            visita = _repoVisita.GetVisita(visitaId);
            mascota = _repoMascota.GetMascota(mascotaId);
            historia = _repoHistoria.GetHistoria(historiaId);
            veterinario = _repoVeterinario.GetVeterinario(visita.IdVeterinario);

            if (visita == null)
            {
                return RedirectToPage("../Error");
            }
            else
            {
                return Page();
            }
        }
    }
}
