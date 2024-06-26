﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QuestionsApp.Web.DB;
using QuestionsApp.Web.Handlers.Commands;
using QuestionsApp.Web.Handlers.Queries;

namespace QuestionsApp.Tests;

public class QuestionsTests
{
    private GetQuestionsQuery NewGetQuestionsQueryHandler => new(_context);
    private AskQuestionCommand NewAskQuestionCommandHandler => new(_context, null);
    private VoteForQuestionCommand NewVoteForQuestionCommandHandler => new(_context, null);
    
    [Fact]
    public async void Empty()
    {
        var response = await NewGetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().BeEmpty();
    }
    
    [Fact]
    public async void OneQuestion()
    {
        var askResponse = await NewAskQuestionCommandHandler.Handle(new AskQuestionRequest { Content = "Dummy Question" }, default);
        askResponse.Should().NotBeNull();

        var response = await NewGetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().HaveCount(1);
    }
    
    [Fact]
    public async void OneQuestionAndVote()
    {
        var askResponse = await NewAskQuestionCommandHandler.Handle(new AskQuestionRequest { Content = "Dummy Question" }, default);
        askResponse.Should().NotBeNull();

        var response = await NewGetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().HaveCount(1);
        response[0].Votes.Should().Be(0);

        var voteResponse = await NewVoteForQuestionCommandHandler.Handle(new VoteForQuestionRequest { QuestionId = response[0].Id }, default);
        voteResponse.Should().NotBeNull();

        response = await NewGetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().HaveCount(1);
        response[0].Votes.Should().Be(1);
    }
    
    private readonly QuestionsContext _context;

    public QuestionsTests()
    {
        var options = new DbContextOptionsBuilder<QuestionsContext>().
            UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = new QuestionsContext(options);
    }
}