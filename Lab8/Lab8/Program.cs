namespace Lab8;

public class Account
{
    private decimal _balance;
    private readonly object _locker = new();

    public Account(decimal initialBalance) => _balance = initialBalance;

    public decimal Balance
    {
        get
        {
            lock (_locker)
            {
                return _balance;
            }
        }
    }

    public void Deposit(decimal amount)
    {
        lock (_locker)
        {
            _balance += amount;
            Console.WriteLine($"Пополнен счет на {amount}. Текущий баланс: {Balance}");
        }
    }

    public async Task<bool> WithdrawAsync(decimal amount, CancellationToken cancellationToken)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Неверная сумма для снятия.");
        }

        while (Balance < amount)
        {
            Console.WriteLine($"\tДоступно {Balance}. Ожидаем пополнения до {amount}...");
            await Task.Delay(1000, cancellationToken);
        }

        lock (_locker)
        {
            if (Balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"Снято {amount}. Текущий баланс: {Balance}");
                return true;
            }
        }
        return false;
    }
}

public class Program
{
    static void Main()
    {
        var account = new Account(100);
        var withdrawalAmount = 500;

        // Поток пополнения счёта
        Task.Run(() =>
        {
            var random = new Random();
            while (true)
            {
                var depositAmount = random.Next(10, 100);
                account.Deposit(depositAmount);
                Thread.Sleep(random.Next(1000, 3000));
            }
        });

        // Снятие денег, когда накопится необходимая сумма
        try
        {
            var withdrawalTask = account.WithdrawAsync(withdrawalAmount, CancellationToken.None);

            withdrawalTask.Wait();

            Console.WriteLine($"Снятие денег завершено успешно.");
        }
        catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is TaskCanceledException)
        {
            Console.WriteLine("Операция снятия отменена.");
        }

        Console.WriteLine($"Текущий баланс: {account.Balance}");
        Console.ReadKey();
    }
}

