using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace AzureTeacherStudentSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly QueueServiceClient _queueServiceClient;

        public IndexModel(ILogger<IndexModel> logger, QueueServiceClient queueServiceClient)
        {
            _logger = logger;
            _queueServiceClient = queueServiceClient;
            Messages = new();
        }

        public List<string> Messages { get; set; }

        [BindProperty]
        public string AddMessage { get; set; } 

        public async Task OnGet()
        {
            var queueClient = _queueServiceClient.GetQueueClient("messages");
            await queueClient.CreateIfNotExistsAsync();
            var response = await queueClient.PeekMessagesAsync(10);

            Messages = new List<string>();
            foreach (var peekMessage in response.Value)
            {
                var message = Encoding.UTF8.GetString(peekMessage.Body.ToArray());
                Messages.Add(message);
            }
        }

        public async Task OnPost()
        {
            var queueClient = _queueServiceClient.GetQueueClient("messages");

            if (string.IsNullOrEmpty(AddMessage?.Trim()))
            {
                return;
            }

            await queueClient.SendMessageAsync(AddMessage);
        }
    }
}
