/**
 * Класс, представляющий собой гонку между двумя
 * потоками - кроликом и черепахой.
 * Для имитации бега использует класс AnimalThread.
 *
 * @see AnimalThread
 * @author Евгений, гр. 22ИТ17
 */
public class RabbitAndTurtle {

    /**
     * Создает два потока: "Кролик" и "Черепаха",
     * устанавливает им приоритеты и запускает их.
     * После запуска потоков, метод отслеживает прогресс каждого из них
     * и меняет их приоритеты, чтобы имитировать
     * динамику гонки: лидеру гонки занижает приоритет, тогда как
     * отстающему наоборот завышает.
     */
    public static void main(String[] args) {
        var rabbit = new AnimalThread("Кролик", Thread.MAX_PRIORITY);
        var turtle = new AnimalThread("Черепаха", Thread.MIN_PRIORITY);

        rabbit.start();
        turtle.start();

        // Процесс гонки (isAlive - пока процесс активен)
        while (rabbit.isAlive() && turtle.isAlive()) {
            if (rabbit.getDistance() > turtle.getDistance()
                    && !rabbit.isLeading) {
                System.out.println("Кролик обгоняет");
                // Меняем приоритеты
                rabbit.setPriority(Thread.MIN_PRIORITY);
                turtle.setPriority(Thread.MAX_PRIORITY);

                // Меняем флаги лидерства
                rabbit.isLeading = true;
                turtle.isLeading = false;
            }

            if (turtle.getDistance() > rabbit.getDistance()
                    && !turtle.isLeading) {
                // Черепаха обогнала кролика
                System.out.println("Черепаха обгоняет");

                rabbit.setPriority(Thread.MAX_PRIORITY);
                turtle.setPriority(Thread.MIN_PRIORITY);

                rabbit.isLeading = false;
                turtle.isLeading = true;
            }

            // Пауза между проверками
            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }

        // Завершение потоков
        try {
            rabbit.join();
            turtle.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        System.out.println("Гонка завершена!");
    }
}
