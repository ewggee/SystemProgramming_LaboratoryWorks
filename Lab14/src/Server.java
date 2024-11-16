import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;

public class Server {
    public static void main(String[] args) throws IOException {
        ServerSocket serverSocket = new ServerSocket(8080);
        System.out.println("Сервер ждет клиента...");

        try (Socket clientSocket = serverSocket.accept();
             var inputStream = clientSocket.getInputStream();
             var outputStream = clientSocket.getOutputStream();
             var oos = new ObjectOutputStream(outputStream);
             var ois = new ObjectInputStream(inputStream)) {

            System.out.println("Новое соединение: " + clientSocket.getInetAddress().toString());
            int number = ois.readInt();
            long factorial = calculateFactorial(number);
            oos.writeLong(factorial);
            oos.flush();
            System.out.println("Факториал посчитан и отправлен.");

        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public static long calculateFactorial(int n) {
        if (n < 0) {
            throw new IllegalArgumentException("Число должно быть неотрицательным.");
        }
        long factorial = 1;
        for (int i = 2; i <= n; i++) {
            factorial *= i;
        }
        return factorial;
    }
}
