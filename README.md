# Mobiles_TP2_ColettaEnzo

Observer Pattern:
Implementé el patrón Observer en el script PlayerManager y en el script Observer para notificar a un script de vida visual del jugador, permitiendo así la modificación del aspecto y partículas del jugador en respuesta a cambios en su estado.

Memento Pattern:
En el script PlayerLoader y Shop, empleé el patrón Memento para almacenar la información completa del jugador, facilitando la capacidad de estos dos scripts para gestionar y guardar la información relacionada con las compras del jugador.

Mediator Pattern:
Integré el patrón Mediator a través del script PlayerDeathMediator, actuando como mediador entre el SceneLoader y el Player. Esto simplifica la comunicación entre estos elementos, evitando llamadas directas y facilitando la integración de más jugadores en la escena de pérdida de manera más eficiente.
