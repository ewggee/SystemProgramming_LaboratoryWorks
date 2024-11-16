import java.io.*;
import java.net.*;
import java.util.Scanner;

/**
 * "Толстый" клиент.
 */
public class ThickClient {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Введите число: ");
        var  number = scanner.nextInt();

        long factorial;
        if (number < 20) { // Вычисляем локально для маленьких чисел
            factorial = calculateFactorial(number);
            System.out.println(number + "! = " + factorial);
        } else { // Используем сервер для больших чисел
            try (Socket socket = new Socket("localhost", 8080);
                 var outputStream = socket.getOutputStream();
                 var inputStream = socket.getInputStream();
                 var oos = new ObjectOutputStream(outputStream);
                 var ois = new ObjectInputStream(inputStream)) {

                oos.writeInt(number);
                oos.flush();
                factorial = ois.readLong();
                System.out.println("Факториал " + number + "! = " + factorial);

            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }


    public static long calculateFactorial(int number) {
        if (number < 0) {
            throw new IllegalArgumentException("Число должно быть неотрицательным.");
        }
        long factorial = 1;
        for (int i = 2; i <= number; i++) {
            factorial *= i;
        }
        return factorial;
    }
}
