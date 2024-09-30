
namespace Chocolates
{
    class Program
    {
        /// <summary>
        /// Имеющиеся деньги.
        /// </summary>
        const int money = 15;
        /// <summary>
        /// Цена одной шоколадки.
        /// </summary>
        const int price = 1;
        /// <summary>
        /// Количество обёрток для получения одной шоколадки.
        /// </summary>
        const int wrap = 3;

        static void Main()
        {
            // Покупаем шоколадки на все деньги
            var chocolates = money / price;
            var wrappers = chocolates;

            // Обмениваем шоколадки, пока есть возможность
            // (кол-во фантиков больше либо равно кол-ву фантиков для получения одной шоколадки)
            while (wrappers >= wrap)
            {
                // Получаем новые шоколадки из оберток
                chocolates += wrappers / wrap;

                // Обмениваем шоколадки и прибавляем оставшиеся + обёртки с новых шоколадок
                wrappers = (wrappers % wrap) + (wrappers / wrap);
            }

            Console.WriteLine($"Максимум шоколадок: {chocolates}");
        }
    }
}
