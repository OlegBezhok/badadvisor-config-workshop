using BadAdvisor.Mvc.Data;
using BadAdvisor.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace BadAdvisor.Mvc.Controllers
{
    [Route("messages")]
    public class MessagesController : Controller
    {
        private static readonly Random Rand = new (DateTime.UtcNow.Millisecond);
        private readonly IMessagesRepository _messagesRepository;
        private readonly ISanitizerService _sanitizerService;
        private readonly IConfiguration _configuration;

        public MessagesController(IMessagesRepository messagesRepository,
            ISanitizerService sanitizerService, IConfiguration configuration)
        {
            _messagesRepository = messagesRepository;
            _sanitizerService = sanitizerService;
            _configuration = configuration;
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {
            var maxNumber = _messagesRepository.GetTotalCount();

            var message = await _messagesRepository.Get(Rand.Next(maxNumber));

            if (IsBadWordsFilterEnabled())
            {
                message.Text = _sanitizerService.SanitizeBadWords(message.Text);
            }

            return new JsonResult(new MessageModel()
            {
                Text = message.Text ,
            });
        }

        private bool IsBadWordsFilterEnabled()
        {
            return bool.Parse(_configuration["BadWordsFeatureEnabled"]);
        }
    }

    public class MessageModel
    {
        public string Text { get; set; }
    }
}
