namespace TaskManagerMAUI.Services
{
	public interface IHttpsClientHandlerService
	{
        HttpMessageHandler GetPlatformMessageHandler();
    }
}

