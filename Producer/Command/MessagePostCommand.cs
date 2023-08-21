using MediatR;

namespace Producer.Command
{
    public record MessagePostCommand : IRequest<bool>
    {
        public string Message { get; set; }
    }
}
