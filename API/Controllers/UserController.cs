//using LIB.Managers;
//using LIB.ViewModels;

//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    [ApiController, Route("[controller]")]
//    public class HeroCrimeController : Controller
//    {
//        private readonly HeroCrimeManager _heroCrimeManager;
//        public HeroCrimeController(HeroCrimeManager heroCrimeManager)
//        {
//            _heroCrimeManager = heroCrimeManager;
//        }

//        [HttpGet, Route("GetAll")]
//        public async Task<IActionResult> Index()
//        {
//            List<HeroCrimeViewModel> viewModels = await _heroCrimeManager.Get();
//            return Ok(viewModels);
//        }

//        [HttpPost, Route("Assign")]
//        public async Task<IActionResult> Assign(int idCrime, int idHero)
//        {
//            try
//            {
//                if (idCrime <= 0 || idHero <= 0) return BadRequest("The hero can't assign to the crime");
               
//                int response = await _heroCrimeManager.Create(idCrime, idHero);
//                if (response == 2) return BadRequest("The hero is already assign to this crime");
//                else if (response != 1) return BadRequest("The hero can't assign to the crime");
//            }
//            catch(Exception ex)
//            {
//                return BadRequest("The hero can't assign to the crime");
//            }
//            return Ok("The hero is assign to the crime");
//        }
//    }
//}
