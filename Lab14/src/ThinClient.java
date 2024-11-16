import java.io.*;
import java.net.*;
import java.util.Scanner;

/**
 * "Тонкий" клиент.
 */
public class ThinClient {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Введите число: ");
        var number = scanner.nextInt();

        try (Socket socket = new Socket("localhost", 8080);
             var outputStream = socket.getOutputStream();
             var inputStream = socket.getInputStream();
             var oos = new ObjectOutputStream(outputStream);
             var ois = new ObjectInputStream(inputStream)) {

            oos.writeInt(number);
            oos.flush();
            long factorial = ois.readLong();
            System.out.println(number + "! = " + factorial);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
