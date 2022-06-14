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

        public MessagesController(IMessagesRepository messagesRepository, ISanitizerService sanitizerService)
        {
            _messagesRepository = messagesRepository;
            _sanitizerService = sanitizerService;
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
            return false;
        }
    }

    public class MessageModel
    {
        public string Text { get; set; }
    }
}
