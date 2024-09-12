import java.util.Random;

public class Program {
    static final int SIZE = 1000; // Количество элементов
    static int BOUND = 10000; // Максимально возможное значение элемента

    public static void main(String[] args) {
        var array = new int[SIZE];

        // Заполнение массива случайными числами
        var random = new Random();
        for(int i = 0; i < SIZE; i++)
            array[i] = random.nextInt(0, BOUND); // Числа в промежутке [0; BOUND]

        var minMultipleOf21 = BOUND;
        var minMultipleOf7 = BOUND;
        var minMultipleOf3 = BOUND;
        var min = BOUND;

        // Поиск минимально кратных чисел
        for (int num : array) {
            if (num % 21 == 0 && num < minMultipleOf21)
                minMultipleOf21 = num;
            else if (num % 7 == 0 && num < minMultipleOf7)
                minMultipleOf7 = num;
            else if (num % 3 == 0 && num < minMultipleOf3)
                minMultipleOf3 = num;
            else if (num < min) min = num;
        }

        var firstPossibleR = 0;  // Случай 1: минимальное число массива, умноженное на минимальное кратное 21-му
        var secondPossibleR = 0; // Случай 2: минимальное кратное 3-м, умноженное на минимальное кратное 7-ми

        if (min != BOUND && minMultipleOf21 != BOUND)
            firstPossibleR = min * minMultipleOf21;
        if (minMultipleOf3 != BOUND && minMultipleOf7 != BOUND)
            secondPossibleR = minMultipleOf3 * minMultipleOf7;

        var minPossible = Math.min(firstPossibleR, secondPossibleR);
        var R = minPossible != 0
            ? minPossible
            : -1;   // Случай 3: -1, если не найдено R
        System.out.printf("R = %d", R);
    }
}