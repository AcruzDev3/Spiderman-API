//using LIB.Managers;
//using LIB.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    [ApiController, Route("[controller]")]
//    public class CrimeController : Controller
//    {
//        private readonly CrimeManager _crimeManager;
//        public CrimeController(CrimeManager crimeManager)
//        {
//            _crimeManager = crimeManager;
//        }
//        [HttpGet, Route("GetAll")]
//        public async Task<IActionResult> GetAll()
//        {
//            return Ok(await _crimeManager.Get());
//        }

//        [HttpPost, Route("Create")]
//        public async Task<IActionResult> Create(CrimeViewModel crimeViewModel)
//        {
//            try{
//                int response = await _crimeManager.Create(crimeViewModel); 
//                if(response != -1) return BadRequest("No se ha podido crear el aviso");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest("No se ha podido crear el aviso");
//            }
//            return Ok("El crimen se creo con exito");
//        }

//        [HttpPut, Route("Solved")]
//        public async Task<IActionResult> Solved(int id)
//        {
//            try
//            {
//                int response = await _crimeManager.Solved(id);
//                if (response != 1) return BadRequest("The crime can't solved");
//            }
//            catch(Exception ex)
//            {
//                return BadRequest("The crime can't be solved");
//            }
//            return Ok("The crime is solved");
//        }
//        [HttpDelete, Route("Delete")]
//        public async Task<IActionResult> DeleteCrime(int id)
//        {
//            try
//            {
//                int response = await _crimeManager.Delete(id);
//                if (response != 1) return BadRequest("No se ha podido eliminar el crimen");
//            }
//            catch(Exception ex)
//            {
//                return BadRequest("No se ha podido eliminar el crimen");
//            }
//            return Ok("El crimen se ha eliminado con exito");
//        }
//    }
//}
