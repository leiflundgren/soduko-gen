using Microsoft.AspNetCore.Mvc;
using SodukoLib.Strategies;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SodukoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {

        public string Strategy { get; set; } = nameof(CombinedReducer);

        // GET: api/<GenerateController>
        [HttpGet]
        public IEnumerable<string> GetStrategies()
        {
            return new string[] {    nameof(OnlyOnePossibleReducer), nameof(PidgeonHolePrinciple), nameof(CombinedReducer) };
        }

        // GET api/<GenerateController>/5
        [HttpGet("{Strategy}")]
        public string Get()
        {
            return Strategy;
        }

        // POST api/<GenerateController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            if (!GetStrategies().Contains(value))
                throw new ArgumentOutOfRangeException("Unknonwn stragegy");
            Strategy = value; ;
        }

        // PUT api/<GenerateController>/5
        [HttpPut]
        public void Put([FromBody] string value)
        {
            Post(value);
        }

    }
}
