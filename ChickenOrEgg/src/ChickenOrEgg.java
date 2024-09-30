public class ChickenOrEgg {
    private static final int MAX_TURNS = 10; // Максимальное количество ходов

    public static void main(String[] args) {
        // Поток курицы
        var chicken = new Thread(() -> {
            var chickenTurns = 0;

            while (chickenTurns < MAX_TURNS) {
                System.out.println("Курица");

                chickenTurns++;

                try {
                    Thread.sleep(100); // Пауза
                } catch (InterruptedException e) {
                    System.err.println("Поток курицы прерван: " + e.getMessage());
                }
            }
        });

        // Поток яйца
        var egg = new Thread(() -> {
            var eggTurns = 0;

            while (eggTurns < MAX_TURNS) {
                System.out.println("Яйцо");

                eggTurns++;

                try {
                    Thread.sleep(100); // Пауза
                } catch (InterruptedException e) {
                    System.err.println("Поток яйца прерван: " + e.getMessage());
                }
            }
        });

        chicken.start();
        egg.start();

        try {
            chicken.join();
        } catch (InterruptedException e) {
            System.err.println("Поток курицы был прерван: " + e.getMessage());
        }

        try {
            egg.join();
        } catch (InterruptedException e) {
            System.err.println("Поток яйца был прерван: " + e.getMessage());
        }

        // Если поток яйца всё ещё активен - первым появилось яйцо, иначе курица
        String last = egg.isAlive() ? "Яйцо" : "Курица";
        System.out.printf("Первым появилось(-ась) %s", last);
    }
}
