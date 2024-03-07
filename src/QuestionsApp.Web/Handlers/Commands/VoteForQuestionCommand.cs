namespace QuestionsApp.Web.Handlers.Commands;
using MediatR;

public class VoteForQuestionRequest : IRequest<IResult>
{
    public int QuestionId { get; set; }
}
public class VoteForQuestionCommand : IRequestHandler<VoteForQuestionRequest, IResult>
{
    public Task<IResult> Handle(VoteForQuestionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

