﻿using static Api.Services.IPortfolioService;

namespace Api.Services;

public class PortfolioService : IPortfolioService
{
    private IRepository repository;

    public PortfolioService(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task Create(string ownerEmail, CreatePortfolioModel portfolio)
    {
        ArgumentNullException.ThrowIfNull(ownerEmail);
        ArgumentNullException.ThrowIfNull(portfolio);
        await repository.Add(new Portfolio
        {
            OwnerEmail = ownerEmail,
            Name = portfolio.Name,
        });
    }
    public IEnumerable<Portfolio> Get(string ownerEmail)
    {
        return repository.Query<Portfolio>().Where(p => p.OwnerEmail == ownerEmail);
    }

    public Portfolio Get(string ownerEmail, string name)
    {
        return repository.Query<Portfolio>().Where(p => p.OwnerEmail == ownerEmail && p.Name == name).AsEnumerable().Single();
    }

    public Task AddTransaction(string ownerEmail, string portfolioName, AddTransactionModel transaction)
    {
        throw new NotImplementedException();
    }
}