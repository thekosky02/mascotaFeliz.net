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
    public class ListaMascotasModel : PageModel
    {
        private readonly IRepositorioMascota _repoMascota;
        public IEnumerable<Mascota> ListaMascotas {get; set;}
        [BindProperty(SupportsGet = true)]
        public String NombreMascota {get; set;}

        public ListaMascotasModel()
        {
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        }

        public void OnGet(String NombreMascota)
        {
            ListaMascotas = _repoMascota.GetMascotasPorFiltro(NombreMascota);
        }
    }
}
