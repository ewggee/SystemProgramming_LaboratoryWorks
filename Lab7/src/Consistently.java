import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

public class Consistently {
    static final String source1 = "Lab7/src/text1.txt";
    static final String source2 = "Lab7/src/text2.txt";
    static final String dest1 = "Lab7/src/copiedText1.txt";
    static final String dest2 = "Lab7/src/copiedText2.txt";

    public static void main(String[] args) {
        var begin = System.nanoTime();

        try{
            var inputStream = new FileInputStream(source1);
            var outputStream = new FileOutputStream(dest1);

            var buffer = new byte[1024];
            int lengthRead;

            // Чтение и запись из файлов 1
            while ((lengthRead = inputStream.read(buffer)) > 0) {
                outputStream.write(buffer, 0, lengthRead);
                outputStream.flush();
            }

            inputStream = new FileInputStream(source2);
            outputStream = new FileOutputStream(dest2);

            // Чтение и запись из файлов 2
            while ((lengthRead = inputStream.read(buffer)) > 0) {
                outputStream.write(buffer, 0, lengthRead);
                outputStream.flush();
            }

            inputStream.close();
            outputStream.close();

        } catch (FileNotFoundException e) {
            System.err.println("Ошибка: файл не найден.");
            e.printStackTrace();
        } catch (IOException e) {
            System.err.println("Ошибка ввода-вывода.");
            e.printStackTrace();
        }

        var end = System.nanoTime();

        System.out.printf("Скопировано последовательно!\nВремя выполнения: %d мс.", (end - begin) / 1000);
    }
}
