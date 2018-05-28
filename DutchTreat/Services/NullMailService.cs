using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
  public class NullMailService : IMailService
  {
    private readonly ILogger<NullMailService> _logger;

    public NullMailService(ILogger<NullMailService> logger)
    {
      _logger = logger;
    }

    public void SendMessage(string recipient, string subject, string body)
    {
      // Log the message.
      _logger.LogInformation($"To: {recipient} Subject: {subject} Body: {body}");
    }
  }
}
