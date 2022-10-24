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
    public class DetallesduenoModel : PageModel
    {
        private readonly IRepositorioDueno _repoDueno;

        public Dueno dueno {get; set;}

        public DetallesduenoModel() 
        {
            this._repoDueno = new RepositorioDueno(new Persistencia.AppContext());
        }

        public IActionResult OnGet(int duenoId)
        {
            dueno = _repoDueno.GetDueno(duenoId);
            
            if (dueno == null) 
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
