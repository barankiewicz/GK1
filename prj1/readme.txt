Program obsługuje się głównie poprzez przyciski znajdujące się z prawej strony. 

Procedura tworzenia figur:
-Polygon - pierwsze kliknięcie na Canvas powoduje utworzenie początkowego wierzchołka. Każde następne kliknięcie powoduje utworzenie kolejnego wierzchołka oraz krawędzi między nim a wierzchołkiem poprzedzającym. Kliknięcie w wierzchołek początkowy oznacza zamknięcie polygonu oraz przejście w tryb edycji. Wymogiem jest, aby polygon był poprawny (a więc zamknięty i posiadający co najmniej 3 wierzchołki), więc 'zamknięcie' polygonu jest możliwe dopiero po utworzeniu 3 wierzchołków.

-Circle - Pierwsze kliknięcie ustanawia punkt środkowy okręgu, drugie jego promień.

-Mirrored Polygon - Pierwsze kliknięcie ustanawia punkt skupienia symetrii lub punkt, przez który poprawodzone będą proste symetrii. Dalsza procedura wygląda tak, jak w przypadku tworzenia zwyczajnego obiektu Polygon

Funkcjonalności:

-Można przesuwać wierzchołki oraz krawędzie polygonów
-Można przesuwać całe polygony - należy w tym celu wybrać przycisk 'Move' aby wejść w tryb przesuwania, po czym kliknąć na daną figurę i przesunąć ją metodą drag-drop
-W trybie 'Move', jeśli kliknie się na wolne pole i zacznie przesuwać, przesunięte zostaną wszystkie figury znajdujące się w canvas.
-Można zmieniać promień okręgów - poprzez kliknięcie na obrys okręgu i przesunięcie myszy metodą drag-drop
-Okręg można przesuwać także przesuwając jego pkt środkowy
-Można utworzyć wierzchołek pośrodku odcinka, klikając na niego dwukrotnie (double-click)
-Canvas można zapisać do pliku oraz go z niego wczytać, jako że wszystkie obiekty są [Serializable]
-W prawym dolnym rogu aplikacji znajduje się przełącznik pozwalający przełączać się pomiędzy WinFormsowymi liniami/okręgami, a tymi customowymi


Implementacja MirroredPolygon:

Rozwiązanie zostało zaimplementowane poprzez utworzenie nowej klasy MirroredPolygon. Zawiera ona kolekcję figur, które będą składały się na obiekt. W momencie utworzenia instancji, w kolekcji figur dodawany jest pojedynczy polygon. Jego tworzenie przebiega analogicznie do tworzenia 'zwykłego' Polygon. w momencie zamknięcia go, utworzone zostają i 'odbite' głębokie kopie pierwszego polygonu. Dla każdej akcji wykonanej na którymkolwiek Polygonie należącym do MirroredPolygon została utworzona metoda, która odwzorowuje ją także dla pozostałych polygonów.
