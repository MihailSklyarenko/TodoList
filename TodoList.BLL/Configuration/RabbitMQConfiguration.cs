namespace TodoList.BLL.Configuration;

public class RabbitMQConfiguration
{
    public string Host { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string GetConnectionString()
    {
        return $"host={Host};port={Port};username={UserName};password={Password}";
    }
}
