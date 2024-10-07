import java.io.*;

public class Parallel {
    static final String source1 = "Lab7/src/text1.txt";
    static final String source2 = "Lab7/src/text2.txt";
    static final String dest1 = "Lab7/src/copiedText1.txt";
    static final String dest2 = "Lab7/src/copiedText2.txt";

    public static void main(String[] args) {
        var begin = System.nanoTime();

        var thread1 = new Thread(new FileCopyingTask(source1, dest1));
        var thread2 = new Thread(new FileCopyingTask(source2, dest2));

        thread1.start();
        thread2.start();

        var end = 0L;
        try {
            thread1.join();
            thread2.join();

            end = System.nanoTime();
        } catch (InterruptedException e) {
            System.err.println("Ошибка ожидания завершения потоков.");
            e.printStackTrace();
        }

        System.out.printf("Скопировано параллельно!\nВремя выполнения: %d мс.", (end - begin) / 1000);
    }
}

record FileCopyingTask(String inputFileName, String outputFileName) implements Runnable {
    @Override
    public void run() {
        try (var inputStream = new FileInputStream(inputFileName);
             var outputStream = new FileOutputStream(outputFileName)) {

            var buffer = new byte[1024];
            int lengthRead;

            while ((lengthRead = inputStream.read(buffer)) > 0) {
                outputStream.write(buffer, 0, lengthRead);
                outputStream.flush();
            }

        } catch (FileNotFoundException e) {
            System.err.println("Ошибка: файл не найден.");
            e.printStackTrace();
        } catch (IOException e) {
            System.err.println("Ошибка ввода-вывода.");
            e.printStackTrace();
        }
    }
}
