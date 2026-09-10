namespace FootballEvents.Domain.Messages;
public static class ErrorCodes
{
    public enum Team
    { 
        InvalidName
    }

    public enum MatchRecord
    { 
        HomeTeamAndAwayTeamCannotBeTheSame
    }   
}
