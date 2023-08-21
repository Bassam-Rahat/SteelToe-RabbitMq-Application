using EventBus;
using MediatR;
using Steeltoe.Messaging.RabbitMQ.Core;

namespace Producer.Command
{
    public class MessagePostHandler : IRequestHandler<MessagePostCommand, bool>
    {
        private readonly RabbitTemplate _rabbitTemplate;

        public MessagePostHandler(RabbitTemplate rabbitTemplate)
        {
            _rabbitTemplate = rabbitTemplate;
        }

        public async Task<bool> Handle(MessagePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _rabbitTemplate.ConvertAndSendAsync(Constants.RECEIVE_AND_CONVERT_QUEUE, request.Message, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
