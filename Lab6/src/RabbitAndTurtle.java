public class RabbitAndTurtle {
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
