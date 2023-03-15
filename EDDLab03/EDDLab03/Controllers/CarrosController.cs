using EDDLab03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic;
using Structuras;
using System.Drawing;
using System.Diagnostics;
namespace EDDLab03.Controllers
{
	[Route("[controller]")]
	public class CarrosController : Controller
	{
		public static List<Carro> CarrosLista = new List<Carro>();
		public static ABB<String> CarrosPropiedadABB = new ABB<String>();
		public static AVL<String> CarrosPropiedadAVL = new AVL<String>();
		public static int CUAL;
		public Stopwatch stopwatch= new Stopwatch();
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
			stopwatch.Start();
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
			stopwatch.Start();
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
			stopwatch.Start();
			CarrosLista.Add(nuevoCarro);
			return RedirectToAction("Index");
			stopwatch.Stop();
		}

		[HttpGet("ABBTipo")]
		public IActionResult ABBTipo()
		{
			return View();
		}
		[HttpPost("GuardarABBTipo")]
		public IActionResult TipoABB(int Colum)
		{
			for (int i = 0; i < CarrosLista.Count(); i++)
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
			CUAL = Colum;
			return RedirectToAction("ABB");
		}
		[Route("ABB")]
		public IActionResult ABB()
		{
			return View();
		}
		[HttpGet("BusquedaABB")]
		public IActionResult BusquedaABB()
		{
			return View();
		}
		[HttpPost("GuardarBusquedaABB")]
		public IActionResult ABBBusqueda(String valor)
		{
			stopwatch.Start();
			CarrosPropiedadABB.Buscar(valor);
			stopwatch.Stop();
			List<Carro> lista_arreglada = new List<Carro>();
			for (int i = 0; i < CarrosLista.Count(); i++)
			{
				if (CUAL == 0)
				{
					if (CarrosLista[i].id == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 1)
				{
					if (CarrosLista[i].email == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 2)
				{
					if (CarrosLista[i].propietario == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 3)
				{
					if (CarrosLista[i].color == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 4)
				{
					if (CarrosLista[i].marca == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 5)
				{
					if (CarrosLista[i].serie == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
			}
			return View(lista_arreglada);
			
		}

		[Route("OrdenamientoABB")]
		public String OrdenamientoABB()
		{
			stopwatch.Start();
			return (CarrosPropiedadABB.Validez());
			stopwatch.Stop();
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
			for (int i = 0; i < CarrosLista.Count(); i++)
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
			CUAL = Colum;
			return RedirectToAction("AVL");
		}
		[Route("AVL")]
		public IActionResult AVL()
		{
			return View();
		}
		[HttpGet("BusquedaAVL")]
		public IActionResult BusquedaAVL()
		{
			return View();
		}
		[HttpPost("GuardarBusquedaAVL")]
		public IActionResult AVLLBusqueda(String valor)
		{
			stopwatch.Start();
			CarrosPropiedadAVL.Buscar(valor);
			stopwatch.Stop();
			List<Carro> lista_arreglada = new List<Carro>();
			for (int i = 0; i < CarrosLista.Count(); i++)
			{
				if (CUAL == 0)
				{
					if (CarrosLista[i].id == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 1)
				{
					if (CarrosLista[i].email == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 2)
				{
					if (CarrosLista[i].propietario == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 3)
				{
					if (CarrosLista[i].color == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 4)
				{
					if (CarrosLista[i].marca == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
				else if (CUAL == 5)
				{
					if (CarrosLista[i].serie == valor)
					{
						lista_arreglada.Add(CarrosLista[i]);
					}
				}
			}
			return View(lista_arreglada);
		}
        [Route("OrdenamientoAVL")]
        public String OrdenamientoVL()
        {
            stopwatch.Start();
            return (CarrosPropiedadAVL.Validez());
            stopwatch.Stop();
        }
    }
}
