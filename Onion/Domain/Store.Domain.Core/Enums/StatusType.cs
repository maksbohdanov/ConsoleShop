namespace Store.Domain.Core.Enums
{
    /// <summary>
    /// Enum StatusType
    /// </summary>
    public enum StatusType
    {
        
        New,        
        CanceledByAdministrator,
        PaymentReceived,
        Sent,
        Received,
        Completed,
        CanceledByUser
    }
}
