using EDDLab03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic;
using Structuras;
using System.Drawing;

namespace EDDLab03.Controllers
{
	[Route("[controller]")]
	public class CarrosController : Controller
	{
		List<Carro> CarrosLista = new List<Carro>();
		ABB<string> CarrosPropiedadABB = new ABB<string>();
		AVL<string> CarrosPropiedadAVL = new AVL<string>();

        int no = 0;

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
							no++;
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
			no++;
			CarrosLista.Add(nuevoCarro);
			return RedirectToAction("Index");
		}

		[HttpGet("ABBTipo")]
        public IActionResult ABBTipo()
        {
            return View();
        }
        [HttpPost("GuardarABBTipo")]
        public IActionResult TipoABB(int Colum)
		{
            for (int i = 0; i < no; i++)
			{
                if (Colum == 0)
                {
                    CarrosPropiedadABB.Insertar(CarrosLista[i].id);
				}
                else if (Colum == 1)
                {
                    CarrosPropiedadABB.Insertar(CarrosLista[i].email);
                }
                else if (Colum == 2)
                {
                    CarrosPropiedadABB.Insertar(CarrosLista[i].propietario);
                }
                else if (Colum == 3)
                {
                    CarrosPropiedadABB.Insertar(CarrosLista[i].color);
                }
                else if (Colum == 4)
                {
                    CarrosPropiedadABB.Insertar(CarrosLista[i].marca);
                }
                else if (Colum == 5)
                {
                    CarrosPropiedadABB.Insertar(CarrosLista[i].serie);
                }
            }
            return RedirectToAction("ABB");
        }
        [Route("ABB")]
        public IActionResult ABB()
        {
            return View();
        }

//------------------------------------------------------------------------------------------------------------------//

        [HttpGet("AVLTipo")]
        public IActionResult AVLTipo()
        {
            return View();
        }
        [HttpPost("GuardarAVLTipo")]
        public IActionResult TipoAVL(int Colum)
        {
            for (int i = 0; i < no; i++)
            {
                if (Colum == 0)
                {
                    CarrosPropiedadAVL.Insertar(CarrosLista[i].id);
                }
                else if (Colum == 1)
                {
                    CarrosPropiedadAVL.Insertar(CarrosLista[i].email);
                }
                else if (Colum == 2)
                {
                    CarrosPropiedadAVL.Insertar(CarrosLista[i].propietario);
                }
                else if (Colum == 3)
                {
                    CarrosPropiedadAVL.Insertar(CarrosLista[i].color);
                }
                else if (Colum == 4)
                {
                    CarrosPropiedadAVL.Insertar(CarrosLista[i].marca);
                }
                else if (Colum == 5)
                {
                    CarrosPropiedadAVL.Insertar(CarrosLista[i].serie);
                }
            }
            return RedirectToAction("AVL");
        }
        [Route("AVL")]
        public IActionResult AVL()
        {
            return View();
        }
    }
}
