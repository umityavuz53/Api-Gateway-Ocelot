namespace PromotionService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class ApplierController : ControllerBase
    {
        private readonly ILogger<ApplierController> _logger;

        public ApplierController(ILogger<ApplierController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult SetPromotoion(Code promoCode)
        {
            return Ok($"{promoCode.No} için {promoCode.Duration} gün süreli promosyon kullanıcı hesabına tanımlanmıştır");
        }
    }

    public class Code
    {
        public string No { get; set; }
        public int Duration { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
    }
}
