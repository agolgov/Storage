namespace Storage.Data;

public class DispatcherStorage
{ 
    // Тип события
    public delegate void MessageEventHandler();

    // Событие обновления интерфейса веб-страниц пользователей
    public event MessageEventHandler? Refresh;

    // Метод вызова события
    public virtual void OnRefresh() => Refresh?.Invoke();
}