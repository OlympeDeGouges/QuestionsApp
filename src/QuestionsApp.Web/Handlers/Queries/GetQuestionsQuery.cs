using Microsoft.EntityFrameworkCore;
using QuestionsApp.Web.DB;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuestionsApp.Web.Handlers.Queries;

public class GetQuestionsResponse
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public int Votes { get; set; }
}

public class GetQuestionsRequest : IRequest<List<GetQuestionsResponse>>
{ }

public class GetQuestionsQuery : IRequestHandler<GetQuestionsRequest, List<GetQuestionsResponse>>
{
    private readonly QuestionsContext _context;

    public GetQuestionsQuery(QuestionsContext context)
    {
        _context = context;
    }
    
    public async Task<List<GetQuestionsResponse>> Handle(GetQuestionsRequest request, CancellationToken cancellationToken)
    {
        return await _context.Questions.Select(q => new GetQuestionsResponse 
        { 
            Id = q.Id, 
            Content = q.Content, 
            Votes = q.Votes.Count 
        }).ToListAsync(cancellationToken);
    }
}