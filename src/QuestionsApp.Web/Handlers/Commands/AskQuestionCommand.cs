namespace QuestionsApp.Web.Handlers.Commands;
using MediatR;

public class AskQuestionRequest :IRequest<IResult>
{
    public string Content { get; set; } = "";
}

public class AskQuestionCommand : IRequestHandler<AskQuestionRequest, IResult>
{
    public Task<IResult> Handle(AskQuestionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}