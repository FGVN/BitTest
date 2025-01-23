namespace BitTest.Core.Requests;

public class UpdateRequest
{
    public int Id { get; set; }
    public string Column { get; set; }
    public string Value { get; set; }

    public UpdateRequest()
    {
        Column = string.Empty;
        Value = string.Empty;
    }
}


