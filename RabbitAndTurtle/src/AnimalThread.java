import java.util.Random;

/**
 * Поток для гонок животных.
 */
public class AnimalThread extends Thread {

    /**
     * Дистанция до финиша.
     */
    private final int maxDistance = 30;

    /**
     * Имя животного.
     */
    private String name;

    /**
     * Пройденная дистанция.
     */
    private int distance = 0;

    /**
     * Флаг, указывающий на лидерство.
     */
    public boolean isLeading = false;

    /**
     * @param name Имя животного.
     * @param priority Приоритет.
     */
    public AnimalThread(String name, int priority) {
        this.name = name;
        setPriority(priority);
    }

    /**
     * Метод запуска бега, имитирует пробежку животного на дистанцию
     * (Пробегает случайное расстояние от 1 до 10 метров за каждый шаг).
     * Если животное лидирует, оно замедляется.
     */
    @Override
    public void run() {
        // Случайные числа для пройденной дистанции
        var random = new Random();

        while (distance < maxDistance) {
            // Если животное лидирует - замедляем его
            if(isLeading){
                try {
                    Thread.sleep(500); // Пауза в полсекунды
                    continue;
                } catch (InterruptedException e) {
                    System.err.println(name + " был прерван: " + e.getMessage());
                }
            }

            // Случайный шаг от 1 до 10 метров
            int step = random.nextInt(1, 10);

            if(distance + step >= maxDistance){
                System.out.println(name + " финишировал!");
                break;
            }

            distance += step;

            System.out.println(name + " пробежал " + step + " метров. Текущая дистанция: " + distance);

            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                System.err.println(name + " был прерван: " + e.getMessage());
            }
        }
    }

    /**
     * @return Пройденная дистанция.
     */
    public int getDistance() {
        return distance;
    }
}