namespace TestTask.Unistrim.Api.Options;

public sealed class TransactionSettings
{
    public decimal MaxAmount { get; set; }
}


/// <summary>
/// ETCDCTL_API=3 etcdctl --endpoints=localhost:2379 --user root:rootpass put /Transaction:MaxAmount 10000.00
///
/// ETCDCTL_API=3 etcdctl --endpoints=localhost:2379 --user root:rootpass get --prefix /transaction
/// ETCDCTL_API=3 etcdctl --endpoints=localhost:2379 --user root:rootpass get /TransactionSettings/MaxAmount
/// </summary>