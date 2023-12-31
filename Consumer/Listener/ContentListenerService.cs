﻿using EventBus;
using Steeltoe.Messaging.RabbitMQ.Attributes;
using Steeltoe.Messaging.RabbitMQ.Core;

namespace Content.API.Events
{
    public class ContentListenerService
    {
        private readonly RabbitTemplate _rabbitTemplate;
        public ContentListenerService(
            RabbitTemplate rabbitTemplate
        )
        {
            _rabbitTemplate = rabbitTemplate ?? throw new ArgumentNullException(nameof(rabbitTemplate));
        }

        [RabbitListener(Constants.RECEIVE_AND_CONVERT_QUEUE)]
        public void ListenForAMessage(string msg)
        {
            // It goes into retry mode in this exception case and keep on hitting this listener
            throw new Exception("new custom exception");
            Console.WriteLine(msg);
        }
    }
}