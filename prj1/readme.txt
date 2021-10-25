Program obsługuje się głównie poprzez przyciski znajdujące się z prawej strony. 

Procedura tworzenia figur:
-Polygon - pierwsze kliknięcie na Canvas powoduje utworzenie początkowego wierzchołka. Każde następne kliknięcie powoduje utworzenie kolejnego wierzchołka oraz krawędzi między nim a wierzchołkiem poprzedzającym. Kliknięcie w wierzchołek początkowy oznacza zamknięcie polygonu oraz przejście w tryb edycji. Wymogiem jest, aby polygon był poprawny (a więc zamknięty i posiadający co najmniej 3 wierzchołki), więc 'zamknięcie' polygonu jest możliwe dopiero po utworzeniu 3 wierzchołków.

-Circle - Pierwsze kliknięcie ustanawia punkt środkowy okręgu, drugie jego promień.

Funkcjonalności:

-Można przesuwać wierzchołki oraz krawędzie polygonów
-Można przesuwać całe polygony - należy w tym celu wybrać przycisk 'Move' aby wejść w tryb przesuwania, po czym kliknąć na daną figurę i przesunąć ją metodą drag-drop
-Można usuwać wierzchołki. Usunięcie wierzchołka powoduje usunięcie wszystkich przyległych relacji. Usuwanie zachodzi poprzez kliknięcie na wierzchołek w trybie edycji i kliknięcie DEL
-W trybie 'Move', jeśli kliknie się na wolne pole i zacznie przesuwać, przesunięte zostaną wszystkie figury znajdujące się w canvas.
-Można zmieniać promień okręgów - poprzez kliknięcie na obrys okręgu i przesunięcie myszy metodą drag-drop
-Okręg można przesuwać także przesuwając jego pkt środkowy
-Można utworzyć wierzchołek pośrodku odcinka, klikając na niego dwukrotnie (double-click). Utworzenie wierzchołka pośrodku krawędzi usuwa relację na niej, jeśli taka istniała.
-Canvas można zapisać do pliku oraz go z niego wczytać, jako że wszystkie obiekty są [Serializable]
-W prawym dolnym rogu aplikacji znajduje się przełącznik pozwalający przełączać się pomiędzy WinFormsowymi liniami/okręgami, a tymi customowymi (algorytm Bresenhama)
-Możliwość ustanawiania relacji: zadanej długości, zadanego promienia, równości krawędzi
-Ustanowione relacje przedstawiają się na canvas w postaci ikonek w pobliżu krawędzi/okręgu ze znaczkami: okręgu dla zadanego promienia, litery 'L' dla zadanej długości, znaku równości dla równości krawędzi
-Relacje można zaznaczać i usuwać (klawiszem DEL)


Implementacja relacji:

Relacje zaimplementowane są za pomocą funkcji rekurencyjnej znajdującej się w FigureSet  (OffsetElement). Funkcja pobiera wierzchołki sąsiadujące do przesuniętego elementu (krawędzi lub wierzchołka) i sprawdza, czy one mają jakieś relacje. Jeśli tak, przesuwani są także sąsiedzi za pomocą kolejnego wywołania funkcji.
