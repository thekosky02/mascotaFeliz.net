using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MascotaFeliz.App.Dominio;
using MascotaFeliz.App.Persistencia;

namespace MascotaFeliz.App.Frontend.Pages
{
    public class CrearEditarMascotaModel : PageModel
    {
        private readonly IRepositorioMascota _repoMascota;
        private readonly IRepositorioDueno _repoDueno;
        private readonly IRepositorioVeterinario _repoVeterinario;
        private readonly IRepositorioHistoria _repoHistoria;
        
        [BindProperty]
        public Mascota mascota { get; set; }
        public Historia historia { get; set; }
        public Veterinario veterinario { get; set; }
        public Dueno dueno { get; set; }
        public IEnumerable<Dueno> listaDuenos { get; set; }
        public IEnumerable<Veterinario> listaVeterinarios { get; set; }

        public CrearEditarMascotaModel()
        {
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
            this._repoDueno = new RepositorioDueno(new Persistencia.AppContext());
            this._repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
            this._repoHistoria = new RepositorioHistoria(new Persistencia.AppContext());
        }

        public IActionResult OnGet(int? mascotaId)
        {
            listaDuenos = _repoDueno.GetAllDuenos();
            listaVeterinarios = _repoVeterinario.GetAllVeterinarios();
            
            if (mascotaId.HasValue)
            {
                mascota = _repoMascota.GetMascota(mascotaId.Value);
            }
            else
            {
                mascota = new Mascota();
                historia = new Historia();
                DateTime date1 = DateTime.Now;
                historia.FechaInicial = date1;
                historia.VisitasPyP = new List<VisitaPyP>();
            }

            if (mascota == null)
            {
                return RedirectToPage("../Error");
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost(Mascota mascota, Historia historia, int duenoId, int veterinarioId)
        {
            if (ModelState.IsValid)
            {
                dueno = _repoDueno.GetDueno(duenoId);
                veterinario = _repoVeterinario.GetVeterinario(veterinarioId);

                if (mascota.Id > 0)
                {
                    mascota.Dueno = dueno;
                    mascota.Veterinario = veterinario;
                    mascota = _repoMascota.UpdateMascota(mascota);
                }
                else
                {
                    _repoHistoria.AddHistoria(historia);
                    _repoMascota.AddMascota(mascota);
                    _repoMascota.AsignarVeterinario(mascota.Id,veterinario.Id);
                    _repoMascota.AsignarDueno(mascota.Id,dueno.Id);
                    _repoMascota.AsignarHistoria(mascota.Id, historia.Id);
                }

                return RedirectToPage("./ListaMascotas");
            }
            else
            {
                return Page();
            }
        }
    }
}
