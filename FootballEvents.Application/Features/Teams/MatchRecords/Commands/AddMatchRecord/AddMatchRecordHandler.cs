using FootballEvents.Application.Abstractions;
using FootballEvents.Application.Features.Teams.Teams.Specifications;
using FootballEvents.Domain.Teams;
using FootballEvents.Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;

namespace FootballEvents.Application.Features.Teams.MatchRecords.Commands.AddMatchRecord;
internal sealed class AddMatchRecordHandler : ICommandHandler<AddMatchRecordCommand, string>
{
    private readonly IRepository<MatchRecord> _matchRecordRepository;
    private readonly IRepository<Team> _teamRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddMatchRecordHandler> _logger;

    public AddMatchRecordHandler(IRepository<MatchRecord> matchRecordRepository,
                                 IRepository<Team> teamRepository,
                                 IUnitOfWork unitOfWork,
                                 ILogger<AddMatchRecordHandler> logger)
    {
        _matchRecordRepository = matchRecordRepository;
        _teamRepository = teamRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<string> Handle(AddMatchRecordCommand request, CancellationToken cancellationToken)
    {
        var homeTeam = await _teamRepository.GetAsync(new FindTeamByNameSpecification(request.HomeTeam), cancellationToken);
        if (homeTeam is null)
        {
            homeTeam = Team.Factory.Create(request.HomeTeam);
            _teamRepository.Add(homeTeam);
        }

        var awayTeam = await _teamRepository.GetAsync(new FindTeamByNameSpecification(request.AwayTeam), cancellationToken);
        if (awayTeam is null)
        {
            awayTeam = Team.Factory.Create(request.AwayTeam);
            _teamRepository.Add(awayTeam);
        }

        //TODO: sprawdzić czy to nie te same druzyny i nie zapisać tego !!

        var newMatchRecord = MatchRecord.Factory.Create(
            homeTeam: homeTeam, 
            awayTeam: awayTeam, 
            homeScore: request.HomeScore,
            awayScore: request.AwayScore,
            matchDate: DateTime.UtcNow // Replace with actual match date if needed
        );
        _matchRecordRepository.Add(newMatchRecord);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var statsOutput = $"{homeTeam.Name} {homeTeam.Statistics.MatchesPlayed} {homeTeam.Statistics.Points} {homeTeam.Statistics.GoalScored} {homeTeam.Statistics.GoalConceded} "+
                          $"{awayTeam.Name} {awayTeam.Statistics.MatchesPlayed} {awayTeam.Statistics.Points} {awayTeam.Statistics.GoalScored} {awayTeam.Statistics.GoalConceded}";

        _logger.LogInformation("{StatsOutput}", statsOutput);

        return statsOutput;
    }
}