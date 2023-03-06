namespace Common;

public struct IdGeneratorRequest
{
    public int Count { get; set; }
    public string Process { get; set; }


    public IdGeneratorRequest()
    {
        
    }
    
    public IdGeneratorRequest(int count, string process)
    {
        Count = count;
        Process = process;
    }
}

public struct IdGeneratorResponse
{
    public List<string> Result { get; set; }
    public string Process { get; set; }


    public IdGeneratorResponse()
    {
        
    }
    
    public IdGeneratorResponse(List<string> result, string process)
    {
        Result = result;
        Process = process;
    }
}