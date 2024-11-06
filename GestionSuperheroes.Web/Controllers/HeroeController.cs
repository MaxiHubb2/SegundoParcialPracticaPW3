using GestionSuperheroes.Data.EF;
using GestionSuperheroes.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestionSuperheroes.Web.Controllers
{
    public class HeroeController : Controller
    {

        private readonly ISuperHeroeServicio _superHeroeServicio;
        private readonly IUniversoServicio _universoServicio;

        public HeroeController(ISuperHeroeServicio superHeroeServicio, IUniversoServicio universoServicio)
        {
            _superHeroeServicio = superHeroeServicio;
            _universoServicio = universoServicio;
        }

        public async Task<IActionResult> AgregarHeroe()
        {
           ViewBag.Universos = await _universoServicio.ObtenerTodosAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AgregarHeroe(Superheroe heroe)
        {
            if (ModelState.IsValid)
            {
                await _superHeroeServicio.AgregarSuperheroe(heroe);
                return RedirectToAction("Lista");
            }

            ViewBag.Universos = await _universoServicio.ObtenerTodosAsync();
            return View(heroe);
        }


        public async Task<IActionResult> Lista(int? idUniverso)
        {
            ViewBag.Universos = await _universoServicio.ObtenerTodosAsync();
            ViewBag.IdUniversoSeleccionado = idUniverso ?? 0;

            var heroes = idUniverso.HasValue && idUniverso.Value != 0 ?
                await _superHeroeServicio.ObtenerPorUniversoAsync(idUniverso.Value) :
                await _superHeroeServicio.ObtenerTodosAsync();

            return View(heroes);
        }

        public async Task<IActionResult> EliminarHeroe(int? idSuperheroe, int? idUniverso)
        {
            if (idSuperheroe.HasValue)
            {
                await _superHeroeServicio.EliminarHeroeAsync(idSuperheroe.Value);
            }

            return RedirectToAction("Lista", new { idUniverso = idUniverso });
        }



    }
}
