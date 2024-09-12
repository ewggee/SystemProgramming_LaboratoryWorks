import java.util.*;

public class Program {
    public static void main(String[] args) {
        var input = new Scanner(System.in);

        double majority;
        while (true) {
            System.out.print("Введите % мажоритарности: ");
            majority = input.nextDouble();

            if(majority < 0 || majority > 100){
                System.out.println("Неверная мажоритарность");
                continue;
            }

            majority /= 100f;
            break;
        }

        int length;
        while (true) {
            System.out.print("Введите кол-во элементов массива: ");
            length = input.nextInt();

            if(length < 1){
                System.out.println("Неверное кол-во");
                continue;
            }

            break;
        }

        var array = GetRandomArray(length);

        System.out.print("Массив: ");
        for(int i = 0; i < length; i++){
            System.out.printf("%d ", array[i]);
        }
        System.out.println();

        var majorityNumber = GetMajorityNumber(array);

        if(majorityNumber.maxRepeat / array.length >= majority){
            System.out.printf("Число: %d\n", majorityNumber.number);
            System.out.printf("Мажоритарность: %.2f %%", majorityNumber.maxRepeat / array.length * 100);
        }
        else{
            System.out.println("Мажоритарное число не найдено");
        }
    }

    static PairResult GetMajorityNumber(int[] array) {
        var repetitions = new HashMap<Integer, Integer>();

        int number = 0;
        int maxRepeat = 0;

        for (var num : array) {
            repetitions.put(
                    num,
                    repetitions.getOrDefault(num, 0) + 1
            );

            if(repetitions.get(num) > maxRepeat){
                number = num;
                maxRepeat = repetitions.get(num);
            }
        }

        return new PairResult(number, maxRepeat);
    }

    static int[] GetRandomArray(int length){
        var array = new int[length];

        var random = new Random();

        for(int i = 0; i < length; i++){
            array[i] = random.nextInt(10);
        }

        return array;
    }
}