using EDDLab03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic;

namespace EDDLab03.Controllers
{
	[Route("[controller]")]
	public class CarrosController : Controller
	{
		List<Carro> CarrosLista = new List<Carro>();

        [Route("Index")]
        public IActionResult Index()
		{
			return View(CarrosLista);
		}
        public IActionResult CargarArchivo()
        {
            return View();
        }

        [HttpPost("cargacsv")]
		public IActionResult CargaCSV(IFormFile archivo)
		{
			if (archivo != null)
			{
				try
				{
					string ruta = Path.Combine(Path.GetTempPath(), archivo.Name);
					using (var stream = new FileStream(ruta, FileMode.Create))
					{
						archivo.CopyTo(stream);
					}

					string informacionArchivo = System.IO.File.ReadAllText(ruta);
					int contador = 0;
					foreach (string linea in informacionArchivo.Split('\n'))
					{
						contador++;
						if (contador == 0)
						{
							contador++;
						}
						if (!string.IsNullOrEmpty(linea) && contador > 1)
						{
							string[] FilaActual = linea.Split(',');
							CarrosLista.Add(new Carro
							{
								id = FilaActual[0],
								email = FilaActual[1],
								propietario = FilaActual[2],
								color = FilaActual[3],
								marca = FilaActual[4],
								serie = FilaActual[5],
							});
						}
					}
				}
				catch (Exception e)
				{
					ViewBag.Error = "Error al leer el archivo" + e.Message;
				}
			}
			else
			{
				ViewBag.Error = "No se ha ingresado la ruta de archivo";
			}
			return View(CarrosLista);
		}
		[Route("cargacsv")]
		public IActionResult CargaCSV()
		{
			return View();
		}

        [HttpGet("cargamanual")]
        public IActionResult CargaManual()
        {
            return View();
        }

        [HttpPost("Guardar")]
        public IActionResult Guardar(Carro nuevoCarro)
        {
            CarrosLista.Add(nuevoCarro);
            return RedirectToAction("Index");
        }
        [Route("AVL")]
        public IActionResult AVL()
		{
			return View();
		}
        [Route("ABB")]
        public IActionResult ABB()
        {
            return View();
        }
    }
}
