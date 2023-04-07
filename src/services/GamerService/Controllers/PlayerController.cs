namespace GamerService.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        public Player Get(string id)
        {
            return new Player
            {
                Id = id,
                Fullname = "Megen Enever",
                Level = 58,
                Location = "Dublin"
            };
        }
    }

    public class Player
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public int Level { get; set; }
        public string Location { get; set; }
    }
}
