namespace Framework.OperationResult
{
    public class OperationResultWithData<T> : OperationResult
    {

        public T? Data { get; set; }
    }
}
