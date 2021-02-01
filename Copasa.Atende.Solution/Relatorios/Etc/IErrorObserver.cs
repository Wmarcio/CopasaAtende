
namespace Etc
{
    /// <summary>
    /// Interface para página notificar erro.
    /// A identificação do erro no código behind da página é retornado pelo GetErrorMessage
    /// Para utilização do diálogo ErrorDialog é necessário definí-la como observador através da inferface IErrorObserver que o ErrorDialog implementa.
    /// O controle ErrorDialog utiliza GetErrorMessage para obter a mensagem de erro da página.
    /// </summary>
    public interface IErrorNotifier
    {
        string GetErrorMessage();        
    }
    /// <summary>
    /// Essa interface é para ser implementada por controles ou componentes que precisam receber mensagem de erro de algum IErrorNotifier. ErrorDialog implementa esta interface para que BasePage possa ser definida nela.
    /// Essa interface pode ser implementada também por um logProvider.
    /// </summary>
    public interface IErrorObserver
    {
        void SetErrorObserver(IErrorNotifier notifier);
    }
}