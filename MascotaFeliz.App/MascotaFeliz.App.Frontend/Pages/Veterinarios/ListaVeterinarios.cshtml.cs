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
    public class ListaVeterinariosModel : PageModel
    {
        private static IRepositorioVeterinario _repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
        public IEnumerable<Veterinario> ListaVeterinarios {get; set;}
        [BindProperty(SupportsGet = true)]
        public String NombreVeterinario {get; set;}

        public void OnGet(String NombreVeterinario)
        {
            ListaVeterinarios = _repoVeterinario.GetVeterinariosPorFiltro(NombreVeterinario);
        }
    }
}
