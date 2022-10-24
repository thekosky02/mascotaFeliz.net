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
    public class HistoriaClinicaModel : PageModel
    {
        private readonly IRepositorioHistoria _repoHistoria;
        private readonly IRepositorioMascota _repoMascota;

        public Historia historia {get; set;}
        public Mascota mascota {get; set;}
        public IEnumerable<VisitaPyP> ListaVisitas {get; set;}

        public HistoriaClinicaModel()
        {
            this._repoHistoria = new RepositorioHistoria(new Persistencia.AppContext());
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        }

        public IActionResult OnGet(int historiaId, int mascotaId)
        {
            historia = _repoHistoria.GetHistoria(historiaId);
            ListaVisitas = historia.VisitasPyP;
            mascota = _repoMascota.GetMascota(mascotaId);

            if (historia == null && mascota != null)
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
