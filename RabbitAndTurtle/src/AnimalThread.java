import java.util.Random;

public class AnimalThread extends Thread {
    private final int maxDistance = 30;
    private String name;
    private int distance = 0;
    public boolean isLeading = false;

    public AnimalThread(String name, int priority) {
        this.name = name;
        setPriority(priority);
    }

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

    public int getDistance() {
        return distance;
    }
}